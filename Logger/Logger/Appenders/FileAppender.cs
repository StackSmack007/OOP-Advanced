namespace Logger.Appenders
{
    using Contracts;
    using Logger.Enumerations;
    using Logger.Layouts.Contracts;
    using Logger.LogFiles;
    using Logger.LogFiles.Contracts;
    using System;
    using System.IO;

    class FileAppender : IAppender
    {
        private readonly ILayout layout;
        public ILogFile Log { get; private set; }
        private const string path = @"..\..\..\log14651.txt";

        public ReportLevel ReportLevel { get; set; }

        public string LayoutType => this.layout.GetType().Name;

        public FileAppender(ILayout layout)
        {
            this.layout = layout;
            ReportLevel = ReportLevel.Info;
            Log = new LogFile();
        }

        public void Append(string date, string reportLevel, string message)
        {
            string result = string.Format(layout.Format, date, reportLevel, message + Environment.NewLine);
            Log.Write(result);
            File.AppendAllText(path, result);
        }

        public override string ToString()
        {
            return $"Appender type: {GetType().Name}, " +
               $"Layout type: {LayoutType}, Report level: {ReportLevel}, " +
               $"Messages appended: {Log.Count}, File size {Log.Size}";
        }
    }
}