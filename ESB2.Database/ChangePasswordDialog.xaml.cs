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
    /// Interaction logic for ChangePasswordDialog.xaml
    /// </summary>
    public partial class ChangePasswordDialog : Window
    {
        private UserLogin user;
        public ChangePasswordDialog(UserLogin user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void ChangePasswordClick(object sender, RoutedEventArgs e)
        {
            var currentEncrypted = PasswordEncrypter.EncryptPassword(user.Username, currrent.Password);
            var newPasswordEncrypted = PasswordEncrypter.EncryptPassword(user.Username, password1.Password);

            if (currentEncrypted.SequenceEqual(user.Password))
            {
                user.Password = newPasswordEncrypted;
                user.UserMustChangePassword = false;
                ESB2UserEventLog.LogUserEvent(user.Username, EventLoggerEvents.UserPasswordChanged);
            }
            else
            {
                new PasswordChangeFailDialog().ShowDialog();
                new ChangePasswordDialog(user).ShowDialog();
            }

            Close();
        }

        private void PasswordTextChanged(object sender, RoutedEventArgs e)
        {
            changePassword.IsEnabled =
                currrent.Password.Length >= 8 &&
                password1.Password.Length >= 8 &&
                password1.Password.Equals(password2.Password);
        }
    }
}
