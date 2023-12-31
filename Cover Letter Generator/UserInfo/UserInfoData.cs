﻿using Cover_Letter_Generator.StaticClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cover_Letter_Generator.UserInfo
{
    public class UserInfoData
    {
        public static string UserDataFolder => FileManager.GetAppDirectory("UserData");
        public static string UserDataFile => UserDataFolder + "\\UserData.json";

        public string Name, Email, PhoneNumber, Website, Gpa, SkillsAboutMe,
            Length = "4 paragraphs";//Length is body length for chatGPT
        public string Street, City, State, Zip;

        public string ChatGPTPrompt =
@"Write me a cover letter for this company. 
Info:
My GPA is %gpa%,
About me:
%aboutme%
The company is: %company%,
I want the body to be under %length%,
I want the cover letter formatted exactly like this:
Dear Hiring Manager,
Body
Sincerely,
[Your Name]

Role Description:
%description%";

        public string Address => $"{Street}, {City}, {State}, {Zip}";

        public Dictionary<string, string> CustomReplacements = new Dictionary<string, string>();

        public Dictionary<string, string> Replacements => new Dictionary<string, string>()
        {
            {"name",Name},
            {"email",Email },
            {"phone",PhoneNumber },
            {"address",Address },
            {"street",Street},
            {"city",City},
            {"state",State},
            {"zip",Zip},
            {"website",Website},
            {"gpa",Gpa},
            {"length", Length},
            {"aboutme",SkillsAboutMe},
        };
        public Dictionary<string, string> AllReplacements => Replacements.Concat(CustomReplacements).GroupBy(p => p.Key).ToDictionary(g => g.Key, g => g.First().Value);


        public static UserInfoData GetSavedData()
        {
            if(File.Exists(UserDataFile))
                try
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<UserInfoData>(File.ReadAllText(UserDataFile));
                }
                catch{ Console.WriteLine("Unable to parse UserDataFile"); }
            return new();
        }
        public static bool SaveData(UserInfoData userInfo)
        {
            try
            {
                File.WriteAllText(UserDataFile,Newtonsoft.Json.JsonConvert.SerializeObject(userInfo));
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static readonly IReadOnlyDictionary<string, string> reservedWords = new Dictionary<string,string>()
        {
             {"name","Your Name"},
            {"email", "Your Email"},
            {"phone", "Your Phone Number"},
            {"gpa","Your GPA" },
            {"address", "Street, City, State, Zip"},
            {"street","Your Street"},
            {"city","Your City"},
            {"state","Your State"},
            {"zip","Your Zip Code"},
            {"website","Your Website"},
            //{"recipient","The Person/Company you are sending this to"},//should this be blacklisted
            //{"jobtitle","The Job you are applying for"},//still need to make an input for this//
            {"body","The cover letter Body generated by ChatGPT"},
            //{"company","The company you are sending this to" },//should this be blacklisted?
            {"length","The length of the generated cover letter body" },
            {"aboutme","Skills/About you, mainly for GPT prompt" }
        };
    }
}
