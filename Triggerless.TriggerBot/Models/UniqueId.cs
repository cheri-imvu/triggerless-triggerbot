using System;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using Microsoft.Win32;

namespace Triggerless.TriggerBot
{
    public static class UniqueId
    {
        // Store a per-app secret (32 bytes). In production, load from config / protected storage.
        // Example here is Base64 for convenience. Generate your own random key!
        private static readonly byte[] AppSecret = Convert.FromBase64String(
            "4yxzM0kqjz5mCqR3uI2h9v+g9o3q3+W6yWl6TgE2x0Q=" /* 32 bytes when decoded */
        );

        /// <summary>
        /// Returns a pseudonymous ID like "A1B2C3D4E5" (default 10 hex, uppercase) based on current user+machine.
        /// </summary>
        public static string ForCurrentUserMachine(int hexLen = 16, string ns = "Triggerless.TriggerBot")
        {
            if (hexLen <= 0 || (hexLen % 2) != 0)
                throw new ArgumentOutOfRangeException(nameof(hexLen), "Use an even hex length, e.g., 10, 12, 16.");

            string userSid = GetCurrentUserSid() ?? "UNKNOWN-SID";
            string machineGuid = GetMachineGuid() ?? "UNKNOWN-MGUID";

            // Compose input with explicit version and namespace delimiters
            string payload = "v1\0" + ns + "\0" + userSid + "\0" + machineGuid;
            byte[] data = Encoding.UTF8.GetBytes(payload);

            // HMAC-SHA256 with app secret (keyed hash)
            byte[] mac;
            using (var h = new HMACSHA256(AppSecret))
                mac = h.ComputeHash(data);

            // Hex encode and truncate
            var sb = new StringBuilder(mac.Length * 2);
            for (int i = 0; i < mac.Length; i++) sb.Append(mac[i].ToString("X2"));
            string hex = sb.ToString();

            return hex.Substring(0, hexLen); // e.g., first 10 hex chars
        }

        private static string GetCurrentUserSid()
        {
            try
            {
                using (var wi = WindowsIdentity.GetCurrent())
                    return wi != null ? wi.User.Value : null;
            }
            catch { return null; }
        }

        private static string GetMachineGuid()
        {
            try
            {
                using (var rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography"))
                    return rk?.GetValue("MachineGuid") as string;
            }
            catch { return null; }
        }
    }

}
