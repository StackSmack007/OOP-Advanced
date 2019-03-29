using System;
using System.Linq;
using System.Reflection;
using TheTankGame.Entities.Parts.Contracts;
using TheTankGame.Entities.Parts.Factories.Contracts;

namespace TheTankGame.Entities.Parts.Factories
{
    public class PartFactory : IPartFactory
    {
        public IPart CreatePart(string partType, string model, double weight, decimal price, int additionalParameter)
        {
            try
            {
                Type type = Assembly.GetCallingAssembly().GetTypes().FirstOrDefault(x => x.Name==(partType+"Part"));
                return (IPart)Activator.CreateInstance(type, new object[] { model, weight, price, additionalParameter });
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}