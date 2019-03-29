using P03_DependencyInversion.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace P03_DependencyInversion.Models.Strategy
{
    public class StrategyFactory
    {
        private IDictionary<char, string> classTypeRegistry;
        public StrategyFactory()
        {
            classTypeRegistry = new Dictionary<char, string>
            {
                ['+'] = "AdditionStrategy",
                ['-'] = "SubtractionStrategy",
                ['*'] = "MultiplicationStragegy",
                ['/'] = "DivisionStragegy",
            };
        }
        public IStrategy CreateStrategy(char operation)
        {
            string trueName = classTypeRegistry[operation];
            Type type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name == trueName);
            return (IStrategy)Activator.CreateInstance(type);
        }
    }
}