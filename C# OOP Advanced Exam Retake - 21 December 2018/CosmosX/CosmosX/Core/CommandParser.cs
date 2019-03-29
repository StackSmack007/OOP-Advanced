using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CosmosX.Core.Contracts;
using CosmosX.Utils;

namespace CosmosX.Core
{
    public class CommandParser : ICommandParser
    {
        private const string CommandNameSuffix = "Command";

        private readonly IManager reactorManager;

        public CommandParser(IManager reactorManager)
        {
            this.reactorManager = reactorManager;
        }

        public string Parse(IList<string> arguments)
        {
            string commandName = arguments[0] + CommandNameSuffix;

            string[] commandArguments = arguments.Skip(1).ToArray();

            MethodInfo methodFound = reactorManager.GetType().GetMethods().FirstOrDefault(x => x.Name == commandName);

            if (methodFound is null) throw new ArgumentException(string.Format(Constants.InvalidOperationMessage, commandName));
            string result = (string)methodFound.Invoke(reactorManager,new[] { commandArguments });
            return result;
        }
    }
}