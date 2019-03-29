namespace _07InfernoInfinity
{
    using Contracts;
    using Core;
    public class StartUp
    {
        static void Main()
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}