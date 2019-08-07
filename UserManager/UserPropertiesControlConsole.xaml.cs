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
    /// Interaction logic for UserPropertiesControl.xaml
    /// </summary>
    public partial class UserPropertiesControlConsole : UserControl
    {

        public UserPropertiesControlConsole()
        {
            InitializeComponent();

        }

        public static readonly DependencyProperty SelectedUserProperty =
            DependencyProperty.Register("SelectedUser", typeof(UserLogin), typeof(UserPropertiesControlConsole));

        public UserLogin SelectedUser
        {
            get { return (UserLogin)GetValue(SelectedUserProperty); }
            set { SetValue(SelectedUserProperty, value); }
        }

        
    }

    internal class SelectedUserToBoolean : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)(value != null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
