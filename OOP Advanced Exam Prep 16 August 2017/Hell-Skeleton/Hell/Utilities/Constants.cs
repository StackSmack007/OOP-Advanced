using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Constants
{
    public const string HeroCreateMessage = "Created {0} - {1}";

    public const string ItemCreateMessage = "Added item - {0} to Hero - {1}";

    public const string RecipeCreatedMessage = "Added recipe - {0} to Hero - {1}";

    public static string HeroUnfoundNameError = "Hero with name {0} was not found in database";

    public static string HeroTypeNotFound = "Hero with type {0} not recognised!";

    public static string GenerateHeroReport(IHero hero)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Hero: {hero.Name}, Class: {hero.GetType().Name}");
        sb.AppendLine($"HitPoints: {hero.HitPoints}, Damage: {hero.Damage}");
        sb.AppendLine($"Strength: {hero.Strength}");
        sb.AppendLine($"Agility: {hero.Agility}");
        sb.AppendLine($"Intelligence: {hero.Intelligence}");

        if (hero.Items.Count == 0)
        {
            sb.AppendLine("Items: None");
        }
        else
        {
            sb.AppendLine("Items:");
            foreach (var item in hero.Items)
            {
                sb.AppendLine(item.ToString());
            }
        }
        return sb.ToString().Trim();
    }

    public static string GenerateCommonItemStats(IItem item)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"###Item: {item.Name}");
        sb.AppendLine($"###+{item.StrengthBonus} Strength");
        sb.AppendLine($"###+{item.AgilityBonus} Agility");
        sb.AppendLine($"###+{item.IntelligenceBonus} Intelligence");
        sb.AppendLine($"###+{item.HitPointsBonus} HitPoints");
        sb.AppendLine($"###+{item.DamageBonus} Damage");

        return sb.ToString().Trim();
    }
          
    public static string GenerateHeroStatsQuit(int number, IHero hero)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"{number}. {hero.GetType().Name}: {hero.Name}");
        sb.AppendLine($"###HitPoints: {hero.HitPoints}");
        sb.AppendLine($"###Damage: {hero.Damage}");
        sb.AppendLine($"###Strength: {hero.Strength}");
        sb.AppendLine($"###Agility: {hero.Agility}");
        sb.AppendLine($"###Intelligence: {hero.Intelligence}");

        if (hero.Items.Count == 0)
        {
            sb.AppendLine($"###Items: None");
            return sb.ToString().Trim();
        }
        sb.AppendLine($"###Items: {string.Join(", ", hero.Items.Select(x=>x.Name))}");
        return sb.ToString().Trim();
    }
}