namespace ListIterator
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    public class ListyIterator<T> : IListyIterator<T>
    {
        private List<T> list;
        private int currentIndex;
        public ListyIterator(T[] inputArray)
        {
            if (inputArray.Length == 0) throw new InvalidOperationException("Invalid Operation!");
            list = new List<T>(inputArray);
            currentIndex = 0;
        }

        public bool Move()
        {
            if (currentIndex >= list.Count) return false;
            currentIndex++;
            return true;
        }

        public bool HasNext()
        {
            if (currentIndex + 1 < list.Count) return true;
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < list.Count; i++)
            {
                yield return list[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

         public string Print()
         {
             if (currentIndex >= list.Count) throw new InvalidOperationException("Invalid Operation!");
             return list[currentIndex].ToString();
         }


    }
}