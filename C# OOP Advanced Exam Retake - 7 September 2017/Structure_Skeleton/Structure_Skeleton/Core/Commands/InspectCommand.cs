using System.Collections.Generic;

public class InspectCommand : Command
{
    private IHarvesterController harvesterController;
    private IProviderController providerController;

    public InspectCommand(IList<string> arguments, IHarvesterController harvesterController, IProviderController providerController)
 : base(arguments)
    {
        this.harvesterController = harvesterController;
        this.providerController = providerController;
    }

    public override string Execute()
    {
        int id = int.Parse(Arguments[1]);
        IEntity harvester = harvesterController.GetById(id);
        IEntity provider = providerController.GetById(id);
        if (harvester is null && provider is null)
        {
            return string.Format(Constants.UnfoundMember, id);
        }
        string type, durability;
        if (harvester != null)
        {
            type = harvester.GetType().Name;
            durability = (((IHarvester)harvester).Durability).ToString();
        }
        else
        {
            type = provider.GetType().Name;
            durability = (((IHarvester)provider).Durability).ToString();
        }

        return string.Format(Constants.FoundMember, type, durability);
    }
}