using Cover_Letter_Generator.ChatGPT;
using Cover_Letter_Generator.StaticClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Cover_Letter_Generator.CoverLetterGenPage
{
    /// <summary>
    /// Interaction logic for PreviewPage.xaml
    /// </summary>
    public partial class PreviewPage : Page
    {
        private ChatGptResponse response;
        private CoverLetterForm.PageRecoveryClass recoveryClass;
        private Frame ownerFrame;
        private GPTUserInfoDocGenerator.Prompt promptClass;
        private readonly string text;

        private string tempFiles = FileManager.GetAppDirectory("TempFiles");
        private string docx,pdf;

        public PreviewPage(ChatGptResponse response, CoverLetterForm.PageRecoveryClass recoveryClass, Frame ownerFrame, GPTUserInfoDocGenerator.Prompt promptClass,string text)
        {
            this.response = response;
            this.recoveryClass = recoveryClass;
            this.ownerFrame = ownerFrame;
            this.promptClass = promptClass;
            this.text = text;
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //ChatGptResponse response, CoverLetterForm.PageRecoveryClass recoveryClass, Frame ownerFrame, Prompt prompt
            webBrowser.Navigate("about:blank");
            ownerFrame.Content = new ReviewGPTResponse(response,recoveryClass,ownerFrame,promptClass);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            docx=tempFiles + @"\temp.docx";
            pdf=tempFiles + @"\temp.pdf";
            DownloadDocxBtn.IsEnabled = false;
            DownloadPdfBtn.IsEnabled = false;
            if (File.Exists(docx))
            {
                try
                {
                    File.Delete(docx);
                }
                catch
                {
                    System.Windows.MessageBox.Show("Unable to Delete Temporary DOCX File", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    LoadingError();
                }
            }
            if (File.Exists(pdf))
            {
                try
                {
                    File.Delete(pdf);
                }
                catch
                {
                    System.Windows.MessageBox.Show("Unable to Delete Temporary PDF File", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    LoadingError();
                }
            }
            GenerateDocuments();
           
        }

        private async Task GenerateDocuments()
        {
            await Task.Delay(0);
            try
            {
                GenerateDocx(docx, promptClass.Replacements, recoveryClass.Template, text);
                try
                {
                    WordTools.ConvertToPdf(docx, pdf);
                    webBrowser.Navigate(pdf);
                }
                catch (Exception ex)
                {
                    await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        System.Windows.MessageBox.Show("Unable to Generate PDF:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }, System.Windows.Threading.DispatcherPriority.Normal);
                    return;
                }
            }
            catch (Exception ex)
            {
                await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    System.Windows.MessageBox.Show("Unable to Generate DOCX:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }, System.Windows.Threading.DispatcherPriority.Normal);
                
                return;
            }
            await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
            {
                DownloadDocxBtn.IsEnabled = true;
                DownloadPdfBtn.IsEnabled = true;
                MainGrid.Visibility = Visibility.Visible;
                ((Grid)LoadingTextBlock.Parent).Visibility = Visibility.Hidden;
            }, System.Windows.Threading.DispatcherPriority.Normal);
        }

        private void LoadingError()
        {
            LoadingTextBlock.Text = "Error";
        }

        public static void GenerateDocx(string outputFilePath, Dictionary<string, string> replacements, Template.Template template, string chatGptText)
        {
            var replacements2 = new Dictionary<string, string>(replacements);
            replacements2.Add("body", chatGptText);
            foreach (var item in replacements2.ToList())
            {
                replacements2.Add($"%{item.Key}%", item.Value);
                replacements2.Remove(item.Key);
            }
            WordTools.MultiReplacement2(template.GetDocxPath(), replacements2, outputFilePath);
        }
        private void SaveDocument(string extension,string file,string filetypes)
        {
            var sfd = new SaveFileDialog()
            {
                FileName = recoveryClass.Company + " Cover."+extension,
                InitialDirectory= new Settings.Settings().DocxOuputLocation,
                RestoreDirectory= true,
                DefaultExt = extension,
                Filter=filetypes,
                OverwritePrompt = true
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.Copy(file, sfd.FileName);
                }
                catch
                {
                    System.Windows.MessageBox.Show("Failed to Export Document", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void DownloadPdfBtn_Click(object sender, RoutedEventArgs e) => SaveDocument("pdf", pdf, "PDF Documents(*.pdf) | *.pdf");

        private void DownloadDocxBtn_Click(object sender, RoutedEventArgs e) => SaveDocument("docx", docx, "Word Documents(*.docx) | *.docx");
    }
}
