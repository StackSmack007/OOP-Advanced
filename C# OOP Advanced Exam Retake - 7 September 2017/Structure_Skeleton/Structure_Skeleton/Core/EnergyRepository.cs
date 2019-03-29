    public class EnergyRepository : IEnergyRepository
    {
        public EnergyRepository()
        {
            EnergyStored = 0;
        }

        public double EnergyStored { get; private set; }

        public void StoreEnergy(double energy)
        {
            EnergyStored += energy;
        }

        public bool TakeEnergy(double energyNeeded)
        {
            return EnergyStored >= energyNeeded;
        }
    }