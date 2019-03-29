using _02.KingSGambit.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace _02.KingSGambit.Models
{
    public class Handler
    {
        private const int FootmenHitTolerance = 2;
        private const int GuardHitTolerance = 3;
        private Dictionary<string, int> nameHits;

        public Handler()
        {
            nameHits = new Dictionary<string, int>();
        }
        public void On_King_GetSomeOneKilled(IKing king, string name)
        {
            if (!nameHits.ContainsKey(name))
            {
                nameHits[name] = 0;
            }
            nameHits[name]++;

            var subject = king.Subjects.FirstOrDefault(x => x.Name == name);
            int subjectHitTolerance = subject.GetType().Name == "Footman" ? FootmenHitTolerance : GuardHitTolerance;
            if (subjectHitTolerance == nameHits[name])
            {
                king.Subjects.FirstOrDefault(x => x.Name == name).Die();
            }
        }


        public void On_King_GotAttacked(IKing king)
        {
            System.Console.WriteLine($"King {king.Name} is under attack!");
            SubordinatesAct(king);
        }

        private void SubordinatesAct(IKing king)
        {
            foreach (var subject in king.Subjects.Where(x => x.GetType().Name == "Guard" && x.IsAlive))
            {
                System.Console.WriteLine($"Royal Guard {subject.Name} is defending!");
            }
            foreach (var subject in king.Subjects.Where(x => x.GetType().Name == "Footman" && x.IsAlive))
            {
                System.Console.WriteLine($"Footman {subject.Name} is panicking!");
            }
        }
    }
}
