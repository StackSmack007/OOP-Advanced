namespace CosmosX.Entities.Modules.Factories.Contracts
{
    using CosmosX.Entities.Modules.Contracts;
    public interface IModuleFactory
    {
        IModule CreateModule(string type,int id, int additionalParameter);
    }
}