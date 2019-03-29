namespace _07InfernoInfinity.Factories
{
    using _07InfernoInfinity.Contracts;
    using System;
    using System.Linq;
    using System.Reflection;
    public class GemFactory
    {
        public IGem CreateGem(string[] data)
        {
            string gemClarity = data[0];
            string gemType = data[1];
            var type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name.ToLower() == gemType.ToLower());
            if (type is null) throw new ArgumentException("Invalid GemType!");
            IGem instance = (IGem)Activator.CreateInstance(type, new object[] { gemClarity });
            return instance;

        }
    }
}