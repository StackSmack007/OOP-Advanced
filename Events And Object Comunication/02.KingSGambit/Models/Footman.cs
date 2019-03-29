using _02.KingSGambit.Contracts;

namespace _02.KingSGambit.Models
{
    public class Footman : ISubject
    {
        public Footman(string name)
        {
            IsAlive = true;
            Name = name;
        }

        public bool IsAlive { get; private set; }

        public string Name { get; }

        public void Die()
        {
            IsAlive=false;
        }

    }
}