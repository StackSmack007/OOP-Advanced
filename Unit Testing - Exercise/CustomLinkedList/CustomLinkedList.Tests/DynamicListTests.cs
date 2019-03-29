using CustomLinkedList;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        //•	Create Test Methods for all public members that need testing.

        //•	Create tests that ensure all methods, getters and setters work correctly (do not test auto-properties).
        //•	Make sure that the methods throw the correct exceptions in case a wrong input is entered.
        //•	Give meaningful assert messages for failed tests.
        private const int ExampValue = 66;

        [Test]
        public void ConstructorInitializes()
        {
            DynamicList<int> dl = new DynamicList<int>();
            int expectedCount = 0;
            int actualCount = dl.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }
        [Test]
        public void AddingIncreasesCount()
        {
            DynamicList<int> dl = new DynamicList<int>();
            dl.Add(1);
            int expectedCount = 1;
            int actualCount = dl.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestCase(1)]
        [TestCase(1, 2, 3, 4, 5, 1)]
        public void AddingIsAtLastIndex(params int[] numbers)
        {
            DynamicList<int> dl = new DynamicList<int>();
            for (int i = 0; i < numbers.Length; i++)
            {
                dl.Add(numbers[i]);
            }
            int index = numbers.Length - 1;
            int expectedValue = numbers[index];
            int actualValue = dl[index];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(1, 2, 3, 4, 5, 1)]
        [TestCase(7, 4, -4, 7, 1)]
        [TestCase(4, 4, -4, 7, 1)]

        public void IndexerGetWorksProperly(int numberToFind, params int[] numbersToAdd)
        {
            DynamicList<int> dl = new DynamicList<int>();
            for (int i = 0; i < numbersToAdd.Length; i++)
            {
                dl.Add(numbersToAdd[i]);
            }
            int index = Array.IndexOf(numbersToAdd, numberToFind);
            int expectedValue = numberToFind;
            int actualValue = dl[index];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(4, 4, -4, 7, 1)]
        [TestCase(-1, 4, -4, 7, 1)]
        public void IndexerThrowsInvalidException(int index, params int[] numbersToAdd)
        {
            DynamicList<int> dl = new DynamicList<int>();
            for (int i = 0; i < numbersToAdd.Length; i++)
            {
                dl.Add(numbersToAdd[i]);
            }
            int actualValue;
            Assert.Throws<ArgumentOutOfRangeException>(() => actualValue = dl[index]);
        }

        [TestCase(1, 77, 4, -4, 7, 1)]
        [TestCase(3, -3, 4, -4, 7, 1)]
        public void IndexerSetWorksProperly(int index, params int[] numbersToAdd)
        {
            DynamicList<int> dl = new DynamicList<int>();
            for (int i = 0; i < numbersToAdd.Length; i++)
            {
                dl.Add(numbersToAdd[i]);
            }

            dl[index] = ExampValue;

            Assert.AreEqual(ExampValue, dl[index]);
        }

        [TestCase(-1, 77, 4, -4, 7, 1)]
        [TestCase(5, -3, 4, -4, 7, 1)]
        public void IndexerSetThrowsArgumentOutOfRangeException(int index, params int[] numbersToAdd)
        {
            DynamicList<int> dl = new DynamicList<int>();
            for (int i = 0; i < numbersToAdd.Length; i++)
            {
                dl.Add(numbersToAdd[i]);
            }
            Assert.Throws<ArgumentOutOfRangeException>(() => dl[index] = ExampValue);
        }

        [TestCase(1, 1, 2, 3, 4, 5)]
        [TestCase(0, 1, 2, 3, 4, 5)]
        [TestCase(4, 1, 2, 3, 4, 5)]
        public void RemoveAtIndex(int index, params int[] numbersToAdd)
        {
            List<int> expectedList = numbersToAdd.ToList();
            expectedList.RemoveAt(index);
            DynamicList<int> dl = new DynamicList<int>();
            for (int i = 0; i < numbersToAdd.Length; i++)
            {
                dl.Add(numbersToAdd[i]);
            }
            dl.RemoveAt(index);
            bool IsSame = true;
            for (int i = 0; i < expectedList.Count; i++)
            {
                if (expectedList.Count != dl.Count || expectedList[i] != dl[i])
                {
                    IsSame = false;
                    break;
                }
            }
            Assert.True(IsSame);
        }

        [TestCase(-1, 1, 2, 3, 4, 5)]
        [TestCase(5, 1, 2, 3, 4, 5)]
        [TestCase(6, 1, 2, 3, 4, 5)]
        public void RemoveAtWrongIndexThrowsArgumentOutOfRangeException(int index, params int[] numbersToAdd)
        {
            DynamicList<int> dl = new DynamicList<int>();
            for (int i = 0; i < numbersToAdd.Length; i++)
            {
                dl.Add(numbersToAdd[i]);
            }
            Assert.Throws<ArgumentOutOfRangeException>(() => dl.RemoveAt(index));
        }


        [TestCase(1, 1, 2, 3, 4, 5)]
        [TestCase(3, 1, 2, 3, 4, 5)]
        [TestCase(5, 1, 2, 3, 4, 5)]
        [TestCase(1, 1, -2, 1, -2, 1)]
        [TestCase(-2, 1, -2, 1, -2, 1)]
        [TestCase(100, 1, -2, 1, -2, 1)]//Item not found no change return index is -1!
        public void RemoveItem(int item, params int[] numbersToAdd)
        {
            List<int> expectedList = numbersToAdd.ToList();
            int indexOfItemExpected = expectedList.IndexOf(item);
            expectedList.Remove(item);
            DynamicList<int> dl = new DynamicList<int>();
            for (int i = 0; i < numbersToAdd.Length; i++)
            {
                dl.Add(numbersToAdd[i]);
            }
            int indexOfItemActual = dl.Remove(item);
            bool IsSame = true;
            for (int i = 0; i < expectedList.Count; i++)
            {
                if (expectedList.Count != dl.Count || expectedList[i] != dl[i] || indexOfItemExpected != indexOfItemActual)
                {
                    IsSame = false;
                    break;
                }
            }
            Assert.True(IsSame);
        }

        [TestCase(1, 1, 2, 3, 4, 5)]
        [TestCase(3, 1, 2, 3, 4, 5)]
        [TestCase(5, 1, 2, 3, 4, 5)]
        public void IndexOf(int item, params int[] numbersToAdd)
        {
            DynamicList<int> dl = new DynamicList<int>();
            for (int i = 0; i < numbersToAdd.Length; i++)
            {
                dl.Add(numbersToAdd[i]);
            }
            int ExpectedIndexOfItem = Array.IndexOf(numbersToAdd,item);
            int ActualIndexOfItem = dl.IndexOf(item);
            Assert.AreEqual(ExpectedIndexOfItem, ActualIndexOfItem);
        }

        [TestCase(5, 1, 2, 3, 4, 5)]
        [TestCase(-122, 1, 2, 3, 4, 5)]
        public void ContainsItem(int item, params int[] numbersToAdd)
        {
            DynamicList<int> dl = new DynamicList<int>();
            for (int i = 0; i < numbersToAdd.Length; i++)
            {
                dl.Add(numbersToAdd[i]);
            }
            bool containsExpected = numbersToAdd.Any(x => x == item);
            bool containsActual = dl.Contains(item);
            Assert.AreEqual(containsExpected, containsActual);
        }


    }
}