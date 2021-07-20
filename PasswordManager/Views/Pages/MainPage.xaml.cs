﻿using System;
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
using PasswordManager.Internal.Contract.Models;
using PasswordManager.Internal.Contract.Services;

namespace PasswordManager.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly IPasswordService _passwordService;
        private readonly ICryptService _cryptService;

        public MainPage(IPasswordService passwordService, ICryptService cryptService)
        {
            _passwordService = passwordService;
            _cryptService = cryptService;
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
                //todo: 
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
    }
}