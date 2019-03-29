namespace Veterinary.Models.Clinic
{
    using Contracts;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Veterinary.Models.Pets.Contracts;

    public class Clinic : IClinic, IEnumerable<IPet>
    {
        private int roomsCount;
        private IPet[] rooms;
        public string Name { get; private set; }
        public int RoomsCount
        {
            get { return roomsCount; }
            private set
            {
                if (value % 2 == 0)
                {
                    throw new InvalidOperationException("Invalid Operation!");
                }
                roomsCount = value;
            }
        }

        public Clinic(string name, int roomsCount)
        {
            Name = name;
            RoomsCount = roomsCount;
            rooms = new IPet[RoomsCount];
        }

        private bool IsRoomFree(int index)
        {
            if (rooms[index] is null)
            {
                return true;
            }
            return false;
        }

        public bool Add(IPet pet)
        {
            int currentIndex = rooms.Length / 2;
            for (int i = 0; i < rooms.Length; i++)
            {
                int signModulator = i % 2 == 0 ? 1 : -1;
                currentIndex += signModulator * i;
                if (IsRoomFree(currentIndex))
                {
                    rooms[currentIndex] = pet;
                    return true;
                }
            }
            return false;
        }

        public bool HasEmptyRooms()
        {
            return rooms.Any(x => x is null);
        }

        public void Print()
        {
            foreach (var pet in rooms)
            {
                if (pet is null)
                {
                    Console.WriteLine("Room empty");
                }
                else
                {
                    Console.WriteLine(pet);
                }
            }
        }

        public void Print(int room)
        {
            var pet = rooms[room - 1];//??index number
            if (pet is null)
            {
                Console.WriteLine("Room empty");
            }
            else
            {
                Console.WriteLine(pet);
            }
        }

        public bool Release()
        {
            int currentIndex = rooms.Length / 2;
            for (int i = 0; i < rooms.Length; i++)
            {
                currentIndex += i;
                if (currentIndex == rooms.Length) currentIndex = 0;

                if (!IsRoomFree(currentIndex))
                {
                    rooms[currentIndex] =null;
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<IPet> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}