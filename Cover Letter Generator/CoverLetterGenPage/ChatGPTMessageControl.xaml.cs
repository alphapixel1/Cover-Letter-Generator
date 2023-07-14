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
    /// Interaction logic for ChatGPTMessageControl.xaml
    /// </summary>
    public partial class ChatGPTMessageControl : UserControl
    {
        private readonly Message message;

        public ChatGPTMessageControl(Message message)
        {
            InitializeComponent();
            this.message = message;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var role = message.role;
            switch (message.role)
            {
                case "system":
                    role = "system";
                    break;
                case "assistant":
                    role = "ChatGPT";
                    break;
            }
            if (role == "system")
                role = "user";
            RoleBlock.Text = $"{role.Substring(0, 1).ToUpperInvariant()}{role.Substring(1)}";
            ContentBlock.Text = GPTUserInfoDocGenerator.GetTextWithoutHeaders(message.content);
            if (message.role == "assistant")
            {
                var brush= new SolidColorBrush(Color.FromRgb(116, 170, 156));
                TopBorder.Background = brush;
                TopBorder.BorderBrush = brush;
                MainBorder.BorderBrush = brush;
                MainBorder.Background = new SolidColorBrush(Color.FromArgb(127, 116, 170, 156));
            }
        }
    }
}
