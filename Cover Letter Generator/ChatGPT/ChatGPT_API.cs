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
        public static async Task<ChatGptResponse?> GetChatGPTResponse(string apiKey, string message, ChatGptResponse? conversation=null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
                object requestBody;
                if (conversation != null)
                {
                    conversation.AddMessage(new() { role = "system", content = message });
                   requestBody=conversation.GetRequestBody();
                }
                else
                {
                    requestBody = new
                    {
                        messages = new[]
                       {
                        new
                        {
                            role = "system",
                            content = message
                        }
                    },
                        model = "gpt-3.5-turbo"//"gpt-3.5-turbo",
                    };
                }
                
                string string_content = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(string_content, Encoding.UTF8, "application/json");
                //    Console.WriteLine(string_content);
                try
                {
                    File.WriteAllText(@"C:\Users\Nick\source\repos\Cover Letter Generator\Cover Letter Generator\ChatGPT\messageSent.json", string_content);
                }
                catch
                {
                    Console.WriteLine("Not running on nicks pc");
                }
                var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    //    Console.WriteLine(responseContent);
                    try
                    {
                        File.WriteAllText(@"C:\Users\Nick\source\repos\Cover Letter Generator\Cover Letter Generator\ChatGPT\ChatGptResponse.json", responseContent);
                    }
                    catch
                    {
                        Console.WriteLine("Not running on nicks pc");
                    }
                  //  dynamic responseData = JsonConvert.DeserializeObject(responseContent);
                    ChatGptResponse? chatGptResponse = JsonConvert.DeserializeObject<ChatGptResponse>(responseContent);
                    if (conversation != null)
                    {
                        conversation.AddMessage(chatGptResponse.GetLastMessage());
                        return conversation;
                    }
                    else
                    {
                        chatGptResponse.choices.Insert(0, new() { index = 0, message = new() { role = "system", content = message } });
                    }
                    return chatGptResponse;
                }
                else
                {
                    //handle error
                    
                    var errorResponseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("ChatGPT.ChatGPT_API error: ");
                    Console.WriteLine(errorResponseContent);
                    throw new Exception($"Failed to get ChatGPT response. API returned an error. Error message: {errorResponseContent}");
                }

            }
        }

    }
}
