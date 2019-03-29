using System;

public abstract class Provider : IProvider
{
    private const int BreakingDecrease = 100;
    private const double InitialDurability = 1000;
    protected Provider(int id,double energyOutput)
    {
        EnergyOutput = energyOutput;
        ID = id;
        Durability = InitialDurability;
    }
    public int ID { get; }

    public double EnergyOutput { get; protected set; }


    public double Durability { get; protected set; }

    public virtual void Broke()
    {
        if (Durability <= BreakingDecrease)
        {
            throw new ArgumentException($"{this.GetType().Name} is caput!");
        }
        Durability -= BreakingDecrease;
    }

    public double Produce()
    {
        if (Durability>0) 
        {
            return EnergyOutput;
        }
        return 0;
    }

    public void Repair(double val)
    {
        Durability += val;
    }
}