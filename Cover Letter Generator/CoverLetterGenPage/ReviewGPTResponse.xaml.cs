using Cover_Letter_Generator.ChatGPT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Cover_Letter_Generator.ChatGPT.GPTUserInfoDocGenerator;

namespace Cover_Letter_Generator.CoverLetterGenPage
{
    /// <summary>
    /// Interaction logic for ReviewGPTResponse.xaml
    /// </summary>
    public partial class ReviewGPTResponse : Page
    {
        private readonly ModifiedUserInfoAndGptResponse? Response;
        private readonly CoverLetterForm.PageRecoveryClass recoveryClass;
        private Frame OwnerFrame;
        private Prompt? PromptClass;
        private readonly string? initResponseBoxText;
        private string? promptText;
        private ChatGptResponse response;

        public ReviewGPTResponse(string prompt,CoverLetterForm.PageRecoveryClass recoveryClass, Frame ownerFrame)//for displaying generated prompts
        {
            OwnerFrame = ownerFrame;
            this.recoveryClass = recoveryClass;
            promptText = prompt;
            Console.WriteLine("constructor 1");
            InitializeComponent();
        }
     

        public ReviewGPTResponse(ChatGptResponse response, CoverLetterForm.PageRecoveryClass recoveryClass, Frame ownerFrame, Prompt prompt,string? initResponseBoxText=null)
        {
            Console.WriteLine("constructor 2");
            this.response = response;
            this.recoveryClass = recoveryClass;
            OwnerFrame = ownerFrame;
            this.PromptClass = prompt;
            this.initResponseBoxText = initResponseBoxText;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (promptText == null)
            {
                
                ResponseBox.Text = GPTUserInfoDocGenerator.GetTextWithoutHeaders(response.GetLastMessage().content);
            }
            else
            {
                ResponseBox.Text = promptText;
                ContinueMenuBar.Visibility = Visibility.Collapsed;
            }
            if (initResponseBoxText != null)
                ResponseBox.Text = initResponseBoxText;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if(promptText == null)
                OwnerFrame.Content = new InitialResponseModifcationPage(PromptClass, recoveryClass, OwnerFrame, response);
            else
                OwnerFrame.Content = new CoverLetterForm(recoveryClass, OwnerFrame);
            
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {

            if (PromptClass == null)
                throw new Exception("PromptClass should not be null here");
            //System.Windows.MessageBox.Show(promptText);
            //OwnerFrame.Content = new PreviewPage(promptText,PromptClass, recoveryClass, OwnerFrame,ResponseBox.Text);
            //ChatGptResponse response, CoverLetterForm.PageRecoveryClass recoveryClass, Frame ownerFrame, Prompt prompt
            OwnerFrame.Content = new PreviewPage(response, recoveryClass, OwnerFrame, PromptClass,ResponseBox.Text);
            return;
            
            //recoveryClass.Template.
            var sfd=new SaveFileDialog();
            sfd.FileName = "Cover.docx";
            sfd.Filter = "Word Documents (*.docx)|*.docx";
            sfd.InitialDirectory = new Settings.Settings().DocxOuputLocation;
            sfd.DefaultExt = "docx";
            sfd.OverwritePrompt = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                GenerateDocx(sfd.FileName,PromptClass.Replacements, recoveryClass.Template, ResponseBox.Text);
                using Process fileopener = new Process();
                fileopener.StartInfo.FileName = "explorer";
                fileopener.StartInfo.Arguments = "\"" + sfd.FileName + "\"";
                fileopener.Start();

                // Open the Word document
                //Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(filePath);
            }
        }
    }
}
