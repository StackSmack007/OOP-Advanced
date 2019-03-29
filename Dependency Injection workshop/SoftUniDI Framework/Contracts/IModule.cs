using System;

namespace SoftUniDI_Framework.Contracts
{
    public  interface IModule
    {
        void Configure();

        Type GetMapping(Type currentIntefrace, object attribute);

        object GetInstance(Type type);

        void SetInstance(Type implementation, object instance);

    }
}