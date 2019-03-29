using System;
namespace GenericCountMethodDouble
{
 public   class StartUp

    {
        static void Main()
        {
            var evaluator = new Evaluator<double>();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var item = double.Parse(Console.ReadLine());
                evaluator.AddItem(item);
            }
            var baseItem = double.Parse(Console.ReadLine());
            Console.WriteLine(evaluator.Evaluate(baseItem));
        }
      }
   }
