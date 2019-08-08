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

        public static SystemGroup AddNewSystemGroup(string nomenclature, string description)
        {
            var newSystemsGroup = new SystemGroup()
            {
                Nomenclature = nomenclature,
                Description = description
            };

            database.SystemGroupings.Add(newSystemsGroup);

            database.SaveChanges();
            ESB2UserEventLog.LogUserEvent(CurrentUserNotifications.CurrentUser.Username, EventLoggerEvents.SystemGroupAdded);
            LoadSystemsList();

            return newSystemsGroup;
        }

        public static EquipmentGroup AddNewEquipmentGroup(SystemGroup selectedSystemGroup, string title, string description)
        {
            var newEquipmentGroup = new EquipmentGroup()
            {
                Title = title,
                Description = description
            };

            selectedSystemGroup.EquipmentGroups.Add(newEquipmentGroup);

            database.SaveChanges();
            ESB2UserEventLog.LogUserEvent(CurrentUserNotifications.CurrentUser.Username, EventLoggerEvents.EquipmentGroupAdded);

            LoadSystemsList();

            return newEquipmentGroup;
        }

        public static Equipment AddNewEquipment(EquipmentGroup selectedEquipmentGroup, string title, string description)
        {
            var newEquipment = new Equipment()
            {
                Nomenclature = title,
                Description = description
            };

            selectedEquipmentGroup.Equipment.Add(newEquipment);

            database.SaveChanges();
            ESB2UserEventLog.LogUserEvent(CurrentUserNotifications.CurrentUser.Username, EventLoggerEvents.EquipmentAdded);

            LoadSystemsList();

            return newEquipment;
        }

        public static void Delete(SystemGroup systemGroup)
        {
            database.SystemGroupings.Remove(systemGroup);
            database.SaveChanges();
            LoadSystemsList();
        }

        public static void Delete(EquipmentGroup equipmentGroup)
        {
            database.EquipmentGroupings.Remove(equipmentGroup);
            database.SaveChanges();
            LoadSystemsList();
        }

        public static void Delete(Equipment equipment)
        {
            database.EquipmentListing.Remove(equipment);
            database.SaveChanges();
            LoadSystemsList();
        }
    }
}
