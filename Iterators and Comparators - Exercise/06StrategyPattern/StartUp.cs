namespace StrategyPattern
{
    using System;
    using System.Collections.Generic;

    public  class StartUp
    {
        static void Main()
        {
            int count = int.Parse(Console.ReadLine());

            NamesComparator nameSort = new NamesComparator();
            AgeComparator ageSort = new AgeComparator();

            SortedSet<Person> personsSordedByName = new SortedSet<Person>(nameSort);
            SortedSet<Person> personsSordedByAge = new SortedSet<Person>(ageSort);

            for (int i = 0; i < count; i++)
            {
                string[] input = Console.ReadLine().Split();
                string name = input[0];
                int age = int.Parse(input[1]);
                personsSordedByName.Add(new Person(name, age));
                personsSordedByAge.Add(new Person(name, age));
            }

            Console.WriteLine(string.Join(Environment.NewLine, personsSordedByName));

            Console.WriteLine(string.Join(Environment.NewLine, personsSordedByAge));
        }
    }
}