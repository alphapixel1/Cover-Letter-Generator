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

namespace Cover_Letter_Generator.Template.Template_User_Controls
{
    /// <summary>
    /// Interaction logic for LargeTemplateControl.xaml
    /// </summary>
    public partial class LargeTemplateControl : UserControl
    {
        private readonly Template template;
        public event EventHandler<Template> selected;
        public LargeTemplateControl(Template template)
        {
            InitializeComponent();
            this.template = template;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TitleBlock.Text = template.Name;
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e) => Background = new SolidColorBrush(Colors.LightGray);

        private void Border_MouseLeave(object sender, MouseEventArgs e) => Background = new SolidColorBrush(Colors.White);

        private void Border_MouseDown(object sender, MouseButtonEventArgs e) => selected?.Invoke(this, template);
    }
}
