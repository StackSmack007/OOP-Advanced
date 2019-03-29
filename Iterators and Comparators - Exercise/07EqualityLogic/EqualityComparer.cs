using System.Collections.Generic;

namespace EqualityLogic
{
    public class EqualityComparer : IEqualityComparer<Person>
    {
        public bool Equals(Person x, Person y)
        {
            if (x.Name == y.Name && x.Age == y.Age) return true;
            return false;
        }

        public int GetHashCode(Person obj)
        {
            int sumOfAll = obj.Age * 103 - 11;
            for (int i = 0; i < obj.Name.Length; i++)
            {
                sumOfAll += obj.Name[i]-8*i+i%2==0?3:-4;
            }
            return sumOfAll;
        }
    }
}
