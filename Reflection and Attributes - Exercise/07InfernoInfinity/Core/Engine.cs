namespace _07InfernoInfinity.Core
{
    using Commands;
    using Contracts;
    using System;

    public class Engine : IEngine
    {
        ICommandInterpreter CI;
        public Engine()
        {
            CI = new CommandInterpreter();
        }

        public void Run()
        {
            string[] inputArgs;
            while ((inputArgs = Console.ReadLine().Split(';'))[0].ToLower() != "end")
            {
                try
                {
                    if (inputArgs.Length==1)
                    {
                        CI.InterpreteCommandForAttribute(inputArgs);
                        continue;
                    }
                    CI.InterpreteCommand(inputArgs);
                }
                catch (ArgumentException ioe)
                {
                    Console.WriteLine(ioe.Message);
                }
            }

        }
    }
}