using Cover_Letter_Generator.CoverLetterGenPage;
using Cover_Letter_Generator.Template;
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
    /// Interaction logic for CoverLetterForm.xaml
    /// </summary>
    public partial class CoverLetterForm : Page
    {
        public class PageRecoveryClass
        {
            public string Recipient, Company, JobTitle,Description,GptPrompt;
            public Template.Template Template;
        }
        private UserInfo.UserInfoData Info= UserInfo.UserInfoData.GetSavedData();
        private TemplateScrollView templateScrollView;
        private Template.Template? SelectedTemplate;
        //private ReviewGPTResponse? ReviewGPTResponsePage;
        private PageRecoveryClass? recovery;

        private Frame OwnerFrame;

        public CoverLetterForm(Frame ownerFrame)
        {
            InitializeComponent();
            OwnerFrame = ownerFrame;
        }

        public CoverLetterForm(PageRecoveryClass recovery, Frame ownerFrame)
        {
            this.recovery = recovery;
            this.OwnerFrame = ownerFrame;
            InitializeComponent();
        }

        private void GenerateCover_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTemplate == null)
            {
                MessageBox.Show("A Template Must Be Selected", "No Template", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            this.IsEnabled = false;
            Generate();
            
        }
        private async void Generate()
        {
            var r=await ChatGPT.GPTUserInfoDocGenerator.Generate(Info, CompanyInput.Value, RecipientInput.Value, DescriptionBox.Text, GptPromptBox.Text);
            Application.Current.Dispatcher.Invoke(()=>{ 
                IsEnabled = true;
                if(r == null)
                {
                    MessageBox.Show("Unable to get a response from ChatGPT", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    
                    OwnerFrame.Content= new ReviewGPTResponse(r, new PageRecoveryClass()
                    {
                        Company=CompanyInput.Value,
                        Recipient=RecipientInput.Value,
                        Description=DescriptionBox.Text,
                        GptPrompt=GptPromptBox.Text,
                        JobTitle=JobTitleInput.Value,
                        Template=SelectedTemplate,
                    },OwnerFrame);
                }
            });

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (recovery != null)
            {
                CompanyInput.Value=recovery.Company;
                RecipientInput.Value = recovery.Recipient;
                JobTitleInput.Value = recovery.JobTitle;
                DescriptionBox.Text = recovery.Description;
                GptPromptBox.Text = recovery.GptPrompt;
                SelectTemplate(recovery.Template);
            }
            else
            {
                var allReplacements = Info.AllReplacements;

                if (allReplacements.ContainsKey("company"))
                    CompanyInput.Value = allReplacements["company"];
                if (allReplacements.ContainsKey("recipient"))
                    RecipientInput.Value = allReplacements["recipient"];
                if (allReplacements.ContainsKey("jobtitle"))
                    JobTitleInput.Value = allReplacements["jobtitle"];

                GptPromptBox.Text = Info.ChatGPTPrompt;
            }

            templateScrollView = new()
            {
                MaxWidth = 180
            };
            templateScrollView.TemplateSelected += (a, t) => SelectTemplate(t);
            TemplateSelectionFrame.Content = templateScrollView;

            var templates = TemplateManager.GetTemplates();
            if(templates!=null && templates.Count>0 && SelectedTemplate==null)
                SelectTemplate(templates[0]);
            
        }
        private void SelectTemplate(Template.Template e)
        {
            SelectedTemplate = e;
            SelectedTemplateBlock.Text = e.Name;
        }
        

        private void TemplateChange_Click(object sender, RoutedEventArgs e)
        {
            TemplateSelectionFrame.Visibility = TemplateSelectionFrame.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
           // new TemplateSelectionWindow().ShowDialog();
        }

        private void PreviewPrompt_Click(object sender, RoutedEventArgs e)
        {
            OwnerFrame.Content = new ReviewGPTResponse(
                ChatGPT.GPTUserInfoDocGenerator.GetPrompt(Info,CompanyInput.Value,RecipientInput.Value,DescriptionBox.Text,GptPromptBox.Text).GptResponse,
                new PageRecoveryClass()
            {
                Company = CompanyInput.Value,
                Recipient = RecipientInput.Value,
                Description = DescriptionBox.Text,
                GptPrompt = GptPromptBox.Text,
                JobTitle = JobTitleInput.Value,
                Template = SelectedTemplate,
            }, OwnerFrame);
        }
    }
}
