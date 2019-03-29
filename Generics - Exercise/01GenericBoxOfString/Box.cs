using System.Collections.Generic;
using System.Linq;

namespace GenericBoxOfString
{
    public class Box<T>
    {
        private IList<T> list;
        public Box()
        {
            list = new List<T>();
        }
        public void Insert(T item)
        {
            list.Add(item);
        }
        public override string ToString()
        {
            T item = list.LastOrDefault();
            return $"{item.GetType()}: {item}";
        }
    }
}