using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericCountMethodDouble
{
    public class Evaluator<T> where T : IComparable
    {

        private List<T> list;
        public Evaluator()
        {
            list = new List<T>();
        }

        public IReadOnlyCollection<T> List { get => list.AsReadOnly();  }

        public void AddItem(T item)
        {
            list.Add(item);
        }

        public int Evaluate(T item)
        {
            return list.Where(x => x.CompareTo(item) > 0).Count();
        }
 
    }
}