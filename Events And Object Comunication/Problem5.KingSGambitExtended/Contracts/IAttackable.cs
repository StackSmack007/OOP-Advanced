namespace _02.KingSGambit.Contracts
{
    public delegate void Attack(IKing king);
    public interface IAttackable
    {
        event Attack GotAttacked;
        void GetAttacked();
    }
}
