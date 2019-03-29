using System;
using System.Collections.Generic;
using System.Linq;

public class RecipeCommand : BaseCommand
{
    public RecipeCommand(IList<string> inputArgs, IContainer container) : base(inputArgs, container) { }

    public override string Execute()
    {

        string heroName = InputArgs.ElementAt(1);
        IHero heroFound = container.Heroes.FirstOrDefault(x => x.Name == heroName);
        if (heroFound is null)
        {
            throw new ArgumentException(string.Format(Constants.HeroUnfoundNameError, heroName));
        }

        string recipeName = InputArgs.ElementAt(0);
        var strengthBonus = long.Parse(InputArgs.ElementAt(2));
        var agilityBonus = long.Parse(InputArgs.ElementAt(3));
        var intelligenceBonus = long.Parse(InputArgs.ElementAt(4));
        var hitpointsBonus = long.Parse(InputArgs.ElementAt(5));
        var damageBonus = long.Parse(InputArgs.ElementAt(6));
        string[] itemsRequiredNames = InputArgs.Skip(7).ToArray();

        IRecipe newRecipe = new Recipe(recipeName, strengthBonus, agilityBonus, intelligenceBonus, hitpointsBonus, damageBonus,itemsRequiredNames);
        heroFound.AddRecipe(newRecipe);
        string resultMessage = string.Format(Constants.RecipeCreatedMessage, recipeName, heroName);
        return resultMessage;
    }
}