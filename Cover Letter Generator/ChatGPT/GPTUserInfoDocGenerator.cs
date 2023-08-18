using Cover_Letter_Generator.StaticClasses;
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
            public ChatGptResponse? GptResponse;
            public ModifiedUserInfoAndGptResponse(Dictionary<string,string> replacements, ChatGptResponse? gptResponse)
            {
                Replacements = replacements;
                GptResponse = gptResponse;
            }
        }
        public class Prompt
        {
            public string PromptText;
            public Dictionary<string, string> Replacements;
            public Prompt(Dictionary<string, string> replacements, string prompt)
            {
                Replacements = replacements;
                this.PromptText = prompt;
            }
        }
        public static Prompt GetPrompt(UserInfoData userInfo, string company, string recipient, string jobDescription, string gptPrompt)
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

        public static void GenerateDocx(Dictionary<string,string> replacements,Template.Template template,string chatGptText)
        {
            var replacements2 = new Dictionary<string, string>(replacements);
            replacements2.Add("body", chatGptText);
            foreach (var item in replacements2.ToList())
            {
                replacements2.Add($"%{item.Key}%", item.Value);
                replacements2.Remove(item.Key);
            }
            WordTools.MultiReplacement(template.GetDocxPath(), replacements2, new Settings.Settings().DocxOuputLocation + "\\test.docx");
        }

        public static string GetTextWithoutHeaders(string message)
        {
            message = new Regex(@"^(\s*\[[^\]]+\])+").Replace(message, "");
            Regex manager = new Regex(@"^\s*Dear Hiring Manager,\s*");
            Regex sincierely = new Regex(@"\s*Sincerely,\s*\[Your Name]\s*");
            message = manager.Replace(message, "");
            message = sincierely.Replace(message, "");
            return message;
        }
    }
}
