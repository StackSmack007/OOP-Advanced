using System.Collections.Generic;
using System.Linq;

public class RegisterCommand : Command
{
    private IHarvesterController harvesterController;
    private IProviderController providerController;

    public RegisterCommand(IList<string> arguments, IHarvesterController harvesterController, IProviderController providerController)
   :base(arguments)
    {
        this.harvesterController = harvesterController;
        this.providerController = providerController;
    }

    public override string Execute()
    {
        string type = Arguments[1];
        if (type.ToLower()=="harvester")
        {
            return harvesterController.Register(Arguments.Skip(2).ToArray());
        }
        return providerController.Register(Arguments.Skip(2).ToArray());
    }
}