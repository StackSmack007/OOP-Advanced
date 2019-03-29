namespace Travel.Entities
{
	using Airplanes.Contracts;
	using Contracts;

	public class Trip : ITrip
	{
		private int id;

		public Trip(string source, string destination, IAirplane airplane)
		{
            this.Source = source;
			this.Destination = destination;
			this.Airplane = airplane;
            IsCompleted = false;
		}
        public int IdNumber { get; set; }//NSH

        public string Id => GenerateId();

		public string Source { get; }

		public string Destination { get; }

		public bool IsCompleted { get; private set; }

		public IAirplane Airplane { get; }

		public void Complete() => this.IsCompleted = true;

		private string GenerateId()
		{
			return $"{this.Source}{this.Destination}{IdNumber}";
		}
	}
}