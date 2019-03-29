namespace _03BarracksFactory.Core.CommandList
{
    using _03BarracksFactory.Contracts;
    using System;

    public class RetireCommand : Command
    {
        public RetireCommand(string[] data, IRepository repository, IUnitFactory unitFactory) : base(data, repository, unitFactory) { }

        public override string Execute()
        {
            string unitType = Data[1];
            try
            {
                Repository.RemoveUnit(unitType);

            }
            catch (InvalidOperationException ioe)
            {
                return ioe.Message;
            }
            return $"{unitType} retired!";
        }
    }
}