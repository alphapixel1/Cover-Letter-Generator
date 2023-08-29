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
             MessageBox.Show("WordTools.MultiReplacement does not respect \\n breaks fix this");
            Console.WriteLine(newFilePath);
            // REPLACE NEW LINES WITH A RANDOM STRING AND THEN ONCE REPLACEMENTS ARE DONE, BREAK UP THE TEXT INTO TEXT BREAK TEXT
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
