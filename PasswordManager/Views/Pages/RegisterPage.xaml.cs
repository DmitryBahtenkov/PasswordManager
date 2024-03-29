﻿using System;
using System.Windows;
using System.Windows.Controls;
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
                PageHelper.NewButton.Visibility = Visibility.Visible;
                PageHelper.SearchButton.Visibility = Visibility.Visible;
                PageHelper.CleanButton.Visibility = Visibility.Visible;
                PageHelper.TxtSearch.Visibility = Visibility.Visible;
                await _accessService.CreateAccess(TxtPassword.Password);
                PageHelper.Navigate(_serviceProvider, nameof(MainPage));
            }
            else
            {
                MessageBox.Show("Подтвердите пароль");
            }
        }
    }
}
