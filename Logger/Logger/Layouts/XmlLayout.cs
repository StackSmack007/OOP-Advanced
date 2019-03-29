namespace Logger.Layouts
{
    using Contracts;
    public class XmlLayout : ILayout
    {
        public string Format
        {
            get
            {
                return "<log>\n" +
                    "   <date>{0}</date>\n" +
                    "   <level>{1}</level>\n" +
                    "   <message>{2}</message>\n" +
                    "</log>";
            }
        }
    }
}