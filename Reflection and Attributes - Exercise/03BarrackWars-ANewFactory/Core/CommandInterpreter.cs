namespace _03BarracksFactory.Core
{
    using _03BarracksFactory.Contracts;
    using System;
    using System.Linq;
    using System.Reflection;
    public class CommandInterpreter : ICommandInterpreter
    {
        //Just a CommandFactory of some kind...
        private IRepository repository;
        private IUnitFactory unitFactory;


        public CommandInterpreter(IRepository repository, IUnitFactory unitFactory)
        {
            this.repository = repository;
            this.unitFactory = unitFactory;
        }

                     
        public IExecutable InterpretCommand(string[] data, string commandName)
        {
            Type type = Assembly.GetExecutingAssembly().GetTypes()
              .FirstOrDefault(x => x.Name.ToLower().StartsWith(commandName.ToLower()));
            if (type is null) throw new InvalidOperationException("Invalid command!");

            IExecutable currentInstance = (IExecutable)Activator.CreateInstance(type, new object[] { data, repository, unitFactory });
            return currentInstance;
        }
    }
}