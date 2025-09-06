using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

#if NET8_0_OR_GREATER
using System.Text.Json;
#else
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Engines;
#endif

namespace Triggerless.TriggerBot
{
    public enum BrowserKind
    {
        Chrome,
        Edge,
        Firefox,
        Opera,
        Brave,
        // Optional extras (often <1% on Windows but harmless to try if installed):
        OperaGX,
        Vivaldi
    }

    public static class AvatarNameReader
    {
        // --- Public convenience for your case ---
        public static bool TryGetAvatarName(out string cookie)
            => TryRead("_imvu_avnm", "imvu.com", out cookie);

        /// <summary>
        /// Tries default browser first (if supported), then other browsers (Chrome, Edge, Firefox, Opera, Brave).
        /// Scans all profiles until it finds cookieName for a host ending with domainSuffix.
        /// </summary>
        public static bool TryRead(string cookieName, string domainSuffix, out string value)
        {
            value = null;

            // 1) Build browser order: default first, then others (~>1% share set)
            var ordered = BuildPreferredOrder();

            // 2) Try each in order
            foreach (var b in ordered)
            {
                if (TryGetCookieFromBrowser(cookieName, domainSuffix, b, out value))
                    return true;
            }

            // Optional: try niche Chromium browsers if present
            foreach (var b in new[] { BrowserKind.OperaGX, BrowserKind.Vivaldi })
            {
                if (ordered.Contains(b)) continue;
                if (TryGetCookieFromBrowser(cookieName, domainSuffix, b, out value))
                    return true;
            }

            return false;
        }

        // ===================== Browser dispatch =====================

        private static bool TryGetCookieFromBrowser(string name, string domainSuffix, BrowserKind browser, out string value)
        {
            value = null;
            if (browser == BrowserKind.Firefox)
                return TryGetCookieFromFirefox(name, domainSuffix, out value);
            else
                return TryGetCookieFromChromium(name, domainSuffix, browser, out value);
        }

        // ===================== Default browser detection & ordering =====================

        private static List<BrowserKind> BuildPreferredOrder()
        {
            var list = new List<BrowserKind>();
            var defProgId = GetDefaultBrowserProgId();
            var def = MapProgId(defProgId);
            if (def.HasValue) list.Add(def.Value);

            // Rough market order (Windows): Chrome, Edge, Firefox, Opera, Brave
            var rest = new[] { BrowserKind.Chrome, BrowserKind.Edge, BrowserKind.Firefox, BrowserKind.Opera, BrowserKind.Brave };
            foreach (var b in rest) if (!list.Contains(b)) list.Add(b);

            return list;
        }

