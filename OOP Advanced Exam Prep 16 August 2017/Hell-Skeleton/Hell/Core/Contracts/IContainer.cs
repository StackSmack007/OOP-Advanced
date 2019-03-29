using System.Collections.Generic;

public  interface IContainer
    {
    IHeroFactory heroFactory { get; }
    List<IHero> Heroes { get; }
}