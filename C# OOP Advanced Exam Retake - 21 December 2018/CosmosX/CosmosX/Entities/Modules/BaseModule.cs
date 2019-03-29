namespace CosmosX.Entities.Modules
{
    using Contracts;
    using Utils;
    public abstract class BaseModule : IModule
    {
        protected BaseModule(int id)
        {
            this.Id = id;
        }

        public int Id { get; }

        public override string ToString()
        {
            return string.Format(Constants.ModuleToStringMessage, this.GetType().Name, this.Id);
        }
    }
}