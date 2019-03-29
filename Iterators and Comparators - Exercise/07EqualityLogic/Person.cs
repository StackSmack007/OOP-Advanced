namespace EqualityLogic
{
    using System;
    public class Person:IComparable<Person>
    {
        public Person(string name, int age)
        {
            Name = name;
            Age = age;

        }

        public string Name { get; private set; }
        public int Age { get; private set; }


        public int CompareTo(Person other)
        {                   
           int compareNames = this.Name.CompareTo(other.Name);
           int compareAges = this.Age.CompareTo(other.Age);
            if (compareNames != 0) return compareNames;
            return compareAges;
        }
    }
}