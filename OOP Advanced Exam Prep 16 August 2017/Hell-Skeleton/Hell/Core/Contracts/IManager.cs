using System.Collections.Generic;
public interface IManager
{
    string ProcessCommand(IList<string> arguments);
}