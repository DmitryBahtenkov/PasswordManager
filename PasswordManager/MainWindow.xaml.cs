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

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IApplicationContext _applicationContext;

        public MainWindow(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;

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

            }
            else
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
