public class StartUp
{
    public static void Main()
    {
        IInputReader reader = new ConsoleReader();
        IOutputWriter writer = new ConsoleWriter();
        IContainer container = new HeroesContainer();
        IManager manager = new HeroManager(container);
        IEngine engine = new Engine(reader, writer, manager);
        engine.Run();
    }
}