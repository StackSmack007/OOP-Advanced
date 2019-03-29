
namespace EqualityLogic
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            SortedSet<Person> setSorted = new SortedSet<Person>();
            HashSet<Person> setHash = new HashSet<Person>(new EqualityComparer());
            for (int i = 0; i < n; i++)
            {
                string[] inputArgs = Console.ReadLine().Split();
                string name = inputArgs[0];
                int age = int.Parse(inputArgs[1]);
                setHash.Add(new Person(name, age));
                setSorted.Add(new Person(name, age));
            }
            Console.WriteLine(setSorted.Count);
            Console.WriteLine(setHash.Count);
        }
    }
}
