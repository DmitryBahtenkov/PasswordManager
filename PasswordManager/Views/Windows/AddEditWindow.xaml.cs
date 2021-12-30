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
using System.Windows.Shapes;
using PasswordManager.Helpers;
using PasswordManager.Internal.Contract.Services;
using PasswordManager.Internal.Contract.ViewModels;
using PasswordManager.Views.Pages;

namespace PasswordManager.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        private readonly PasswordViewModel _viewModel;

        private readonly IPasswordService _passwordService;
        private readonly IServiceProvider _serviceProvider;

        public AddEditWindow(IPasswordService passwordService, PasswordViewModel viewModel, IServiceProvider serviceProvider)
        {
            _passwordService = passwordService;
            InitializeComponent();
            _viewModel = viewModel;
            _serviceProvider = serviceProvider;
            DataContext = _viewModel;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtName.Text) || string.IsNullOrEmpty(TxtPassword.Password) || string.IsNullOrEmpty(TxtLogin.Text))
            {
                MessageBox.Show("Please, enter data", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _viewModel.Password = TxtPassword.Password;
            if (string.IsNullOrEmpty(_viewModel.Id))
            {
                var result = await _passwordService.CreateRecord(_viewModel);
                if (!result.IsSuccess)
                {
                    MessageBox.Show(result.Error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                await _passwordService.UpdateRecord(_viewModel);
            }

            MessageBox.Show("Update succefully!", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
            PageHelper.Navigate(_serviceProvider, nameof(MainPage));
            Close();
        }

        private void BtnGenerate_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new PasswordOptionsWindow(_serviceProvider);
            window.OptionsAppliedHandler += (_, args) =>
            {
                TxtPassword.Password = args.Password;
                TxtPasswordVisible.Text = args.Password;
            }; 
            window.ShowDialog();
        }

        private void CheckPasswordVisible_OnChecked(object sender, RoutedEventArgs e)
        {
            if (CheckPasswordVisible?.IsChecked is true)
            {
                TxtPassword.Visibility = Visibility.Hidden;
                TxtPasswordVisible.Visibility = Visibility.Visible;
                TxtPasswordVisible.Text = TxtPassword.Password;
            }
            else
            {
                TxtPassword.Visibility = Visibility.Visible;
                TxtPasswordVisible.Visibility = Visibility.Hidden;
                TxtPassword.Password = TxtPasswordVisible.Text;
            }
        }
    }
}
