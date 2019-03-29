
using System;
public abstract class Harvester : IHarvester
{
    private const int InitialDurability = 1000;
    private const int BreakingDecrease = 100;

    protected Harvester(int id, double oreOutput, double energyRequirement)
    {
        this.ID = id;
        this.OreOutput = oreOutput;
        this.EnergyRequirement = energyRequirement;
        this.Durability = InitialDurability;
    }

    public int ID { get; }

    public double OreOutput { get; protected set; }

    public double EnergyRequirement { get; protected set; }

    public virtual double Durability { get; protected set; }


    public void Broke()
    {
        if (Durability<=BreakingDecrease)
        {
            throw new ArgumentException($"{this.GetType().Name} is caput!");
        }
        Durability -= BreakingDecrease;
    }

    public double Produce()
    {
        if (Durability <= 0)
        {
            return 0;
        }
        return OreOutput;
    }
}