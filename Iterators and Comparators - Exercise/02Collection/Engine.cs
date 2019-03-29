namespace ListIterator
{
    using System;
    using System.Linq;
    public class Engine
    {
        public void Run()
        {
            IListyIterator<string> myList;
            string[] inputArgs = Console.ReadLine().Split().Skip(1).ToArray();
            try
            {
                myList = new ListyIterator<string>(inputArgs);
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
                            case "PRINTALL": Console.WriteLine(string.Join(" ", myList)); break;
                        }
                    }
                    catch (InvalidOperationException ioe)
                    {
                        Console.WriteLine(ioe.Message);
                    }
                }
            }
            catch (InvalidOperationException ioe)
            {
                Console.WriteLine(ioe.Message);
                Environment.Exit(0);
            }
        }
    }
}