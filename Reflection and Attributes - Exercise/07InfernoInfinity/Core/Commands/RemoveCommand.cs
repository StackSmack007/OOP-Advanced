namespace _07InfernoInfinity.Core.Commands
{
    using _07InfernoInfinity.Contracts;
    using System;
    using System.Reflection;
    public class RemoveCommand : ICommand
    {
        public void Execute(IWeapon weapon, string[] info)
        {
            
                int socketIndex = int.Parse(info[0]);

                if (socketIndex < 0 || socketIndex >= weapon.Sockets) throw new ArgumentException("Invalid socketIndex");

                Type typeOfWeapon = weapon.GetType();

                FieldInfo gemField = typeOfWeapon.BaseType.GetField("gems", BindingFlags.Instance | BindingFlags.NonPublic);
            //TODO
                var hiddenArray = (IGem[])gemField.GetValue(weapon);

                hiddenArray[socketIndex] = default(IGem);
            
        }
    }
}