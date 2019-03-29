using System;
using System.Collections;
using System.Collections.Generic;

namespace IteratorsAndComparators
{
    public class Library : IEnumerable<Book>
    {
        private readonly List<Book> books;

        public Library(params Book[] books)
        {
            this.books = new List<Book>(books);
        }

        public IReadOnlyList<Book> Books => books;

        public IEnumerator<Book> GetEnumerator()
        {
            var comparator = new BookComparator();
            books.Sort(comparator);
            for (int i = 0; i < books.Count; i++)    //that or the other commented lines!
            {                                        //that or the other commented lines!
                yield return books[i];               //that or the other commented lines!
            }                                        //that or the other commented lines!
            //return new LibraryIterator(this.books);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        //  private class LibraryIterator : IEnumerator<Book>
        //  {
        //      private List<Book> books;
        //     private int count;
        //      public LibraryIterator(List<Book> books1)
        //      {
        //          this.books = books1;
        //          Reset();
        //      }
        //
        //      public Book Current => books[count];
        //
        //      object IEnumerator.Current => Current;
        //
        //      public void Dispose() { }
        //
        //      public bool MoveNext()
        //      {
        //          count++;
        //          return count < books.Count;
        //      }
        //
        //      public void Reset()
        //      {
        //          count = -1;
        //      }
        //  }
    }
}