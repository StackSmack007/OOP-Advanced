namespace P02_BlackBoxInteger
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class BlackBoxIntegerTests
    {
        public static void Main()
        {
            //TODO put your reflection code here
           Type type = Assembly.GetCallingAssembly().GetTypes().FirstOrDefault(x => x.Name == "BlackBoxInteger");
            var instance =(BlackBoxInteger) Activator.CreateInstance(type,true);
            
            MethodInfo[] methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

            var constructor = methods.Where(x=>x.GetParameters()==new object[0]).FirstOrDefault(x => x.Name == "BlackBoxInteger");

          // var instance=constructor.Invoke()
           string[] input;

            FieldInfo innerField = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == "innerValue");

            while ((input = Console.ReadLine().Split('_'))[0] != "END")
            {
                string commandName = input[0];
                int value = int.Parse(input[1]);
                MethodInfo currentMethod = methods.FirstOrDefault(x => x.Name == commandName);
                if (currentMethod is null) throw new InvalidOperationException("Invalid MethodName!");
                currentMethod.Invoke(instance, new object[] { value });
                Console.WriteLine(innerField.GetValue(instance));
            }
        }
    }
}