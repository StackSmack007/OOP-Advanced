namespace CustomListIterator.Core
{
    using Core.Enums;
    using Models;
    using Models.Contracts;
    using System;

    public class CommandInterpreter
    {
        private ICustomerList<string> myList;

        public CommandInterpreter()
        {
            myList =new CustomerList<string>();
        }
        
        public void ReadCommand(string input)
        {
            string[] inputArgs = input.Split();
            Command command;
            if (!Enum.TryParse(inputArgs[0],true,out command))
            {
                throw new ArgumentException("Invalid Command!");
            }
            switch (command)
            {
                case Command.Add:
                    myList.Add(inputArgs[1]);
                    break;
                case Command.Remove:
                    myList.Remove(int.Parse(inputArgs[1]));
                    break;
                case Command.Contains:
                    Console.WriteLine(myList.Contains(inputArgs[1])); 
                    break;
                case Command.Swap:
                    myList.Swap(int.Parse(inputArgs[1]), int.Parse(inputArgs[2]));
                    break;
                case Command.Greater:
                    Console.WriteLine(myList.CountGreaterThan(inputArgs[1]));  
                    break;
                case Command.Max:
                    Console.WriteLine(myList.Max());
                    break;
                case Command.Min:
                    Console.WriteLine(myList.Min());
                    break;
                case Command.Sort:
                    myList.Sort();
                    break;
                case Command.Print:
                    foreach (var item in myList)
                    {
                        Console.WriteLine(item);
                    }
                    break;
            }

        }

    }
}