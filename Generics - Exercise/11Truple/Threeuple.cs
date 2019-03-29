namespace Truple
{
    public class Threeuple<T,U,Z>
    {
        private T item1;
        private U item2;
        private Z item3;
        public Threeuple(T item1,U item2, Z item3)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
        }

        public T Item1 { get => item1; set => item1 = value; }
        public U Item2 { get => item2; set => item2 = value; }
        public Z Item3 { get => item3; set => item3 = value; }

        public override string ToString()
        {
            return $"{item1} -> {item2} -> {item3}";
        }
    }
}