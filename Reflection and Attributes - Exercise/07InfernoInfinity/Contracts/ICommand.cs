namespace _07InfernoInfinity.Contracts
{
    public  interface ICommand
    {
        void Execute(IWeapon weapon, string[] data);
    }
}