﻿using Cover_Letter_Generator.CoverLetterGenPage;
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
        private UserInfo.UserInfoData Info= UserInfo.UserInfoData.GetSavedData();
        private TemplateScrollView templateScrollView;
        private Template.Template? SelectedTemplate;

        public CoverLetterForm()
        {
            InitializeComponent();
        }

        private void GenerateCover_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GptPromptBox.Text = Info.ChatGPTPrompt;

            templateScrollView = new();
            templateScrollView.TemplateSelected += (a, t) => SelectTemplate(t);
            TemplateSelectionFrame.Content = templateScrollView;

            var templates = TemplateManager.GetTemplates();
            if(templates!=null && templates.Count>0)
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
            //new TemplateSelectionWindow().ShowDialog();
        }
    }
}
