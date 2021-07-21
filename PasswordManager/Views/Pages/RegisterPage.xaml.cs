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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private readonly IAccessService _accessService;
        private readonly IServiceProvider _serviceProvider;
        
        public RegisterPage(IAccessService accessService, IServiceProvider serviceProvider)
        {
            _accessService = accessService;
            _serviceProvider = serviceProvider;
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TxtPassword.Password == TxtConfirmPassword.Password)
            {
                await _accessService.CreateAccess(TxtPassword.Password);
                PageHelper.NewButton.Visibility = Visibility.Visible;
                PageHelper.Navigate(_serviceProvider, nameof(MainPage));
            }
        }
    }
}
