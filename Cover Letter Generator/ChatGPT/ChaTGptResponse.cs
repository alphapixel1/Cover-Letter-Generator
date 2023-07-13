using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Cover_Letter_Generator.ChatGPT
{
    internal class ChatGptResponse
    {
        public string id;
        [JsonProperty("object")]
        public string _object;
        public string model;
        public List<Choice> choices;
    }
    internal class Choice
    {
        public int index;
        public Message message;

    }
    internal class Message
    {
        public string role, content;
    }
}
