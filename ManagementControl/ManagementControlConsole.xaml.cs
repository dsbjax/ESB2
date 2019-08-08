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

namespace ManagementControl
{
    /// <summary>
    /// Interaction logic for ManagementControlConsole.xaml
    /// </summary>
    public partial class ManagementControlConsole : UserControl, ICurrentUserNotifications
    {
        public ManagementControlConsole()
        {
            InitializeComponent();
            CurrentUserNotifications.Subscribe(this);
        }

        public void CurrentUserChanged(User newCurrentUser)
        {
            if (newCurrentUser != null)
            {
                Visibility = Visibility.Visible;

                reportManager.Visibility =
                    newCurrentUser.UserPermissions == UserPermissions.ReportsOnly ? Visibility.Visible : Visibility.Collapsed;

                statusManager.Visibility =
                    (newCurrentUser.UserPermissions & UserPermissions.StatusUpdates) == UserPermissions.StatusUpdates ?
                    Visibility.Visible : Visibility.Collapsed;

                equipmentManager.Visibility =
                    (newCurrentUser.UserPermissions & UserPermissions.EquipmentMangement) == UserPermissions.EquipmentMangement ||
                    (newCurrentUser.UserPermissions & UserPermissions.Admin) == UserPermissions.Admin ?
                    Visibility.Visible : Visibility.Collapsed;

                boardManager.Visibility =
                    (newCurrentUser.UserPermissions & UserPermissions.StatusBoardManagement) == UserPermissions.StatusBoardManagement ||
                    (newCurrentUser.UserPermissions & UserPermissions.Admin) == UserPermissions.Admin ?
                    Visibility.Visible : Visibility.Collapsed;

                userManager.Visibility =
                    (newCurrentUser.UserPermissions & UserPermissions.UserManagement) == UserPermissions.UserManagement ||
                    (newCurrentUser.UserPermissions & UserPermissions.Admin) == UserPermissions.Admin ?
                    Visibility.Visible : Visibility.Collapsed;

                admin.Visibility =
                    (newCurrentUser.UserPermissions & UserPermissions.Admin) == UserPermissions.Admin ?
                    Visibility.Visible : Visibility.Collapsed;
            }
            else
                Visibility = Visibility.Collapsed;


        }

        private void ConsoleSelectClick(object sender, RoutedEventArgs e)
        {
            CollapseAllConsoles();
            ((UserControl)((Button)sender).Tag).Visibility = Visibility.Visible;
        }

        private void CollapseAllConsoles()
        {
            userManagementConsole.Visibility = Visibility.Collapsed;
            equipmentMangementConsole.Visibility = Visibility.Collapsed;
            statusBoardManagementConsole.Visibility = Visibility.Collapsed;
            databaseAdminControlConsole.Visibility = Visibility.Collapsed;
            statusUdpdateControlConsole.Visibility = Visibility.Collapsed;
            reportControlConsole.Visibility = Visibility.Collapsed;
        }
    }
}
