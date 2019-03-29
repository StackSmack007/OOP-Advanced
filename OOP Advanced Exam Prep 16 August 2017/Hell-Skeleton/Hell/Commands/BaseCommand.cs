using System.Collections.Generic;
using System.Linq;

public abstract class BaseCommand : ICommand
{

    private List<string> inputArgs;
    protected IContainer container;
    protected BaseCommand(IList<string> inputArgs, IContainer container)
    {
        this.inputArgs = inputArgs.ToList();
        this.container = container;
    }
    public IReadOnlyCollection<string> InputArgs => inputArgs;

    public abstract string Execute();
}