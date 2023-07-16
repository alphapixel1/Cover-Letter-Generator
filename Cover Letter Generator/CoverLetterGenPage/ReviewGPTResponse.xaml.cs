using Cover_Letter_Generator.ChatGPT;
using System;
using System.Collections.Generic;
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
using static Cover_Letter_Generator.ChatGPT.GPTUserInfoDocGenerator;

namespace Cover_Letter_Generator.CoverLetterGenPage
{
    /// <summary>
    /// Interaction logic for ReviewGPTResponse.xaml
    /// </summary>
    public partial class ReviewGPTResponse : Page
    {
        private readonly GPTUserInfoDocGenerator.ModifiedUserInfoAndGptResponse? Response;
        private readonly CoverLetterForm.PageRecoveryClass recoveryClass;
        private Frame OwnerFrame;
        private Prompt? PromptClass;
        private string? promptText;
        private ChatGptResponse response;

        public ReviewGPTResponse(string prompt,CoverLetterForm.PageRecoveryClass recoveryClass, Frame ownerFrame)//for displaying generated prompts
        {
            OwnerFrame = ownerFrame;
            this.recoveryClass = recoveryClass;
            this.promptText = prompt;
            InitializeComponent();
        }
     

        public ReviewGPTResponse(ChatGptResponse response, CoverLetterForm.PageRecoveryClass recoveryClass, Frame ownerFrame, Prompt prompt)
        {
            this.response = response;
            this.recoveryClass = recoveryClass;
            OwnerFrame = ownerFrame;
            this.PromptClass = prompt;
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
            //recoveryClass.Template.
            GenerateDocx(PromptClass.Replacements, recoveryClass.Template, ResponseBox.Text);
        }
    }
}
