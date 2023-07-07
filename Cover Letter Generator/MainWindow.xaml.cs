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
using Cover_Letter_Generator.Template;

namespace Cover_Letter_Generator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Navbar navbar;
        public MainWindow()
        {
            TemplateManager.InitialSetup();
            InitializeComponent();
            //CreateDocument();
            UserInfo.UserInfoData info = new()
            {
                PhoneNumber = "513-867-5309",
                Email = "email@mail.uc.edu",
                Name = "Richy Rich",
                Street = "1600 Pennsylvania Avenue NW",
                City="DC",
                State="Washington",
                Zip= "20500",
                Website="https://github.com"
            };
            // var fs = new FileStream(@"C:\Users\Nick\source\repos\Cover Letter Generator\Cover Letter Generator\Document.docx", FileMode.Create, FileAccess.Write);

            //            var doc = new Template.Templates.BasicTemplate().ApplyTemplate(info, "Hiring Manager", "hello");
            //          doc.Write(fs);

            // ReplaceTextInWordDocument2(@"C:\Users\Nick\source\repos\Cover Letter Generator\Cover Letter Generator\Basic Template.docx", "%name%", "Nick Bell", @"C:\Users\Nick\source\repos\Cover Letter Generator\Cover Letter Generator\output.docx");
            //   gptAsync();
            Dictionary<string, string> replacements = info.Replacements;
            replacements.Add("%recipient%", "Hiring Manager");
            replacements.Add("%body%", "this is the body");
            replacements.Add("%company%", "company name");
            replacements.Add("%jobtitle%", "this is the job title");
            replacements.Add("%today%", DateTime.Now.ToString("M/d/y"));

            var input = @"C:\Users\Nick\source\repos\Cover Letter Generator\Cover Letter Generator\Templates\Basic Template.docx";
            var t2 = @"C:\Users\Nick\source\repos\Cover Letter Generator\Cover Letter Generator\Templates\MicrosoftTemplates\Letterhead Simple Template.docx";
            var med = @"C:\Users\Nick\source\repos\Cover Letter Generator\Cover Letter Generator\Templates\MicrosoftTemplates\Social media marketing Template.docx";
            var output = @"C:\Users\Nick\source\repos\Cover Letter Generator\Cover Letter Generator\output.docx";
            StaticClasses.WordTools.MultiReplacement2(med, replacements, output);
            /*TemplateManager.SaveTemplates(new()
            {
                new("Letterhead Simple","Letterhead Simple Template.docx",TemplateGroup.Microsoft)
            });*/
            Console.WriteLine(TemplateManager.MicrosoftTemplates);
            
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
            MainContentFrame.Content = new TemplateSelectionPage();
            navbar = new Navbar();
            NavbarContentFrame.Content = navbar;
            navbar.NavigationEvent += Navbar_NavigationEvent;
        }

        private void Navbar_NavigationEvent(object? sender, Navigation.NavigationPage e)
        {
            MainContentFrame.Content = Navigation.NavigationPages.GetPage(e);
        }
    }
}
