using System;
using System.Collections.Generic;
using System.Linq;

public class ItemCommand : BaseCommand
{
    public ItemCommand(IList<string> inputArgs, IContainer container) : base(inputArgs, container) { }

   public override string Execute()
    {

        string heroName = InputArgs.ElementAt(1);
        IHero heroFound = container.Heroes.FirstOrDefault(x => x.Name == heroName);
        if (heroFound is null)
        {
            throw new ArgumentException(string.Format(Constants.HeroUnfoundNameError, heroName));
        }

        string itemName = InputArgs.ElementAt(0);
        var strengthBonus = long.Parse(InputArgs.ElementAt(2));
        var agilityBonus = long.Parse(InputArgs.ElementAt(3));
        var intelligenceBonus = long.Parse(InputArgs.ElementAt(4));
        var hitpointsBonus = long.Parse(InputArgs.ElementAt(5));
        var damageBonus = long.Parse(InputArgs.ElementAt(6));

        IItem newItem = new CommonItem(itemName, strengthBonus, agilityBonus, intelligenceBonus, hitpointsBonus, damageBonus);
        heroFound.AddCommonItem(newItem);

        string resultMessage = string.Format(Constants.ItemCreateMessage, itemName, heroName);
        return resultMessage;
    }
}