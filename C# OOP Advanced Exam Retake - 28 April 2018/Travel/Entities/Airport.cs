namespace Travel.Entities
{
    using Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class Airport:IAirport
	{
		private List<IBag> checkedBags;
		private List<IBag> confiscatedBags;
		private List<ITrip> trips;
		private List<IPassenger> passengers;

        public Airport()
        {
            checkedBags = new List<IBag>();
            confiscatedBags = new List<IBag>();
            trips = new List<ITrip>();
            passengers = new List<IPassenger>();
        }

        public IReadOnlyCollection<IBag> CheckedInBags => checkedBags.AsReadOnly();

        public IReadOnlyCollection<IBag> ConfiscatedBags => confiscatedBags.AsReadOnly();

        public IReadOnlyCollection<IPassenger> Passengers => passengers.AsReadOnly();

        public IReadOnlyCollection<ITrip> Trips => trips.AsReadOnly();

        public void AddCheckedBag(IBag bag)
        {
            checkedBags.Add(bag);
        }

        public void AddConfiscatedBag(IBag bag)
        {
            confiscatedBags.Add(bag);
        }

        public void AddPassenger(IPassenger passenger)
        {
            passengers.Add(passenger);
        }

        public void AddTrip(ITrip trip)
        {
            trips.Add(trip);
        }

        public IPassenger GetPassenger(string username)
        {
            return Passengers.FirstOrDefault(x => x.Username == username);
        }

        public ITrip GetTrip(string id)
        {
            return Trips.FirstOrDefault(x => x.Id == id);
        }

    }
}