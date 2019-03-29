namespace _02.KingSGambit.Contracts
{
    public interface IKillable
    {
        bool IsAlive { get; }
        void Die();
    }
}