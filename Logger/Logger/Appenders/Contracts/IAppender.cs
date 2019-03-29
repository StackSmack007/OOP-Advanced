using Logger.Enumerations;
using Logger.LogFiles.Contracts;

namespace Logger.Appenders.Contracts
{
    public interface IAppender
    {
        void Append(string date, string reportLevel, string message);
        ReportLevel ReportLevel { get; set; }
        ILogFile Log { get; }
        string LayoutType { get; }
    }
}