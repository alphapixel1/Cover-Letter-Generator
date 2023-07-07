using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cover_Letter_Generator.Template
{
    public enum TemplateGroup
    {
        User,Microsoft,Other
    }
    public class Template
    {
        public TemplateGroup TemplateGroup;
        public string Name,Filename,PdfName;

        public Template(string name,string filename,string pdfName,TemplateGroup templateGroup)
        {
            Name= name;
            Filename = filename;
            PdfName = pdfName;
            TemplateGroup = templateGroup;
        }

        public string? GetDocxPath()
        {
            switch (TemplateGroup)
            {
                case TemplateGroup.User:
                    return TemplateManager.UserTemplates + "\\" + Filename;
                case TemplateGroup.Microsoft:
                    return TemplateManager.MicrosoftTemplates + "\\" + Filename;
                case TemplateGroup.Other:
                    return TemplateManager.OtherTemplates+"\\"+Filename;
                default:
                    return null;
            }
        }
    }
}
