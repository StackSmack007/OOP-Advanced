using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class CommandInterpreter : ICommandInterpreter
{
    public CommandInterpreter(IHarvesterController harvesterController, IProviderController providerController)
    {
        HarvesterController = harvesterController;
        ProviderController = providerController;
    }

    public IHarvesterController HarvesterController { get; private set; }

    public IProviderController ProviderController { get; private set; }

    public string ProcessCommand(IList<string> args)
    {
        string commandName = args[0] + "Command";

        Type type = Assembly.GetCallingAssembly().GetTypes().FirstOrDefault(x => x.Name == commandName);

        if (type is null)
        {
            throw new ArgumentException(Constants.CommandNotFound, args[0]);
        }
                                   
        ICommand command = (ICommand)Activator.CreateInstance(type, GetRequsiredParameters(type,args));

        string result = command.Execute();
        return result;
    }

    private object[] GetRequsiredParameters(Type type,IList<string> inputFromCommand)
    {
        ParameterInfo[] parametersRequired = type.GetConstructors().First().GetParameters();
        object[] resultParameters = new object[parametersRequired.Length];
        for (int i = 0; i < parametersRequired.Length; i++)
        {
            Type parameterTypeRequired = parametersRequired[i].ParameterType;
            if (parameterTypeRequired==typeof(IList<string>))
            {
                resultParameters[i] = inputFromCommand;
                continue;
            }
            PropertyInfo foundProperty = this.GetType().GetProperties().FirstOrDefault(x => x.PropertyType == parameterTypeRequired);
            if (foundProperty is null)
            {
                throw new ArgumentException(String.Format(Constants.CommandParameterNotFound, parameterTypeRequired.Name));
            }
            resultParameters[i] = foundProperty.GetValue(this);
        }
        return resultParameters;
    }
}