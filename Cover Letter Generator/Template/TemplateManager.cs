using Cover_Letter_Generator.StaticClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cover_Letter_Generator.Template
{
    public static class TemplateManager
    {
        public static string TemplatesFolder => FileManager.GetAppDirectory("Templates");

        public static string TemplatesFile=> TemplatesFolder + "\\TemplatesList.templates";
        private static string getGroupPath(string folderName)
        {
            var ret = TemplatesFolder + "\\"+folderName;
            if (!Directory.Exists(ret))
                Directory.CreateDirectory(ret);
            return ret;
        }
        public static string UserTemplates => getGroupPath("UserTemplates");
        
        public static string MicrosoftTemplates => getGroupPath("MicrosoftTemplates");

        public static string OtherTemplates => getGroupPath("OtherTemplates");

        public static List<Template>? GetTemplates()
        {
            if (File.Exists(TemplatesFile))
            {
                try
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Template>>(File.ReadAllText(TemplatesFile));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An Error Occurred While Getting TemplatesList.templates");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("TemplatesList.templates Cannot Be Found");
                return null;
            }   
        }
        public static void SaveTemplates(List<Template> templates)
        {
            var t=Newtonsoft.Json.JsonConvert.SerializeObject(templates, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(TemplatesFile, t);
        }


        public static void InitialSetup()
        {
            if (!File.Exists(TemplatesFile))
            {
                var temps = new List<Template>();

                //microsoft
                Dictionary<string, byte[]> mc = new()
                {
                    {"Letterhead Simple" ,Properties.Resources.Letterhead_Simple_Template},
                    {"Social Sedia Marketing",Properties.Resources.Social_media_marketing_Template }

                };
                foreach (var item in mc)
                {
                    var fname = item.Key + ".docx";
                    var path = $"{MicrosoftTemplates}\\{fname}";
                    var pdfpath = Path.GetFileNameWithoutExtension(path) + ".pdf";
                    File.WriteAllBytes(path, item.Value);
                    //WordTools.ConvertDocxToPdf(path, pdfpath);
                    temps.Add(new(item.Key, fname, "", TemplateGroup.Microsoft));
                }

                Dictionary<string, byte[]> other = new()
                {
                    {"Nicks Choice" ,Properties.Resources.Basic_Template},
                };
                foreach (var item in other)
                {
                    var fname= item.Key+".docx";
                    var path = $"{OtherTemplates}\\{fname}";
                    var pdfpath = Path.GetFileNameWithoutExtension(path) + ".pdf";
                    File.WriteAllBytes(path, item.Value);
                    //WordTools.ConvertDocxToPdf(path,pdfpath);
                    temps.Add(new(item.Key, fname,"", TemplateGroup.Other));
                }

                SaveTemplates(temps);
            }
        }
    }
}
