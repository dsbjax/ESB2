using ESB2.Database;
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

namespace UserManager
{
    /// <summary>
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewUserControlConsole : UserControl
    {
        public NewUserControlConsole()
        {
            InitializeComponent();
        }

        private void NewUserTextChanged(object sender, TextChangedEventArgs e)
        {
            addNewUser.IsEnabled = username.Text.Length >= 5;
        }

        private void AddNewUserClick(object sender, RoutedEventArgs e)
        {
            UserList.AddUser(username.Text, firstname.Text, lastname.Text);

            username.Clear();
            firstname.Clear();
            lastname.Clear();
            username.Focus();
        }
    }
}
