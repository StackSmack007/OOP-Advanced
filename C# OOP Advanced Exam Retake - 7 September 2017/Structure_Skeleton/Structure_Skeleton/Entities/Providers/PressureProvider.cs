    public class PressureProvider : Provider
    {
        public PressureProvider(int id, double energyOutput) : base(id, energyOutput)
        {
            Durability -= 300;
            EnergyOutput *= 2;
        }
    }