using _02.KingSGambit.Contracts;
using _02.KingSGambit.Models;
using System;

namespace _02.KingSGambit
{
   public class StartUp
    {
        static void Main()
        {
            Handler handler = new Handler();

            string kingName = Console.ReadLine();
            string guards = Console.ReadLine();
            string footmen = Console.ReadLine();

            IKing king = new King(kingName);
            king.GotAttacked += handler.On_King_GotAttacked;
            king.GetSomeoneKilled += handler.On_King_GetSomeOneKilled;
            king.RegisterGuard(guards);
            king.RegisterFootman(footmen);

            string[] input = Console.ReadLine().Split();
            while (input[0]!="End")
            {
                if (input[0].ToLower()=="attack")
                {
                    king.GetAttacked();
                }
               else if (input[0].ToLower() == "kill")
                {
                    string name = input[1];
                    king.Murder(name);
                }
                input = Console.ReadLine().Split();
            }

        }
    }
}
