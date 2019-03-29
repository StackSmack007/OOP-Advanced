using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem2KingSGambit.Models
{
    public delegate void KingAssaulted(King instance);
    public class King
    {
        public event KingAssaulted KingUnderFire;

        private List<Footman> footmen;
        private List<RoyalGuard> royalGuards;

        public King(string name)
        {
            Name = name;
            footmen = new List<Footman>();
            royalGuards = new List<RoyalGuard>();
        }
        public string Name { get; }

        public void RegisterRoyalGuards(string input)
        {
            foreach (string name in input.Split())
            {
                RoyalGuard rg = new RoyalGuard(name);
                royalGuards.Add(rg);
            }
        }
        public IReadOnlyCollection<Footman> FootMen => footmen.AsReadOnly();
        public IReadOnlyCollection<RoyalGuard> RoyalGuards => royalGuards.AsReadOnly();


        public void RegisterFootmen(string input)
        {
            foreach (string name in input.Split())
            {
                Footman footMen = new Footman(name);
                footmen.Add(footMen);
            }
        }

        public void Attack()
        {
            Console.WriteLine($"King {Name} is under attack!");
            KingUnderFire(this);
        }

        public void Kill(string name)
        {
            Footman footmanFound = footmen.FirstOrDefault(x => x.Name == name);
            RoyalGuard guardFound = royalGuards.FirstOrDefault(x => x.Name == name);
            if (footmanFound != null) footmen.Remove(footmanFound);
            if (guardFound != null) royalGuards.Remove(guardFound);
        }

    }
}
