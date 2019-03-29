namespace Frog
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    public class Lake : IEnumerable<int>
    {
        int[] stones;

        public Lake(string input)
        {
            stones = input.Split(", ").Select(int.Parse).ToArray();
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < stones.Length; i++)
            {
                if (i%2==0) yield return stones[i];
            }
            for (int i = stones.Length-1; i>= 0; i--)
            {
                if (i % 2 != 0) yield return stones[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}