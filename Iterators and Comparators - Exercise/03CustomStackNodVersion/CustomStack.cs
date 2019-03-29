namespace CustomStackNodVersion
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    public class CustomStack<T>:IEnumerable<T>
    {
        private Node<T> Top;

        public CustomStack()
        {
            Top = null;
        }

        public void Push( T[] elements)
        {
            foreach (var item in elements)
            {
                var nodeNew = new Node<T>(item);
                if (Top is null)
                {
                    Top = nodeNew;
                }
                else
                {
                    var previousTop = Top;
                    Top = nodeNew;
                    Top.Previous = previousTop;
                }
            }
        }

        public void Pop()
        {
            if (Top is null)throw new InvalidOperationException("No elements");
            Top = Top.Previous;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = Top;
            while (current!=null)
            {
                yield return current.Element;
                current = current.Previous;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Node<T>
        {
            public T Element { get; private set; }
            public Node<T> Previous { get; set; }
            public Node(T element)
            {
                Element = element;
                Previous = null;
            }
        }
    }
}