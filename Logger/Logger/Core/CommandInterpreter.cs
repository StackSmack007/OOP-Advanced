namespace Logger.Core
{
    using Appenders.Contracts;
    using Logger.Enumerations;
    using Logger.Factories;
    using Logger.Loggers.Contracts;
    using System;
    using System.Collections.Generic;
    using Loggers;
    class CommandInterpreter
    {
        private List<IAppender> appenders;

        private AppenderFactory appenderFactory;
        private ILogger logger;
        public CommandInterpreter()
        {
            appenders = new List<IAppender>();
            appenderFactory = new AppenderFactory();
        }

        internal void AddAppender(string[] inputArray)
        {
            string appenderType = inputArray[0];
            string layoutType = inputArray[1];
            IAppender appender = appenderFactory.CreateAppender(appenderType, layoutType);
            if (inputArray.Length == 3)
            {
                appender.ReportLevel = ReportLevelParser(inputArray[2]);
            }
            appenders.Add(appender);
        }

        internal void ReadCommand(string[] inputArray)
        {
            if (logger is null) logger = new Logger(appenders);
            ReportLevel reportLevel = ReportLevelParser(inputArray[0]);
            string date = inputArray[1];
            string message = inputArray[2];

            switch (reportLevel)
            {
                case ReportLevel.Info:
                    logger.Info(date, message);
                    break;
                case ReportLevel.Warning:
                    logger.Warning(date, message);
                    break;
                case ReportLevel.Error:
                    logger.Error(date, message);
                    break;
                case ReportLevel.Critical:
                    logger.Critical(date, message);
                    break;
                case ReportLevel.Fatal:
                    logger.Fatal(date, message);
                    break;
            }
        }
        public void PrintFinalStats()
        {
            Console.WriteLine("Logger info");
            foreach (var appender in appenders)
            {
                Console.WriteLine(appender);
            }
        }
        private ReportLevel ReportLevelParser(string input)
        {
            ReportLevel reportLevel;

            if (!Enum.TryParse(input, true, out reportLevel))
            {
                throw new ArgumentException("Incorrect ReportLevel type!");
            }
            return reportLevel;
        }
    }
}