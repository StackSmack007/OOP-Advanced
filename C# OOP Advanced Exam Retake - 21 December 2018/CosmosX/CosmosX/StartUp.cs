namespace CosmosX
{
    using Core;
    using Core.Contracts;
    using IO;
    using IO.Contracts;
    public class StartUp
    {
        public static void Main()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            IManager reactorManager = new ReactorManager();
            ICommandParser commandParser = new CommandParser(reactorManager);
            IEngine engine = new Engine(reader, writer, commandParser);
            engine.Run();
        }
    }
}