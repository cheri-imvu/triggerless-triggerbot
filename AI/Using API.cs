// API-Key: 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

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; // <-- Install Newtonsoft.Json via NuGet

class Program
{
    private static readonly HttpClient httpClient = new HttpClient();
    private static readonly string apiKey = "your-api-key-here";

    static async Task Main(string[] args)
    {
        Console.WriteLine("Enter your song clue:");
        string input = Console.ReadLine();

        while (!string.Equals(input, "STOP", StringComparison.OrdinalIgnoreCase))
        {
            string guess = await SendGuessAsync(input);
            Console.WriteLine($"ChatGPT's guess: {guess}");

            Console.WriteLine("\nEnter another clue (or type STOP to quit):");
            input = Console.ReadLine();
        }

        Console.WriteLine("Game stopped!");
    }

    static async Task<string> SendGuessAsync(string userInput)
    {
        var requestUri = "https://api.openai.com/v1/chat/completions";

        var requestBody = new
        {
            model = "gpt-4o", // You can use "gpt-4o" or "gpt-4"
            messages = new[]
            {
                new { role = "system", content = "You are playing a guessing game. When the user sends a text, respond ONLY with your best guess inside double quotes, even if it's wrong. No extra words, no commentary." },
                new { role = "user", content = userInput }
            },
            temperature = 0.3
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        HttpResponseMessage response = await httpClient.PostAsync(requestUri, jsonContent);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        dynamic responseJson = JsonConvert.DeserializeObject(responseBody);
        string reply = responseJson.choices[0].message.content;

        return reply;
    }
}
