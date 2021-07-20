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
        private PasswordViewModel _viewModel;

        private readonly IPasswordService _passwordService;

        public AddEditWindow(IPasswordService passwordService, PasswordViewModel viewModel)
        {
            _passwordService = passwordService;
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtName.Text) || string.IsNullOrEmpty(TxtPassword.Password))
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
            //todo: navigate
            Close();
        }
    }
}