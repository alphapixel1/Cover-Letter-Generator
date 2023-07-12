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
        CoverCreator,Settings,UserInfo,Templates
    }
    internal class NavigationPages
    {
        public static Page GetPage(NavigationPage page,Frame frame)
        {
            switch (page)
            {
                case NavigationPage.CoverCreator:
                    return new CoverLetterForm(frame);
                case NavigationPage.Settings:
                    return null;
                case NavigationPage.UserInfo:
                    return new UserInfo.UserInfoPage();
                case NavigationPage.Templates:
                    return new TemplateSelectionPage();
                default:
                    return null;
            }
        }
    }
}
