using Cover_Letter_Generator.Navigation;
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

namespace Cover_Letter_Generator
{
    /// <summary>
    /// Interaction logic for NavBar.xaml
    /// </summary>
    public partial class Navbar : Page
    {
        private bool Collapsed = false;
        public Navbar()
        {
            InitializeComponent();
        }


        private void NavBarOpenClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Collapsed = !Collapsed;
            foreach (var item in ButtonGrid.Children.OfType<NavigationButtonUC>())
            {
                item.Collapsed = Collapsed;
            }
        }
    }
}
