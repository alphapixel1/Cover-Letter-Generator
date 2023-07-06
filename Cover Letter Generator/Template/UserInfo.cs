using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cover_Letter_Generator.Templates
{
    internal class UserInfo
    {
        public string Name, Email, PhoneNumber;
        public string Street,City, State, Zip;

        public string Address => $"{Street}, {City}, {State}, {Zip}";
    }
}
