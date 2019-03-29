using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomStack
{
    public class Stack<T>:IEnumerable<T>
    {
        private IList<T> list;

        public Stack()
        {
            list = new List<T>();
        }


        public void Push(params T[] items)
        {
            foreach (var item in items)
            {
                list.Add(item);
            }
        }
        public void Pop()
        {
            if (list.Count == 0) throw new InvalidOperationException("No elements");
              list.RemoveAt(list.Count - 1);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = list.Count-1; i >= 0; i--)
            {
                yield return list[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}