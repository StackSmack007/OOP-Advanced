using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class AbstractHero : IHero
{
    private IInventory inventory;
    private long strength;
    private long agility;
    private long intelligence;
    private long hitPoints;
    private long damage;

    protected AbstractHero(string name, long strength, long agility, long intelligence, long hitPoints, long damage)
    {
        this.Name = name;
        this.Strength = strength;
        this.Agility = agility;
        this.Intelligence = intelligence;
        this.HitPoints = hitPoints;
        this.Damage = damage;
        this.inventory = new HeroInventory();
    }

    public string Name { get; }

    public List<IItem> Items
    {
        get
        {
            FieldInfo items = typeof(HeroInventory)
           .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
           .FirstOrDefault(x => x.Name == "commonItems");
            var commonItems = ((Dictionary<string, IItem>)items.GetValue(this.Inventory)).Values.ToList();
            return commonItems;
        }
    }

    public long Strength
    {
        get { return this.strength + this.Inventory.TotalStrengthBonus; }
        private set { this.strength = value; }
    }

    public long Agility
    {
        get { return this.agility + this.Inventory.TotalAgilityBonus; }
        private set { this.agility = value; }
    }

    public long Intelligence
    {
        get { return this.intelligence + this.inventory.TotalIntelligenceBonus; }
        set { this.intelligence = value; }
    }

    public long HitPoints
    {
        get { return this.hitPoints + this.inventory.TotalHitPointsBonus; }
        set { this.hitPoints = value; }
    }

    public long Damage
    {
        get { return this.damage + this.inventory.TotalDamageBonus; }
        set { this.damage = value; }
    }

    public long PrimaryStats
    {
        get { return this.Strength + this.Agility + this.Intelligence; }
    }

    public long SecondaryStats
    {
        get { return this.HitPoints + this.Damage; }
    }


    public IInventory Inventory => inventory;

    public void AddRecipe(IRecipe recipe)
    {
        this.inventory.AddRecipeItem(recipe);
    }

    public void AddCommonItem(IItem item)
    {
        this.inventory.AddCommonItem(item);
    }

    public int CompareTo(IHero other)
    {
        long result;
        if (this.PrimaryStats != other.PrimaryStats)
        {
            result = (this.PrimaryStats - other.PrimaryStats);
        }
        else
        {
            result = (int)(this.SecondaryStats - other.SecondaryStats);
        }
        if (result != 0)
        {
            return (int)(result / Math.Abs(result));
        }
        return (int)result;
    }

}