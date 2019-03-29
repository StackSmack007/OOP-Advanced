namespace GenericArrayCreator
{
    public static class ArrayCreator
    {
        public static T[] Create<T>(int count,T element)
        {
            T[] result = new T[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = element;
            }
            return result;
        }
    }
}