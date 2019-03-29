using System;
using System.Collections.Generic;

namespace Experimental
{
  public  class StartUp
    {
        static void Main()
        {
            IList<int> tempList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            MyClass myclass = new MyClass(tempList);
            myclass.RemoveLast();
            myclass.RemoveLast();
            myclass.RemoveLast();
            myclass.RemoveLast();
            myclass.RemoveLast();
            Console.WriteLine(tempList[tempList.Count-1]);
        }
    }
}
