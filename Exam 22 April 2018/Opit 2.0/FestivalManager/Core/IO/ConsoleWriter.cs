namespace FestivalManager.Core.IO
{
    using FestivalManager.Core.IO.Contracts;
    public class ConsoleWriter : IWriter
    {
        public void Write(string contents)
        {
            System.Console.Write(contents);
        }

        public void WriteLine(string contents)
        {
            System.Console.WriteLine(contents);
        }
    }
}