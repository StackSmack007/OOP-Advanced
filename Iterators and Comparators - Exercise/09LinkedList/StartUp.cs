namespace nodeMastery
{
    using System;
    public class StartUp
    {
        static void Main()
        {
            LinkedList<int> myList = new LinkedList<int>();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] args = Console.ReadLine().Split();
                string command = args[0];
                int number = int.Parse(args[1]);
                if (command == "Add")
                {
                    myList.Add(number);
                }
                else if (command == "Remove")
                {
                    myList.Remove(number);
                }
            }
            Console.WriteLine(myList.Count);
            Console.WriteLine(string.Join(" ", myList));
        }
    }
}