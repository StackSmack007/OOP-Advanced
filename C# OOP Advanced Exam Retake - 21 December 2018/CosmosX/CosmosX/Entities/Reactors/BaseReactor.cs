namespace CosmosX.Entities.Reactors
{
    using Containers.Contracts;
    using CosmosX.Utils;
    using Modules.Absorbing.Contracts;
    using Modules.Energy.Contracts;
    using Reactors.Contracts;
    public abstract class BaseReactor : IReactor
    {
        private readonly IContainer moduleContainer;

        protected BaseReactor(int id, IContainer moduleContainer)
        {
            this.Id = id;
            this.moduleContainer = moduleContainer;
        }

        public int Id { get; }

        public virtual long TotalEnergyOutput => this.moduleContainer.TotalEnergyOutput;

        public virtual long TotalHeatAbsorbing => this.moduleContainer.TotalHeatAbsorbing;

        public int ModuleCount => this.moduleContainer.ModulesByInput.Count;

        public void AddEnergyModule(IEnergyModule energyModule)
        {
            this.moduleContainer.AddEnergyModule(energyModule);
        }

        public void AddAbsorbingModule(IAbsorbingModule absorbingModule)
        {
            this.moduleContainer.AddAbsorbingModule(absorbingModule);
        }

        public override string ToString()
        {

            var energy = TotalEnergyOutput;

            string result = string.Format(Constants.ReactorToStringMessage, this.GetType().Name,Id, TotalEnergyOutput, TotalHeatAbsorbing, ModuleCount);
          //  public static string ReactorToStringMessage = "{0} - {1}\nEnergy Output: {2}\nHeat Absorbing: {3}\nModules: {4}";
            return result;
        }
    }
}