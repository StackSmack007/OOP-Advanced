namespace TheTankGame.Entities.Parts
{
    using Contracts;
    using System;
    using TheTankGame.Utils;

    public abstract class BasePart : IPart
    {
        private string model;
        private double weight;
        private decimal price;
      //  ArsenalPart part3 = new ArsenalPart(model, weight, price, additionalParameter);
        protected BasePart(string model, double weight, decimal price)
        {
            this.Model = model;
            this.Weight = weight;
            this.Price = price;
        }

        public string Model
        {
            get
            {
                return model;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(GlobalConstants.ModelNameIsNullOrEmptyErrorMessage);
                }

                this.model = value;
            }
        }

        public double Weight
        {
            get
            {
                return this.weight;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(GlobalConstants.WeightIsLessThanZeroOrZeroErrorMessage);
                }

                this.weight = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this.price;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(GlobalConstants.WeightIsLessThanZeroOrZeroErrorMessage);
                }

                this.price = value;
            }
        }

        public override string ToString()
        {
            string partName = this.GetType().Name.Replace("Part", "");

            return $"{partName} Part - {this.Model}";
        }
    }
}