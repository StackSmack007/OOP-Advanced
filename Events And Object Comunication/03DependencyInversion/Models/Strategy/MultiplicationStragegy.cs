namespace P03_DependencyInversion.Models.Strategy
{
    using Contracts;
    public class MultiplicationStragegy : IStrategy
    {
        public int Calculate(int firstOperand, int secondOperand)
        {
            return firstOperand * secondOperand;
        }
    }
}