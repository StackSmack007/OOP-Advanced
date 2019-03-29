namespace CosmosX.Entities.Reactors
{
using Containers.Contracts;
    public class HeatReactor : BaseReactor
    {
        public HeatReactor(int id, IContainer moduleContainer, int heatReduction)
            : base(id, moduleContainer)
        {
            this.HeatReductionIndex = heatReduction;
        }

        public int HeatReductionIndex { get; }

        public override long TotalHeatAbsorbing => base.TotalHeatAbsorbing + HeatReductionIndex;

        public override long TotalEnergyOutput
        {
            get
            {
                if (this.TotalHeatAbsorbing < base.TotalEnergyOutput)
                {
                    return 0;
                }
                return base.TotalEnergyOutput;
            }
        }
    }
}