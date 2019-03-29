namespace Logger.Layouts
{
    using Contracts;
    public class SimpleLayout : ILayout
    {
        public string Format {get;}
        public SimpleLayout()
        {
            Format = "{0} - {1} - {2}";
        }
    }
}