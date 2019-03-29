using System.Collections.Generic;
public class HeroesContainer:IContainer
{
    public HeroesContainer()
    {
        heroFactory = new HeroFactory();
        Heroes = new List<IHero>();
    }
    public IHeroFactory heroFactory { get; }
    public List<IHero> Heroes { get;}
   
}
