using Moq;
using NUnit.Framework;

namespace Skeleton.Tests
{
    [TestFixture]
    public class HeroTests
    {
        private const string Name = "Kuncho";
        [Test]
        public void HeroGainingExpiriance()
        {


            ITarget fakeTarget = new FakeTarget();
            IWeapon fakeWeapon = new FakeWeapon();
            Hero hero = new Hero(Name, fakeWeapon);
            int initialHeroEXP = hero.Experience;

            hero.Attack(fakeTarget);

            //1990 is the fake exp the dummy will give to Hero and it will always return Isdead=true!
            Assert.That(initialHeroEXP + 1990 == hero.Experience);
        }
        [Test]
        public void HeroGainingExpirianceMockery()
        {

            Mock<ITarget> fakeTarget = new Mock<ITarget>();
            Mock<IWeapon> fakeWeapon = new Mock<IWeapon>();

            fakeTarget.Setup(x => x.IsDead()).Returns(true);
            fakeTarget.Setup(x => x.GiveExperience()).Returns(2);
            //2 is the fake exp the dummy will give to Hero and it will always return Isdead=true!

            Hero hero = new Hero(Name, fakeWeapon.Object);
            int initialHeroEXP = hero.Experience;


            hero.Attack(fakeTarget.Object);
            Assert.That(initialHeroEXP + 2 == hero.Experience);
        }
    }
}