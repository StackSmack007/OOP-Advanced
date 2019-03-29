using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class InspectCommand : BaseCommand
    {
        public InspectCommand(IList<string> inputArgs, IContainer container) : base(inputArgs, container) { }

        public override string Execute()
        {

        string heroName = InputArgs.ElementAt(0);
        IHero heroFound = container.Heroes.FirstOrDefault(x => x.Name == heroName);


        if (heroFound is null)
        {
            throw new ArgumentException(string.Format(Constants.HeroUnfoundNameError, heroName));
        }

        string result = Constants.GenerateHeroReport(heroFound);
        return result;
        }
    }