using Problem2KingSGambit.Models;
using System;

namespace Problem2KingSGambit
{
    public class StartUp
    {
        static void Main()
        {
            string kingName = Console.ReadLine();
            King king = new King(kingName);
            string guards = Console.ReadLine();
            king.RegisterRoyalGuards(guards);
            string footmen = Console.ReadLine();
            king.RegisterFootmen(footmen);
            king.KingUnderFire += King_Attacked_RoyalGuardsAction;
            king.KingUnderFire += King_Attacked_Footmen_Action;

            string[] command = Console.ReadLine().Split();
            while (command[0] != "End")
            {
                switch (command[0].ToLower())
                {
                    case "attack": king.Attack(); break;
                    case "kill":
                        {
                            string targetName = command[1];
                            king.Kill(targetName);
                            break;
                        }
                }
                command = Console.ReadLine().Split();
            }
        }
        private static void King_Attacked_RoyalGuardsAction(King king)
        {
            foreach (var guard in king.RoyalGuards)
            {
                Console.WriteLine($"Royal Guard {guard.Name} is defending!");
            }
        }

        private static void King_Attacked_Footmen_Action(King king)
        {
            foreach (var footman in king.FootMen)
            {
                Console.WriteLine($"Footman {footman.Name} is panicking!");
            }
        }
    }
}
