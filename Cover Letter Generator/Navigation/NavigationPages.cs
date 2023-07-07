using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cover_Letter_Generator.Navigation
{
    public enum NavigationPage
    {
        Main,Settings,UserInfo,Templates
    }
    internal class NavigationPages
    {
        public static Page GetPage(NavigationPage page)
        {
            switch (page)
            {
                case NavigationPage.Main:
                    return new CoverLetterForm();
                case NavigationPage.Settings:
                    return null;
                case NavigationPage.UserInfo:
                    return null;
                case NavigationPage.Templates:
                    return new TemplateSelectionPage();
                default:
                    return null;
            }
        }
    }
}
