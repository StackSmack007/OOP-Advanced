using System;
using System.Collections;
using System.Linq;

namespace Problem_2_Extended_Database
{
    public class Database : IEnumerable
    {
        private IPerson[] innerBox;
        private int count;
        private const int MAXCAPACITY = 16;
        public Database(params IPerson[] elements)
        {
            if (elements.Length > MAXCAPACITY) throw new InvalidOperationException($"Elements more than {MAXCAPACITY}!");
            count = elements.Length;
            innerBox = new IPerson[MAXCAPACITY];
            for (int i = 0; i < elements.Length; i++)
            {
                innerBox[i] = elements[i];
            }
        }

        public void Add(IPerson element)
        {
            if (count == MAXCAPACITY) throw new InvalidOperationException("No more space in database!");
            if (innerBox.Any(x=>x.Name==element.Name)|| innerBox.Any(x => x.Id == element.Id))
            {
                throw new InvalidOperationException("Person already exists in database!");
            }
            innerBox[count] = element;
            count++;
        }

        public void Remove()
        {
            if (count == 0) throw new InvalidOperationException("No elements to remove!");
            count--;
            innerBox[count] = default(IPerson);
        }

        public IPerson FindByUserName(string userName)
        {
            if (userName is null)
            {
                throw new ArgumentNullException("null is invalid name");
            }
            IPerson result = innerBox.Take(count).FirstOrDefault(x => x.Name == userName);
            if (result is null)
            {
                throw new InvalidOperationException("No user is present by this username");
            }
            return result;
        }

        public IPerson FindByUserId(long id)
        {
            if (id <0)
            {
                throw new ArgumentOutOfRangeException("Negative Id");
            }
            IPerson result = innerBox.Take(count).FirstOrDefault(x => x.Id == id);
            if (result is null)
            {
                throw new InvalidOperationException("No user is present by this Id");
            }
            return result;
        }

        public IPerson[] Fetch()
        {
            IPerson[] result = innerBox.Take(count).ToArray();
            return result;
        }


        public IPerson this[byte number]
        {
            get
            {
                if (number < 0 || number >= MAXCAPACITY) throw new IndexOutOfRangeException("Invalid index!");
                return innerBox[number];
            }
            set
            {
                if (number < 0 || number >= MAXCAPACITY) throw new IndexOutOfRangeException("Invalid index!");
                innerBox[number] = value;
            }

        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < count; i++)
            {
                yield return innerBox[i];
            }
        }
    }
}