using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
        private readonly ConfigPage _configPage;

        public MainWindow(IApplicationContext applicationContext, IServiceProvider serviceProvider, IPasswordService passwordService, ConfigPage configPage)
        {
            _applicationContext = applicationContext;
            _serviceProvider = serviceProvider;
            _passwordService = passwordService;
            _configPage = configPage;

            Init();
        }

        private void Init()
        {
            InitializeComponent();
            TxtSearch.Effect = null;
            PageHelper.Frame = MainFrame;
            PageHelper.MainPageText = TxtHelper;
            PageHelper.NewButton = BtnNew;
            PageHelper.NewButton.Visibility = Visibility.Hidden;
            
            PageHelper.SearchButton = BtnSearch;
            
            PageHelper.CleanButton = BtnClean;
            
            PageHelper.TxtSearch = TxtSearch;
            PageHelper.ConfigFrame = ConfigFrame;
            
            PageHelper.SetSearchVisibility(Visibility.Hidden);
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

        private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
        {
            PageHelper.InvokeSearch(this, new SearchEventArgs(TxtSearch.Text));
        }

        private void BtnClean_OnClick(object sender, RoutedEventArgs e)
        {
            PageHelper.InvokeSearch(this, new SearchEventArgs(new SearchOptions()));
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl tabControl)
            {
                var textBlock = ((TabItem)tabControl.SelectedValue).Header as TextBlock;

                if (textBlock?.Text is "Экспорт")
                {
                    PageHelper.SetSearchVisibility(Visibility.Hidden);
                }
                else if(textBlock?.Text is "Пароли" && PageHelper.Frame?.Content is MainPage)
                {
                    PageHelper.SetSearchVisibility(Visibility.Visible);
                }
            }
        }

        private void BtnImport_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
