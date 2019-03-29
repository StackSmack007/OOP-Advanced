namespace CustomStack
{
    using System;
    using System.Linq;

    public class Engine
    {
        public void Run()
        {
            Stack<int> myStack = new Stack<int>();

            string[] command;
            while ((command = Console.ReadLine().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))[0] != "END")
            {
                switch (command[0].ToUpper())
                {
                    case "PUSH": myStack.Push(command.Skip(1).Select(int.Parse).ToArray()); break;
                    case "POP":
                        {
                            try
                            {
                                myStack.Pop();
                            }
                            catch (InvalidOperationException ioe)
                            {
                                Console.WriteLine(ioe.Message);
                            }
                            break;
                        }
                }
            }
            for (int i = 0; i < 2; i++)
            {
                foreach (var item in myStack)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}