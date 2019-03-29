using System;
using System.Linq;
using System.Reflection;
using System.Text;

public class Spy
{
    public string StealFieldInfo(string className, params string[] fieldNames)
    {
        var classType = Type.GetType(className);

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Class under investigation: {className}");
        FieldInfo[] fields = classType.GetFields(BindingFlags.Instance | BindingFlags.Static |
                                           BindingFlags.Public | BindingFlags.NonPublic);
        object myInstance = Activator.CreateInstance(classType,new Type[] { });
        foreach (FieldInfo field in fields.Where(x=>fieldNames.Contains(x.Name)))
        {
                sb.AppendLine($"{field.Name} = {field.GetValue(myInstance)}");
        }
        return sb.ToString().Trim();
   }
}