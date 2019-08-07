using ESB2.Database;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace UserManager
{
    /// <summary>
    /// Interaction logic for UserAccountSettingsControl.xaml
    /// </summary>
    public partial class AccountSettingsControlConsole : UserControl
    {
        public static readonly DependencyProperty CurrentUserProprty =
            DependencyProperty.Register("CurrentUser", typeof(UserLogin), typeof(AccountSettingsControlConsole));

        public UserLogin CurrentUser
        {
            get { return (UserLogin) GetValue(CurrentUserProprty); }

            set {
                SetValue(CurrentUserProprty, value);
            }
        }

        public AccountSettingsControlConsole()
        {
            InitializeComponent();
        }

        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            
            password1.Password = password2.Password = "";
            passwordChnage.IsEnabled = false;
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            password1.Password = password2.Password = "";
            passwordChnage.IsEnabled = false;
        }

        private void PasswordTextChanged(object sender, RoutedEventArgs e)
        {
            submit.IsEnabled =
                password1.Password.Length >= 8 &&
                password1.Password.Equals(password2.Password);
        }

        private void ChangePasswordClick(object sender, RoutedEventArgs e)
        {
            passwordChnage.IsEnabled = true;
            password1.Focus();
        }
    }
}
