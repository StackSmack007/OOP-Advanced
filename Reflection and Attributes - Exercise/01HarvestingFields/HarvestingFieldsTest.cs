namespace P01_HarvestingFields
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class HarvestingFieldsTest
    {
        public static void Main()
        {
            string input = Console.ReadLine().ToLower();

            Type type = Assembly.GetCallingAssembly().GetTypes().FirstOrDefault(x => x.Name.EndsWith("Fields"));
            if (type is null)
            {
                Console.WriteLine("Class Not Found!");
                return;
            }

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            
            while (input != "harvest")
            {
                if (input == "private") PrintFields(fields.Where(x => x.IsPrivate).ToArray());
                if (input == "public") PrintFields(fields.Where(x => x.IsPublic).ToArray());
                if (input == "protected") PrintFields(fields.Where(x => x.IsFamily).ToArray());
                if (input == "all") PrintFields(fields);
                                                  
                input = Console.ReadLine().ToLower();
            }

        }
        static void PrintFields(FieldInfo[] fieldsToPrint)
        {
            foreach (var item in fieldsToPrint)
            {
                Console.WriteLine($"{item.GetAccessModifier()} {item.FieldType.Name} {item.Name}");
            }
        }
    }
}