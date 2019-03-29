namespace WorkForce.Factories
{
    using Contracts;
    using System;
    using System.Linq;
    using System.Reflection;
    public class EmployeeFactory
    {
        public IEmployee CreateEmployee(string input)
        {
            string[] args = input.Split();
            string typeName = args[0];
            string name = args[1];
            var typeClass = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name == typeName);
            return (IEmployee)Activator.CreateInstance(typeClass, new object[] { name });
        }
    }
}