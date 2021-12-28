using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

                if (textBlock?.Text is "Import/Export")
                {
                    PageHelper.SetSearchVisibility(Visibility.Hidden);
                }
                else if(textBlock?.Text is "Passwords" && PageHelper.Frame?.Content is MainPage)
                {
                    PageHelper.SetSearchVisibility(Visibility.Visible);
                }
            }
        }
    }
}
