using NUnit.Framework;

[TestFixture]
public class HeroInventoryTests
{
    private IInventory testInstance;
    [SetUp]
    public void Setup()
    {
        testInstance = new HeroInventory();
    }

    [Test]
    public void CheckInitialisation()
    {
        Assert.IsNotNull(testInstance);
        long expectedInitiallValue = 0;
        Assert.AreEqual(expectedInitiallValue, testInstance.TotalAgilityBonus);
        Assert.AreEqual(expectedInitiallValue, testInstance.TotalDamageBonus);
        Assert.AreEqual(expectedInitiallValue, testInstance.TotalHitPointsBonus);
        Assert.AreEqual(expectedInitiallValue, testInstance.TotalIntelligenceBonus);
        Assert.AreEqual(expectedInitiallValue, testInstance.TotalStrengthBonus);
    }

    [TestCase(1, 2, 3, 4, 5)]
    [TestCase(0, 0, 0, 0, 0)]
    [TestCase(1073741824, 1073741824, 1073741824, 1073741824, 1073741824)]
    public void CheckAddItem(long strengthBonus, long agilityBonus, long intelligenceBonus, long hitpointsBonus, long damageBonus)
    {
        IItem commonItem = new CommonItem("item", strengthBonus, agilityBonus, intelligenceBonus, hitpointsBonus, damageBonus);
        testInstance.AddCommonItem(commonItem);
        Assert.AreEqual(agilityBonus, testInstance.TotalAgilityBonus);
        Assert.AreEqual(damageBonus, testInstance.TotalDamageBonus);
        Assert.AreEqual(hitpointsBonus, testInstance.TotalHitPointsBonus);
        Assert.AreEqual(intelligenceBonus, testInstance.TotalIntelligenceBonus);
        Assert.AreEqual(strengthBonus, testInstance.TotalStrengthBonus);

        long additionalValue = 3;
        IItem commonItem2 = new CommonItem("item2", additionalValue, additionalValue, additionalValue, additionalValue, additionalValue);
        testInstance.AddCommonItem(commonItem2);
        Assert.AreEqual(agilityBonus + additionalValue, testInstance.TotalAgilityBonus);
        Assert.AreEqual(damageBonus + additionalValue, testInstance.TotalDamageBonus);
        Assert.AreEqual(hitpointsBonus + additionalValue, testInstance.TotalHitPointsBonus);
        Assert.AreEqual(intelligenceBonus + additionalValue, testInstance.TotalIntelligenceBonus);
        Assert.AreEqual(strengthBonus + additionalValue, testInstance.TotalStrengthBonus);
    }

    [TestCase(10, 1, 2, 3, 4, 5)]
    [TestCase(3, 0, 0, 0, 0, 0)]
    [TestCase(0, 1073741824, 1073741824, 1073741824, 1073741824, 1073741824)]
    public void CheckAddingRecepyAndCompletingRecepyWhenItemAdded(long doneRecipeStat, long strengthBonus, long agilityBonus, long intelligenceBonus, long hitpointsBonus, long damageBonus)
    {

        IRecipe recipe = new Recipe("magiq1", doneRecipeStat, doneRecipeStat, doneRecipeStat, doneRecipeStat, doneRecipeStat, "item");
        testInstance.AddRecipeItem(recipe);
        long expectedInitiallValue = 0;
        Assert.AreEqual(expectedInitiallValue, testInstance.TotalAgilityBonus);
        Assert.AreEqual(expectedInitiallValue, testInstance.TotalDamageBonus);
        Assert.AreEqual(expectedInitiallValue, testInstance.TotalHitPointsBonus);
        Assert.AreEqual(expectedInitiallValue, testInstance.TotalIntelligenceBonus);
        Assert.AreEqual(expectedInitiallValue, testInstance.TotalStrengthBonus);

        IItem commonItem = new CommonItem("item", strengthBonus, agilityBonus, intelligenceBonus, hitpointsBonus, damageBonus);
        testInstance.AddCommonItem(commonItem);

        Assert.AreEqual(doneRecipeStat, testInstance.TotalAgilityBonus);
        Assert.AreEqual(doneRecipeStat, testInstance.TotalDamageBonus);
        Assert.AreEqual(doneRecipeStat, testInstance.TotalHitPointsBonus);
        Assert.AreEqual(doneRecipeStat, testInstance.TotalIntelligenceBonus);
        Assert.AreEqual(doneRecipeStat, testInstance.TotalStrengthBonus);
    }

    [TestCase(10, 1, 2, 3, 4, 5)]
    [TestCase(3, 0, 0, 0, 0, 0)]
    [TestCase(0, 1073741824, 1073741824, 1073741824, 1073741824, 1073741824)]
    public void CheckAddingRecepyTransformsStats(long doneRecipeStat, long strengthBonus, long agilityBonus, long intelligenceBonus, long hitpointsBonus, long damageBonus)
    {
        IItem commonItem = new CommonItem("item", strengthBonus, agilityBonus, intelligenceBonus, hitpointsBonus, damageBonus);
        testInstance.AddCommonItem(commonItem);

        IRecipe recipe = new Recipe("magiq1", doneRecipeStat, doneRecipeStat, doneRecipeStat, doneRecipeStat, doneRecipeStat, "item");
        testInstance.AddRecipeItem(recipe);
        long expectedInitiallValue = 0;
        Assert.AreEqual(doneRecipeStat, testInstance.TotalAgilityBonus);
        Assert.AreEqual(doneRecipeStat, testInstance.TotalDamageBonus);
        Assert.AreEqual(doneRecipeStat, testInstance.TotalHitPointsBonus);
        Assert.AreEqual(doneRecipeStat, testInstance.TotalIntelligenceBonus);
        Assert.AreEqual(doneRecipeStat, testInstance.TotalStrengthBonus);
    }
    
}