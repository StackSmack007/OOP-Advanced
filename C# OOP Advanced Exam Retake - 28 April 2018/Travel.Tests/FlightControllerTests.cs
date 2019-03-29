//using Travel.Core.Controllers;
//using Travel.Entities;
//using Travel.Entities.Contracts;
//using Travel.Entities.Airplanes;
//using Travel.Entities.Airplanes.Contracts;
//using Travel.Entities.Items;
//using Travel.Entities.Items.Contracts;

namespace Travel.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class FlightControllerTests
    {
        [Test]
        public void CheckConstructorInitialiseClass()
        {
            IAirport airport = new Airport();
            FlightController fcTest = new FlightController(airport);
            Assert.IsNotNull(fcTest);
        }

        [Test]
        public void CheckTakeOffEmptyAirport()
        {
            IAirport airport = new Airport();
            FlightController fcTest = new FlightController(airport);
            string actualResult = fcTest.TakeOff();
            string expectedMessage = "Confiscated bags: 0 (0 items) => $0";
            Assert.AreEqual(expectedMessage, actualResult);
        }

        [TestCase("London", "Paris", 0)]
        [TestCase("Honkong", "Moscow", 2)]
        public void CheckTakeOffCompletesTrip(string source, string destination, int passagersCount)
        {
            IAirport airport = new Airport();

            FlightController fcTest = new FlightController(airport);

            IAirplane mediumAirplane = new MediumAirplane();
            IPassenger passenger = new Passenger("traveler11");

            for (int i = 0; i < passagersCount; i++)
            {
                mediumAirplane.AddPassenger(passenger);
            }

            ITrip trip = new Trip(source, destination, mediumAirplane);

            airport.AddTrip(trip);

            Assert.IsFalse(trip.IsCompleted, "Initially trip is set to completed instead of not completed!");

            string actualResultMessage = fcTest.TakeOff();

            string expectedMessage = $"{trip.Id}:\r\nSuccessfully transported {passagersCount} passengers from {source} to {destination}.\r\nConfiscated bags: 0 (0 items) => $0";

            Assert.AreEqual(expectedMessage, actualResultMessage);

            Assert.IsTrue(trip.IsCompleted, "TakeOff does not complete trip");

        }




        [TestCase("Honkong", "Moscow")]
        public void CheckTakeOffLoadsBagage(string source, string destination)
        {
            //Arrange
            IAirport airport = new Airport();

            FlightController fcTest = new FlightController(airport);

            IAirplane mediumAirplane = new MediumAirplane();
            //passager
            IPassenger passenger = new Passenger("traveler11");

            IItem cellFone = new CellPhone();

            IBag bag = new Bag(passenger, new List<IItem>() { cellFone, cellFone, cellFone, cellFone, });

            passenger.Bags.Add(bag);
            //  airport.AddPassenger(passenger);
            mediumAirplane.AddPassenger(passenger);

            //passager
            ITrip trip = new Trip(source, destination, mediumAirplane);

            airport.AddTrip(trip);
            //Act
            fcTest.TakeOff();
            //Assert
            //  int expectedPassengerBagsCount = 0;
            //  int actualPassengerBagsCount = passenger.Bags.Count;
            //  Assert.AreEqual(expectedPassengerBagsCount, actualPassengerBagsCount, "bags are not unloaded properly");

            int expectedPlaneBaggageCount = 1;
            int actualPlaneBaggageCount = mediumAirplane.BaggageCompartment.Count;
            Assert.AreEqual(expectedPlaneBaggageCount, actualPlaneBaggageCount, "bags are not loaded properly");
        }


        [TestCase(0)]
        [TestCase(3)]
        [TestCase(13)]
        public void CheckEjectingOverbookedPassagers(int passagersCount)
        {
            string source = "Honkong";
            string destination = "Tokio";

            //Arrange
            IAirport airport = new Airport();

            FlightController fcTest = new FlightController(airport);

            IAirplane lightAirplane = new LightAirplane();
            //passager
            IPassenger passenger = new Passenger("traveler11");

            for (int i = 0; i < passagersCount; i++)
            {
                lightAirplane.AddPassenger(passenger);
            }

            //passager
            ITrip trip = new Trip(source, destination, lightAirplane);

            airport.AddTrip(trip);
            //Act
            string outputmessage = fcTest.TakeOff();
            //Assert
            int expectedPassagers = Math.Min(lightAirplane.Passengers.Count, passagersCount);
            int actualPassagers = lightAirplane.Passengers.Count;


            Assert.AreEqual(expectedPassagers, actualPassagers, "passagers not properly thrown out");

            if (passagersCount > lightAirplane.Seats)
            {
                Assert.That(outputmessage.Contains($"Overbooked! Ejected traveler11"));
            }
            else
            {
                Assert.That(!outputmessage.Contains($"Overbooked! Ejected traveler11"));
            }

        }

        [TestCase("Honkong", "Moscow")]
        public void CheckTakeOffCompletedFlightsNotMatter(string source, string destination)
        {

            IAirport airport = new Airport();

            FlightController fcTest = new FlightController(airport);

            IAirplane mediumAirplane = new MediumAirplane();

            ITrip trip = new Trip(source, destination, mediumAirplane);

            airport.AddTrip(trip);
            trip.Complete();

            string actualResult = fcTest.TakeOff();
            string expectedMessage = "Confiscated bags: 0 (0 items) => $0";
            Assert.AreEqual(expectedMessage, actualResult);
        }

    }
}