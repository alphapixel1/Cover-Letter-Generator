using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cover_Letter_Generator.ChatGPT
{
    internal class ChatGPT_API
    {
        public static string Key => File.ReadAllText(@"C:\Users\Nick\Desktop\gptSecret.txt");
        public static async Task<string> GetChatGPTResponse(string apiKey, string message)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

                var requestBody = new
                {
                    messages = new[]
                    {
                        new
                        {
                            role = "system",
                            content = message
                        }
                    },
                    model = "gpt-3.5-turbo"
                };

                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    File.WriteAllText(@"C:\Users\Nick\source\repos\Cover Letter Generator\Cover Letter Generator\ChatGPT\ChatGptResponse.json", responseContent);
                  //  dynamic responseData = JsonConvert.DeserializeObject(responseContent);
                    ChatGptResponse? chaTGptResponse = JsonConvert.DeserializeObject<ChatGptResponse>(responseContent);
                    return chaTGptResponse.choices[0].message.content;
                    //var chatResponse = responseData.choices[0].message.content;
                    //return chatResponse;
                }
                else
                {
                    //handle error
                    var errorResponseContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to get ChatGPT response. API returned an error. Error message: {errorResponseContent}");
                }

            }
        }
    }
}
