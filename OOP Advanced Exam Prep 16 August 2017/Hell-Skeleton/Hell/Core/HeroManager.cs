using System;
using System.Collections.Generic;
using System.Linq;

public class HeroManager : IManager
{
    private IContainer container;

    public HeroManager(IContainer container)
    {
        this.container = container;
    }

    public string ProcessCommand(IList<string> arguments)
    {
        try
        {
            string commandName = arguments[0];
            Type commandType = Type.GetType(commandName + "Command");
            ICommand command = (ICommand)Activator.CreateInstance(commandType, new object[] { arguments.Skip(1).ToList(), container });
            return command.Execute();
        }
        catch (Exception ex)
        {
            throw ex.InnerException ?? ex;
        }
    }
}