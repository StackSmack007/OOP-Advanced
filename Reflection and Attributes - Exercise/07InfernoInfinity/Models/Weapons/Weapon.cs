namespace _07InfernoInfinity.Models.Weapons
{
    using _07InfernoInfinity.Attributes;
    using _07InfernoInfinity.Contracts;
    using _07InfernoInfinity.Enumerations;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [MyCustomAttribute("Pesho",3,"Used for C# OOP Advanced Course - Enumerations and Attributes.", "Pesho", "Svetlio")]
    public abstract class Weapon : IWeapon
    {
        private IGem[] gems;
        private int minDamage;
        private int maxDamage;

        public string Name { get; protected set; }

        private IGem[] ActiveSlots => gems.Where(x => x != null).ToArray();
        public int MinDamage => (int)Rarity * minDamage+ActiveSlots.Sum(x=>x.AddMINDamage);

        public int MaxDamage => (int)Rarity * maxDamage + ActiveSlots.Sum(x => x.AddMAXDamage);

        public byte Sockets => (byte)gems.Length;

        public Rarity Rarity { get; }

        public IReadOnlyCollection<IGem> AppliedGems => Array.AsReadOnly(gems);

        protected Weapon(string rarity, string name, byte sockets, int minDamage, int maxDamage)
        {
            Rarity temp;
            if (!Enum.TryParse(rarity, true, out temp)) throw new ArgumentException("No such GemType!");
            Rarity = temp;

            Name = name;
            this.minDamage = minDamage;
            this.maxDamage = maxDamage;
            gems = new IGem[sockets];
        }

        public override string ToString()
        {
            int strength = ActiveSlots.Sum(x => x.Strength);
            int agility = ActiveSlots.Sum(x => x.Agility);
            int vitality = ActiveSlots.Sum(x => x.Vitality);
            return $"{this.Name}: {this.MinDamage}-{this.MaxDamage} Damage, +{strength} Strength, +{agility} Agility, +{vitality} Vitality";
        }
    }
}