using System;
using System.Linq;
using System.Data.Entity;

namespace ESB2.Database
{
    public static class ESB2Login
    {
        private static EntityDataModelESB2Container database = ESB2db.GetDatabase();
        private static DbSet<User> users = database.Users;

        public static User Login()
        {
            var loginDialog = new UserLoginDialog();
            User user = null;

            try
            {
                loginDialog.ShowDialog();
                if ((bool)loginDialog.DialogResult)
                {
                    user = users.FirstOrDefault(u => u.Username.Equals(loginDialog.Username.ToLower()));

                    if (user != null)
                        user = ProcessLogin((UserLogin)user, loginDialog.Password);
                    else
                    {
                        new InvalidUsernameDialog().ShowDialog();
                        ESB2UserEventLog.LogUserEvent(loginDialog.Username, EventLoggerEvents.InvalidUsername);
                    }
                }
            }catch(Exception e)
            {
                ESB2ExceptionEventLog.LogExceptionEvent(e);
            }

            return user;
        }

        public static User Logout(User currentUser)
        {
            ESB2UserEventLog.LogUserEvent(currentUser.Username, EventLoggerEvents.Logout);
            return null;
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
                ESB2UserEventLog.LogUserEvent(user.Username, EventLoggerEvents.LoginSuccess);

                if (user.UserMustChangePassword)
                    new ChangePasswordDialog(user).ShowDialog();
            }
            else
            {
                user.FailedLoginCount++;

                user.FailedLoginTimestamp = DateTime.Now;
                new LoginFailDialog().ShowDialog(user.FailedLoginCount);
                ESB2UserEventLog.LogUserEvent(user.Username, EventLoggerEvents.LoginFail);

                user.AccountLocked = user.FailedLoginCount == 5;
                if (user.AccountLocked)
                    ESB2UserEventLog.LogUserEvent(user.Username, EventLoggerEvents.UserAccountLocked);

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
                ESB2UserEventLog.LogUserEvent(user.Username, EventLoggerEvents.UserAccountUnlocked);
            }

            return user;
        }
    }
}
