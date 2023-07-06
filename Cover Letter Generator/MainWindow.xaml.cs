using Cover_Letter_Generator.Templates;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.Model;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;

namespace Cover_Letter_Generator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //CreateDocument();
            UserInfo info = new()
            {
                PhoneNumber = "513-867-5309",
                Email = "email@mail.uc.edu",
                Name = "Richy Rich",
                Address = "1600 Pennsylvania Avenue NW, Washington, DC 20500"
            };
            // var fs = new FileStream(@"C:\Users\Nick\source\repos\Cover Letter Generator\Cover Letter Generator\Document.docx", FileMode.Create, FileAccess.Write);

            //            var doc = new Template.Templates.BasicTemplate().ApplyTemplate(info, "Hiring Manager", "hello");
            //          doc.Write(fs);

            // ReplaceTextInWordDocument2(@"C:\Users\Nick\source\repos\Cover Letter Generator\Cover Letter Generator\Basic Template.docx", "%name%", "Nick Bell", @"C:\Users\Nick\source\repos\Cover Letter Generator\Cover Letter Generator\output.docx");
            //   gptAsync();
            Dictionary<string, string> replacements = new()
            {
                {"%name%",info.Name},
                {"%email%",info.Email },
                {"%phone%",info.PhoneNumber },
                {"%address%",info.Address },
                {"%recipient%","Hiring Manager" },
                {"%body%","this is the body" }
            };
            var input = @"C:\Users\Nick\source\repos\Cover Letter Generator\Cover Letter Generator\Basic Template.docx";
            var output = @"C:\Users\Nick\source\repos\Cover Letter Generator\Cover Letter Generator\output.docx";
            StaticClasses.WordTools.MultiReplacement2(input, replacements, output);
        }

     
        private async Task gptAsync()
        {
            try
            {
                var resp = await ChatGPT.ChatGPT_API.GetChatGPTResponse(ChatGPT.ChatGPT_API.Key, "write me a cover letter for this company,I do not want the response to include a greeting or a signature , here are the details: \n" + File.ReadAllText(@"C:\Users\Nick\source\repos\Cover Letter Generator\Cover Letter Generator\job description.txt"));
                Console.WriteLine(resp);

                Console.WriteLine("Done");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CreateDocument()
        {
            var newFile2 = @"C:\Users\Nick\source\repos\Cover Letter Generator\Cover Letter Generator\Document.docx";
            using (var fs = new FileStream(newFile2, FileMode.Create, FileAccess.Write))
            {
                XWPFDocument doc = new XWPFDocument();
                var p0 = doc.CreateParagraph();
                p0.Alignment = ParagraphAlignment.CENTER;
                XWPFRun r0 = p0.CreateRun();
                r0.FontFamily = "microsoft yahei";
                r0.FontSize = 18;
                r0.IsBold = true;
                r0.SetText("This is title");

                var p1 = doc.CreateParagraph();
                p1.Alignment = ParagraphAlignment.LEFT;
                p1.IndentationFirstLine = 500;
                XWPFRun r1 = p1.CreateRun();
                r1.FontFamily = "·ÂËÎ";
                r1.FontSize = 12;
                r1.IsBold = true;
                r1.SetText("This is content, content content content content content content content content content");

                doc.Write(fs);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Content = new CoverLetterForm();
            NavbarContentFrame.Content = new Navbar();
            LoadRtfIntoFlowDocumentViewer(@"C:\Users\Nick\Desktop\test.rtf", documentViewer);
          /*  string filePath = @"C:\Users\Nick\source\repos\Cover Letter Generator\Cover Letter Generator\output.docx";

            FlowDocument flowDocument = new FlowDocument();
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, false))
            {
                MainDocumentPart mainPart = wordDoc.MainDocumentPart;
                using (var stream = mainPart.GetStream())
                {
                    TextRange textRange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
                    textRange.Load(stream, DataFormats.Rtf);
                }
            }

            // Set the FlowDocument as the source of the FlowDocumentReader
            documentViewer.Document = flowDocument;*/
        }
        public void LoadRtfIntoFlowDocumentViewer(string filePath, FlowDocumentReader flowDocumentViewer)
        {
            // Read the RTF content from the file
            string rtfText = File.ReadAllText(filePath);

            // Create a FlowDocument from the RTF content
            FlowDocument flowDocument = new FlowDocument();
            TextRange textRange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
            using (MemoryStream rtfMemoryStream = new MemoryStream())
            {
                using (StreamWriter rtfStreamWriter = new StreamWriter(rtfMemoryStream))
                {
                    rtfStreamWriter.Write(rtfText);
                    rtfStreamWriter.Flush();
                    rtfMemoryStream.Seek(0, SeekOrigin.Begin);

                    // Load the RTF content into the FlowDocument
                    textRange.Load(rtfMemoryStream, DataFormats.Rtf);
                }
            }

            // Set the FlowDocument as the Document property of the FlowDocumentViewer
            flowDocumentViewer.Document = flowDocument;
        }
    }
}
