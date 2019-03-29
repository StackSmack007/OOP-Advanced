namespace Tuple
{
    public class Tuple<T,U>
    {
        private T item1;
        private U item2;
        public Tuple(T item1,U item2)
        {
            this.item1 = item1;
            this.item2 = item2;
        }

        public T Item1 { get => item1; set => item1 = value; }
        public U Item2 { get => item2; set => item2 = value; }

        public override string ToString()
        {
            return $"{item1} -> {item2}";
        }
    }
}