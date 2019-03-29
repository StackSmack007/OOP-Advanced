using System;

namespace Problem1EventImplementation.Contracts
{
    public interface INameChangeable
    {
        event EventHandler<NameChangeEventArgs> NameChangeEvent;
        string Name { set; }
    }
}