namespace ListIterator
{
    using System;
    using System.Linq;
    public class Engine
    {
        public void Run()
        {
            string[] inputArgs = Console.ReadLine().Split().Skip(1).ToArray();
            IListyIterator myList = new ListyIterator<string>(inputArgs);
            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                try
                {
                    switch (command.ToUpper())
                    {
                        case "HASNEXT": Console.WriteLine(myList.HasNext()); break;
                        case "PRINT": Console.WriteLine(myList.Print()); ; break;
                        case "MOVE": Console.WriteLine(myList.Move()); break;
                        default:
                            throw new InvalidOperationException("Invalid Operation - Command!");
                    }
                }
                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine(ioe.Message);
                }
            }

        }
    }
}