namespace ComparingObjects
{
    using System;
    public class Person:IComparable<Person>
    {
        public Person(string name, int age, string town)
        {
            Name = name;
            Age = age;
            Town = town;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Town { get; private set; }

        public int CompareTo(Person other)
        {
            int compareNames = this.Name.CompareTo(other.Name);
            int compareAges = this.Age.CompareTo(other.Age);
            int compareTowns = this.Town.CompareTo(other.Town);

            if (compareNames != 0) return compareNames;
            if (compareAges != 0) return compareAges;
            return compareTowns;
        }
    }
}