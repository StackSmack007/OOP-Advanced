using System;
namespace GenericScale
{
    public class Scale<T>
        where T : IComparable
    {
        private T left;
        private T right;
        public Scale(T left, T right)
        {
            this.left = left;
            this.right = right;
        }

        public T GetHeavier()
        {
            //positive result of compare is 1. Negative result is -1. Equallity is 0!
            if (left.CompareTo(right) == 1)
            {
                return left;
            }
            else if (left.CompareTo(right) == -1)
            {
                return right;
            }
            return default(T);
        }
    }
}