using _02.KingSGambit.Contracts;
using System.Collections.Generic;

namespace _02.KingSGambit.Models
{

   
    public class King : IKing
    {

        private List<ISubject> subjects;

        public event Attack GotAttacked;
        public event Kill GetSomeoneKilled;

        public King(string name)
        {
            subjects = new List<ISubject>();
            Name = name;
        }

        public string Name { get; }

        public IReadOnlyCollection<ISubject> Subjects => subjects;

        public void GetAttacked()
        {
            GotAttacked(this);
        }

        public void RegisterGuard(string names)
        {
            foreach (var name in names.Split())
            {
                subjects.Add(new Guard(name));
            }
        }

        public void RegisterFootman(string names)
        {
            foreach (var name in names.Split())
            {
                subjects.Add(new Footman(name));
            }
        }

        public void Murder(string name)
        {
            GetSomeoneKilled(this,name);
        }
    }
}