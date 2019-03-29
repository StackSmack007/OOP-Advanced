using System.Collections.Generic;

namespace ListIterator
{
    public  interface IListyIterator<T>:IEnumerable<T>
    {
        bool Move();
        bool HasNext();
        string Print(); 
    }
}
