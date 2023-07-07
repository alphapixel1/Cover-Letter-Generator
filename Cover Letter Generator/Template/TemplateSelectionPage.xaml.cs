using Cover_Letter_Generator.Template;
using Cover_Letter_Generator.Template.Template_User_Controls;
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
    /// Interaction logic for TemplateSelectionPage.xaml
    /// </summary>
    public partial class TemplateSelectionPage : Page
    {
        public TemplateSelectionPage()
        {
            InitializeComponent();
        }
        private void RefreshTemplates()
        {
            foreach (var item in TemplateManager.GetTemplates())
            {
                TemplateWrap.Children.Add(new LargeTemplateControl(item));
            }
        }
    }
}
