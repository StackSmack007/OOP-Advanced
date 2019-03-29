namespace nodeMastery
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    public class LinkedList<T> : IEnumerable<T>
    {
        private Node<T> Base;
        public int Count { get; private set; }

        public LinkedList()
        {
            Base = null;
            Count = 0;
        }

        public void Add(T element)
        {
            if (Base is null)
            {
                Base = new Node<T>(element);
                Count++;
                return;
            }
            Node<T> current = Base;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = new Node<T>(element);
            Count++;
        }

        public bool Remove(T element)
        {
            bool IsRemoved = false;
            if (Base is null) return IsRemoved;
            if (Base.Element.Equals(element))
            {
                Base = Base.Next;
                IsRemoved = true;
                Count--;
                return IsRemoved;
            }

            Node<T> current = Base;
            while (current.Next != null)
            {
                if (current.Next.Element.Equals(element))
                {
                    IsRemoved = true;
                    Count--;
                    current.Next = current.Next.Next;
                    return IsRemoved;
                }
                current = current.Next;
            }
            return IsRemoved;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = Base;
            while (current != null)
            {
                yield return current.Element;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T this[int num]
        {
            get
            {
                if (num < 0 || num >= Count) throw new IndexOutOfRangeException("Invalid index!");
                var current = Base;
                for (int i = 0; i < num; i++)
                {
                    current = current.Next;
                }
                return current.Element;
            }
            set
            {
                if (num < 0 || num >= Count) throw new IndexOutOfRangeException("Invalid index!");
                var current = Base;
                for (int i = 0; i < num; i++)
                {
                    current = current.Next;
                }
                current.Element = value;
            }
        }

        private class Node<T>
        {
            public T Element { get; set; }
            public Node<T> Next { get; set; }

            public Node(T element)
            {
                Element = element;
                Next = null;
            }
        }
    }
}