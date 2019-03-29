using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

public class QuitCommand : BaseCommand
{
    public QuitCommand(IList<string> inputArgs, IContainer container) : base(inputArgs, container) { }

    public override string Execute()
    {
        StringBuilder sb = new StringBuilder();
        int counter = 1;
        foreach (var hero in container.Heroes.OrderByDescending(x => x))
        {
            sb.AppendLine(Constants.GenerateHeroStatsQuit(counter++, hero));
        }
        return sb.ToString().Trim();
    }
}