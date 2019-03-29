namespace CustomList.Models
{
    using Contracts;
    using System;
    using System.Text;

    public class CustomerList<T> : ICustomerList<T>
        where T : IComparable
    {
        private T[] array;
        private int count;

        public CustomerList()
        {
            array = new T[0];
            count = 0;
        }

        public void Sort()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                if (array[i].CompareTo(array[i + 1]) == 1)
                {
                    T element = array[i];
                    array[i] = array[i + 1];
                    array[i + 1] = element;
                    i = -1;
                }
            }
        }

        public int Count => count;

        public void Add(T element)
        {
            count++;
            var newArr = new T[count];
            newArr[count - 1] = element;
            for (int i = 0; i < count - 1; i++)
            {
                newArr[i] = array[i];
            }
            array = newArr;
        }

        public T Remove(int index)
        {
            count--;
            T element = array[index];
            var newArr = new T[count];
            int newArrCounter = -1;
            for (int i = 0; i <= count; i++)
            {
                if (i == index) continue;
                newArr[++newArrCounter] = array[i];
            }
            array = newArr;
            return element;
        }

        public bool Contains(T element)
        {
            for (int i = 0; i < count; i++)
            {
                if (array[i].Equals(element))
                {
                    return true;
                }
            }
            return false;
        }

        public void Swap(int index1, int index2)
        {
            T element1 = array[index1];
            array[index1] = array[index2];
            array[index2] = element1;
        }

        public int CountGreaterThan(T element)
        {
            int counter = 0;
            for (int i = 0; i < count; i++)
            {
                if (array[i].CompareTo(element) == 1)
                {
                    counter++;
                }
            }
            return counter;
        }

        public T Max()
        {
            T element;
            for (int i = 0; i < count; i++)
            {
                T currentElement = array[i];
                bool isGreatest = true;
                for (int j = 0; j < count; j++)
                {
                    if (currentElement.CompareTo(array[j]) < 0)
                    {
                        isGreatest = false;
                        break;
                    }
                }
                if (isGreatest) return currentElement;
            }
            return default(T);
        }

        public T Min()
        {
            T element;
            for (int i = 0; i < count; i++)
            {
                T currentElement = array[i];
                bool isSmallest = true;
                for (int j = 0; j < count; j++)
                {
                    if (currentElement.CompareTo(array[j]) > 0)
                    {
                        isSmallest = false;
                        break;
                    }
                }
                if (isSmallest) return currentElement;
            }
            return default(T);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in array)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString().TrimEnd();
        }

    }
}