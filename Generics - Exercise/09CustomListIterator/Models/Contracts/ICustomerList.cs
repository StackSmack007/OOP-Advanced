using System.Collections;

namespace CustomListIterator.Models.Contracts
{
    public interface ICustomerList<T>
    {
        void Add(T element);
        T this[int number] { get; }
        IEnumerator GetEnumerator();
        T Remove(int index);
        bool Contains(T element);
        void Swap(int index1, int index2);
        int CountGreaterThan(T element);
        int Count { get; }
        void Sort();
        T Max();
        T Min();
    }
}