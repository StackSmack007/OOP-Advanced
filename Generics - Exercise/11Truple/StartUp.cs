using System;
using System.Linq;

namespace Truple
{
    public class StartUp
    {
        static void Main()
        {
            string[] info1 = Console.ReadLine().Split();
            string names = string.Join(" ", info1.Take(2));
            string adress = info1[2];
            string city = info1[3];
            var truple1 = new Threeuple<string, string, string>(names, adress, city);
            Console.WriteLine(truple1);

            string[] info2 = Console.ReadLine().Split();
            string name = info2[0];
            int beerQuantity = int.Parse(info2[1]);
            string status = info2[2];
            var truple2 = new Threeuple<string, int, bool>(name, beerQuantity, status.Equals("drunk"));
            Console.WriteLine(truple2);

            string[] info3 = Console.ReadLine().Split();
            string bankUser = info3[0];
            double doubleValue = double.Parse(info3[1]);
            string bankName = info3[2];
            var truple3 = new Tuple<string, double, string>(bankUser, doubleValue, bankName);
           // Console.WriteLine(truple3);
          Console.WriteLine($"{truple3.Item1} -> {truple3.Item2} -> {truple3.Item3}");
        }
    }
}