using System.Collections.Generic;

public interface IInventory
{
    void AddCommonItem(IItem item);
    void AddRecipeItem(IRecipe recipe);

    long TotalStrengthBonus { get; }
    long TotalAgilityBonus { get; }
    long TotalIntelligenceBonus { get; }
    long TotalHitPointsBonus { get; }
    long TotalDamageBonus { get; }
}