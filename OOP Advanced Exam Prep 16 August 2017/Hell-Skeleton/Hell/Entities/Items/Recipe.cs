using System.Collections.Generic;

public class Recipe:IRecipe
    {

    public Recipe(string name, long strengthBonus, long agilityBonus, long intelligenceBonus, long hitpointsBonus, long damageBonus,params string[] itemsRequired)
    {
        Name = name;

       StrengthBonus = strengthBonus;
       AgilityBonus = agilityBonus;
       IntelligenceBonus = intelligenceBonus;
       HitPointsBonus = hitpointsBonus;
       DamageBonus = damageBonus;
        RequiredItems = itemsRequired;
    }

    public string Name { get; }


    public long StrengthBonus { get; }

    public long AgilityBonus { get; }

    public long IntelligenceBonus { get; }

    public long HitPointsBonus { get; }

    public long DamageBonus { get; }

    public IList<string> RequiredItems { get; }
}