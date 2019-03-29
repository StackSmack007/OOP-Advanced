using _07InfernoInfinity.Attributes;

namespace _07InfernoInfinity.Models.Weapons
{
 
    public class Sword : Weapon
    {
        public Sword(string rarity, string name) : base(rarity, name, 3, 4, 6) { }
    }
}