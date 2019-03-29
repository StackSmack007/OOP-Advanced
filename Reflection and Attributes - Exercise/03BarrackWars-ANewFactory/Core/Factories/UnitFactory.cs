namespace _03BarracksFactory.Core.Factories
{
    using Contracts;
    using System;
    using System.Linq;
    using System.Reflection;

    public class UnitFactory : IUnitFactory
    {
        public IUnit CreateUnit(string unitType)
        {
            Type type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x=>x.Name==unitType);

            if (type is null) throw new InvalidOperationException("Invalid type of unit");

            IUnit unit = (IUnit)Activator.CreateInstance(type,new object[] { });
            return unit;

        }
    }
}