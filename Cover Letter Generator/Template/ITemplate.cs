using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cover_Letter_Generator.Templates
{
    internal interface ITemplate
    {

        //add display image

        public string Name { get; }
        
        public XWPFDocument ApplyTemplate(UserInfo info, string recipient, string Text);
    }
}
