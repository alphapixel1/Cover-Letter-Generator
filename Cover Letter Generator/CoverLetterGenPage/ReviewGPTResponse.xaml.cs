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

namespace Cover_Letter_Generator.CoverLetterGenPage
{
    /// <summary>
    /// Interaction logic for ReviewGPTResponse.xaml
    /// </summary>
    public partial class ReviewGPTResponse : Page
    {
        private readonly GPTUserInfoDocGenerator.ModifiedUserInfoAndGptResponse Response;
        private readonly CoverLetterForm.RecoveryClass recoveryClass;
        private Frame OwnerFrame;

        public ReviewGPTResponse(GPTUserInfoDocGenerator.ModifiedUserInfoAndGptResponse r, CoverLetterForm.RecoveryClass recoveryClass, Frame ownerFrame)
        {
            this.Response = r;
            OwnerFrame = ownerFrame;
            this.recoveryClass = recoveryClass;
            InitializeComponent();
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ResponseBox.Text = Response.GptResponse;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            OwnerFrame.Content = new CoverLetterForm(recoveryClass,OwnerFrame);
        }
    }
}
