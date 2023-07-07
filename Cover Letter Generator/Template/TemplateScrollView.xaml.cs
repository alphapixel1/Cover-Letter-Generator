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

namespace Cover_Letter_Generator.Template
{
    /// <summary>
    /// Interaction logic for TemplateScrollView.xaml
    /// </summary>
    public partial class TemplateScrollView : Page
    {
        public event EventHandler<Template> TemplateSelected;
        public TemplateScrollView()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshTemplates();
        }
        public void RefreshTemplates()
        {
            TemplateStack.Children.Clear();
            var t = TemplateManager.GetTemplates();
            if (t != null)
            {
                var users = t.Where(e => e.TemplateGroup == TemplateGroup.User);
                var microsoft = t.Where(e => e.TemplateGroup == TemplateGroup.Microsoft);
                var other = t.Where(e => e.TemplateGroup == TemplateGroup.Other);
                foreach (var item in new Dictionary<string, IEnumerable<Template>>(){
                    {"User Templates",users},
                    {"Microsoft Templates",microsoft },
                    {"Other Templates",other }
                })
                {
                    if (item.Value.Count() > 0)
                    {
                        TemplateStack.Children.Add(new TextBlock()
                        {
                            FontSize = 17,
                            Text = item.Key,
                            Margin = new Thickness(0, 5, 0, 2)
                        });
                        var wrap = new WrapPanel();
                        foreach (var temp in item.Value)
                        {
                            var uc = new LargeTemplateControl(temp);
                            uc.selected += (a, b) => TemplateSelected?.Invoke(this,b);
                            wrap.Children.Add(uc);
                        }
                        TemplateStack.Children.Add(wrap);
                    }
                }

            }
        }
    }
}
