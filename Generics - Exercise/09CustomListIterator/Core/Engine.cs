namespace CustomListIterator.Core
{
    using System;
    public class Engine
    {
        CommandInterpreter ci;
        public Engine()
        {
            ci = new CommandInterpreter();
        }

        public void Run()
        {
            string input = Console.ReadLine();
            while (input != "END")
            {
                ci.ReadCommand(input);
                input = Console.ReadLine();
            }
        }
    }
}