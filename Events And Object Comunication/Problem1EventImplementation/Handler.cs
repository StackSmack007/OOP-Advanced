using Problem1EventImplementation.Contracts;
using System;

namespace Problem1EventImplementation
{
    public  class Handler:IHandler
    {
        public void On_NameChange(object unusedSender, NameChangeEventArgs args) => Console.WriteLine($"Dispatcher's name changed to {args.Name}.");
    }
}