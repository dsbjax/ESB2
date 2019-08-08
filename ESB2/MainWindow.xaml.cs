using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ESB2.Database;

namespace ESB2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ESB2db.InitializeDatabase();
            InitializeComponent();
            CurrentUserNotifications.SendNotification(null);
        }

        private void MainWindowKeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.F2:
                    CurrentUserNotifications.SendNotification(ESB2Login.Login());
                    break;

                case Key.Escape:
                    if(CurrentUserNotifications.CurrentUser != null)
                        CurrentUserNotifications.SendNotification(ESB2Login.Logout(CurrentUserNotifications.CurrentUser));

                    if (ESB2db.GetDatabase().ChangeTracker.HasChanges())
                        ESB2db.GetDatabase().SaveChanges();

                    break;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if(CurrentUserNotifications.CurrentUser != null)
                ESB2Login.Logout(CurrentUserNotifications.CurrentUser);

            if (ESB2db.GetDatabase().ChangeTracker.HasChanges())
                ESB2db.GetDatabase().SaveChanges();

            base.OnClosing(e);
        }
    }
}
