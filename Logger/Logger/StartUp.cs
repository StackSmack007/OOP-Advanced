﻿namespace Logger
{
    using Logger.Core;
    using Logger.Core.Contracts;

    public class StartUp
    {
        static void Main()
        {
            IEngine engine = new Engine();
            engine.Run();

        }
    }
}