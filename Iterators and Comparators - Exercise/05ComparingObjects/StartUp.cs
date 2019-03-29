namespace ComparingObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class StartUp
    {
        static void Main()
        {
            List<Person> persons = new List<Person>();
            string[] input = Console.ReadLine().Split();
            while (input[0] != "END")
            {
                string name = input[0];
                int age = int.Parse(input[1]);
                string town = input[2];
                persons.Add(new Person(name, age, town));
                input = Console.ReadLine().Split();
            }
            int index = int.Parse(Console.ReadLine())-1;
            Statistics(persons, index);
        }
       static void Statistics(List<Person> list,int index)
        {
            int equalPeople = list.Where(x => x.CompareTo(list[index]) == 0).Count();
            if (equalPeople==1)
            {
                Console.WriteLine("No matches");
                return;
            }
            int notEqualPeople = list.Where(x => x.CompareTo(list[index]) != 0).Count();
            int totalNumberOfPeople = list.Count();
            Console.WriteLine($"{equalPeople} {notEqualPeople} {totalNumberOfPeople}");
        }
    }
}