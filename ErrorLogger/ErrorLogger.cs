using ESB2.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorLogger
{
    public static class ErrorLogger
    {
        private static EntityDataModelESB2Container database = ESB2db.GetDatabase();

        public static void LogException(Exception ex)
        {
            BuildExceptionEvent(ex, false);

            
        }

        private static ApplicationExceptionEntry BuildExceptionEvent(Exception ex, bool isInner)
        {
            var appEx = new ApplicationExceptionEntry()
            {
                IsInnerException = isInner,
                Timestamp = DateTime.Now,
                Message = ex.Message,
                Source = ex.Source,
                StackTrace = ex.StackTrace
            };

            if (ex.InnerException != null)
                appEx.InnerException = BuildExceptionEvent(ex.InnerException, true);

            return appEx;
        }
    }
}
