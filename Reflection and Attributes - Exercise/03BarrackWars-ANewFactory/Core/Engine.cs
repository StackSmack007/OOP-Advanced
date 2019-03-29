namespace _03BarracksFactory.Core
{
    using Contracts;
    using System;

    class Engine : IRunnable
    {
        private ICommandInterpreter ICI;

        public Engine(IRepository repository, IUnitFactory unitFactory)
        {
            ICI = new CommandInterpreter(repository, unitFactory);
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    string[] data = input.Split();
                    string commandName = data[0];
                    var command = ICI.InterpretCommand(data, commandName);
                    string result = command.Execute();
                    Console.WriteLine(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}