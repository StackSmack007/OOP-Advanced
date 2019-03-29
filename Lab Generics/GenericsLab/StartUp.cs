using BoxOfT.Contracts;
using System;

namespace BoxOfT
{
  public  class StartUp
    {
        static void Main()
        {
            IBox<string> box = new Box<string>();
            box.Add(1.ToString());
            box.Add(2.ToString());
            box.Add(3.ToString());
            Console.WriteLine(box.Remove());
            box.Add(4.ToString());
            box.Add(5.ToString());
            Console.WriteLine(box.Remove());

        }
    }
}
