using NUnit.Framework;

namespace Skeleton.Tests
{

    [TestFixture]
    public class DummyTests
    {
        private Dummy dummy;
        private const string name="Samuil";
        private const int baseExpirienceOfDummy=10;
        private const int baseHealthOfDummy=10;
        private  const int axeHitPoints= 10;
        private IWeapon weapon;
        
        [SetUp]
        public void SetUp()
        {
            weapon = new Axe(10,10);
        }


        //•	Dummy loses health if attacked
        [TestCase(10)]
        [TestCase(50)]
        [TestCase(5)]
        [TestCase(11)]
        public void DummyLosesHealthWhenAttacked(int initialDummyHealth)
        {

            dummy = new Dummy(initialDummyHealth, baseExpirienceOfDummy);
            Hero hero = new Hero(name, weapon);

            hero.Attack(dummy);

            Assert.That(dummy.Health == initialDummyHealth - axeHitPoints, $"Problem: Health is {dummy.Health}");
        }


        //•	Dead Dummy throws an exception if attacked
        [TestCase(0)]
        public void DeadDummyThrowsException(int initialDummyHealth)
        {
            dummy = new Dummy(initialDummyHealth, baseExpirienceOfDummy);
            Hero hero = new Hero(name, weapon);

            Assert.That(() => hero.Attack(dummy), Throws.InvalidOperationException.With.Message.EqualTo("Dummy is dead."),"Unexpected Error Type thrown or no error trown");
        }

        //•	Dead Dummy can give XP
        [TestCase(5, 5)]
        [TestCase(9, 8)]
        [TestCase(1, 77)]
        public void DeadDummycanGiveXp(int initialDummyHealth, int dummyExperience)
        {
            Dummy dummy = new Dummy(initialDummyHealth, dummyExperience);
            Hero hero = new Hero(name, weapon);

            int initialXp = hero.Experience;
            hero.Attack(dummy);
            int afterKillXp = hero.Experience;

            Assert.That(initialXp < afterKillXp,"Problem xp of hero does not increase after kill");
            Assert.That(initialXp + dummyExperience == afterKillXp, "Problem XP of hero does not increase after kill");
        }

        //•	Alive Dummy can't give XP
        [TestCase(15, 5)]
        [TestCase(11, 8)]

        public void AliveDummycantGiveXp(int initialDummyHealth, int dummyExperience)
        {
            Dummy tartget = new Dummy(initialDummyHealth, dummyExperience);
            Hero hero = new Hero(name, weapon);

            int initialXp = hero.Experience;
            hero.Attack(tartget);
            int afterKillXp = hero.Experience;

            Assert.That(initialXp == afterKillXp,"Problem XP of hero does increase with no kill");
        }
    }
}