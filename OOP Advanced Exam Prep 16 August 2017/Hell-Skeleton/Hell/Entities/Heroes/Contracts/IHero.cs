using System;
using System.Collections.Generic;

public interface IHero : IComparable<IHero>
{
    List<IItem> Items { get; }

    string Name { get; }

    long Strength { get; }

    long Agility { get; }

    long Intelligence { get; }

    long HitPoints { get; }

    long Damage { get; }

    IInventory Inventory { get; }

    long PrimaryStats { get; }
    long SecondaryStats { get; }

        void AddRecipe(IRecipe recipe);

        void AddCommonItem(IItem item);
}