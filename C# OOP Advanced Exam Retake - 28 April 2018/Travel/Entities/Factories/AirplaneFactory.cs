namespace Travel.Entities.Factories
{
    using Airplanes.Contracts;
    using Contracts;
    using System;
    using System.Linq;
    using System.Reflection;

    public class AirplaneFactory : IAirplaneFactory
	{
		public IAirplane CreateAirplane(string type)
		{
            Type airplaneType = Assembly.GetCallingAssembly().GetTypes().FirstOrDefault(x => x.Name == type);
            return (IAirplane)Activator.CreateInstance(airplaneType);
		}
	}
}