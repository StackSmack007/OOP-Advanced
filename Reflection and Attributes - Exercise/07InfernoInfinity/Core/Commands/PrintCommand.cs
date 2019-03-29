namespace _07InfernoInfinity.Core.Commands
{
    using Contracts;
    using System;

    public class PrintCommand : ICommand
    {

        public void Execute(IWeapon weapon, string[] data)
        {
            Console.WriteLine(weapon);
        }
    }
}