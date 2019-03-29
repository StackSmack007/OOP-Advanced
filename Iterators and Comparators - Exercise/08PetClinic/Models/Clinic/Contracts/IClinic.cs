namespace Veterinary.Models.Clinic.Contracts
{
    using Pets.Contracts;
    public interface IClinic
    {
        string Name { get; }
        int RoomsCount { get; }
        bool Add(IPet pet);
        bool Release();
        bool HasEmptyRooms();
        void Print();
        void Print(int room);
    }
}
