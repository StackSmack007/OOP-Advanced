namespace Travel.Entities.Airplanes
{
    using Entities.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Travel.Entities.Airplanes.Contracts;


    public abstract class Airplane : IAirplane
    {
        private List<IBag> luggages;
        private List<IPassenger> passengers;

        protected Airplane(int seats, int bagageCappacityt)
        {
            this.passengers = new List<IPassenger>();
            this.luggages = new List<IBag>();
            this.Seats = seats;
            this.BaggageCompartments = bagageCappacityt;
        }
        public int Seats { get; }
        public int BaggageCompartments { get; }

        public IReadOnlyCollection<IBag> BaggageCompartment => luggages;

        public IReadOnlyCollection<IPassenger> Passengers => this.passengers;

        public bool IsOverbooked => this.Passengers.Count > this.Seats;


        public void AddPassenger(IPassenger traveler)
        {
            this.passengers.Add(traveler);
        }

        public IPassenger RemovePassenger(int seatIndeks)
        {
            var traveler = this.passengers[seatIndeks];
            this.passengers.RemoveAt(seatIndeks);
            return traveler;
        }

        public IEnumerable<IBag> EjectPassengerBags(IPassenger passenger)
        {
            var passengerBags = this.luggages
               .Where(b => b.Owner == passenger)
               .ToArray();

            luggages = luggages.Where(b => b.Owner != passenger).ToList();

            return passengerBags;
        }

        public void LoadBag(IBag bag)
        {
            var isBaggageCompartmentFull = this.BaggageCompartment.Count > this.BaggageCompartments;
            if (isBaggageCompartmentFull)
                throw new InvalidOperationException($"No more bag room in {this.GetType().ToString()}!");

            this.luggages.Add(bag);
        }
    }
}