﻿namespace CosmosX.Entities.Containers
{
    using Contracts;
    using CosmosX.Utils;
    using Modules.Absorbing.Contracts;
    using Modules.Contracts;
    using Modules.Energy.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class ModuleContainer : IContainer
    {
        private int moduleCapacity;
        private IList<IModule> modulesByInput;
        private IDictionary<int, IEnergyModule> energyModules;
        private IDictionary<int, IAbsorbingModule> absorbingModules;

        public ModuleContainer(int moduleCapacity)
        {
            this.moduleCapacity = moduleCapacity;
            this.modulesByInput = new List<IModule>();
            this.energyModules = new Dictionary<int, IEnergyModule>();
            this.absorbingModules = new Dictionary<int, IAbsorbingModule>();
        }

        public long TotalEnergyOutput => this.energyModules.Values.Sum(m => m.EnergyOutput);

        public long TotalHeatAbsorbing => this.absorbingModules.Values.Sum(m => m.HeatAbsorbing);

        public IReadOnlyCollection<IModule> ModulesByInput => this.modulesByInput.ToList().AsReadOnly();

        public void AddEnergyModule(IEnergyModule energyModule)
        {
            if (energyModule == null)
            {
                throw new ArgumentException(string.Format(Constants.ModuleNullErrorMessage));
            }

            if (this.ModulesByInput.Count == this.moduleCapacity)
            {
                this.RemoveOldestModule();
            }

            this.energyModules.Add(energyModule.Id, energyModule);
            this.modulesByInput.Add(energyModule);
        }

        public void AddAbsorbingModule(IAbsorbingModule absorbingModule)
        {
            if (absorbingModule == null)
            {
                throw new ArgumentException(string.Format(Constants.ModuleNullErrorMessage));
            }

            if (this.ModulesByInput.Count == this.moduleCapacity)
            {
                this.RemoveOldestModule();
            }

            this.absorbingModules.Add(absorbingModule.Id, absorbingModule);
            this.modulesByInput.Add(absorbingModule);
        }

        private void RemoveOldestModule()
        {
            int removeId = this.modulesByInput[0].Id;

            this.modulesByInput.RemoveAt(0);

            if (this.energyModules.ContainsKey(removeId))
            {
                this.energyModules.Remove(removeId);
            }

            else if (this.absorbingModules.ContainsKey(removeId))
            {
                this.absorbingModules.Remove(removeId);
            }
        }
    }
}