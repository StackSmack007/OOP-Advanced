using System.Collections.Generic;

namespace IteratorsAndComparators
{
    public class BookComparator : IComparer<Book>
    {
        public int Compare(Book x, Book y)
        {
            int result = x.Title.CompareTo(y.Title);
            if (result != 0) return result;
            result = y.Year.CompareTo(x.Year);
            return result;
            //BookComparator must compare two books by:
            //1.Book title - alphabetical order
            //2.Year of publishing a book -from the newest to the oldest
        }
    }
}