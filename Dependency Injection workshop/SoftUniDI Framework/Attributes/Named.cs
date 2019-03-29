using System;

namespace SoftUniDI_Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter)]
    public class Named : Attribute
    {
        public string Name { get; }
        public Named(string name)
        {
            Name = name;
        }
    }
}