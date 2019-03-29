namespace Travel
{
    using Core;
    using Core.Controllers;
    using Core.IO;
    using Core.IO.Contracts;
    using Entities;
    using Travel.Core.Contracts;
    using Travel.Core.Controllers.Contracts;
    using Travel.Entities.Contracts;

    // god loves ugly
    public static class StartUp
	{
		public static void Main()
		{
			IReader reader=new ConsoleReader();
			IWriter writer = new ConsoleWriter();

			IAirport airport = new Airport();
            IAirportController airportController = new AirportController(airport);
			IFlightController flightController = new FlightController(airport);

			IEngine engine = new Engine(reader, writer, airportController, flightController);

			engine.Run();
		}
	}
}