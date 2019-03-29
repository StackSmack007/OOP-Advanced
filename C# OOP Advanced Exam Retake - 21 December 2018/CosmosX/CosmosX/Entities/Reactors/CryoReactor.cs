namespace CosmosX.Entities.Reactors
{
    using Containers.Contracts;
    public class CryoReactor : BaseReactor
    {
        public CryoReactor(int id, IContainer moduleContainer, int cryoProductionIndex)
            : base(id, moduleContainer)
        {
            this.CryoProductionIndex = cryoProductionIndex;
        }

        public int CryoProductionIndex { get; }

        public override long TotalEnergyOutput
        {
            get
            {
                long totalEnergyFromModules = base.TotalEnergyOutput * this.CryoProductionIndex;

                if (totalEnergyFromModules > base.TotalHeatAbsorbing)
                {//Overheating and no energy produced
                    totalEnergyFromModules = 0;
                }
                return totalEnergyFromModules;
            }
        }
    }
}