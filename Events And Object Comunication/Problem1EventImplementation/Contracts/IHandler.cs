namespace Problem1EventImplementation.Contracts
{
    public interface IHandler
    {
        void On_NameChange(object unusedSender, NameChangeEventArgs args);
    }
}
