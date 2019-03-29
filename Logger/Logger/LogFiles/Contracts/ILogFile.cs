namespace Logger.LogFiles.Contracts
{
    public interface ILogFile
    {
        int Size { get; }
        int Count { get; }
        void Write(string message);
    }
}