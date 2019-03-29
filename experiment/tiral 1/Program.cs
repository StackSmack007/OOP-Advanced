using System;
using System.Linq;
using System.Reflection;
using tiral_1.Contracts;

namespace tiral_1
{
   public class Program
    {
        static void Main()
        {
            var proba = new Person<int>("Asparuh", "Leshnikov");
          var mylist=  proba.CreateList();
            Type type = typeof(Person<int>);
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Where(x=>x.IsFamily).ToArray();
            for (int i = 0; i < fields.Length; i++)
            {
                fields[i].SetValue(proba, "Krokodil");
            }
            Console.WriteLine(proba.firstName+" "+ proba.LastName);

            Console.WriteLine(mylist.Count);


        }
    }
}
