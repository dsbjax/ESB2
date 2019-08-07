using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ESB2.Database
{
    public static class UserList
    {
        private static EntityDataModelESB2Container database = ESB2db.GetDatabase();
        private static ObservableCollection<UserLogin> userList = new ObservableCollection<UserLogin>();

        public static ObservableCollection<UserLogin> GetUserList()
        {
            LoadUserList();
            return userList;
        }

        private static void LoadUserList()
        {
            var users = database.Users.ToList();
            users.Sort((u1, u2) => u1.Username.CompareTo(u2.Username));

            userList.Clear();
            foreach (var user in users)
                userList.Add((UserLogin)user);
        }

        public static UserLogin AddUser(string username, string firstname, string lastname)
        {
            UserLogin newUser = null;

            if(!database.Users.Any(u=> u.Username == username))
            {
                newUser = new UserLogin()
                {
                    Username = username.ToLower(),
                    Firstname = firstname,
                    Lastname = lastname,
                    UserPermissions = UserPermissions.ReportsOnly,
                    Password = PasswordEncrypter.EncryptPassword(username.ToLower(), "ffj" + username.ToLower()),
                    LastLogin = DateTime.Today,
                    FailedLoginCount = 0,
                    FailedLoginTimestamp = DateTime.Today.AddDays(-1),
                    AccountLocked = false,
                    UserMustChangePassword = true,
                };

                database.Users.Add(newUser);
                database.SaveChanges();
                ESB2UserEventLog.LogUserEvent(newUser.Username, EventLoggerEvents.UserCreated);
                
                LoadUserList();
            }

            return newUser;
        }

        public static bool HasChanges()
        {
            return database.ChangeTracker.HasChanges();
        }

        public static void SaveChanges()
        {
            database.SaveChanges();
        }
    }
}
