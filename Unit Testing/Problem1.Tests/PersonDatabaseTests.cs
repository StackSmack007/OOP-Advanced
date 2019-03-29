
namespace Tests
{
    using NUnit.Framework;
    using Problem_2_Extended_Database;
    using System;
    using System.Linq;
    using System.Reflection;
    [TestFixture]
    public class PersonDatabaseTests
    {
        string initialName = "Genadi";
        long initialId = 12;
        IPerson person;
        [SetUp]
        public void SetUp()
        {
            person = new Person(initialName, initialId);
        }


        [Test]
        public void TestConstructorAdding()
        {
            Database dbP = new Database(person);
            int counterExpected = 1;
            int counterActual = (int)(typeof(Database).GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                                                      .First(x => x.Name == "count")
                                                      .GetValue(dbP));
            Assert.That(counterExpected == counterActual);
        }

        [Test]
        public void TestConstructorThrowInvalidOperationException()
        {
            IPerson[] personsTemp = new IPerson[17];

            Database dbP;

            Assert.That(() => dbP = new Database(personsTemp), Throws.InvalidOperationException.With.Message.EqualTo("Elements more than 16!"));
        }

        [TestCase("asd", 234)]
        [TestCase("asDa", 2346)]
        [TestCase("Genadi", 12)]
        public void TestFindUserByUserNameSuccessfully(string name, long id)
        {
            IPerson person1 = new Person("asd", 234);
            IPerson person2 = new Person("asDa", 2346);
            Database db = new Database(person1, person, person2);

            var foundPersonID = db.FindByUserName(name).Id;
            Assert.AreEqual(id, foundPersonID);
        }

        [TestCase("Asd")]
        [TestCase("name not present")]
        public void TestFindUserByUserNameThrowsInvalidOperationException(string name)
        {
            IPerson person1 = new Person("asd", 234);
            IPerson person2 = new Person("asDa", 2346);
            Database db = new Database(person1, person, person2);

            long foundPersonID;
            Assert.That(() => foundPersonID = db.FindByUserName(name).Id, Throws.InvalidOperationException);
        }

        [Test]
        public void TestFindUserByUserNameThrowsArgumentNullException()
        {
            IPerson person1 = new Person("asd", 234);
            IPerson person2 = new Person("asDa", 2346);
            Database db = new Database(person1, person, person2);
            long foundPersonID;
            Assert.That(() => foundPersonID = db.FindByUserName(null).Id, Throws.ArgumentNullException);
        }

        [TestCase("asd", 234)]
        [TestCase("asDa", 2346)]
        [TestCase("Genadi", 12)]
        public void TestFindUserById(string name, long id)
        {
            IPerson person1 = new Person("asd", 234);
            IPerson person2 = new Person("asDa", 2346);
            Database db = new Database(person1, person, person2);

            string foundPersonName = db.FindByUserId(id).Name;
            Assert.AreEqual(name, foundPersonName);
        }

        [Test]
        public void TestFindUserByIdThrowsArgumentOutOfRangeException()
        {
            IPerson person1 = new Person("asd", 234);
            IPerson person2 = new Person("asDa", 2346);
            Database db = new Database(person1, person, person2);
            bool CorrectError = false;
            try
            {
                IPerson foundPersonName = db.FindByUserId(-1);
            }
            catch (ArgumentOutOfRangeException)
            {
                CorrectError = true;
            }
            Assert.IsTrue(CorrectError);
        }




    }
}