using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;

// IMVU CID Reader
namespace Triggerbot.Support;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("This tool will collect your IMVU and Triggerbot cache files and send them to Triggerbot Support.");
        Console.WriteLine("Files to be sent:");
        Console.WriteLine("  1) %APPDATA%\\IMVU\\productInfoCache.db");
        Console.WriteLine("  2) %APPDATA%\\Triggerless\\Triggerbot\\appCache.sqlite");
        Console.WriteLine();
        Console.Write("Do you approve sending these files? (yes/no): ");

        string consent = Console.ReadLine()?.Trim().ToLower() ?? "";
        if (consent != "yes")
        {
            Console.WriteLine("Operation cancelled. Nothing sent.");
            return;
        }

        //---------------------------------------------------------------------
        // DETECT CID + USERNAME
        //---------------------------------------------------------------------
        long cid = AvatarNameReader.GetAvatarId();
        string username = "Unknown";

        if (cid > 0)
        {
            Console.WriteLine($"Detected IMVU Customer ID: {cid}");
            username = await GetUsernameFromApi(cid);
            Console.WriteLine($"Detected Username: {username}");
        }
        else
        {
            Console.WriteLine("Could not detect IMVU Customer ID.");
        }

        //---------------------------------------------------------------------
        // ZIP FILES
        //---------------------------------------------------------------------
        string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string file1 = Path.Combine(appdata, "IMVU", "productInfoCache.db");
        string file2 = Path.Combine(appdata, "Triggerless", "Triggerbot", "appCache.sqlite");

        string zipPath = Path.Combine(Environment.CurrentDirectory, "imvu_triggerbot_cache.zip");
        if (File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }

        Console.WriteLine("\nCreating ZIP: " + zipPath);

        using (ZipArchive zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
        {
            if (File.Exists(file1))
                zip.CreateEntryFromFile(file1, "productInfoCache.db");

            if (File.Exists(file2))
                zip.CreateEntryFromFile(file2, "appCache.sqlite");
        }

        Console.WriteLine("ZIP file created.");

        //---------------------------------------------------------------------
        // EMAIL FIELDS
        //---------------------------------------------------------------------
        string gmailSender = "triggerbot.support@gmail.com";

        // Dummy encrypted password — replace this with your encrypted value
        string encryptedPassword = "4yX8UQbryUdXwzMNMkjMCL1NlZkdW/kajNfm5MAyj6rGgsmiOQvIoO/Pd9Ot43k6";

        // Your 32-character AES key (replace this)
        string decryptKey = "c5yî@^z@pj0o≈∙f6AN|1>(!uaqp";

        string appPassword = DecryptString(encryptedPassword, decryptKey);
        Console.WriteLine("DEBUG DECRYPTED PASSWORD: " + appPassword);
        //---------------------------------------------------------------------
        // SEND EMAIL WITH MAILKIT
        //---------------------------------------------------------------------
        try
        {
            Console.WriteLine("\nSending email...");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Triggerbot Support", gmailSender));
            message.To.Add(new MailboxAddress("Developer", "cheritriggers@gmail.com"));
            message.Subject = "IMVU/Triggerbot Cache Upload";

            var builder = new BodyBuilder();
            builder.TextBody =
                $"IMVU Support Upload\n\n" +
                $"Customer ID: {cid}\n" +
                $"Username:   {username}\n\n" +
                $"Attached: IMVU + Triggerbot cache\n";

            builder.Attachments.Add(zipPath);
            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.CheckCertificateRevocation = false;

                // 100% Gmail-compatible STARTTLS negotiation
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                // App Password auth
                client.Authenticate(gmailSender, appPassword);

                client.Send(message);
                client.Disconnect(true);
            }

            Console.WriteLine("Email sent successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR sending email:");
            Console.WriteLine(ex.ToString());
        }

        Console.ReadKey();
    }

    // =====================================================================
    // Helpers
    // =====================================================================

    static async Task<string> GetUsernameFromApi(long cid)
    {
        try
        {
            using HttpClient client = new HttpClient();
            string url = $"https://api.imvu.com/user/user-{cid}";
            string json = await client.GetStringAsync(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var response = JsonSerializer.Deserialize<ImvuUserResponse>(json, options);

            if (response?.denormalized != null && response.denormalized.Count > 0)
            {
                var item = response.denormalized.First().Value;
                return item.data.username;
            }
        }
        catch
        {
        }

        return "Unknown";
    }

    public static string DecryptString(string encryptedBase64, string key)
    {
        byte[] fullCipher = Convert.FromBase64String(encryptedBase64);

        byte[] iv = new byte[16];
        byte[] cipherText = new byte[fullCipher.Length - 16];

        Array.Copy(fullCipher, iv, 16);
        Array.Copy(fullCipher, 16, cipherText, 0, cipherText.Length);

        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream(cipherText);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);

        return sr.ReadToEnd();
    }
}

// =====================================================================
// JSON Models for IMVU API Response
// =====================================================================

public class ImvuUserResponse
{
    public string status { get; set; }
    public string id { get; set; }
    public Dictionary<string, ImvuUserDenormalized> denormalized { get; set; }
}

public class ImvuUserDenormalized
{
    public ImvuUserData data { get; set; }
}

public class ImvuUserData
{
    public string username { get; set; }
}