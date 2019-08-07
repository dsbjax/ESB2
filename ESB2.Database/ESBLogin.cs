using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ESB2.Database
{
    public static class ESBLogin
    {
        private static EntityDataModelESB2Container database = ESB2db.GetDatabase();
        private static DbSet<User> users = database.Users;

        public static User Login()
        {
            var loginDialog = new UserLoginDialog();
            User user = null;

            loginDialog.ShowDialog();
            if ((bool)loginDialog.DialogResult)
            {
                user = users.FirstOrDefault(u => u.Username.Equals(loginDialog.Username));

                if (user == null)
                    user = ProcessLogin((UserLogin)user, loginDialog.Password);
            }

            return user;
        }

        private static User ProcessLogin(UserLogin user, string password)
        {
            if (user.AccountLocked)
                user = HandleAccountLocked(user);

            if (user == null)
                return user;

            if (ValidatePassword(user, password))
            {
                user.FailedLoginCount = 0;
                user.LastLogin = DateTime.Now;
            }
            else
            {
                user.FailedLoginCount++;

                if (user.FailedLoginCount == 1)
                    user.FailedLoginTimestamp = DateTime.Now;

                new LoginFailDialog().ShowDialog(user.FailedLoginCount);
                user.AccountLocked = user.FailedLoginCount == 5;

                user = null;
            }

            database.SaveChanges();
            return user;
        }

        private static bool ValidatePassword(UserLogin user, string password)
        {
            return user.Password.SequenceEqual(PasswordEncrypter.EncryptPassword(user.Username, password));
        }

        private static UserLogin HandleAccountLocked(UserLogin user)
        {
            var locked = DateTime.Now - user.FailedLoginTimestamp;

            if (locked.Minutes < 15)
            {
                new LockedAccountDialog().ShowDialog();
                user = null;
            }
            else
            {
                user.AccountLocked = false;
            }

            return user;
        }
    }
}
