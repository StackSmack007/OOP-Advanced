namespace StrategyPattern
{
    public class Person//:IComparable<Person>
    {
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }

        public override string ToString()
        {
            return $"{Name} {Age}";
        }
    }
}