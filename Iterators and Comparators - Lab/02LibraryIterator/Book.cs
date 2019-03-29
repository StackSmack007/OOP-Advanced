using System.Collections.Generic;

namespace IteratorsAndComparators
{
    public class Book : IBook
    {
        public string Title { get;private set; }

        public int Year { get;private set; }

        public IList<string> Authors { get; private set; }

        public Book(string title, int year, params string[] autors)
        {
            Title = title;
            Year = year;
            Authors = new List<string>(autors);
        }
    }
}