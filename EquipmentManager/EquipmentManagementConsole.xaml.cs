using ESB2.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace EquipmentManager
{
    /// <summary>
    /// Interaction logic for EquipmentManagementConsole.xaml
    /// </summary>
    public partial class EquipmentManagementConsole : UserControl
    {
        public ObservableCollection<SystemGroup> SystemGroups = SystemsList.GetSystemsList();

        public EquipmentManagementConsole()
        {
            InitializeComponent();
            systems.ItemsSource = SystemsList.GetSystemsList();

        }

        private void AddSystemsGroupClick(object sender, RoutedEventArgs e)
        {
            systems.SelectedItem =
                SystemsList.AddNewSystemGroup("New System", "New System Description");

            systemGroupNomenclature.Focus();
        }

        private void AddEquipmentGroupClick(object sender, RoutedEventArgs e)
        {
            var oldSelectedSystemsGroup = (SystemGroup)systems.SelectedItem;
            var newEquipmentGroup = 
                SystemsList.AddNewEquipmentGroup((SystemGroup)systems.SelectedItem,
                "New Equipment Group", "New Equipment Group Description");

            systems.SelectedItem = SystemsList.GetSystemsList().First(s => s.Id == oldSelectedSystemsGroup.Id);
            equipmentGroups.SelectedItem = newEquipmentGroup;

            equipmentGroupTitle.Focus();
        }

        private void AddEquipmentClick(object sender, RoutedEventArgs e)
        {
            var oldSelectedSystemGroup = (SystemGroup)systems.SelectedItem;
            var oldSelectedEquipmentGroup = (EquipmentGroup)equipmentGroups.SelectedItem;

            var newEquipment =
                SystemsList.AddNewEquipment((EquipmentGroup)equipmentGroups.SelectedItem,
                "New Equipment", "New Equipment Description");


            systems.SelectedItem = SystemsList.GetSystemsList().First(s => s.Id == oldSelectedSystemGroup.Id);
            equipmentGroups.SelectedItem = ((SystemGroup)systems.SelectedItem).EquipmentGroups.First(eg => eg.Id == oldSelectedEquipmentGroup.Id);
            equipment.SelectedItem = newEquipment;

            equipmentNomenclature.Focus();
        }

        private void DeleteSystemClick(object sender, RoutedEventArgs e)
        {
            SystemsList.Delete((SystemGroup)systems.SelectedItem);
        }

        private void DeleteEquipmentGroupClick(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteEquipmentClick(object sender, RoutedEventArgs e)
        {

        }

        private void TextBoxGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }

    public class NullValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
