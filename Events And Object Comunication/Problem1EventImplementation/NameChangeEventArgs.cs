using System;
namespace Problem1EventImplementation
{
    public class NameChangeEventArgs : EventArgs
    {
        public NameChangeEventArgs(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}