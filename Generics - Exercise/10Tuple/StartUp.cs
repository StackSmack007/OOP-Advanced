using System;
using System.Linq;

namespace Tuple
{
   public class StartUp
    {
        static void Main()
        {
            string[] info1 = Console.ReadLine().Split();
            string names = string.Join(" ", info1.Take(2));
            string adress = info1[2];
            var tuple1 = new Tuple<string, string>(names,adress);
            Console.WriteLine(tuple1);

            string[] info2 = Console.ReadLine().Split();
            string name = info2[0];
            int beerQuantity = int.Parse(info2[1]);
            var tuple2 = new Tuple<string, int>(name, beerQuantity);
            Console.WriteLine(tuple2);

            string[] info3 = Console.ReadLine().Split();
            int intValue = int.Parse(info3[0]);
            double doubleValue = double.Parse(info3[1]);
            var tuple3 = new Tuple<int, double>(intValue, doubleValue);
            Console.WriteLine(tuple3);
        }
    }
}