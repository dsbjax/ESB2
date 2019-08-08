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

namespace StatusBoardManager
{
    /// <summary>
    /// Interaction logic for StatusBoardManagementConsole.xaml
    /// </summary>
    public partial class StatusBoardManagementConsole : UserControl
    {
        public StatusBoardManagementConsole()
        {
            InitializeComponent();
        }

        private void PageSelectClick(object sender, RoutedEventArgs e)
        {
            ColapseAllPages();
            ((UserControl)((Button)sender).Tag).Visibility = Visibility.Visible;
        }

        private void ColapseAllPages()
        {
            staticPageControlConsole.Visibility = Visibility.Collapsed;
            dynamicPageControlConsole.Visibility = Visibility.Collapsed;
        }
    }
}
