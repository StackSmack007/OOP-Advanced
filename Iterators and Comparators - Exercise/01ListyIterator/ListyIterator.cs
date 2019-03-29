namespace ListIterator
{
    using System;
    using System.Collections.Generic;
    public class ListyIterator<T> : IListyIterator
    {
        private List<T> list;
        private int currentIndex;
        public ListyIterator(T[] inputArray)
        {
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

        public string Print()
        {
            if (currentIndex >= list.Count) throw new InvalidOperationException("Invalid Operation!");
            return list[currentIndex].ToString();
        }
    }
}