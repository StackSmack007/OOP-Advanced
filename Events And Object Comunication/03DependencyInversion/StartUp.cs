namespace P03_DependencyInversion
{
    using Core;
    public class StartUp
    {
        static void Main()
        {
            var engine = new Engine();
            engine.Run();
        }
    }
}