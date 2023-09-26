using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Drawing.Printing;
using System.Windows;
using System.Text.RegularExpressions;

namespace Cover_Letter_Generator.StaticClasses
{
    public static class WordTools
    {
         public static void MultiReplacement(string template, Dictionary<string, string> replacements, string newFilePath)
        {

            // REPLACE NEW LINES WITH A RANDOM STRING AND THEN ONCE REPLACEMENTS ARE DONE, BREAK UP THE TEXT INTO TEXT BREAK TEXT
            File.Copy(template, newFilePath, true);

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
            }

        }
        /*     public static HashSet<string> FindRegex(Regex regex,string docxPath)
             {
                 if (File.Exists(docxPath))
                 {
                     using (WordprocessingDocument doc = WordprocessingDocument.Open(docxPath, true))
                     {
                         MainDocumentPart mainPart = doc.MainDocumentPart;

                         List<Text> texts = mainPart.RootElement.Descendants<Text>().ToList();
                         //add header texts
                         texts.AddRange(mainPart.HeaderParts.SelectMany(e => e.Header.Descendants<Run>().SelectMany(e => e.Descendants<Text>())));

                         //add footer
                         texts.AddRange(mainPart.FooterParts.SelectMany(e => e.Footer.Descendants<Run>().SelectMany(e => e.Descendants<Text>())));
                         HashSet<string> matches = new HashSet<string>();
                         foreach (var text in texts)
                         {
                             Console.WriteLine(text.Text);
                             if (regex.IsMatch(text.Text))
                             {
                                 matches.Add(regex.)
                             }

                         }
                         return matches;
                     }
                 }
                 else
                     throw new FileNotFoundException("File Not Found: "+docxPath);
                 return new HashSet<string>();
             }*/

        /*  public static void MultiReplacement(string template, Dictionary<string, string> replacements, string newFilePath)
          {
             // MessageBox.Show("WordTools.MultiReplacement does not respect \\n breaks fix this");
              File.Copy(template, newFilePath, true);

              using (WordprocessingDocument doc = WordprocessingDocument.Open(newFilePath, true))
              {
                  MainDocumentPart mainPart = doc.MainDocumentPart;


                  List<Run> runs = mainPart.RootElement.Descendants<Run>().ToList();
                  runs.AddRange(mainPart.HeaderParts.SelectMany(e => e.Header.Descendants<Run>()));
                  runs.AddRange(mainPart.FooterParts.SelectMany(e => e.Footer.Descendants<Run>()));



                  foreach (var run in runs)
                  {
                      foreach (var text in run.Descendants<Text>())
                      {
                          foreach (KeyValuePair<string, string> entry in replacements)
                          {
                              if (text.Text.Contains(entry.Key))
                              {
                                  int index = text.Text.IndexOf(entry.Key);
                                  string prefix = text.Text.Substring(0, index);
                                  string suffix = text.Text.Substring(index + entry.Key.Length);
                                  Run newRun = (Run)run.CloneNode(true);

                                  Text newText = newRun.Descendants<Text>().First();
                                  newText.Text = entry.Value;

                                  run.RemoveAllChildren<Text>();

                                  if (!string.IsNullOrEmpty(prefix))
                                      run.AppendChild(new Text(prefix));

                                  run.AppendChild(newRun);

                                  if (!string.IsNullOrEmpty(suffix))
                                      run.AppendChild(new Text(suffix));

                                  break; // Exit the keyword replacement loop for this Text element
                              }
                          }
                      }
                  }


                  doc.Save();
              }
          }*/

        public static void ConvertDocxToPdf(string docxFilePath, string pdfFilePath)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(docxFilePath, true))
            {
                doc.Clone(pdfFilePath);
            }
        }
    }
}
