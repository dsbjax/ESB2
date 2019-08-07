using ESB2.Database;
using System.Windows;
using System.Windows.Controls;

namespace UserManager
{
    /// <summary>
    /// Interaction logic for UserPermissionsControl.xaml
    /// </summary>
    public partial class UserPermissionsControlConsole : UserControl
    {
        public static readonly DependencyProperty UserPermissionsProperty =
            DependencyProperty.Register("UserPermissions", typeof(UserPermissions), typeof(UserPermissionsControlConsole),
                new PropertyMetadata(new PropertyChangedCallback(UserPermissionPropertyChanged)));

        public UserPermissions UserPermissions
        {
            get { return (UserPermissions)GetValue(UserPermissionsProperty); }
            set { SetValue(UserPermissionsProperty, value); }
        }

        public UserPermissionsControlConsole()
        {
            InitializeComponent();
        }

        public static void UserPermissionPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((UserPermissionsControlConsole)sender).UpdateUserPermissions((UserPermissions)e.NewValue);
        }

        private void UpdateUserPermissions(UserPermissions newValue)
        {
            statusUpdates.IsChecked = (newValue & UserPermissions.StatusUpdates) == UserPermissions.StatusUpdates;
            equipmentManger.IsChecked = (newValue & UserPermissions.EquipmentMangement) == UserPermissions.EquipmentMangement;
            boardManager.IsChecked = (newValue & UserPermissions.StatusBoardManagement) == UserPermissions.StatusBoardManagement;
            userManager.IsChecked = (newValue & UserPermissions.UserManagement) == UserPermissions.UserManagement;
            admin.IsChecked = (newValue & UserPermissions.Admin) == UserPermissions.Admin;
        }

        private void UserPermissionsClick(object sender, RoutedEventArgs e)
        {
            var newUserPermissions = UserPermissions.StatusUpdates;

            if (statusUpdates.IsChecked.Value) newUserPermissions |= UserPermissions.StatusUpdates;
            if (equipmentManger.IsChecked.Value) newUserPermissions |= UserPermissions.EquipmentMangement;
            if (boardManager.IsChecked.Value) newUserPermissions |= UserPermissions.StatusBoardManagement;
            if (userManager.IsChecked.Value) newUserPermissions |= UserPermissions.UserManagement;
            if (admin.IsChecked.Value) newUserPermissions |= UserPermissions.Admin;

            UserPermissions = newUserPermissions;
        }
    }
}
