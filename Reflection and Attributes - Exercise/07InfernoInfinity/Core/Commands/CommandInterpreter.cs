namespace _07InfernoInfinity.Core.Commands
{
    using _07InfernoInfinity.Contracts;
    using _07InfernoInfinity.Factories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;


    public class CommandInterpreter : ICommandInterpreter
    {
        private List<IWeapon> weaponPool;
        private WeaponFactory weaponFactory;

        public CommandInterpreter()
        {
            weaponPool = new List<IWeapon>();
            weaponFactory = new WeaponFactory();
        }

        public void InterpreteCommand(string[] inputArgs)
        {

            string commandName = inputArgs[0];
            string weaponName = inputArgs[1];
            string[] data;
            if (commandName == "Create")
            {
                data = inputArgs.Skip(1).ToArray();
                weaponPool.Add(weaponFactory.CreateWeapon(data));
                return;
            }

            Type type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name.EndsWith(commandName + "Command"));

            if (type is null) throw new ArgumentException("Not Existing Command!");

            ICommand curentCommand = (ICommand)Activator.CreateInstance(type);

            IWeapon currentWeapon = weaponPool.FirstOrDefault(x => x.Name.ToLower() == weaponName.ToLower());
            if (currentWeapon is null) throw new ArgumentException("Weapon not Found!");
            data = inputArgs.Skip(2).ToArray();
            curentCommand.Execute(currentWeapon, data);
        }

        public void InterpreteCommandForAttribute(string[] inputArgs)
        {
            Type type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name == inputArgs[0] + "Command");
            if (type is null) throw new ArgumentException("Unrecognised Attributte Request!");
            IAttributteRequest request = (IAttributteRequest)Activator.CreateInstance(type);
            request.Execute();
        }
    }
}