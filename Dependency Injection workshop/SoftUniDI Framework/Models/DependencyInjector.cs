using SoftUniDI_Framework.Contracts;

namespace SoftUniDI_Framework.Models
{
    public static  class DependencyInjector
    {
        public static Injector CreateInjector(IModule module)
        {
            module.Configure();
            return new Injector(module);
        }
    }
}
