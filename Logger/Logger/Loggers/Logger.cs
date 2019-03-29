namespace Logger.Loggers
{
    using Appenders.Contracts;
    using Contracts;
    using global::Logger.Enumerations;
    using System.Collections.Generic;

    public class Logger : ILogger
    {
        private readonly ICollection<IAppender> appenders;
 
        public Logger(ICollection<IAppender> appenders)
        {
            this.appenders = appenders;
        }


        public void Info(string date, string message)
        {
            AppendMessageToAllAvailable(date, message, ReportLevel.Info);
        }

        public void Warning(string date, string message)
        {
            AppendMessageToAllAvailable(date, message, ReportLevel.Warning);
        }

        public void Error(string date, string message)
        {
            AppendMessageToAllAvailable(date, message, ReportLevel.Error);
        }

        public void Critical(string date, string message)
        {
            AppendMessageToAllAvailable(date, message, ReportLevel.Critical);
        }

        public void Fatal(string date, string message)
        {
            AppendMessageToAllAvailable(date, message, ReportLevel.Fatal);
        }

        private void AppendMessageToAllAvailable(string date, string message, ReportLevel reportLevel)
        {
            foreach (IAppender appender in appenders)
            {
                if ((int)appender.ReportLevel <= (int)reportLevel)
                {
                    appender.Append(date, reportLevel.ToString(), message);

                }
            }
        }
    }
}