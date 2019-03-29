namespace Logger.Factories
{
    using Appenders;
    using Appenders.Contracts;
    using Layouts.Contracts;
    using LogFiles;
    using LogFiles.Contracts;
    using System;
    public class AppenderFactory
    {
        private LayoutFactory lf;
        public AppenderFactory()
        {
            lf = new LayoutFactory();
        }

        public IAppender CreateAppender(string appenderType, string layoutType)
        {
            ILayout layout = lf.CreateLayout(layoutType);
            IAppender appender;
            switch (appenderType.ToUpper())
            {
                case "CONSOLEAPPENDER": appender = new ConsoleAppender(layout); break;
                case "FILEAPPENDER": appender = new FileAppender(layout); break;
                default: throw new ArgumentException("Invalid Appender Type!");
            }
            return appender;
        }
    }
}