namespace IteratorsAndComparators
{
    using System;
    using System.Collections.Generic;
    public  interface IBook:IComparable<Book>
    {
        string Title { get; }
        int Year { get; }
        IList<string> Authors { get; }
    }
}