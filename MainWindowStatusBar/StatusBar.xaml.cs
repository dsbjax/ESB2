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
using WPFLibrary;

namespace MainWindowStatusBar
{
    /// <summary>
    /// Interaction logic for StatusBar.xaml
    /// </summary>
    public partial class StatusBar : UserControl, ICurrentUserNotifications, IAppTimer
    {
        public StatusBar()
        {
            InitializeComponent();

            SetTime();
            SetDate();

            CurrentUserNotifications.Subscribe(this);
            AppTimer.Subscribe(this, TimerInterval.Minutes);
            AppTimer.Subscribe(this, TimerInterval.NewDayLocal);
        }

        public void CurrentUserChanged(User newCurrentUser)
        {
            if (newCurrentUser == null)
                currentUser.Text = "<F2> to login";
            else
                currentUser.Text = newCurrentUser.Firstname + " " + newCurrentUser.Lastname + 
                    " <ESC> to logout";
        }

        public void Tick(TimerInterval interval)
        {
            switch(interval)
            {
                case TimerInterval.Minutes:
                    SetTime();
                    break;

                case TimerInterval.NewDayLocal:
                    SetDate();
                    break;
            }
        }

        private void SetDate()
        {
            date.Text = DateTime.Now.ToLongDateString();
        }

        private void SetTime()
        {
            utcTime.Text = DateTime.UtcNow.ToString("HHmm");
            localTime.Text = DateTime.Now.ToString("HHmm");
        }
    }
}
