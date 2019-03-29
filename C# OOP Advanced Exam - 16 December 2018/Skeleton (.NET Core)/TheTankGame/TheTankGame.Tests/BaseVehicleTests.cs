using TheTankGame.Entities.Miscellaneous;
using TheTankGame.Entities.Miscellaneous.Contracts;
using TheTankGame.Entities.Vehicles;
using TheTankGame.Entities.Parts;
using TheTankGame.Entities.Parts.Contracts;
using TheTankGame.Entities.Vehicles.Contracts;
namespace TheTankGame.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Reflection;

    [TestFixture]
    public class BaseVehicleTests
    {



        [Test]
        public void CheckExistingOfAbstractClassWithConstructor()
        {
            Type type = typeof(BaseVehicle);
            Assert.IsNotNull(type, "No class found with name BaseVehicle");
            Assert.IsTrue(type.IsAbstract, "The Vehicle Class is not abstract");
            Type[] expectedConstructorParameters = new Type[] { typeof(string), typeof(double), typeof(decimal), typeof(int), typeof(int), typeof(int), typeof(IAssembler) };

            ConstructorInfo constructor = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).First();
            Assert.IsNotNull(constructor, "No constructorFound");
            Assert.IsTrue(constructor.IsFamily, "Consturcor of abstract class is not private");
            Type[] actualConstructorParameters = constructor.GetParameters().Select(x => x.ParameterType).ToArray();
            Assert.IsTrue(expectedConstructorParameters.SequenceEqual(actualConstructorParameters), "Constructor does not receive correct parameter types");
        }

        [Test]
        public void CheckInitialisationCorrect()
        {

            string model = "Rhino-CE";
            double weight = 100.5;
            decimal price = 99.99m;
            int attack = 15;
            int defense = 20;
            int hitPoints = 60;
            IAssembler assembler = new VehicleAssembler();

            BaseVehicle testVehicle = new Revenger(model, weight, price, attack, defense, hitPoints, assembler);
            Assert.IsNotNull(testVehicle);
            Assert.That(testVehicle.Model == model);
            Assert.That(testVehicle.Weight == weight);
            Assert.That(testVehicle.Price == price);
            Assert.That(testVehicle.Attack == attack);
            Assert.That(testVehicle.Defense == defense);
            Assert.That(testVehicle.HitPoints == hitPoints);
            Assert.AreEqual(0, testVehicle.Parts.Count());

        }


        [TestCase("", 100.5d, 99.8, 15, 16, 17)]
        [TestCase(null, 100.5d, 99.8, 15, 16, 17)]
        [TestCase("Rhino-CE", 0, 99.8, 15, 16, 17)]
        [TestCase("Rhino-CE", -4, 99.8, 15, 16, 17)]
        [TestCase("Rhino-CE", 100.5d, 0, 15, 16, 17)]
        [TestCase("Rhino-CE", 100.5d, -4, 15, 16, 17)]
        [TestCase("Rhino-CE", 100.5d, 99.8, -4, 16, 17)]
        [TestCase("Rhino-CE", 100.5d, 99.8, 15, -4, 17)]
        [TestCase("Rhino-CE", 100.5d, 99.8, 15, 16, -4)]
        public void CheckInvalidExceptions(string model, double weight, decimal price, int attack, int defense, int hitPoints)
        {
            IAssembler assembler = new VehicleAssembler();
            BaseVehicle testVehicle;
            Assert.That
                (
               () => testVehicle = new Revenger(model, weight, price, attack, defense, hitPoints, assembler), Throws.ArgumentException
                );
        }

        [Test]
        public void CheckAddingItemsAndModifiengProperties()
        {

            string model = "Rhino-CE";
            double weight = 10;
            decimal price = 10m;
            int attack = 10;
            int defense = 10;
            int hitPoints = 10;
            IAssembler assembler = new VehicleAssembler();

            BaseVehicle testVehicle = new Revenger(model, weight, price, attack, defense, hitPoints, assembler);

            IPart arsenalPart = new ArsenalPart("shel1", 1, 1, 2);
            testVehicle.AddArsenalPart(arsenalPart);
            Assert.That(testVehicle.Parts.Count() == 1 && testVehicle.Parts.Contains(arsenalPart), "Arsenal part not added properly");

            IPart shellPart = new ShellPart("shel1", 1, 1, 2);
            testVehicle.AddShellPart(shellPart);
            Assert.That(testVehicle.Parts.Count() == 2 && testVehicle.Parts.Contains(shellPart), "Shell part not added properly");

            IPart endurancePart = new EndurancePart("shel1", 1, 1, 2);
            testVehicle.AddEndurancePart(endurancePart);
            Assert.That(testVehicle.Parts.Count() == 3 && testVehicle.Parts.Contains(endurancePart), "Endurance part not added properly");

            double TotalWeightExpected = 3 + 10;
            Assert.AreEqual(TotalWeightExpected, testVehicle.TotalWeight);

            decimal TotalPriceExpected = 3 + 10;
            Assert.AreEqual(TotalPriceExpected, testVehicle.TotalPrice);

            long TotalAttackExpected = 10 + 2;
            Assert.AreEqual(TotalAttackExpected, testVehicle.TotalAttack);

            long TotalDefenseExpected = 10 + 2;
            Assert.AreEqual(TotalDefenseExpected, testVehicle.TotalDefense);

            long TotalHitPointsExpected = 10 + 2;
            Assert.AreEqual(TotalHitPointsExpected, testVehicle.TotalHitPoints);
        }

        [Test]
        public void CheckOverrideToString()
        {
            string model = "Rhino-CE";
            double weight = 12;
            decimal price = 19;
            int attack = 80;
            int defense = 11;
            int hitPoints = 3;
            IAssembler assembler = new VehicleAssembler();

            BaseVehicle testVehicle = new Revenger(model, weight, price, attack, defense, hitPoints, assembler);

            IPart arsenalPart = new ArsenalPart("arse1", 1, 1, 2);
            testVehicle.AddArsenalPart(arsenalPart);


            IPart shellPart = new ShellPart("hel1", 6, 1, 8);
            testVehicle.AddShellPart(shellPart);


            IPart endurancePart = new EndurancePart("end1", 1, 3, 2);
            testVehicle.AddEndurancePart(endurancePart);

            string expectedMessage = "Revenger - Rhino-CE\r\nTotal Weight: 20.000\r\nTotal Price: 24.000\r\nAttack: 82\r\nDefense: 19\r\nHitPoints: 5\r\nParts: arse1, hel1, end1";

            string actualMessage = testVehicle.ToString();
            Assert.AreEqual(expectedMessage, actualMessage);
        }
    }
}