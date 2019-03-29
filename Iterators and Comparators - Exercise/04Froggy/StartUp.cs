using System;
namespace Frog
{
   public class StartUp
    {
        static void Main()
        {
            string input = Console.ReadLine();
            Lake lake = new Lake(input);
            Console.WriteLine(string.Join(", ",lake));
        }
    }
}