namespace FestivalManager.Core
{
    using Contracts;
    using Controllers.Contracts;
    using FestivalManager.Core.IO;
    using IO.Contracts;
    using System;
    using System.Linq;
    using System.Reflection;

    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;

        private IFestivalController festivalCоntroller;
        private ISetController setCоntroller;

        public Engine(IFestivalController festivalCоntroller, ISetController setCоntroller)
        {
            this.festivalCоntroller = festivalCоntroller;
            this.setCоntroller = setCоntroller;
            reader = new ConsoleReader();
            writer = new ConsoleWriter();
        }

        public void Run()
        {
            string input = reader.ReadLine();
            while (input != "END") 
            {
                try
                {
                    string result = ProcessCommand(input);
                    writer.WriteLine(result);
                }
                catch (InvalidOperationException ioe)
                {
                    this.writer.WriteLine("ERROR: " + ioe.Message);
                }
                input = reader.ReadLine();
            }

            string report = festivalCоntroller.ProduceReport();

            writer.WriteLine("Results:");
            writer.WriteLine(report);
        }

        public string ProcessCommand(string input)
        {
            string[] tokens = input.Split(" ".ToCharArray().First());

            string commandType = tokens.First();
            string[] inputArgs = tokens.Skip(1).ToArray();

            if (commandType == "LetsRock")
            {
                string result = setCоntroller.PerformSets();
                return result;
            }

            var festivalControllerMethod = this.festivalCоntroller.GetType()
                .GetMethods()
                .FirstOrDefault(x => x.Name == commandType);

            string message = string.Empty;
            try
            { 
            return (string)festivalControllerMethod.Invoke(festivalCоntroller, new object[] { inputArgs });
            }
            catch (TargetInvocationException up)
            {
                throw up.InnerException;
            }
             return message;
        }
    }
}