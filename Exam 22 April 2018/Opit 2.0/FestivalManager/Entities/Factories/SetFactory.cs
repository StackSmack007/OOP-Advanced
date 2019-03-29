namespace FestivalManager.Entities.Factories
{
    using Contracts;
    using Entities.Contracts;
    using System;
    using System.Linq;
    using System.Reflection;

    public class SetFactory : ISetFactory
    {
        public ISet CreateSet(string name, string type)
        {
            Type classType = Assembly.GetCallingAssembly().GetTypes().FirstOrDefault(x => x.Name == type);
            return (ISet)Activator.CreateInstance(classType, new object[] { name });
      
        }
    }
}