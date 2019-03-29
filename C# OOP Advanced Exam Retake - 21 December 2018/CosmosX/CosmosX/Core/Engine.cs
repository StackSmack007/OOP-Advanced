namespace CosmosX.Core
{
    using Core.Contracts;
    using IO.Contracts;
    using System;
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private ICommandParser commandParser;
        private bool isRunning;

        public Engine(IReader reader, IWriter writer, ICommandParser commandParser)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandParser = commandParser;
            this.isRunning = false;
        }

        public void Run()
        {
            while (true)
            {
                string[] input = reader.ReadLine().Split();
                try
                {
                    writer.WriteLine(commandParser.Parse(input));
                    if (input[0] == "Exit") break;
                }
                catch (ArgumentException ae)
                {
                    writer.WriteLine(ae.Message);
                }
            }
        }
    }
}