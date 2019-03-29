using System;

namespace Skeleton
{
   public class FakeTarget : ITarget
    {
        public int Health => throw new NotImplementedException();

        public int GiveExperience()
        {
            return 1990;
        }

        public bool IsDead()
        {
            return true;
        }

        public void TakeAttack(int attackPoints) { }

    }
}