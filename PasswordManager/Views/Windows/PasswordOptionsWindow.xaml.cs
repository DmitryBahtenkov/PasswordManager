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
using Microsoft.Extensions.DependencyInjection;
using PasswordManager.Internal.Contract.Commands;
using PasswordManager.Internal.Contract.ViewModels;

namespace PasswordManager.Views.Windows
{
    public delegate void OptionsEventHandler(object sender, OptionsEventArgs args);

    /// <summary>
    /// Логика взаимодействия для PasswordOptionsWindow.xaml
    /// </summary>
    public partial class PasswordOptionsWindow : Window
    {
        private readonly PasswordOptions _current;
        private readonly IServiceProvider _serviceProvider;
        
        public PasswordOptionsWindow(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
            _current = new PasswordOptions();
            DataContext = _current;
        }

        private async void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            var passwordCommand = _serviceProvider.GetService<IGeneratePasswordCommand>();
            if (passwordCommand != null)
            {
                var password = await passwordCommand.Execute(_current);
                OptionsAppliedHandler?.Invoke(this, new OptionsEventArgs(_current, password));
                Close();
            }
        }

        public event OptionsEventHandler OptionsAppliedHandler;
    }
    
    public class OptionsEventArgs : EventArgs
    {
        public PasswordOptions Options { get; init; }
        public string Password { get; init; }

        public OptionsEventArgs(PasswordOptions options, string password)
        {
            Options = options;
            Password = password;
        }
    }
}
