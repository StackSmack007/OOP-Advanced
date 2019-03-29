using System;

namespace GenericCountMethodString
{
 public   class StartUp

    {
        static void Main()
        {
            var evaluator = new Evaluator<string>();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string item = Console.ReadLine();
                evaluator.AddItem(item);
            }
            string baseItem = Console.ReadLine();
            Console.WriteLine(evaluator.Evaluate(baseItem));
        }
      }
   }
