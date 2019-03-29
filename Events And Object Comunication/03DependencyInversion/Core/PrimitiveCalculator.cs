namespace P03_DependencyInversion.Core
{
    using Contracts;
    using P03_DependencyInversion.Models.Strategy;

    public class PrimitiveCalculator
    {
        private IStrategy currentStrategy;
        private StrategyFactory strategyFactory;

        public PrimitiveCalculator()
        {
            strategyFactory = new StrategyFactory();
            currentStrategy = new AdditionStrategy();
        }

        public void ChangeStrategy(char @operator)
        {
            currentStrategy = strategyFactory.CreateStrategy(@operator);
        }

        public int PerformCalculation(int firstOperand, int secondOperand)
        {
            return currentStrategy.Calculate(firstOperand, secondOperand);
        }
    }
}