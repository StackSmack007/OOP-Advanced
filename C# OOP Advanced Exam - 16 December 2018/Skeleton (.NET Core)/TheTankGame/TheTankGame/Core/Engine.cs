namespace TheTankGame.Core
{
    using System;
    using Contracts;
    using IO.Contracts;

    public class Engine : IEngine
    {
        private bool IsRunning;
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICommandInterpreter commandInterpreter;

        public Engine(
            IReader reader, 
            IWriter writer, 
            ICommandInterpreter commandInterpreter)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandInterpreter = commandInterpreter;

            this.IsRunning = true;
        }

        public void Run()
        {
            while (IsRunning == true) 
            {
                string[] input = reader.ReadLine().Split();

                try
                {
                    writer.WriteLine(commandInterpreter.ProcessInput(input));
                }
                catch (ArgumentException ae)
                {
                    writer.WriteLine(ae.Message);
                }

                if (input[0] == "Terminate") IsRunning = false;
            }
        }
    }
}