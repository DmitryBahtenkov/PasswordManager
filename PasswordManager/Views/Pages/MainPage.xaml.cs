using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PasswordManager.Helpers;
using PasswordManager.Internal.Contract.Models;
using PasswordManager.Internal.Contract.Services;
using PasswordManager.Internal.Contract.ViewModels;
using PasswordManager.Views.Windows;

namespace PasswordManager.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly IPasswordService _passwordService;
        private readonly ICryptService _cryptService;
        private readonly IServiceProvider _serviceProvider;

        public MainPage(IPasswordService passwordService, ICryptService cryptService, IServiceProvider serviceProvider)
        {
            _passwordService = passwordService;
            _cryptService = cryptService;
            _serviceProvider = serviceProvider;
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListPasswords.ItemsSource = (await _passwordService.GetAll()).Content;
        }

        private async void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            if (ListPasswords.SelectedValue is Password password)
            {
                var text = await _cryptService.Decrypt(password.Crypt);
                Clipboard.SetText(text);
                PageHelper.MainPageText.Text = "Скопировано";
                await Task.Delay(1000);
                PageHelper.MainPageText.Text = "";
            }

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ListPasswords.SelectedValue is Password password)
            {
                var window = new AddEditWindow(_passwordService, new PasswordViewModel
                {
                    Id = password.Id,
                    Name = password.Name,
                    Login = password.Login,
                }, _serviceProvider);
                window.Show();
            }
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ListPasswords.SelectedValue is Password password)
            {
                if (MessageBox.Show("Delete record?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    await _passwordService.DeleteRecord(password.Id);
                    Page_Loaded(null, null);
                }
            }
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (ListPasswords.SelectedValue is Password password)
            {
                var text = password.Login;
                Clipboard.SetText(text);
                PageHelper.MainPageText.Text = "Скопировано";
                await Task.Delay(1000);
                PageHelper.MainPageText.Text = "";
            }
        }
    }
}
