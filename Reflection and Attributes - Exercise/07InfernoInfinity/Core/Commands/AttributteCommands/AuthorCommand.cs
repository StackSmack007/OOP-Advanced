namespace _07InfernoInfinity.Core.Commands.AttributteCommands
{
using _07InfernoInfinity.Attributes;
using _07InfernoInfinity.Contracts;
using System;
using System.Reflection;
    class AuthorCommand : IAttributteRequest
    {
        public void Execute()
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var member in types)
            {
                var attrib = member.GetCustomAttribute<MyCustomAttribute>(false);
                if (attrib!=null)
                {
                    Console.WriteLine($"Author: {attrib.Author}");
                }
            }
        }
    }
}