using System;

namespace GenericBoxOfInteger
{
    public class StartUp
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            var box = new Box<int>();
            for (int i = 0; i < n; i++)
            {
                var item = int.Parse(Console.ReadLine());
                box.Insert(item);
                Console.WriteLine(box);
            }
        }
    }
}