using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class AxeTests
    {
        [SetUp]
        public void Setup() { }

        // •Test if weapon loses durability after each attack
        [Test]
        public void WeaponLosesDurabilityAfterEachAttack()
        {
            Axe axe = new Axe(10, 20);

            Dummy target = new Dummy(20, 10);
            axe.Attack(target);

            Assert.DoesNotThrow(() => axe.Attack(target));
            var expectedResult = 18;
            var actualResult = axe.DurabilityPoints;

            Assert.AreEqual(expectedResult, actualResult);
        }
        //Test attacking with a broken weapon
        [Test]
        public void BrokenWeaponAttack()
        {
            Axe axe = new Axe(10, 1);

            Dummy target = new Dummy(20, 10);
            axe.Attack(target);
            var expectedResult = 0;
            var actualResult = axe.DurabilityPoints;
            Assert.AreEqual(expectedResult, actualResult);
            Assert.Throws(typeof(InvalidOperationException), () => axe.Attack(target));
            Assert.Throws<InvalidOperationException>(() => axe.Attack(target));
            Assert.That(() => axe.Attack(target), Throws.InvalidOperationException.With.Message.EqualTo("Axe is broken."));
        }

    }
}