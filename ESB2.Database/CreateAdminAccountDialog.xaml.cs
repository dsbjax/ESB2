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
    /// Interaction logic for CreateAdminAccount.xaml
    /// </summary>
    public partial class CreateAdminAccount : Window
    {
        private byte[] password = null;
        public byte[] Password => password;

        public CreateAdminAccount()
        {
            InitializeComponent();
            password1.Focus();
        }

        private void CreateAdminAccountClicked(object sender, RoutedEventArgs e)
        {
            password = PasswordEncrypter.EncryptPassword("admin", password1.Password);
            Close();
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            createAccount.IsEnabled =
                password1.Password.Length >= 8 && 
                password1.Password.Equals(password2.Password);
        }
    }
}
