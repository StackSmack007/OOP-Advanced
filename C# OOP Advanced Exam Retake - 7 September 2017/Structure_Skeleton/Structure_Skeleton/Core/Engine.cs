public class Engine
{
    private IReader reader;
    private IWriter writer;
    private ICommandInterpreter commandInterpreter;
    private IHarvesterController harvesterController;
    private IProviderController providerController;


    public Engine(IReader reader,  IWriter writer,  IHarvesterController harvesterController, IProviderController providerController, ICommandInterpreter commandInterpreter)
    {
        this.reader = reader;
        this.writer = writer;

        this.harvesterController = harvesterController;
        this.providerController = providerController;
        this.commandInterpreter = commandInterpreter;
    }

    public void Run()
    {

        while (true)
        {
            var inputArgs = reader.ReadLine().Split();
            string result = commandInterpreter.ProcessCommand(inputArgs);
            writer.WriteLine(result);
            try
            {
                var command = inputArgs[0];
                if (command == "Shutdown") break;
            }
            catch (System.Exception) { }
        }
    }
}