        private static string GetDefaultBrowserProgId()
        {
            try
            {
                using (var k = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice"))
                {
                    return k?.GetValue("ProgId") as string;
                }
            }
            catch { return null; }
        }

        private static BrowserKind? MapProgId(string progId)
        {
            if (string.IsNullOrEmpty(progId)) return null;
            var p = progId.ToLowerInvariant();
            if (p.Contains("chromehtml")) return BrowserKind.Chrome;
            if (p.Contains("microsoftedge") || p.Contains("msedgehtm") || p.Contains("msegehtml")) return BrowserKind.Edge;
            if (p.Contains("firefox")) return BrowserKind.Firefox; // FirefoxURL / FirefoxHTML
            if (p.Contains("operastable")) return BrowserKind.Opera;
            if (p.Contains("operagx")) return BrowserKind.OperaGX;
            if (p.Contains("brave")) return BrowserKind.Brave;
            if (p.Contains("vivaldi")) return BrowserKind.Vivaldi;
            return null;
        }

        // ===================== Chromium path =====================

        private static bool TryGetCookieFromChromium(string cookieName, string domainSuffix, BrowserKind browser, out string value)
        {
            value = null;

            var (userDataRoot, profileDirs) = GetChromiumUserDataAndProfiles(browser);
            if (userDataRoot == null || profileDirs.Count == 0) return false;

            // Read & decrypt master key (Local State)
            string localStatePath = Path.Combine(userDataRoot, "Local State");
            byte[] masterKey;
            try { masterKey = GetChromiumAesKey(localStatePath); }
            catch { return false; }

            foreach (var profileDir in profileDirs)
            {
                if (TryGetCookieFromChromiumProfile(cookieName, domainSuffix, profileDir, masterKey, out value))
                    return true;
            }
            return false;
        }

        private static bool TryGetCookieFromChromiumProfile(string cookieName, string domainSuffix, string profileDir, byte[] masterKey, out string value)
        {
            value = null;

            string cookiesPath = Path.Combine(profileDir, "Network", "Cookies");
            if (!File.Exists(cookiesPath)) cookiesPath = Path.Combine(profileDir, "Cookies");
            if (!File.Exists(cookiesPath)) return false;

            string tempCopy = Path.Combine(Path.GetTempPath(), "ck_" + Guid.NewGuid().ToString("N") + ".db");
            try
            {
                File.Copy(cookiesPath, tempCopy, true);

                using (var cxn = new SQLiteConnection("Data Source=" + tempCopy + ";Version=3;Read Only=True;"))
                {
                    cxn.Open();
                    using (var cmd = cxn.CreateCommand())
                    {
                        cmd.CommandText =
    @"SELECT host_key, name, path, encrypted_value, value
  FROM cookies
 WHERE name = @name
   AND (
         host_key = @dom
      OR host_key = '.' || @dom
      OR host_key LIKE '%.' || @dom ESCAPE '\'
       )
 ORDER BY LENGTH(path) DESC
 LIMIT 1;";
                        cmd.Parameters.AddWithValue("@name", cookieName);
                        cmd.Parameters.AddWithValue("@dom", domainSuffix);

                        using (var rdr = cmd.ExecuteReader())
                        {
                            if (!rdr.Read()) return false;

                            byte[] enc = rdr["encrypted_value"] as byte[];
                            string plain = rdr["value"] as string;

                            value = (enc != null && enc.Length > 0)
                                    ? DecryptChromiumCookie(enc, masterKey)
                                    : plain;

                            return !string.IsNullOrEmpty(value);
                        }
                    }
                }
            }
            catch { return false; }
            finally { try { File.Delete(tempCopy); } catch { } }
        }

        private static (string userDataRoot, List<string> profileDirs) GetChromiumUserDataAndProfiles(BrowserKind b)
        {
            string lad = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string root = null;

            switch (b)
            {
                case BrowserKind.Chrome:
                    root = Path.Combine(lad, "Google", "Chrome", "User Data"); break;
                case BrowserKind.Edge:
                    root = Path.Combine(lad, "Microsoft", "Edge", "User Data"); break;
                case BrowserKind.Brave:
                    root = Path.Combine(lad, "BraveSoftware", "Brave-Browser", "User Data"); break;
                case BrowserKind.Opera:
                    root = Path.Combine(lad, "Opera Software", "Opera Stable"); break;
                case BrowserKind.OperaGX:
                    root = Path.Combine(lad, "Opera Software", "Opera GX Stable"); break;
                case BrowserKind.Vivaldi:
                    root = Path.Combine(lad, "Vivaldi", "User Data"); break;
            }
            if (root == null || !Directory.Exists(root)) return (null, new List<string>());

            // Opera* roots already point to the profile dir; others have a "User Data" with sub-profiles
            var dirs = new List<string>();
            if (b == BrowserKind.Opera || b == BrowserKind.OperaGX)
            {
                // root is a profile; also include "Default"/"Profile *" if present
                dirs.Add(root);
                var defaultDir = Path.Combine(root, "Default");
                if (Directory.Exists(defaultDir)) dirs.Add(defaultDir);
                dirs.AddRange(Directory.GetDirectories(root, "Profile *", SearchOption.TopDirectoryOnly));
            }
            else
            {
                // Standard Chromium layout
                var defaultDir = Path.Combine(root, "Default");
                if (Directory.Exists(defaultDir)) dirs.Add(defaultDir);

                var others = Directory.GetDirectories(root, "Profile *", SearchOption.TopDirectoryOnly)
                                      .OrderBy(p =>
                                      {
                                          var name = Path.GetFileName(p);
                                          if (name != null && name.StartsWith("Profile ", StringComparison.OrdinalIgnoreCase) &&
                                              int.TryParse(name.Substring(8), out int n)) return n;
                                          return int.MaxValue;
                                      });
                dirs.AddRange(others);

                // Include any other plausible profiles (excluding System/Guest)
                foreach (var d in Directory.GetDirectories(root))
                {
                    var baseName = Path.GetFileName(d);
                    if (string.Equals(baseName, "Default", StringComparison.OrdinalIgnoreCase)) continue;
                    if (baseName != null && baseName.StartsWith("Profile ", StringComparison.OrdinalIgnoreCase)) continue;
                    if (string.Equals(baseName, "System Profile", StringComparison.OrdinalIgnoreCase)) continue;
                    if (string.Equals(baseName, "Guest Profile", StringComparison.OrdinalIgnoreCase)) continue;

                    if (Directory.Exists(Path.Combine(d, "Network")) ||
                        File.Exists(Path.Combine(d, "Cookies")) ||
                        File.Exists(Path.Combine(d, "Network", "Cookies")))
                        dirs.Add(d);
                }
            }

            // Deduplicate and return
            dirs = dirs.Where(Directory.Exists).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            return (root, dirs);
        }

        // ---- Chromium crypto helpers ----

        private static byte[] GetChromiumAesKey(string localStatePath)
        {
            if (!File.Exists(localStatePath))
                throw new FileNotFoundException("Local State not found", localStatePath);

            string json = File.ReadAllText(localStatePath, Encoding.UTF8);

#if NET8_0_OR_GREATER
        using var doc = JsonDocument.Parse(json);
        if (!doc.RootElement.TryGetProperty("os_crypt", out var oscrypt) ||
            !oscrypt.TryGetProperty("encrypted_key", out var ek))
            throw new InvalidOperationException("encrypted_key missing");
        string b64 = ek.GetString();
#else
            var jo = JObject.Parse(json);
            string b64 = (string)jo.SelectToken("os_crypt.encrypted_key");
            if (string.IsNullOrEmpty(b64)) throw new InvalidOperationException("encrypted_key missing");
#endif

            byte[] raw = Convert.FromBase64String(b64);
            // Strip "DPAPI" prefix and unprotect
            var prefix = Encoding.ASCII.GetBytes("DPAPI");
            byte[] blob = raw.Length > prefix.Length && raw.Take(prefix.Length).SequenceEqual(prefix)
                          ? raw.Skip(prefix.Length).ToArray()
                          : raw;
            return ProtectedData.Unprotect(blob, null, DataProtectionScope.CurrentUser);
        }

        private static string DecryptChromiumCookie(byte[] encryptedValue, byte[] key)
        {
            // New: "v10"/"v11" | 12-byte nonce | ciphertext | 16-byte tag
            if (encryptedValue.Length >= 3 &&
                encryptedValue[0] == (byte)'v' &&
                encryptedValue[1] == (byte)'1' &&
                (encryptedValue[2] == (byte)'0' || encryptedValue[2] == (byte)'1'))
            {
                byte[] nonce = encryptedValue.Skip(3).Take(12).ToArray();
                byte[] ctAndTag = encryptedValue.Skip(3 + 12).ToArray();
                if (ctAndTag.Length < 16) return null;

                int ctLen = ctAndTag.Length - 16;
                byte[] ct = new byte[ctLen];
                byte[] tag = new byte[16];
                Buffer.BlockCopy(ctAndTag, 0, ct, 0, ctLen);
                Buffer.BlockCopy(ctAndTag, ctLen, tag, 0, 16);

#if NET8_0_OR_GREATER
            byte[] plaintext = new byte[ctLen];
            using (var aes = new AesGcm(key))
                aes.Decrypt(nonce, ct, tag, plaintext, null);
            return Encoding.UTF8.GetString(plaintext);
#else
                var cipher = new GcmBlockCipher(new AesEngine());
                var aead = new AeadParameters(new KeyParameter(key), 128, nonce, null);
                cipher.Init(false, aead);
                byte[] input = new byte[ct.Length + tag.Length];
                Buffer.BlockCopy(ct, 0, input, 0, ct.Length);
                Buffer.BlockCopy(tag, 0, input, ct.Length, tag.Length);
                byte[] outBuf = new byte[cipher.GetOutputSize(input.Length)];
                int len = cipher.ProcessBytes(input, 0, input.Length, outBuf, 0);
                len += cipher.DoFinal(outBuf, len);
                var plain = new byte[len];
                Buffer.BlockCopy(outBuf, 0, plain, 0, len);
                return Encoding.UTF8.GetString(plain);
#endif
            }
            // Legacy: DPAPI
            try
            {
                var plain = ProtectedData.Unprotect(encryptedValue, null, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(plain);
            }
            catch { return null; }
        }

        // ===================== Firefox path =====================

        private static bool TryGetCookieFromFirefox(string cookieName, string domainSuffix, out string value)
        {
            value = null;

            var profiles = GetFirefoxProfilesOrdered();
            foreach (var p in profiles)
            {
                var cookiesDb = Path.Combine(p, "cookies.sqlite");
                if (!File.Exists(cookiesDb)) continue;

                string tempCopy = Path.Combine(Path.GetTempPath(), "ffck_" + Guid.NewGuid().ToString("N") + ".db");
                try
                {
                    File.Copy(cookiesDb, tempCopy, true);

                    using (var cxn = new SQLiteConnection("Data Source=" + tempCopy + ";Version=3;Read Only=True;"))
                    {
                        cxn.Open();
                        using (var cmd = cxn.CreateCommand())
                        {
                            // moz_cookies schema: host, name, value, path, expiry, ...
                            cmd.CommandText =
    @"SELECT host, name, value, path
  FROM moz_cookies
 WHERE name = @name
   AND (
         host = @dom
      OR host = '.' || @dom
      OR host LIKE '%.' || @dom ESCAPE '\'
       )
 ORDER BY LENGTH(path) DESC
 LIMIT 1;";
                            cmd.Parameters.AddWithValue("@name", cookieName);
                            cmd.Parameters.AddWithValue("@dom", domainSuffix);

                            using (var rdr = cmd.ExecuteReader())
                            {
                                if (!rdr.Read()) continue;
                                value = rdr["value"] as string;
                                if (!string.IsNullOrEmpty(value)) return true;
                            }
                        }
                    }
                }
                catch { /* try next profile */ }
                finally { try { File.Delete(tempCopy); } catch { } }
            }

            return false;
        }

        private static List<string> GetFirefoxProfilesOrdered()
        {
            var list = new List<string>();
            string roaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); // %APPDATA% (Roaming)
            string baseDir = Path.Combine(roaming, "Mozilla", "Firefox");
            string profilesRoot = Path.Combine(baseDir, "Profiles");
            string iniPath = Path.Combine(baseDir, "profiles.ini");

            // Try profiles.ini to prioritize default profile
            if (File.Exists(iniPath))
            {
                try
                {
                    var blocks = ParseFirefoxProfilesIni(iniPath);
                    // Default first, then others
                    foreach (var b in blocks.Where(b => b.IsDefault))
                    {
                        var p = b.IsRelative ? Path.Combine(baseDir, b.Path) : b.Path;
                        if (Directory.Exists(p)) list.Add(p);
                    }
                    foreach (var b in blocks.Where(b => !b.IsDefault))
                    {
                        var p = b.IsRelative ? Path.Combine(baseDir, b.Path) : b.Path;
                        if (Directory.Exists(p)) list.Add(p);
                    }
                }
                catch { /* fall back below */ }
            }

            // Fallback: any folder under Profiles
            if (Directory.Exists(profilesRoot))
            {
                foreach (var d in Directory.GetDirectories(profilesRoot))
                    if (!list.Contains(d, StringComparer.OrdinalIgnoreCase))
                        list.Add(d);
            }

            // Only keep those that actually look like a Firefox profile (has cookies.sqlite)
            list = list.Where(d => File.Exists(Path.Combine(d, "cookies.sqlite"))).ToList();
            return list;
        }

        private sealed class FxProfileIni
        {
            public string Path;
            public bool IsRelative;
            public bool IsDefault;
        }

        private static List<FxProfileIni> ParseFirefoxProfilesIni(string iniPath)
        {
            var result = new List<FxProfileIni>();
            FxProfileIni cur = null;

            foreach (var raw in File.ReadAllLines(iniPath))
            {
                var line = raw.Trim();
                if (line.Length == 0 || line.StartsWith(";", StringComparison.Ordinal)) continue;

                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    // New section
                    var header = line.Substring(1, line.Length - 2);
                    if (header.StartsWith("Profile", StringComparison.OrdinalIgnoreCase))
                    {
                        cur = new FxProfileIni();
                        result.Add(cur);
                    }
                    else
                    {
                        cur = null; // Ignore other sections
                    }
                    continue;
                }

                if (cur == null) continue;

                int eq = line.IndexOf('=');
                if (eq <= 0) continue;
                var key = line.Substring(0, eq).Trim();
                var val = line.Substring(eq + 1).Trim();

                if (key.Equals("Path", StringComparison.OrdinalIgnoreCase)) cur.Path = val;
                else if (key.Equals("IsRelative", StringComparison.OrdinalIgnoreCase)) cur.IsRelative = (val == "1");
                else if (key.Equals("Default", StringComparison.OrdinalIgnoreCase)) cur.IsDefault = (val == "1");
            }

            return result;
        }
    }

}
