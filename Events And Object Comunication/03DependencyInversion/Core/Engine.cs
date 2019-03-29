using System;
using System.Linq;

namespace P03_DependencyInversion.Core
{
    public class Engine
    {
        private PrimitiveCalculator calcualator;
        public Engine()
        {
            calcualator = new PrimitiveCalculator();
        }

        public void Run()
        {
            string input = Console.ReadLine();
            while (input != "End")
            {
                if (input.ToLower().StartsWith("mode"))
                {
                    char @operator = input[input.Length - 1];
                    calcualator.ChangeStrategy(@operator);
                }
                else
                {
                    int[] numbers = input.Split().Select(int.Parse).ToArray();
                    int result = calcualator.PerformCalculation(numbers[0], numbers[1]);
                    Console.WriteLine(result);
                }
                input = Console.ReadLine();
            }
        }
    }
}