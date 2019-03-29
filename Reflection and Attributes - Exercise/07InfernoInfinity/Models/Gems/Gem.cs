namespace _07InfernoInfinity.Models.Gems
{
    using _07InfernoInfinity.Contracts;
    using _07InfernoInfinity.Enumerations;
    using System;

    public abstract class Gem : IGem
    {

        private int strength;
        private int agility;
        private int vitality;

        public int Strength => strength + (int)Clarity;

        public int Agility => agility + (int)Clarity;

        public int Vitality => vitality + (int)Clarity;

        public Clarity Clarity { get; }

        public int AddMINDamage => Strength * 2+Agility;

        public int AddMAXDamage => Strength * 3 + Agility*4;

        protected Gem(int strength, int agility, int vitality, string clarity)
        {
            this.strength = strength;
            this.agility = agility;
            this.vitality = vitality;
            Clarity temp;
            if (!Enum.TryParse(clarity, true, out temp)) throw new ArgumentException("No such ClarityType!");
            Clarity = temp;
        }
    }
}