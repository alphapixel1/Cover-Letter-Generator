using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
namespace Cover_Letter_Generator.StaticClasses
{
    public static class WordTools
    {
        public static void MultiReplacement2(string template, Dictionary<string, string> replacements, string newFilePath)
        {
            File.Copy(template, newFilePath, true);

            using (WordprocessingDocument doc = WordprocessingDocument.Open(newFilePath, true))
            {
                MainDocumentPart mainPart = doc.MainDocumentPart;
                //get body texts
                List<Text> texts = mainPart.RootElement.Descendants<Text>().ToList();
                Console.WriteLine(texts.Count());
                //add header texts
                texts.AddRange(mainPart.HeaderParts.SelectMany(e =>
                {
                    return e.Header.Descendants<Run>().SelectMany(e => e.Descendants<Text>());
                }));
                //add footer
                texts.AddRange(mainPart.FooterParts.SelectMany(e =>
                {
                    return e.Footer.Descendants<Run>().SelectMany(e => e.Descendants<Text>());
                }));

                foreach (var text in texts)
                {
                    foreach (KeyValuePair<string, string> entry in replacements)
                    {
                        if (text.Text.Contains(entry.Key))
                        {
                            text.Text = text.Text.Replace(entry.Key, entry.Value);
                        }
                    }
                }
                doc.Save();
            }
        }
    }
}
