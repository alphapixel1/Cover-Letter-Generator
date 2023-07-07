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

namespace Cover_Letter_Generator.Form
{
    /// <summary>
    /// Interaction logic for CustomTextControl.xaml
    /// </summary>
    public partial class CustomTextControl : UserControl
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(CustomTextControl), new PropertyMetadata(null));
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }


        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(CustomTextControl), new PropertyMetadata(string.Empty));

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }


        public CustomTextControl()
        {
            InitializeComponent();
        }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e) => IconImage.Visibility = Visibility.Collapsed;

        private void TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e) => IconImage.Visibility = Visibility.Visible;
    }
}
