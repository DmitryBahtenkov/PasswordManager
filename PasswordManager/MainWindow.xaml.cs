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
using PasswordManager.Internal.Contract;
using PasswordManager.Internal.Contract.Services;
using PasswordManager.Internal.Contract.ViewModels;
using PasswordManager.Views.Pages;
using PasswordManager.Views.Windows;

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IApplicationContext _applicationContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly IPasswordService _passwordService;

        public MainWindow(IApplicationContext applicationContext, IServiceProvider serviceProvider, IPasswordService passwordService)
        {
            _applicationContext = applicationContext;
            _serviceProvider = serviceProvider;
            _passwordService = passwordService;

            Init();
        }

        private void Init()
        {
            InitializeComponent();
            PageHelper.Frame = MainFrame;
            PageHelper.MainPageText = TxtHelper;
            PageHelper.NewButton = BtnNew;
            PageHelper.NewButton.Visibility = Visibility.Hidden;

            if (_applicationContext.Accesses.FirstOrDefault() is not null)
            {
                PageHelper.Navigate(_serviceProvider, nameof(AuthPage));
            }
            else
            {
                PageHelper.Navigate(_serviceProvider, nameof(RegisterPage));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddEditWindow(_passwordService, new PasswordViewModel(), _serviceProvider);
            window.ShowDialog();
        }
    }
}
