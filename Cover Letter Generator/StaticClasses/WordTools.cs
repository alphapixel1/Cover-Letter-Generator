using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Drawing.Printing;
using System.Windows;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using Paragraph = Microsoft.Office.Interop.Word.Paragraph;
using Application = Microsoft.Office.Interop.Word.Application;

namespace Cover_Letter_Generator.StaticClasses
{
    public static class WordTools
    {
        [Obsolete("MultiReplacement is deprecated, please use MultiReplacement2 instead.")]
        public static void MultiReplacement(string template, Dictionary<string, string> replacements, string newFilePath)
        {

            // REPLACE NEW LINES WITH A RANDOM STRING AND THEN ONCE REPLACEMENTS ARE DONE, BREAK UP THE TEXT INTO TEXT BREAK TEXT
       /*     File.Copy(template, newFilePath, true);

            using (WordprocessingDocument doc = WordprocessingDocument.Open(newFilePath, true))
            {
                MainDocumentPart mainPart = doc.MainDocumentPart;

                List<Text> texts = mainPart.RootElement.Descendants<Text>().ToList();
                //add header texts
                texts.AddRange(mainPart.HeaderParts.SelectMany(e => e.Header.Descendants<Run>().SelectMany(e => e.Descendants<Text>())));

                //add footer
                texts.AddRange(mainPart.FooterParts.SelectMany(e => e.Footer.Descendants<Run>().SelectMany(e => e.Descendants<Text>())));
                foreach (var text in texts)
                {
                    //Console.WriteLine(text.Text);
                    foreach (KeyValuePair<string, string> entry in replacements)
                    {
                        while (text.Text.Contains(entry.Key))
                        {
                            if (entry.Value.Contains("\n"))
                            {
                                var index = text.Text.IndexOf(entry.Key);
                                var start = text.Text.Substring(0, index);

                                var end = text.Text.Substring(start.Length + entry.Key.Length, text.Text.Length - (index + entry.Key.Length));

                                var lines = Regex.Split(entry.Value, "[\n\r]{1,}");


                                Paragraph firstParagraph = (Paragraph)text.Parent.Parent;
                                var paraProperties = firstParagraph.ParagraphProperties; // Get the paragraph properties

                                text.Text = start + lines[0];

                                Paragraph lastParagraph = firstParagraph;

                                for (int i = 1; i < lines.Length; i++)
                                {
                                    // Create a new paragraph
                                    Paragraph p = new Paragraph();

                                    // Clone and set the paragraph properties
                                    if (paraProperties != null)
                                    {
                                        p.ParagraphProperties = (ParagraphProperties)paraProperties.CloneNode(true);
                                    }

                                    // Clone and set the run properties
                                    Run run = new Run();
                                    RunProperties runProperties = ((Run)text.Parent).RunProperties;

                                    if (runProperties != null)
                                    {
                                        run.RunProperties = (RunProperties)runProperties.CloneNode(true);
                                    }

                                    // Add text to the run
                                    var t = (lines.Length == i + 1) ? lines[i] + end : lines[i];
                                    run.AppendChild(new Text(t));

                                    // Add the run to the paragraph
                                    p.AppendChild(run);

                                    // Insert the new paragraph after the lastParagraph
                                    lastParagraph.InsertAfterSelf(p);
                                    lastParagraph = p;
                                }

                            }
                            else
                            {
                                text.Text = text.Text.Replace(entry.Key, entry.Value);
                            }

                        }
                    }
                }
            }*/

        }

        public static bool MultiReplacement2(string templatePath, Dictionary<string, string> replacements, string newFilePath)
        {
            File.Copy(templatePath, newFilePath, true);
            Application app = new();
            Microsoft.Office.Interop.Word.Document document = app.Documents.Open(newFilePath);
            try
            {
                foreach (var replacement in replacements)
                {
                    foreach (Paragraph paragraph in document.Paragraphs)
                    {
                        FindAndReplaceInStoryRange(paragraph.Range, replacement.Key,replacement.Value);
                    }
                }
                foreach (var replacement in replacements)
                {
                    foreach (Section section in document.Sections)
                    {
                        foreach (HeaderFooter hf in section.Headers)
                        {
                            FindAndReplaceInStoryRange(hf.Range, replacement.Key,replacement.Value);
                        }
                        foreach (HeaderFooter hf in section.Footers)
                        {
                            FindAndReplaceInStoryRange(hf.Range, replacement.Key,replacement.Value);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.Message);
                document.Close();
                Marshal.ReleaseComObject(app);
                try
                {
                    File.Delete(newFilePath);
                }
                catch
                {
                    Console.WriteLine("Unable to delete output file \"" + newFilePath + "\"");
                }

                return false;
            }
            document.Save();
            document.Close();
            app.Quit();
            Marshal.ReleaseComObject(app);
            return true;
        }
        static void FindAndReplaceInStoryRange(Microsoft.Office.Interop.Word.Range range, string Key,string Value)
        {
                range.Find.ClearFormatting();
                range.Find.Text = Key;

                while (range.Find.Execute())
                {
                    Console.WriteLine("looping: "+Key);
                    if (range.Find.Found)
                    {
                        range.Text = range.Text.Replace(Key, Value);
                    }
                    else
                    {
                        break;
                    }
                }
            
        }

        public static bool ConvertToPdf(string docx, string output)
        {
            try
            {
                Application wordApp = new Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(docx);
                doc.ExportAsFixedFormat(output, WdExportFormat.wdExportFormatPDF);
                doc.Close();
                wordApp.Quit();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
