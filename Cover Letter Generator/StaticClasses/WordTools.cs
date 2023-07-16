using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Drawing.Printing;
using System.Windows;

namespace Cover_Letter_Generator.StaticClasses
{
    public static class WordTools
    {
        public static void MultiReplacement(string template, Dictionary<string, string> replacements, string newFilePath)
        {
            MessageBox.Show("WordTools.MultiReplacement does not respect \\n breaks fix this");
            File.Copy(template, newFilePath, true);

            using (WordprocessingDocument doc = WordprocessingDocument.Open(newFilePath, true))
            {
                MainDocumentPart mainPart = doc.MainDocumentPart;
           
                List<Text> texts = mainPart.RootElement.Descendants<Text>().ToList();
                //add header texts
                texts.AddRange(mainPart.HeaderParts.SelectMany(e =>e.Header.Descendants<Run>().SelectMany(e => e.Descendants<Text>())));
                //add footer
                texts.AddRange(mainPart.FooterParts.SelectMany(e =>e.Footer.Descendants<Run>().SelectMany(e => e.Descendants<Text>())));

                foreach (var text in texts)
                {
                    Console.WriteLine(text.Text);
                    foreach (KeyValuePair<string, string> entry in replacements)
                    {
                        while (text.Text.Contains(entry.Key))
                        {
                            text.Text = text.Text.Replace(entry.Key, entry.Value);
                        }
                    }
                }
                doc.Save();
            }
        }

        public static void ConvertDocxToPdf(string docxFilePath, string pdfFilePath)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(docxFilePath, true))
            {
                doc.Clone(pdfFilePath);
            }
        }
    }
}
