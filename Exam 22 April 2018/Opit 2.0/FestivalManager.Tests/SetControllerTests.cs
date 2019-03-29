// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to your project (entities/controllers/etc)

namespace FestivalManager.Tests
{
    using FestivalManager.Entities.Contracts;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Reflection;

    [TestFixture]
    public class SetControllerTests
    {

        [Test]
        public void CheckConstructor()
        {
       Type setControllerType= GetType("SetController");
            var stageType = GetType("Stage");
            IStage stage = (IStage)Activator.CreateInstance(stageType);
            var setControllerInstane = Activator.CreateInstance(setControllerType, new object[] { stage });
            Assert.NotNull(setControllerInstane, "Instance not created at all");
            var innerstage = setControllerType.GetField("stage", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(setControllerInstane);
            Assert.AreSame(stage, innerstage, "Stage not set!");
        }

        [Test]
        public void CheckPerformSetsExistsAndCorrectInputOutputTypes()
        {
            Type setControllerType = GetType("SetController");
            MethodInfo PerformSetMethod = setControllerType.GetMethod("PerformSets");
            Assert.AreEqual(PerformSetMethod.ReturnType, typeof(string), "Method does not return string");
            Assert.IsEmpty(PerformSetMethod.GetParameters(), "Method should Accept no parameters");
        }

        [Test]
        public void CheckPerformSetsReturnMessageNotSongs()
        {
            Type setControllerType = GetType("SetController");
            var stageType = GetType("Stage");
            IStage stage = (IStage)Activator.CreateInstance(stageType);

            Type typeSetLong = GetType("Long");
            var stageInstance1 = (ISet)Activator.CreateInstance(typeSetLong, new object[] { "stage" });
            stage.AddSet(stageInstance1);
            var stageInstance2 = (ISet)Activator.CreateInstance(typeSetLong, new object[] { "stage2" });
            stage.AddSet(stageInstance2);
            Type typeSetShort = GetType("Short");
            var stageInstance3 = (ISet)Activator.CreateInstance(typeSetShort, new object[] { "stage3" });
            stage.AddSet(stageInstance3);

            var setControllerInstane = Activator.CreateInstance(setControllerType, new object[] { stage });

            MethodInfo PerformSetMethod = setControllerType.GetMethod("PerformSets");
            string actualMessage = (string)PerformSetMethod.Invoke(setControllerInstane, new object[0]);
            string expectedMessage = "1. stage:\r\n-- Did not perform\r\n2. stage2:\r\n-- Did not perform\r\n3. stage3:\r\n-- Did not perform";
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [Test]
        public void CheckPerformSetsReturnSongPresent()
        {
            Type setControllerType = GetType("SetController");
            var stageType = GetType("Stage");
            IStage stage = (IStage)Activator.CreateInstance(stageType);

            Type typeSetLong = GetType("Long");
            var stageInstance1 = (ISet)Activator.CreateInstance(typeSetLong, new object[] { "stage" });
            stage.AddSet(stageInstance1);
            var stageInstance2 = (ISet)Activator.CreateInstance(typeSetLong, new object[] { "stage2" });
            stage.AddSet(stageInstance2);
            Type typeSetShort = GetType("Short");
            var stageInstance3 = (ISet)Activator.CreateInstance(typeSetShort, new object[] { "stage3" });
            stage.AddSet(stageInstance3);

            Type typeSetSong = GetType("Song");
            var songInstance = (ISong)Activator.CreateInstance(typeSetSong, new object[] { "myTestSong", new TimeSpan(0, 1, 20) });


            Type performerType = GetType("Performer");
            var performer = (IPerformer)Activator.CreateInstance(performerType, new object[] { "Jackson", (int)30 });

            Type instrumentType = GetType("Drums");
            var instrument = (IInstrument)Activator.CreateInstance(instrumentType);
            double wearActual222222 = instrument.Wear;
            performer.AddInstrument(instrument);

            stageInstance3.AddSong(songInstance);
            stageInstance3.AddPerformer(performer);

            var setControllerInstane = Activator.CreateInstance(setControllerType, new object[] { stage });

            MethodInfo PerformSetMethod = setControllerType.GetMethod("PerformSets");
            string actualMessage = (string)PerformSetMethod.Invoke(setControllerInstane, new object[0]);

            Assert.That(actualMessage.Contains("-- Set Successful"));

            double wearExpected = 80;
            double wearActual = instrument.Wear;
            Assert.AreEqual(wearExpected, wearActual,"Wearing of instrument is not occuring!");
        }

        private Type GetType(string className)
        {
            return typeof(FestivalManager.StartUp).Assembly.GetTypes().FirstOrDefault(x => x.Name == className);
        }
    }
}