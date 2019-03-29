using System;
using System.Collections.Generic;

public class ShutdownCommand : Command
{
    private IHarvesterController harvesterController;
    private IProviderController providerController;

    public ShutdownCommand(IList<string> arguments, IHarvesterController harvesterController, IProviderController providerController)
                            : base(arguments)
    {
        this.harvesterController = harvesterController;
        this.providerController = providerController;
    }

    public override string Execute()
    {
        double totalEnergy = providerController.TotalEnergyProduced;
        double totalOre = harvesterController.OreProduced;
        return String.Format(Constants.ShutDownMessage, totalEnergy, totalOre);
    }
}