using System.Collections.Generic;

namespace IteratorsAndComparators
{
    public class Book : IBook
    {
        public string Title { get; private set; }

        public int Year { get; private set; }

        public IList<string> Authors { get; private set; }

        public Book(string title, int year, params string[] autors)
        {
            Title = title;
            Year = year;
            Authors = new List<string>(autors);
        }

        public int CompareTo(Book other)
        {
            int result = this.Year.CompareTo(other.Year);
            if (result == 0) result = this.Title.CompareTo(other.Title);
            return result;
        }
        //•	First sort them in ascending chronological order(by year)
        //•	If two books are published in the same year, sort them alphabetically
        public override string ToString()
        {
            return $"{Title} - {Year}";
        }
    }
}