using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Cover_Letter_Generator.ChatGPT
{
    public class ChatGptResponse
    {
        public string id;
        [JsonProperty("object")]
        public string _object;
        public string model;
        public List<Choice> choices=new();

        public object GetRequestBody() => new
        {
            messages = choices.Select(e => e.message.Clone()).ToArray(),
            model = "gpt-3.5-turbo",
            //conversation_id = id,
        };

        public void AddMessage(Message message)
        {
            choices.Add(new() { index=choices.Count,message= message.Clone() });
        }
        public Message? GetLastMessage()
        {
            if(choices.Count>0)
                return choices.Last().message;
            return null;
        }
    }
    public class Choice
    {
        public int index;
        public Message message;
        public override string ToString() => message.content;
    }
    public class Message
    {
        public string role, content;
        public override string ToString() => content;
        internal Message Clone() => new() { role = role, content = content };
    }
}
