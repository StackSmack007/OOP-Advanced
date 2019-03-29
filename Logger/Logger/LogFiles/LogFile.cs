namespace Logger.LogFiles
{
    using Contracts;
    using System.Linq;
    using System.Text;

    public class LogFile : ILogFile
    {
        private StringBuilder sb;
        public int Size => sb.ToString().Where(x => char.IsLetter(x)).Sum(x => x);
        public int Count { get; private set; }
        public LogFile()
        {
            sb = new StringBuilder();
            Count = 0;
        }
        public void Write(string message)
        {
            sb.Append(message);
            Count++;
        }
    }
}