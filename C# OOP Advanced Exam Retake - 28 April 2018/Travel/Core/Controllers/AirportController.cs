namespace Travel.Core.Controllers
{
    using Contracts;
    using Entities;
    using Entities.Contracts;
    using Entities.Factories;
    using Entities.Factories.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Travel.Entities.Airplanes.Contracts;

    public class AirportController : IAirportController
	{
		private const int bagValueMaxLimit = 3000;
        private int tripRegistrationNumber;
        private IAirport airport;

		private IAirplaneFactory airplaneFactory;
		private IItemFactory itemFactory;

		public AirportController(IAirport airport)
		{
            tripRegistrationNumber = 1;
            this.airport = airport;
			this.airplaneFactory = new AirplaneFactory();
			this.itemFactory = new ItemFactory();
		}

		public string RegisterPassenger(string username)
		{
			if (this.airport.GetPassenger(username) != null)
			{
				throw new InvalidOperationException($"Passenger {username} already registered!");
			}

			IPassenger passenger = new Passenger(username);

			this.airport.AddPassenger(passenger);

			return $"Registered {username}";
		}

        public string RegisterTrip(string source, string destination, string planeType)
		{
			IAirplane airplane =airplaneFactory.CreateAirplane(planeType);

            
			var trip = new Trip(source, destination,airplane);
            trip.IdNumber = tripRegistrationNumber++;
			this.airport.AddTrip(trip);

			return $"Registered trip {trip.Id}";
		}

        public string RegisterBag(string username, IEnumerable<string> bagItems)
		{
			var passenger = this.airport.GetPassenger(username);

			var items = bagItems.Select(i => itemFactory.CreateItem(i)).ToArray();
			var bag = new Bag(passenger, items);

			passenger.Bags.Add(bag);

			return $"Registered bag with {string.Join(", ", bagItems)} for {username}";
		}

		public string CheckIn(string username, string tripId, IEnumerable<int> bagIndexes)
		{
			IPassenger passenger = this.airport.GetPassenger(username);
			ITrip trip = airport.GetTrip(tripId);

            bool IsCheckedIn = airport.Trips.SelectMany(x => x.Airplane.Passengers).Contains(passenger); 

			if (IsCheckedIn)
			{
				throw new InvalidOperationException ($"{username} is already checked in!");
			}

			var confiscatedBags = CheckInBags(passenger, bagIndexes);

            trip.Airplane.AddPassenger(passenger);

			return
				$"Checked in {passenger.Username} with {bagIndexes.Count() - confiscatedBags}/{bagIndexes.Count()} checked in bags";
		}


		private int CheckInBags(IPassenger passenger, IEnumerable<int> indexesOfBagsToCheckIn)
		{
			IList<IBag> bags = passenger.Bags;

			int confiscatedBagCount = 0;

			foreach (var index in indexesOfBagsToCheckIn)
			{
				IBag currentBag = bags[index];
				bags.RemoveAt(index);

				if (ShouldConfiscate(currentBag))
				{
					airport.AddConfiscatedBag(currentBag);
					confiscatedBagCount++;
				}
				else
				{
					this.airport.AddCheckedBag(currentBag);
				}
			}

			return confiscatedBagCount;
		}

		private static bool ShouldConfiscate(IBag bag)
		{
			var bagValue = bag.Items.Sum(x=>x.Value);
            return bagValue > bagValueMaxLimit;
		}
	}
}