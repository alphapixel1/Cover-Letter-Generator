using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cover_Letter_Generator.UserInfo
{
    internal class UserInfoData
    {
        public string Name, Email, PhoneNumber, Website;
        public string Street, City, State, Zip;

        public string Address => $"{Street}, {City}, {State}, {Zip}";

        public Dictionary<string, string> CustomReplacements = new Dictionary<string, string>();

        public Dictionary<string, string> Replacements => new Dictionary<string, string>()
        {
            {"%name%",Name},
            {"%email%",Email },
            {"%phone%",PhoneNumber },
            {"%address%",Address },
            {"%street%",Street},
            {"%city%",City},
            {"%state%",State},
            {"%zip%",Zip},
            {"%website%",Website},
        };
    }
}
