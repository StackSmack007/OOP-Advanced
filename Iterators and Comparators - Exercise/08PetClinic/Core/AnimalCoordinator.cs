namespace Veterinary.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Clinic.Contracts;
    using Models.Pets.Contracts;
    using Veterinary.Models.Clinic;

    public class AnimalCoordinator
    {
        private List<IPet> petPool;
        private List<IClinic> clinics;

        public AnimalCoordinator()
        {
            petPool = new List<IPet>();
            clinics = new List<IClinic>();
        }

        public void CreatePet(string[] inputArgs)
        {
            string name = inputArgs[0];
            int age = int.Parse(inputArgs[1]);
            string kind = inputArgs[2];
            petPool.Add(new Pet(name, age, kind));
        }

        public void CreateClinic(string[] inputArgs)
        {
            string name = inputArgs[0];
            int roomsNumber = int.Parse(inputArgs[1]);
            clinics.Add(new Clinic(name, roomsNumber));//exception possible!
        }

        public bool AddPetToClinic(string[] inputArgs)
        {
            string petName = inputArgs[0];
            string clinicName = inputArgs[1];
            IClinic clinic = GetClinic(clinicName);

            IPet pet = petPool.FirstOrDefault(x => x.Name == petName);
           if ( pet is null)
            {
                throw new InvalidOperationException("Not existing animal!");
            }
            petPool.Remove(pet);
            return clinic.Add(pet);
        }

        public bool Release(string[] inputArgs)
        {
            IClinic clinic = clinics.FirstOrDefault(x => x.Name == inputArgs[0]);
            if (clinic is null) throw new InvalidOperationException("Not existing clinic!");
            return clinic.Release();
        }

        public bool HasEmptyRooms(string[] inputArgs)
        {
            return GetClinic(inputArgs[0]).HasEmptyRooms();
        }

        public void Print(string[] inputArgs)
        {
            IClinic clinic = GetClinic(inputArgs[0]);
            if (inputArgs.Length==1)
            {
                clinic.Print();
                return;
            }
            int room = int.Parse(inputArgs[1]);
            clinic.Print(room);
        }

        private IClinic GetClinic(string nameOfClinic)
        {
            IClinic clinic = clinics.FirstOrDefault(x => x.Name == nameOfClinic);
            if (clinic is null) throw new InvalidOperationException("Not existing clinic!");
            return clinic;
        }
    }
}