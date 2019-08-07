using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESB2.Database
{
    public static class ESB2UserEventLog
    {
        private static EntityDataModelESB2Container database = ESB2db.GetDatabase();

        public static void LogUserEvent(string username, EventLoggerEvents userEvent)
        {
            try
            {
                database.EventLogLog.Add(new EventLoggerEvent()
                {
                    Timestamp = DateTime.Now,
                    User = username,
                    Event = userEvent
                });
                database.SaveChanges();
            }
            catch (Exception e)
            {
                ESB2ExceptionEventLog.LogExceptionEvent(e);
            }
        }

    }
}
