using System.Collections.Generic;
using System.Linq;

public class HeroCommand : BaseCommand
{
    public HeroCommand(IList<string> inputArgs, IContainer container) : base(inputArgs, container) { }

    public override string Execute()
    {
        string heroName = InputArgs.ElementAt(0);
        string heroType = InputArgs.ElementAt(1);
        IHero newHero = container.heroFactory.CreateHero(heroName, heroType);
        container.Heroes.Add(newHero);
        string resultMessage = string.Format(Constants.HeroCreateMessage, heroType, heroName);
        return resultMessage;
    }
}