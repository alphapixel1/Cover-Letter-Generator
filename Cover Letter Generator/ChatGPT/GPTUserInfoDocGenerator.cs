using Cover_Letter_Generator.UserInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cover_Letter_Generator.ChatGPT
{
    public static class GPTUserInfoDocGenerator
    {
        public class ModifiedUserInfoAndGptResponse
        {
            public Dictionary<string, string> Replacements;
            public string? GptResponse;
            public ModifiedUserInfoAndGptResponse(Dictionary<string,string> replacements,string? gptResponse)
            {
                Replacements = replacements;
                GptResponse = gptResponse;
            }
        }
        public static ModifiedUserInfoAndGptResponse GetPrompt(UserInfoData userInfo, string company, string recipient, string jobDescription, string gptPrompt)
        {
            var replacements = userInfo.AllReplacements;


            foreach (var item in new Dictionary<string, string>()//I honestly dont know if this is needed
            {
                {"company",company },
                {"recipient",recipient },
                {"description",jobDescription},
            })
            {
                if (replacements.ContainsKey(item.Key))
                {
                    replacements[item.Key] = item.Value;
                }
                else
                {
                    replacements.Add(item.Key, item.Value);
                }
            }

            var prompt = gptPrompt;
            foreach (KeyValuePair<string, string> termReplacement in replacements)
            {
                prompt = prompt.Replace($"%{termReplacement.Key}%", termReplacement.Value);
            }
            return new(replacements,prompt);
        }
        public async static Task<ModifiedUserInfoAndGptResponse?> Generate(UserInfoData userInfo,string company,string recipient, string jobDescription,string gptPrompt)
        {
            var prompt = GetPrompt(userInfo, company, recipient, jobDescription, gptPrompt);
            Console.WriteLine(prompt);
            string? resp = null;
            try
            {
                resp = await ChatGPT_API.GetChatGPTResponse(ChatGPT_API.Key, prompt.GptResponse);//im using gptresponse because the class fits just the name doesn't
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            Console.WriteLine("\n\n response:");
            Console.WriteLine(resp);
            if (resp == null)
                return null;
            resp = new Regex(@"^(\s*\[[^\]]+\])+").Replace(resp, "");
            Regex manager = new Regex(@"^\s*Dear Hiring Manager,\s*");
            Regex sincierely = new Regex(@"\s*Sincerely,\s*\[Your Name]\s*");
            resp = manager.Replace(resp, "");
            resp = sincierely.Replace(resp, "");
            return new(prompt.Replacements,resp);
            //ChatGPT_API.GetChatGPTResponse(ChatGPT_API.Key,)
        }
    }
}
