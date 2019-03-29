using System;
using System.Linq;
using System.Reflection;

public class Tracker
{
    public void PrintMethodsByAuthor()
    {
        Type type = typeof(StartUp);
        MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        foreach (MethodInfo method in methods.Where(x => x.CustomAttributes.Any(n => n.AttributeType == typeof(SoftUniAttribute))))
        {
            var attrs = method.GetCustomAttributes();
            foreach (SoftUniAttribute attr in attrs)
            {
                Console.WriteLine($"{method.Name} is written by {attr.Name}");
            }
        }
    }
}