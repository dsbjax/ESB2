using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESB2.Database
{
    public static class ESB2db
    {
        private static EntityDataModelESB2Container database;

        public static EntityDataModelESB2Container GetDatabase()
        {
            if (database == null)
                InitializeDatabase();

            return database;
        }

        public static void InitializeDatabase()
        {
            InitializeConnectionString();

            database = new EntityDataModelESB2Container();
            database.Database.CreateIfNotExists();

            if (!database.Users.Any(u => u.Username.Equals("admin")))
                CreateAdminAccount();
        }

        private static void CreateAdminAccount()
        {
            var createAdminAccount = new CreateAdminAccount();
            createAdminAccount.ShowDialog();

            if (createAdminAccount.Password != null)
            {
                database.Users.Add(new UserLogin()
                {
                    Username = "admin",
                    Firstname = "Administrator",
                    Lastname = "Account",
                    UserPermissions = UserPermissions.Admin,
                    Password = createAdminAccount.Password,
                    LastLogin = DateTime.Today,
                    FailedLoginCount = 0,
                    FailedLoginTimestamp = DateTime.Today.AddDays(-1),
                    AccountLocked = false,
                    UserMustChangePassword = false,
                });

                database.SaveChanges();
                ESB2UserEventLog.LogUserEvent("admin", EventLoggerEvents.UserCreated);
            }
        }

       

        private static void InitializeConnectionString()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionString = config.ConnectionStrings.ConnectionStrings["EntityDataModelESB2Container"];

            if (connectionString.ConnectionString.Contains("xxxxx"))
            {
                var findDatabase = new OpenFileDialog()
                {
                    Title = "Select/Create Equipment Status Board Database",
                    Multiselect = false,
                    AddExtension = true,
                    DefaultExt = "sdf",
                    Filter = "SQL CE|*.sdf",
                    CheckFileExists = false
                };

                findDatabase.ShowDialog();

                try
                {
                    connectionString.ConnectionString = connectionString.ConnectionString.Replace("xxxxx", findDatabase.FileName);
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("connectionStrings");
                }catch(Exception e)
                {
                    ESB2ExceptionEventLog.LogExceptionEvent(e);
                }
            }
        }

        public static void Delete(SystemGroup selectedItem)
        {
            throw new NotImplementedException();
        }

        public static bool ChangePassword(UserLogin user, string password)
        {
            try
            {
                user.Password = PasswordEncrypter.EncryptPassword(user.Username.ToLower(), password);
                database.SaveChanges();
            }
            catch (Exception e)
            {
                ESB2ExceptionEventLog.LogExceptionEvent(e);
                return false;
            }

            return true;
        }
    }
}
