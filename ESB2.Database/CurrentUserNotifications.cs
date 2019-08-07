using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESB2.Database
{
    public static class CurrentUserNotifications
    {
        private static List<ICurrentUserNotifications> subscribers = new List<ICurrentUserNotifications>();
        public static User CurrentUser = null;

        public static void Subscribe(ICurrentUserNotifications subscriber)
        {
            subscribers.Add(subscriber);
        }

        public static void SendNotification(User newCurrentUser)
        {
            CurrentUser = newCurrentUser;
            foreach (var subscriber in subscribers)
                subscriber.CurrentUserChanged(newCurrentUser);
        }
    }

    public interface ICurrentUserNotifications
    {
        void CurrentUserChanged(User newCurrentUser);
    }
}
