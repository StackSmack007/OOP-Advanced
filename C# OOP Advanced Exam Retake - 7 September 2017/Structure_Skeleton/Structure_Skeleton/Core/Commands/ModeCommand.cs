using System.Collections.Generic;

public class ModeCommand : Command
{
    private IHarvesterController harvesterController;


    public ModeCommand(IList<string> arguments, IHarvesterController harvesterController):base(arguments)
    {
        this.harvesterController = harvesterController;
    }
       
    public override string Execute()
    {
        string mode = Arguments[1];

        return harvesterController.ChangeMode(mode);

    }
}