namespace Logger.Appenders
{
    using Layouts.Contracts;
    using Contracts;
    using System;
    using Logger.Enumerations;
    using Logger.LogFiles.Contracts;
    using Logger.LogFiles;

    public class ConsoleAppender:IAppender
    {
        private readonly ILayout layout;
        public ILogFile Log { get; private set; }
        public ConsoleAppender(ILayout layout)
        {
            this.layout = layout;
            ReportLevel = ReportLevel.Info;
            Log = new LogFile();
        }

        public ReportLevel ReportLevel { get;  set; }

        public string LayoutType => this.layout.GetType().Name;

        public void Append(string date, string reportLevel, string message)
        {
            string result = string.Format(layout.Format, date, reportLevel, message);
            Log.Write(result);
            Console.WriteLine(result);
        }
        public override string ToString()
        {
            return $"Appender type: {GetType().Name}, " +
               $"Layout type: {LayoutType}, Report level: {ReportLevel}, " +
               $"Messages appended: {Log.Count}";
        }
    }
}