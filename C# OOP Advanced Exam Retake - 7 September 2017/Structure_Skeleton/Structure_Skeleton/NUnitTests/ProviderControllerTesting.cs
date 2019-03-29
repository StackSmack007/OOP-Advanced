using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

public class Tests
{
    EnergyRepository energyRepository;
    private const int durabilityDecreaseStep = 100;
    [SetUp]
    public void Setup()
    {
        energyRepository = new EnergyRepository();
    }

    [Test]
    public void ConstructorInitialisesAndExists()
    {
        ProviderController providerControler = new ProviderController(energyRepository);
        Assert.NotNull(providerControler, "ProviderController was not created properly");
        Assert.That(((ProviderController)providerControler).Entities.Count == 0, "Entities not set to zero");
    }

    [TestCase("Pressure 40 100")]
    [TestCase("Solar 80 100")]
    [TestCase("Standart 80 100")]
    public void RegisterRegistersProperly(string input)
    {
        List<string> arguments = input.Split().ToList();

        var providerControler = new ProviderController(energyRepository);
        string actualMessage = providerControler.Register(arguments);
        string expectedMessage = $"Successfully registered {arguments[0]}Provider";
        Assert.AreEqual(expectedMessage, actualMessage, "Register message differs from expected!");
        int addedEntities = providerControler.Entities.Count;
        Assert.AreEqual(1, addedEntities, "Provider is not added to pool!");
    }

    [TestCase("Pressure 40 100")]
    [TestCase("Solar 80 100")]
    [TestCase("Standart 80 100")]
    public void ProducesGivesProperMessageAndDecreasesDurability(string input)
    {
        List<string> arguments = input.Split().ToList();

        double energyOutput = double.Parse(arguments[2]);
        if (arguments[0] == "Pressure") energyOutput *= 2;

        var providerControler = new ProviderController(energyRepository);
        providerControler.Register(arguments);
        var initialDurability = providerControler.Entities.First().Durability;

        string actualMessage = providerControler.Produce();
        var durabilityAfterProduce = providerControler.Entities.First().Durability;
        Assert.That(initialDurability - durabilityDecreaseStep == durabilityAfterProduce, "Durability Does Not Decrease after 1 day!");
        string expectedMessage = $"Produced {energyOutput} energy today!";

        Assert.AreEqual(expectedMessage, actualMessage, "Output message differs from expected!");
        int addedEntities = providerControler.Entities.Count;


    }

    [TestCase("Pressure 40 100")]
    [TestCase("Solar 80 100")]
    [TestCase("Standart 80 100")]
    public void ProducesRemovesBrokenProvider(string input)
    {
        List<string> arguments = input.Split().ToList();
        double energyOutput = double.Parse(arguments[2]);
        if (arguments[0] == "Pressure") energyOutput *= 2;
        var providerControler = new ProviderController(energyRepository);
        providerControler.Register(arguments);

        var provider = providerControler.Entities.First();
        while (provider.Durability > 100)
        {
            provider.Broke();
        }
        string actualMessage = providerControler.Produce();
        int expectedCountOfProviders = 0;
        int actualCountOfProviders = providerControler.Entities.Count;
        string expectedMessage = $"Produced {energyOutput} energy today!";
        Assert.AreEqual(expectedMessage, actualMessage, "Output message differs from expected!");
        Assert.AreEqual(expectedCountOfProviders, actualCountOfProviders, "Broken Provider is not removed");
    }

    [TestCase("Pressure 40 100", 30.4)]
    [TestCase("Solar 80 100", 50)]
    [TestCase("Standart 80 100", 10)]
    public void RepairGetsProperMessage(string input, double repairAmmount)
    {
        List<string> arguments = input.Split().ToList();

        var providerControler = new ProviderController(energyRepository);
        providerControler.Register(arguments);
        var provider = providerControler.Entities.First();
        var providerDurabilityBeforeRepair = provider.Durability;
        string actualMessage = providerControler.Repair(repairAmmount);
        var providerDurabilityAfterRepair = provider.Durability;
        string expectedMessage = $"Providers are repaired by {repairAmmount}";
        Assert.AreEqual(expectedMessage, actualMessage, "Repair message is not correct!");
        Assert.AreEqual(providerDurabilityBeforeRepair + repairAmmount, providerDurabilityAfterRepair, "Durability does not increase properly after repair!");
    }

    [TestCase("Pressure 40 100", 40)]
    [TestCase("Solar 80 100", 10)]
    [TestCase("Standart 80 100", 10)]
    public void GetById(string input, int idTofind)
    {
        List<string> arguments = input.Split().ToList();
        int id = int.Parse(arguments[1]);
        var providerControler = new ProviderController(energyRepository);
        providerControler.Register(arguments);
        var providerFound = providerControler.GetById(idTofind);

        if (id == idTofind)
        {
            Assert.IsNotNull(providerFound);
        }
        else
        {
            Assert.IsNull(providerFound);
        }
    }
}