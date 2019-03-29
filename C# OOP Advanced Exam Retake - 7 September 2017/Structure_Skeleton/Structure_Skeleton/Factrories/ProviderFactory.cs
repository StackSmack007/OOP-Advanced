using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class ProviderFactory : IProviderFactory
{
    public IProvider GenerateProvider(IList<string> args)
    {
        string type = args[0] + "Provider";
        int id = int.Parse(args[1]);
        double energyOutput = double.Parse(args[2]);

        Type classType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == type);
        return (IProvider)Activator.CreateInstance(classType, new object[] { id, energyOutput });
    }
}