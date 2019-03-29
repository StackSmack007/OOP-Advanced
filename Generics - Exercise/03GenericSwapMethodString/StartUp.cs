using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericSwapMethodString
{
    public class StartUp
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            IList<string> list = new List<string>();

            for (int i = 0; i < n; i++)
            {
                var element = Console.ReadLine();
                list.Add(element);
            }
            int[] indexesToSwap = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Swapper(list, indexesToSwap[0], indexesToSwap[1]);
        }
        static void Swapper<T>(IList<T> list, int firstIndex, int secondIndex)
        {
            var firstElement = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = firstElement;
            foreach (T item in list)
            {
                Console.WriteLine(item.GetType() + ": " + item);
            }
        }
    }
}