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

namespace Cover_Letter_Generator.UserInfo
{
    /// <summary>
    /// Interaction logic for UserInfoPage.xaml
    /// </summary>
    public partial class UserInfoPage : Page
    {
        private Dictionary<string, string> CustomReplacements = new();
        private UserInfoData GetUserInfoFromForm()
        {
            return new UserInfoData()
            {
                Name = FullNameInput.Value,
                PhoneNumber= PhoneInput.Value,
                Email= EmailInput.Value,
                Website=WebsiteInput.Value,

                Street=StreetInput.Value,
                City=CityInput.Value,
                State=StateInput.Value,
                Zip=ZipInput.Value,
            };
        }

        private void LoadFromUserInfo(UserInfoData dat)
        {
            FullNameInput.Value = dat.Name;
            PhoneInput.Value = dat.PhoneNumber;
            EmailInput.Value = dat.Email;
            WebsiteInput.Value = dat.Website;

            StreetInput.Value = dat.Street;
            CityInput.Value = dat.City;
            StateInput.Value = dat.State;
            ZipInput.Value = dat.Zip;
        }
        public UserInfoPage()
        {
            InitializeComponent();
        }

        private void AddUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(CodeInput.Value.Length>0 && ReplacementInput.Value.Length > 0)
            {

            }
            else
            {
                MessageBox.Show("Empty Inputs", "Code and Replacement cannot be blank.",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
