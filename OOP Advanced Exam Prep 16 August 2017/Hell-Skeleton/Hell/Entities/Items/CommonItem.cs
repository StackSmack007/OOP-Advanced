public class CommonItem : IItem
{
    public CommonItem(string name, long strengthBonus, long agilityBonus, long intelligenceBonus, long hitpointsBonus, long damageBonus)
    {
        Name = name;
        StrengthBonus = strengthBonus;
        AgilityBonus = agilityBonus;
        IntelligenceBonus = intelligenceBonus;
        HitPointsBonus = hitpointsBonus;
        DamageBonus = damageBonus;
    }

    public string Name { get; }

    public long StrengthBonus { get; }

    public long AgilityBonus { get; }

    public long IntelligenceBonus { get; }

    public long HitPointsBonus { get; }

    public long DamageBonus { get; }

    public override string ToString()
    {
        return Constants.GenerateCommonItemStats(this);
    }
}
