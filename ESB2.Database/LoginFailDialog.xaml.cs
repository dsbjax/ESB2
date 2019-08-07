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
    /// Interaction logic for LoginFailDialog.xaml
    /// </summary>
    public partial class LoginFailDialog : Window
    {
        public LoginFailDialog()
        {
            InitializeComponent();
        }

        internal void ShowDialog(int count)
        {
            failCount.Content = ((string)failCount.Content).Replace("x", count.ToString());
            ShowDialog();
        }
    }
}
