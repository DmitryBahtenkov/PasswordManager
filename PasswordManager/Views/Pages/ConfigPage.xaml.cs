using System.IO;
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

            await _exportService.FromChrome(file);
        }
    }
}