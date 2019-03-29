namespace CustomStackNodVersion
{
    using System;
    using System.Linq;

    public class Program
    {
        static void Main()
        {
            CustomStack<string> stack = new CustomStack<string>();



            string[] command;
            while ((command = Console.ReadLine().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))[0] != "END")
            {
                switch (command[0].ToUpper())
                {
                    case "PUSH": stack.Push(command.Skip(1).ToArray()); break;
                    case "POP":
                        {
                            try
                            {
                                stack.Pop();
                            }
                            catch (InvalidOperationException ioe)
                            {
                                Console.WriteLine(ioe.Message);
                            }
                            break;
                        }
                }
            }
            Console.WriteLine(string.Join(Environment.NewLine, stack));
            Console.WriteLine(string.Join(Environment.NewLine, stack));


            private class sss


        }
    }
}