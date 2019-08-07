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
using System.Data.Entity;

namespace UserManager
{
    /// <summary>
    /// Interaction logic for UserManagerControl.xaml
    /// </summary>
    public partial class UserManagerControlConsole : UserControl
    {
        public UserManagerControlConsole()
        {
            InitializeComponent();

            userListView.ItemsSource = UserList.GetUserList();
            userListView.DisplayMemberPath = "Username";

            if (userListView.Items.Count > 0)
                userListView.SelectedIndex = 0;

            IsVisibleChanged += UserManagerControlIsVisibleChanged;
        }

        private void UserManagerControlIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (UserList.HasChanges())
                UserList.SaveChanges();
        }
    }
}
