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
using PasswordManager.Helpers;
using PasswordManager.Internal.Contract.Services;

namespace PasswordManager.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private readonly IAccessService _accessService;
        private readonly IServiceProvider _serviceProvider;

        public AuthPage(IAccessService accessService, IServiceProvider serviceProvider)
        {
            _accessService = accessService;
            _serviceProvider = serviceProvider;
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (await _accessService.CheckAccess(TxtPassword.Password))
            {
                PageHelper.NewButton.Visibility = Visibility.Visible;
                PageHelper.SearchButton.Visibility = Visibility.Visible;
                PageHelper.CleanButton.Visibility = Visibility.Visible;
                PageHelper.TxtSearch.Visibility = Visibility.Visible;

                PageHelper.Navigate(_serviceProvider, nameof(MainPage));
            }
            else
            {
                MessageBox.Show("Incorrect password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
