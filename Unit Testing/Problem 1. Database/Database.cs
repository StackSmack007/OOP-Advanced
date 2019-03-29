using System;
using System.Collections;
using System.Collections.Generic;

namespace Problem_1._Database
{
    public class Database<T> : IEnumerable<T>
        where T : IComparable
    {
        private T[] innerBox;
        private int count;
        private const int MAXCAPACITY = 16;
        public Database(params T[] elements)
        {
            if (elements.Length > MAXCAPACITY) throw new InvalidOperationException($"Elements more than {MAXCAPACITY}!");
            count = elements.Length;
            innerBox = new T[MAXCAPACITY];
            for (int i = 0; i < elements.Length; i++)
            {
                innerBox[i] = elements[i];
            }
        }

        public void Add(T element)
        {
            if (count == MAXCAPACITY) throw new InvalidOperationException("No more space in database!");
            innerBox[count] = element;
            count++;
        }

       // public void Remove(T element)
        public void Remove()
        {
            if (count == 0) throw new InvalidOperationException("No elements to remove!");
            count--;
            innerBox[count] = default(T);
        }

        public T[] Fetch()
        {
            T[] result = new T[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = innerBox[i];
            }
            return result;
        }

        public T this[byte number]
        {
            get
            {
                if (number < 0 || number >= MAXCAPACITY) throw new IndexOutOfRangeException("Invalid index!");
                return innerBox[number];
            }
            set
            {
                if (number < 0 || number >= MAXCAPACITY) throw new IndexOutOfRangeException("Invalid index!");
                innerBox[number] = value;
            }

        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
            {
                yield return innerBox[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}