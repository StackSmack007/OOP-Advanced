//using CosmosX.Entities.Containers;
//using CosmosX.Entities.Containers.Contracts;
//using CosmosX.Entities.Modules.Absorbing;
//using CosmosX.Entities.Modules.Absorbing.Contracts;
//using CosmosX.Entities.Modules.Energy;
//using CosmosX.Entities.Modules.Energy.Contracts;
namespace CosmosX.Tests
{
    using NUnit.Framework;
    using System.Linq;

    [TestFixture]
    public class ModuleContainerTests
    {
        [Test]
        public void CheckInitialisation()
        {
            IContainer container = new ModuleContainer(10);
            Assert.IsNotNull(container, "Container not instantioned properly");
            long expectedTotalEnergyOutput = 0;
            long expectedTotalHeatAbsorbing = 0;
            long ModulesByInputCount = 0;
            Assert.AreEqual(expectedTotalEnergyOutput, container.TotalEnergyOutput, "Energy not set to 0 initially");
            Assert.AreEqual(expectedTotalHeatAbsorbing, container.TotalHeatAbsorbing, "HeatAbsorbing not set to 0 initially");
            Assert.AreEqual(ModulesByInputCount, container.ModulesByInput.Count, "Modules By input not instantioned properly");
        }

        [Test]
        public void CheckNullModuleAddThrowsArgumentException()
        {
            IContainer container = new ModuleContainer(10);
            Assert.That(() => container.AddEnergyModule(null), Throws.ArgumentException);
            Assert.That(() => container.AddAbsorbingModule(null), Throws.ArgumentException);
        }

        [Test]
        public void CheckAddEnergyModule()
        {
            int cappacityOfContainer = 1;
            int moduleId = 1;
            int moduleAdditionalParameter = 10;
            IContainer container = new ModuleContainer(cappacityOfContainer);
            IEnergyModule module0 = new CryogenRod(moduleId, moduleAdditionalParameter);
            container.AddEnergyModule(module0);
            int expectedModuleCount = 1;
            int actuallModuleCount = container.ModulesByInput.Count;
            Assert.AreEqual(expectedModuleCount, actuallModuleCount, "Module is not added count not increased");
        }


        [Test]
        public void CheckTotalEnergyAndAbsorbingOutput()
        {
            int cappacityOfContainer = 3;
            int moduleId = 1;
            int moduleAdditionalParameter = 15;
            IContainer container = new ModuleContainer(cappacityOfContainer);

            container.AddEnergyModule(new CryogenRod(moduleId++, moduleAdditionalParameter));
            container.AddEnergyModule(new CryogenRod(moduleId++, moduleAdditionalParameter));
            container.AddAbsorbingModule(new HeatProcessor(moduleId++, moduleAdditionalParameter));
            int expectedTotalEnergy = 30;
            Assert.AreEqual(expectedTotalEnergy, container.TotalEnergyOutput);

            int expectedTotalAbsorbing = 15;
            Assert.AreEqual(expectedTotalAbsorbing, container.TotalHeatAbsorbing);
        }

        [Test]
        public void CheckAddAbsorbingModule()
        {
            int cappacityOfContainer = 1;
            int moduleId = 1;
            int moduleAdditionalParameter = 10;
            IContainer container = new ModuleContainer(cappacityOfContainer);
            IAbsorbingModule module0 = new HeatProcessor(moduleId, moduleAdditionalParameter);
            container.AddAbsorbingModule(module0);
            int expectedModuleCount = 1;
            int actuallModuleCount = container.ModulesByInput.Count;
            Assert.AreEqual(expectedModuleCount, actuallModuleCount, "Module is not added count not increased");
        }

        [Test]
        public void CheckAddAfterReachingCappacity()
        {
            int cappacityOfContainer = 2;
            int moduleId = 1;
            int moduleAdditionalParameter = 10;
            IContainer container = new ModuleContainer(cappacityOfContainer);
            IEnergyModule module0 = new CryogenRod(moduleId, moduleAdditionalParameter);
            container.AddEnergyModule(module0);
            IAbsorbingModule module1 = new HeatProcessor(moduleId, moduleAdditionalParameter);
            container.AddAbsorbingModule(module1);
            int expectedModuleCount = 2;
            int actuallModuleCount = container.ModulesByInput.Count;
            Assert.AreEqual(expectedModuleCount, actuallModuleCount, "Module is not added count not increased");
            moduleId++;
            container.AddAbsorbingModule(new HeatProcessor(moduleId, moduleAdditionalParameter));
            actuallModuleCount = container.ModulesByInput.Count;
            Assert.AreEqual(expectedModuleCount, actuallModuleCount, "Module is not added count not increased");
            Assert.That(container.ModulesByInput.Where(x => x is IEnergyModule).Count() == 0, "Container does not remove uppon overflow the first to enter");

            container.AddEnergyModule(new CryogenRod(moduleId++, moduleAdditionalParameter));
            container.AddEnergyModule(new CryogenRod(moduleId, moduleAdditionalParameter));
            Assert.That(container.ModulesByInput.Where(x => x is IAbsorbingModule).Count() == 0, "Container does not remove uppon overflow the first to enter");
        }
    }
}