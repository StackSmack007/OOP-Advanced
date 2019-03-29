namespace BoxOfT
{
    using Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class Box<T> : IBox<T>
    {
        private IList<T> list;
        public Box()
        {
            list = new List<T>();
        }

        public int Count => list.Count;

          public void Add(T element)
          {
              list.Add(element);
          }
       
          public T Remove()
          {
              T item = list.Last();
              list.RemoveAt(list.Count - 1);
              return item;
          }
    }
}