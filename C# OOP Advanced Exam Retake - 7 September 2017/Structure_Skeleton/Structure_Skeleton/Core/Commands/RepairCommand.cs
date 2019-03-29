using System.Collections.Generic;

public class RepairCommand : Command
{

    private IProviderController providerController;

    public RepairCommand(IList<string> arguments, IProviderController providerController) : base(arguments)
    {
        this.providerController = providerController;
    }


    public override string Execute()
    {
        double value = double.Parse(Arguments[1]);
        return providerController.Repair(value);
    }
}