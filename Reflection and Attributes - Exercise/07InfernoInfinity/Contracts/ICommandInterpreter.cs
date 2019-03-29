namespace _07InfernoInfinity.Contracts
{
  public  interface ICommandInterpreter
    {
        void InterpreteCommand(string[] data);
        void InterpreteCommandForAttribute(string[] inputArgs);
    }
}