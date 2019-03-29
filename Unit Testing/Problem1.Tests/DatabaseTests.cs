using NUnit.Framework;
using Problem_1._Database;
using System;
using System.Linq;
using System.Reflection;

namespace Tests
{
    [TestFixture]
    public class DatabaseTests
    {
        private Database<int> db;
        private int initialCountOfDatabase;
        private int[] innerArray;
        private FieldInfo[] fields;
        [SetUp]
        public void Setup()
        {
            db = new Database<int>(1, 2, 4, 6, -4, 7, -10, 9, 100, 52, -37, 1, -2, 3);
            //14 elements
            Type type = typeof(Database<int>);
           fields = type.GetFields(BindingFlags.Instance|BindingFlags.NonPublic);
            var field = fields.FirstOrDefault(x => x.Name=="innerBox");
            innerArray = (int[])field.GetValue(db);
            initialCountOfDatabase = (int)fields.FirstOrDefault(x => x.Name == "count").GetValue(db);
        }

        [TestCase(1)]
        [TestCase(-777)]
        [TestCase(2, -2)]
        public void AddTest(params int[] numbers)
        {
            foreach (var number in numbers)
            {
                db.Add(number);
            }
            FieldInfo innerCount = fields.FirstOrDefault(x => x.Name == "count");
            int afterAddingCountOfDatabase = (int)innerCount.GetValue(db);

            Assert.That(initialCountOfDatabase < afterAddingCountOfDatabase);
            Assert.That(initialCountOfDatabase + numbers.Length == afterAddingCountOfDatabase);

            int lastNumberAddedExpected = numbers.Last();
            int lastNumberAddedActual = db.Fetch().Last();
            Assert.AreEqual(lastNumberAddedExpected, lastNumberAddedActual);
        }

        [TestCase(2, -2, 3)]
        [TestCase(1, 1, 1)]
        public void AddFullDatabaseTest(params int[] numbers)
        {
            Assert.That(() =>
            {
                foreach (var number in numbers)
                {
                    db.Add(number);
                }
            }, Throws.InvalidOperationException.With.Message.EqualTo("No more space in database!"));
        }

        [TestCase(3)]
        [TestCase(1)]
        [TestCase(20)]
        public void RemoveElements(int count)
        {

            for (int i = 1; i <= count; i++)
            {
                if (initialCountOfDatabase - i < 0)
                {
                    Assert.That(() => db.Remove(), Throws.InvalidOperationException.With.Message.EqualTo("No elements to remove!"));
                    return;
                }
                db.Remove();
            }
            var elementCountInDb = (int)fields.FirstOrDefault(x => x.Name == "count").GetValue(db);

            Assert.That(elementCountInDb == initialCountOfDatabase - count);
        }


        [TestCase(1, 2, 4, 6, -4, 7, -10, 9, 100, 52, -37, 1, -2, 3, 1, 1)]
        [TestCase(1, -2, 3, 1, 1)]
        [TestCase()]
        public void TestConstructorInitInnerArray(params int[] numbers)
        {
            var type = typeof(Database<int>);

            FieldInfo fieldArr = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(x => x.Name.StartsWith("inner"));
         FieldInfo fieldCount= type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(x => x.Name.StartsWith("count"));

            var database2 = new Database<int>(numbers);
            int[] innerArray = (int[])fieldArr.GetValue(database2);
            int innerCount = (int)fieldCount.GetValue(database2);




            Assert.IsTrue(Enumerable.SequenceEqual(innerArray.Take(innerCount),numbers));
        }

        [TestCase(1, 2, 4, 6, -4, 7, -10, 9, 100, 52, -37, 1, -2, 3, 1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 1, 1, 1, 1, 1, 1, 2)]
        public void TestConstructorMoreThanCappacity(params int[] numbers)
        {
            Assert.That(() => new Database<int>(numbers), Throws.InvalidOperationException.With.Message.EqualTo("Elements more than 16!"));
        }

        [TestCase(1, 2, 4, 6, -4)]
        [TestCase(4, 5, 1)]
        [TestCase(11, 22, 33, 446, -3)]
        [TestCase()]
        public void TestFetch(params int[] numbers)
        {
            Database<int> database1 = new Database<int>(numbers);

            int[] fetchResult = database1.Fetch();
            bool IsSame = true;
            for (int i = 0; i < fetchResult.Length; i++)
            {
                if (fetchResult[i] != numbers[i] || fetchResult.Length != numbers.Length)
                {
                    IsSame = false;
                    break;
                }
            }
            Assert.IsTrue(IsSame);
        }

    }
}