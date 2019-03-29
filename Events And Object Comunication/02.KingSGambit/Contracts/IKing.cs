using System.Collections.Generic;

namespace _02.KingSGambit.Contracts
{
    public delegate void Kill(IKing king,string name);
    public interface IKing : IAttackable, INameable
    {
        event Kill GetSomeoneKilled;

        IReadOnlyCollection<ISubject> Subjects { get; }
        void RegisterFootman(string name);
        void RegisterGuard(string name);
        void Murder(string name);
    }
}