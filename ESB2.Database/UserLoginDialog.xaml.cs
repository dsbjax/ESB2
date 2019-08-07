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

namespace ESB2.Database
{
    /// <summary>
    /// Interaction logic for UserLoginDialog.xaml
    /// </summary>
    public partial class UserLoginDialog : Window
    {
        public string Username { get { return username.Text; } }
        public string Password {  get { return password.Password; } }

        public UserLoginDialog()
        {
            InitializeComponent();
            username.Focus();
        }

        private void LoginTextChanged(object sender, RoutedEventArgs e)
        {
            login.IsEnabled =
                username.Text.Length >= 4 &&
                password.Password.Length >= 8;
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
