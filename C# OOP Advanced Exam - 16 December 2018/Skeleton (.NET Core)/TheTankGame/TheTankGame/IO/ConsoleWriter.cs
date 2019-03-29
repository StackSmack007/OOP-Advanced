namespace TheTankGame.IO
{
    using Contracts;
    using System;

    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}