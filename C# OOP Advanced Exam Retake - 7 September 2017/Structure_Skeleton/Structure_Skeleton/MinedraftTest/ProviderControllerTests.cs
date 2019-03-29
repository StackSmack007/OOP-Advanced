using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

public class ProviderControllerTests
{
    [Test]
    public void ConstructorInitialisesAndExists()
    {
        IEnergyRepository energyRepository = new EnergyRepository();
        IProviderController providerControler = new ProviderController(energyRepository);
        Assert.NotNull(providerControler, "ProviderController was not created properly");
        Assert.That(((ProviderController)providerControler).Entities.Count == 0, "Entities not set to zero");
    }

    [TestCase("Pressure 40 100")]
    [TestCase("Solar 80 100")]
    [TestCase("Standart 80 100")]
    public void RegisterRegistersProperly(string input)
    {
        IList<string> arguments = input.Split().ToList();

        IEnergyRepository energyRepository = new EnergyRepository();
        var providerControler = new ProviderController(energyRepository);
        string actualMessage = providerControler.Register(arguments);
        string expectedMessage = $"Successfully registered {arguments[0]}Provider";
        Assert.AreEqual(expectedMessage, actualMessage, "Output message differs from expected!");
        int addedEntities = providerControler.Entities.Count;
        Assert.AreEqual(1,addedEntities, "Provider is not added to pool!");
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}