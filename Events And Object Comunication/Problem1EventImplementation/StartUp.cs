using Problem1EventImplementation.Contracts;
using System;

namespace Problem1EventImplementation
{
    public class StartUp
    {
        static void Main()
        {
            INameChangeable dispacher = new Dispatcher("Asparuh");
            IHandler handler = new Handler();
            dispacher.NameChangeEvent += handler.On_NameChange;

            string input;
            while ((input=Console.ReadLine())!="End")
            {
                dispacher.Name = input;
            }
        }
    }
}