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
using System.Windows.Shapes;

namespace Cover_Letter_Generator.CoverLetterGenPage
{
    /// <summary>
    /// Interaction logic for TemplateSelectionWindow.xaml
    /// </summary>
    public partial class TemplateSelectionWindow : Window
    {
        public Template.Template? SelectedTemplate=null;
        public TemplateSelectionWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var tsv= new Template.TemplateScrollView();
            tsv.TemplateSelected += Tsv_TemplateSelected;
            MainFrame.Content = tsv;
        }

        private void Tsv_TemplateSelected(object? sender, Template.Template e)
        {
            SelectedTemplateBlock.Text = e.Name;
            SelectedTemplate = e;
        }
    }
}
