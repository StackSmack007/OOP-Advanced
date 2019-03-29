using System;

namespace GenericBoxOfString
{
   public class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            var box = new Box<string>();
            for (int i = 0; i < n; i++)
            {
                var item = Console.ReadLine();
                box.Insert(item);
                Console.WriteLine(box);
            }
        }
    }
}