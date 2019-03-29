using System.Collections.Generic;

namespace CustomList.Models.Contracts
{
  public  interface ICustomerList<T>
    { 
        void Add(T element);
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