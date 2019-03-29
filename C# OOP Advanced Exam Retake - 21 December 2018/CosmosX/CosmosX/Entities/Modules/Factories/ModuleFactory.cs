namespace CosmosX.Entities.Modules.Factories
{
    using CosmosX.Entities.Modules.Contracts;
    using CosmosX.Entities.Modules.Factories.Contracts;
    using CosmosX.Utils;
    using System;
    using System.Linq;
    using System.Reflection;

    public class ModuleFactory : IModuleFactory
    {
        public IModule CreateModule(string moduleType, int id, int additionalParameter)
        {
            try
            {
            Type type = Assembly.GetCallingAssembly().GetTypes().FirstOrDefault(x => x.Name == moduleType);
            if (type is null)
            {
                throw new ArgumentException(Constants.ModuleCreateMessage, moduleType);
            }
            IModule result = (IModule)Activator.CreateInstance(type, new object[] { id, additionalParameter });
            return result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException??ex;
            }
                    }
    }
}