using _02.KingSGambit.Contracts;
using System.Linq;

namespace _02.KingSGambit.Models
{
    public class Handler
    {
        public void On_King_GetSomeOneKilled(IKing king, string name)
        {
            king.Subjects.FirstOrDefault(x => x.Name == name).Die();
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
