using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESB2.Database
{
    public static class ESB2ExceptionEventLog
    {
        private static EntityDataModelESB2Container database = ESB2db.GetDatabase();

        public static void LogExceptionEvent(Exception ex)
        {
            if (ex != null)
            {
                    new ApplicationExceptionDialog().ShowDialog();
                    database.ExceptionEventLog.Add(BuildExceptionEventLogEntry(ex, false));
                    database.SaveChanges();
            }
        }

        private static ExceptionEventLogEntry BuildExceptionEventLogEntry(Exception ex, bool isInnerException)
        {
            var eventLogEntry = new ExceptionEventLogEntry()
            {
                Timestamp = DateTime.Now,
                Source = ex.Source,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                IsInnerException = isInnerException
            };

            if (ex.InnerException != null)
                eventLogEntry.InnerException = BuildExceptionEventLogEntry(ex.InnerException, true);

            return eventLogEntry;
        }
    }
}
