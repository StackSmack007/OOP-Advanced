using System;
public class HeroFactory : IHeroFactory
{
    public IHero CreateHero(string name, string heroType)
    {

        Type type = Type.GetType(heroType);
        IHero newHero = (IHero)Activator.CreateInstance(type, new object[] {name });
        if (newHero is null)
        {
            throw new ArgumentException(Constants.HeroTypeNotFound, heroType);
        }
        return newHero;
    }
}