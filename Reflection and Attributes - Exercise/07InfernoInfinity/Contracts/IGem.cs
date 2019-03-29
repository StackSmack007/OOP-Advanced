namespace _07InfernoInfinity.Contracts
{
using _07InfernoInfinity.Enumerations;
    public interface IGem
    {
        int Strength { get; }
        int Agility { get; }
        int Vitality { get; }
        Clarity Clarity { get; }
        int AddMINDamage { get; }
        int AddMAXDamage { get; }
    }
}