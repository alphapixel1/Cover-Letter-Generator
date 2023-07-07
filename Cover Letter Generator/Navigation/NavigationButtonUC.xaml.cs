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

namespace Cover_Letter_Generator.Navigation
{
    /// <summary>
    /// Interaction logic for NavigationButtonUC.xaml
    /// </summary>
    public partial class NavigationButtonUC : UserControl
    {
        public static readonly DependencyProperty PageProperty = DependencyProperty.Register("Page", typeof(NavigationPage), typeof(NavigationButtonUC), new PropertyMetadata(null));
        public NavigationPage Page
        {
            get { return (NavigationPage)GetValue(PageProperty); }
            set { SetValue(PageProperty, value); }
        }

        public static readonly DependencyProperty BoldProperty = DependencyProperty.Register("Bold", typeof(bool), typeof(NavigationButtonUC), new PropertyMetadata(false,OnBoldChanged ));
        public bool Bold
        {
            get { return (bool)GetValue(BoldProperty); }
            set { SetValue(BoldProperty, value); }
        }
        private static void OnBoldChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = ((NavigationButtonUC)d);
            c.TitleBlock.FontWeight=c.Bold? FontWeights.Bold:FontWeights.Normal;
        }



        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(NavigationButtonUC), new PropertyMetadata(null));
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }


        public static readonly DependencyProperty DisplayTextProperty = DependencyProperty.Register("DisplayText", typeof(string), typeof(NavigationButtonUC),  new PropertyMetadata(string.Empty));

        public string DisplayText
        {
            get { return (string)GetValue(DisplayTextProperty); }
            set { SetValue(DisplayTextProperty, value); }
        }
        public NavigationButtonUC()
        {
            InitializeComponent();
        }
        public bool Collapsed {set{
                TitleBlock.Visibility = value ? Visibility.Collapsed : Visibility.Visible;
            } 
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Background= new SolidColorBrush(Colors.LightGray);

        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Background = null;
        }
    }
}
