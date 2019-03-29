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
        object myInstance = Activator.CreateInstance(classType, new Type[] { });
        foreach (FieldInfo field in fields.Where(x => fieldNames.Contains(x.Name)))
        {
            sb.AppendLine($"{field.Name} = {field.GetValue(myInstance)}");
        }
        return sb.ToString().Trim();
    }

    public string AnalyzeAcessModifiers(string className)
    {
        StringBuilder sb = new StringBuilder();
        var classType = Type.GetType(className);
        var fields = classType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
        foreach (FieldInfo field in fields)
        {
            sb.AppendLine($"{field.Name} must be private!");
        }
        var methods = classType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (MethodInfo method in methods.Where(x => !x.IsPublic && x.Name.StartsWith("get")))
        {
            sb.AppendLine($"{method.Name} have to be public!");
        }
        foreach (MethodInfo method in methods.Where(x => x.IsPublic && x.Name.StartsWith("set")))
        {
            sb.AppendLine($"{method.Name} have to be private!");
        }
        return sb.ToString().Trim();
    }
}