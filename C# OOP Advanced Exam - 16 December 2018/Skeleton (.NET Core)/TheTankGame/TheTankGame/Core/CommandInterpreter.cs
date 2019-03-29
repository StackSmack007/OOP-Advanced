namespace TheTankGame.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Contracts;

    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly IManager tankManager;

        public CommandInterpreter(IManager tankManager)
        {
            this.tankManager = tankManager;
        }

        public string ProcessInput(IList<string> inputParameters)
        {
            string command = inputParameters[0];
            inputParameters= inputParameters.Skip(1).ToArray();
            try
            {
            MethodInfo tankManagerMethod = tankManager.GetType().GetMethods().FirstOrDefault(x => x.Name.Contains(command));
            string result = (string)tankManagerMethod.Invoke(tankManager, new object[] { inputParameters });
            return result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }
    }
}