namespace _07InfernoInfinity.Core.Commands
{
    using Contracts;
    using Factories;
    using System;
    using System.Reflection;

    public class AddCommand : ICommand
    {
        private GemFactory gemFactory;
        public AddCommand()
        {
            gemFactory = new GemFactory();
        }

        public void Execute(IWeapon weapon, string[] info)
        {
            int socketIndex = int.Parse(info[0]);

            if (socketIndex < 0 || socketIndex >= weapon.Sockets) throw new ArgumentException("Invalid socketIndex");

            Type typeOfWeapon = weapon.GetType();

            FieldInfo gemField = typeOfWeapon.BaseType.GetField("gems", BindingFlags.Instance | BindingFlags.NonPublic);

            var hiddenArray = (IGem[])gemField.GetValue(weapon);
            IGem newGem= gemFactory.CreateGem(info[1].Split());

            hiddenArray[socketIndex] = newGem;
        }
    }
}