using System;
using System.Collections.Generic;
using System.Linq;

public class Engine : IEngine
{
    private IInputReader reader;
    private IOutputWriter writer;
    private IManager heroManager;

    public Engine(IInputReader reader, IOutputWriter writer, IManager heroManager)
    {
        this.reader = reader;
        this.writer = writer;
        this.heroManager = heroManager;
    }

    public void Run()
    {
        while (true)
        {
            string inputLine = this.reader.ReadLine();
            List<string> arguments = inputLine.Split(new char[] { ' '},StringSplitOptions.RemoveEmptyEntries).ToList();
            try
            {
                string result = heroManager.ProcessCommand(arguments);
                writer.WriteLine(result);
            }
            catch (ArgumentException ae)
            {
                writer.WriteLine(ae.Message);
            }
            if (inputLine.Equals("Quit")) break;
        }
    }
}