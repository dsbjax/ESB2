using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESB2.Database
{
    public static class SystemsList
    {
        private static EntityDataModelESB2Container database = ESB2db.GetDatabase();
        private static ObservableCollection<SystemGroup> systemsList = new ObservableCollection<SystemGroup>();

        public static ObservableCollection<SystemGroup> GetSystemsList()
        {
            LoadSystemsList();

            return systemsList;
        }

        private static void LoadSystemsList()
        {
            var systems = database.SystemGroupings.OrderBy(s => s.Nomenclature);
            foreach (var system in systems)
            {
                system.EquipmentGroups = system.EquipmentGroups.OrderBy(e => e.Title).ToList();
                foreach (var group in system.EquipmentGroups)
                    group.Equipment = group.Equipment.OrderBy(e => e.Nomenclature).ToList();
            }

            systemsList.Clear();
            foreach (var system in systems)
                systemsList.Add(system);
        }

        public static void AddNewSystemGroup(string nomenclature, string description)
        {
            database.SystemGroupings.Add(new SystemGroup()
            {
                Nomenclature = nomenclature,
                Description = description
            });

            database.SaveChanges();
            ESB2UserEventLog.LogUserEvent(CurrentUserNotifications.CurrentUser.Username, EventLoggerEvents.SystemGroupAdded);
            LoadSystemsList();
        }

        public static SystemGroup AddNewEquipmentGroup(SystemGroup selectedItem, string title, string description)
        {
            selectedItem.EquipmentGroups.Add(new EquipmentGroup()
            {
                Title = title,
                Description = description
            });

            database.SaveChanges();
            ESB2UserEventLog.LogUserEvent(CurrentUserNotifications.CurrentUser.Username, EventLoggerEvents.EquipmentGroupAdded);

            LoadSystemsList();

            return systemsList.First(s => s.Id == selectedItem.Id);
        }

        public static EquipmentGroup AddNewEquipmentGroup(EquipmentGroup selectedItem, string title, string description)
        {
            selectedItem.Equipment.Add(new Equipment()
            {
                Nomenclature = title,
                Description = description
            });

            database.SaveChanges();
            ESB2UserEventLog.LogUserEvent(CurrentUserNotifications.CurrentUser.Username, EventLoggerEvents.EquipmentAdded);

            LoadSystemsList();

            return database.EquipmentGroupings.First(e=> e.Id == selectedItem.Id);
        }

        public static void Delete(SystemGroup systemGroup)
        {
            foreach(var group in systemGroup.EquipmentGroups)
            {

            }
            
            database.SystemGroupings.Remove(systemGroup);
            database.SaveChanges();
            LoadSystemsList();
        }
    }
}
