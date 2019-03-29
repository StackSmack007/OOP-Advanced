namespace Logger.Factories
{
    using Layouts;
    using Layouts.Contracts;
    using System;

    public class LayoutFactory
    {
        public ILayout CreateLayout(string type)
        {
            ILayout layout;
            switch (type.ToUpper())
            {
                case "SIMPLELAYOUT": layout = new SimpleLayout(); break;
                case "XMLLAYOUT": layout = new XmlLayout(); break;
                default: throw new ArgumentException("Invalid layout type!");
            }
            return layout;
        }
    }
}