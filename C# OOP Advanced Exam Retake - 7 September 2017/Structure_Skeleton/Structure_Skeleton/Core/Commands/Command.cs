using System.Collections.Generic;

public abstract class Command : ICommand
{
    public Command(IList<string> list)
    {
        Arguments = list;
    }

    public IList<string> Arguments { get; }

    public abstract string Execute();
}