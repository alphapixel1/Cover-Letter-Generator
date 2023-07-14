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
    /// Interaction logic for InitialResponseModifcationPage.xaml
    /// </summary>
    public partial class InitialResponseModifcationPage : Page
    {
        private Prompt Prompt;
        private readonly Frame OwnerFrame;
        private readonly ChatGptResponse response;
        private readonly CoverLetterForm.PageRecoveryClass recoveryClass;

        public InitialResponseModifcationPage(Prompt prompt, CoverLetterForm.PageRecoveryClass recoveryClass, Frame ownerFrame,ChatGptResponse response)
        {
            this.Prompt = prompt;
            OwnerFrame = ownerFrame;
            this.response = response;
            this.recoveryClass = recoveryClass;
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            OwnerFrame.Content = new CoverLetterForm(recoveryClass, OwnerFrame);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshMessages();
        }

        private void SubmitPrompt_Click(object sender, RoutedEventArgs e)
        {
            if (PromptBox.Text.Length > 0)
            {
                this.IsEnabled = false;
                SendPromptAsync();
            }
        }
        private void RefreshMessages()
        {
            ResponseBox.Text=string.Join("\n\n"+new string('=',50)+"\n\n",response.choices.Select(e => $"{e.message.role.Substring(0, 1).ToUpperInvariant()}{e.message.role.Substring(1)}:\n\n"+e.message.content).ToList());
            ResponseBox.ScrollToEnd();
        }
        private async void SendPromptAsync()
        {
            var r=await ChatGPT_API.GetChatGPTResponse(ChatGPT_API.Key, PromptBox.Text, response);
            
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (r == null)
                {
                    MessageBox.Show("An error occured with ChatGPT", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    RefreshMessages();
                    //ResponseBox.Text = response.GetLastMessage().ToString();
                    Console.WriteLine(r.GetLastMessage());
                }
                IsEnabled = true;
                PromptBox.Focus();
            });
        }
    }
}
