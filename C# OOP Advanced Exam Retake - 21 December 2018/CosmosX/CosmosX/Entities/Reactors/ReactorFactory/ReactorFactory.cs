namespace CosmosX.Entities.Reactors.ReactorFactory
{
    using Containers.Contracts;
    using CosmosX.Utils;
    using Reactors.Contracts;
    using Reactors.ReactorFactory.Contracts;
    using System;
    using System.Linq;
    using System.Reflection;

    public class ReactorFactory : IReactorFactory
    {
        public IReactor CreateReactor(string reactorTypeName, int id, IContainer moduleContainer, int additionalParameter)
        {
            reactorTypeName += "Reactor";
            try
            {
                Type type = Assembly.GetCallingAssembly().GetTypes().FirstOrDefault(x => x.Name == reactorTypeName);
                if (type is null)
                {
                    throw new ArgumentException(Constants.ReactorTypeNotFound, reactorTypeName);
                }
                IReactor result = (IReactor)Activator.CreateInstance(type, new object[] { id, moduleContainer, additionalParameter });
                return result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }
    }
}