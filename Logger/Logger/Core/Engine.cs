using Logger.Core.Contracts;
using System;

namespace Logger.Core
{
    public class Engine : IEngine
    {
        private CommandInterpreter ci;

        public Engine()
        {
            ci = new CommandInterpreter();
        }

        public void Run()
        {
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] inputArray = Console.ReadLine().Split(' ',StringSplitOptions.RemoveEmptyEntries);
                ci.AddAppender(inputArray);
            }

            string[] inputArgs;
            while ((inputArgs=Console.ReadLine().Split('|'))[0]!="END")
            {
                ci.ReadCommand(inputArgs);
            }
            ci.PrintFinalStats();
        }
    }
}