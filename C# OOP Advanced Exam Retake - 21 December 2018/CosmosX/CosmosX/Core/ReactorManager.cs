namespace CosmosX.Core
{
    using Core.Contracts;
    using CosmosX.Entities.Modules.Absorbing.Contracts;
    using CosmosX.Entities.Modules.Factories;
    using CosmosX.Entities.Modules.Factories.Contracts;
    using CosmosX.Entities.Reactors;
    using CosmosX.Entities.Reactors.ReactorFactory;
    using CosmosX.Entities.Reactors.ReactorFactory.Contracts;
    using Entities.Containers;
    using Entities.Containers.Contracts;
    using Entities.Modules.Contracts;
    using Entities.Modules.Energy.Contracts;
    using Entities.Reactors.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utils;

    public class ReactorManager : IManager
    {
        private int currentId;
        private readonly IDictionary<int, IReactor> reactors;
        private readonly IDictionary<int, IModule> modules;
        private IReactorFactory reactorFactory;
        private IModuleFactory moduleFactory;
        public ReactorManager()
        {
            moduleFactory = new ModuleFactory();
            reactorFactory = new ReactorFactory();
            this.currentId = Constants.StartingId;
            this.reactors = new Dictionary<int, IReactor>();
            this.modules = new Dictionary<int, IModule>();
        }

        public string ReactorCommand(IList<string> arguments)
        {
            string reactorType = arguments[0];
            int additionalParameter = int.Parse(arguments[1]);
            int moduleCapacity = int.Parse(arguments[2]);

            IContainer container = new ModuleContainer(moduleCapacity);

            IReactor reactor = reactorFactory.CreateReactor(reactorType, currentId++, container, additionalParameter);

            this.reactors.Add(reactor.Id, reactor);


            string result = string.Format(Constants.ReactorCreateMessage, reactor.Id, reactorType);
            return result;
        }

        public string ModuleCommand(IList<string> arguments)
        {
            int reactorId = int.Parse(arguments[0]);
            string moduleType = arguments[1];
            int additionalParameter = int.Parse(arguments[2]);

            IModule module = moduleFactory.CreateModule(moduleType, currentId, additionalParameter);

            if (module is IEnergyModule)
            {
                reactors[reactorId].AddEnergyModule((IEnergyModule)module);
            }
            else if (module is IAbsorbingModule)
            {
                reactors[reactorId].AddAbsorbingModule((IAbsorbingModule)module);
            }

            this.modules.Add(module.Id, module);

            string result = string.Format(Constants.ModuleCreateMessage, moduleType, this.currentId++, reactorId);
            return result;
        }
        
        public string ReportCommand(IList<string> arguments)
        {
            int id = int.Parse(arguments[0]);

            var reactorFound = reactors.Values.FirstOrDefault(x => x.Id == id);
            var moduleFound = modules.Values.FirstOrDefault((x => x.Id == id));
            if (reactorFound is null && moduleFound is null)
            {
                throw new ArgumentException(string.Format(Constants.IdNotFound, id));
            }

            return reactorFound is null ? moduleFound.ToString() : reactorFound.ToString();
        }

        public string ExitCommand(IList<string> arguments)
        {
            int cryoReactorCount = this.reactors
                .Values
                .Count(r => r is CryoReactor);

            int heatReactorCount = this.reactors
                .Values
                 .Count(r => r is HeatReactor);

            int energyModulesCount = this.modules
                .Values
              .Count(r => r is IEnergyModule);

            int absorbingModulesCount = this.modules
                .Values
                 .Count(r => r is IAbsorbingModule);

            long totalEnergyOutput = this.reactors
                .Values
                .Sum(r => r.TotalEnergyOutput);

            long totalHeatAbsorbing = this.reactors
                .Values
                .Sum(r => r.TotalHeatAbsorbing);

            string result = $"Cryo Reactors: {cryoReactorCount}\n" +
                            $"Heat Reactors: {heatReactorCount}\n" +
                            $"Energy Modules: {energyModulesCount}\n" +
                            $"Absorbing Modules: {absorbingModulesCount}\n" +
                            $"Total Energy Output: {totalEnergyOutput}\n" +
                            $"Total Heat Absorbing: {totalHeatAbsorbing}";
            return result;
        }
    }
}