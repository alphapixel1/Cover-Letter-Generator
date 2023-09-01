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
using System.Xml;
using HtmlAgilityPack;
using System.Net;
using Cover_Letter_Generator.External_Job_Boards;

namespace Cover_Letter_Generator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Navbar navbar;
        public async void tryGet()
        {
            //var info=await new LinkedIn().GetInfo("https://www.linkedin.com/jobs/view/3702678631");
            Console.WriteLine(await new LinkedIn().GetHtmlFromUrl("https://www.indeed.com/viewjob?jk=70ad3edea9ac538e&tk=1h99b2umogrh2800&from=hp&advn=8516238283002983&adid=288770639&ad=-6NYlbfkN0Ar5Izk-CWwVEbwUG7ji387dnTDr8mbbPq87XdjCmmdhtl7H4HvQB9HLwmZWWlrspPN6CguQpgrr6b9QQJBslzybQRAgiBpJmXWPIFqi4JPgWkjYepFHTRRtttsukqS91vnCqbX5WfttR6qmCWk1Uwv6oaTUxeEIkZ7V7CLdqwr18lVW1PaXe_lHwEwoEeOE1shkfYcwHFQTunhpe-17OtR77fKrXB-xtfBwG7Vr6GuTScR6oFrTv62asv0EXO0VCit3Z88xYmO6ZAzw-Q4EolJrKICmV8JZjn6ZeRacTWBl86s7B9zNFdHPq-wBkJvtYQaOn_XqvVVblRofyAxLTJCCq2dFxA3fimf_8khj3DjeoSt6FxF-iHCmWTa2fdwO3hUg1VN7zrA9a6no-chNIam6MEqT_iUlKtnOBvI7EEoC9GXVuhclHpAnmEZRoR2U_N-n9nK4j86cl898UwbFBLzmn7b1aEP4yk%3D&sjdu=Y4DFEt1XKMzyZfMUGiwoWbdjPoTA-P6TrsTT3brrlpcX7M0muZToxxuDRS1NRwgpxpfgcM-9ymkFMDoFKYaRyJunmW_Ct3TOcZi2NFmrLms&acatk=1h99b3o01is2u800&pub=4a1b367933fd867b19b072952f68dceb&xkcb=SoBC-_M3LpeEPMRd9h0LbzkdCdPP&vjs=3"));
        }
        public MainWindow()
        {
            //tryGet();
          //  return;
            TemplateManager.InitialSetup();
            InitializeComponent();
            //CreateDocument();
            //RTFTest();
           /// gptAsync();
            return;

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
            StaticClasses.WordTools.MultiReplacement(med, replacements, output);
            /*TemplateManager.SaveTemplates(new()
            {
                new("Letterhead Simple","Letterhead Simple Template.docx",TemplateGroup.Microsoft)
            });*/
            Console.WriteLine(TemplateManager.MicrosoftTemplates);
            
        }
        private void RTFTest()
        {
            string path = @"C:\Users\Nick\Desktop\test resume.rtf";
            System.Windows.Forms.RichTextBox richTextBox = new();

            // Load the RTF content from a file
            richTextBox.LoadFile(path);
            var data=UserInfo.UserInfoData.GetSavedData().AllReplacements;
            data.Add("body", "test\nbitch");
            foreach (var item in data)
            {
                richTextBox.Rtf = richTextBox.Rtf.Replace("%"+item.Key+"%", item.Value);
            }
            

            // Save the modified RTF content to a new file
            richTextBox.SaveFile(@"C:\Users\Nick\Desktop\test resume output.rtf");

            // Display success message
            Console.WriteLine("Text replacement completed.");
        }
     
        private async Task gptAsync()
        {
            
            var resp = await ChatGPT.ChatGPT_API.GetChatGPTResponse(ChatGPT.ChatGPT_API.Key, "take this number: 10");
            Console.WriteLine(resp.GetLastMessage());
            await ChatGPT.ChatGPT_API.GetChatGPTResponse(ChatGPT.ChatGPT_API.Key, "Now add 5", resp);
            Console.WriteLine(resp.GetLastMessage());
            await ChatGPT.ChatGPT_API.GetChatGPTResponse(ChatGPT.ChatGPT_API.Key, "Now add 7", resp);
            Console.WriteLine(resp.GetLastMessage());

            Console.WriteLine("Done");
            
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
            MainContentFrame.Content = new CoverLetterForm(new CoverLetterForm.PageRecoveryClass()
                    {
                Company = "CetriK",
                Recipient = "CetriK",
                Description = " About the job\nWe are seeking talented interns to experience, contribute and grow at a fast-moving Blockchain Startup in NYC!\n\nAbout The Company\n\nOne of the fastest-growing and most trusted companies in blockchain security, CertiK is a true market leader. To date, CertiK has worked with over 3,200 Enterprise clients, secured over $310 billion worth of digital assets, and has detected over 60,000 vulnerabilities in blockchain code. Our clients include leading projects such as Aave, Polygon, Binance Smart Chain, Terra, Yearn, and Chiliz.\n\nInvestors = Insight Partners, Sequoia, Tiger Global, Coatue Management, Lightspeed, Advent International, SoftBank, Hillhouse Capital, Goldman Sachs, Coinbase Ventures, Binance, Shunwei Capital, IDG Capital, Wing, Legend Star, Danhua Capital and other investors.\n\nYou are\n\n    Thriving in a fast-paced, dynamic working environment.\n    Self-driven and passionate about cutting-edge technologies. Problem solver and fast learner with good analysis skills.\n    A strong team-player and always willing to make a positive impact on the team. Good verbal and written communication skills.\n\nRequirements\n\n    A BS/MS/PhD degree in Computer Science or relevant field or equivalent professional experience. Mastery of one or more backend languages: Python, Javascript(NodeJS), Golang, Ruby, Scala, Java, C/C++, etc.\n    Solid computer science fundamentals in object-oriented design, data structure and algorithms, computer networks, database systems, distributed systems, etc.\n    Excellent written communication/presentation skills, able to conduct client-facing meetings, output technical blogs addressing general/security topics, great team collaborations.\n    CI/CD, Docker, Git, Shell, Databases, Messaging Systems, Data Processing (Spark).\n\nCompensation: depending on level of experience: $2000 - $6000/month (fulltime)",
                GptPrompt =UserInfo.UserInfoData.GetSavedData().ChatGPTPrompt,
                JobTitle = "Software Engineer Intern",
                Template = TemplateManager.GetTemplates()[0],
            }, MainContentFrame);
            navbar = new Navbar();
            NavbarContentFrame.Content = navbar;
            navbar.NavigationEvent += Navbar_NavigationEvent;
        }

        private void Navbar_NavigationEvent(object? sender, Navigation.NavigationPage e)
        {
            MainContentFrame.Content = Navigation.NavigationPages.GetPage(e,MainContentFrame);
        }
    }
}
