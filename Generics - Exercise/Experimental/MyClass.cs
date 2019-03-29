using System.Collections.Generic;

namespace Experimental
{
    public class MyClass
    {
        private IList<int> myList;

        public IReadOnlyCollection<int> MyList { get; }
        public MyClass(IList<int> list)
        {
            myList = list;
        }
        public void RemoveLast()
        {
            myList.RemoveAt(myList.Count - 1);
        }
    }
}