using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using PasswordManager.Internal.Contract.Services;

namespace PasswordManager.Views.Pages
{
    public partial class ConfigPage : Page
    {
        private readonly IExportService _exportService;
        
        public ConfigPage(IExportService exportService)
        {
            _exportService = exportService;
            InitializeComponent();
        }

        private void BtnChromeFile_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            
            openFileDialog.DefaultExt = ".csv";  
            openFileDialog.Filter = "Csv documents (.csv)|*.csv";
            
            if (openFileDialog.ShowDialog() == true)
            {
                TxtChromeFile.Text = openFileDialog.FileName;
            }
        }

        private async void BtnChromeExport_OnClick(object sender, RoutedEventArgs e)
        {
            var file = TxtChromeFile.Text;
            if (string.IsNullOrEmpty(file) || !File.Exists(file))
            {
                MessageBox.Show("Please select the file", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = await _exportService.Import(file);

            if (result.Any())
            {
                MessageBox.Show($"Imported {result.Count} passwords", 
                    "Information", 
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void BtnExport_OnClick(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "passwords.csv";
            
            if (saveFileDialog.ShowDialog() == true)
            {
                TxtExport.Text = saveFileDialog.FileName;
            }
        }


        private async void BtnExecute_OnClick(object sender, RoutedEventArgs e)
        {
            var file = TxtExport.Text;
            if (string.IsNullOrEmpty(file))
            {
                MessageBox.Show("Please select the file", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var text =
                "При импорте файла все пароли будут расшифрованы, утечка этого файла может привести к серьёзным последствиям. Вы уверены, что хотите выполнить импорт?";
            if (MessageBox.Show(text, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning)
                is MessageBoxResult.Yes)
            {
                file = await _exportService.Export(file);
                MessageBox.Show($"Saved to file: \n{file}", "Import", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}