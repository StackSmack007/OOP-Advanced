namespace CosmosX.IO
{
    using Contracts;
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string text)
        {
            System.Console.WriteLine(text);
        }
    }
}