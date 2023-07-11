using Cover_Letter_Generator.UserInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cover_Letter_Generator.ChatGPT
{
    public static class GPTUserInfoDocGenerator
    {
        public async static void Generate(UserInfoData userInfo,Template.Template template,string company,string recipient, string jobDescription,string gptPrompt)
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
                prompt=prompt.Replace($"%{termReplacement.Key}%", termReplacement.Value);
            }

            //ChatGPT_API.GetChatGPTResponse(ChatGPT_API.Key,)
        }
    }
}
