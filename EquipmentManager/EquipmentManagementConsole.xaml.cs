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
            SystemsList.AddNewSystemGroup(newSystemNomenclature.Text, newSystemDescription.Text);
        }

        private void AddEquipmentGroupClick(object sender, RoutedEventArgs e)
        {
            systems.SelectedItem =
            SystemsList.AddNewEquipmentGroup((SystemGroup)systems.SelectedItem, 
                newEquipmentGroupTitle.Text, newEquipmentGroupDescription.Text);
        }

        private void AddEquipmentClick(object sender, RoutedEventArgs e)
        {
            var oldSelectedItem = (SystemGroup) systems.SelectedItem;

            EquipmentGroup selectedGroup =
                SystemsList.AddNewEquipmentGroup((EquipmentGroup)equipmentGroups.SelectedItem, newEquipmentNomenclature.Text, newEquipmentDescription.Text);

            var selectedSystem = ((ObservableCollection<SystemGroup>)systems.ItemsSource).First(s => s.Id == oldSelectedItem.Id);

            
            systems.SelectedItem = selectedSystem;
            equipmentGroups.SelectedItem = selectedGroup;

        }

        private void SystemsTextChanged(object sender, TextChangedEventArgs e)
        {
            addSystem.IsEnabled =
                newSystemDescription.Text.Length > 3 &&
                newSystemNomenclature.Text.Length > 3;
        }

        private void DeleteSystemClick(object sender, RoutedEventArgs e)
        {
            SystemsList.Delete((SystemGroup)systems.SelectedItem);
        }
    }

    public class HasTitleConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (((TextBox)value).Text.Length > 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
