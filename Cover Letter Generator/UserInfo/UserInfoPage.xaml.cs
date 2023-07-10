using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                ChatGPTPrompt= GPTPromptBox.Text,
                Length=CoverLengthInput.Value,
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
            CustomReplacements = dat.CustomReplacements;
            CoverLengthInput.Value = dat.Length;

            GPTPromptBox.Text = dat.ChatGPTPrompt;
            RefreshReplacementListBox();
        }
        public UserInfoPage()
        {
            InitializeComponent();
        }
        private void RefreshReplacementListBox()
        {
            ReplacementListBox.ItemsSource = null;
         ReplacementListBox.ItemsSource = CustomReplacements;
        }
        private void AddUpdate_Click(object sender, RoutedEventArgs e)
        {
            var code = CodeInput.Value.ToLower();
            var replacement = ReplacementInput.Value;
            if(code.Length>0 && replacement.Length > 0)
            {
                if(new Regex("[^a-z0-9]").IsMatch(code))
                {
                    MessageBox.Show("Code can only contain a-zA-Z0-9 Characters.", "Invalid Inputs", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (UserInfoData.reservedWords.Keys.Contains(code))
                    {
                        MessageBox.Show("Code has already been reserved.", "Reserved Code", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        if (CustomReplacements.ContainsKey(code))
                            CustomReplacements.Remove(code);
                        CustomReplacements.Add(code, replacement);
                        RefreshReplacementListBox();
                    }
                }
            }
            else
            {
                MessageBox.Show("Code and Replacement cannot be blank.", "Empty Inputs", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var code = CodeInput.Value.ToLower();
            if (CustomReplacements.ContainsKey(code))
            {
                CustomReplacements.Remove(code);
                RefreshReplacementListBox();
            }
        }

        private void ReservedWords_MouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadFromUserInfo(UserInfoData.GetSavedData());
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (UserInfoData.SaveData(GetUserInfoFromForm()))
            {
                MessageBox.Show("User Info Saved.", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("User Info was not able to be saved", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to reset your info to default.","Reset Info",MessageBoxButton.YesNo,MessageBoxImage.Warning)==MessageBoxResult.Yes)
                LoadFromUserInfo(new());
        }
    }
}
