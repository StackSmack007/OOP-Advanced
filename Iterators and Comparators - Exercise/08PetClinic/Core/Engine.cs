namespace Veterinary.Core
{
    using System;
    using System.Linq;
    public class Engine
    {
        private AnimalCoordinator ac;
        public Engine()
        {
            ac = new AnimalCoordinator();
        }
        public void Run()
        {
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var inputArgs = Console.ReadLine().Split();
                string command = inputArgs[0];
                inputArgs = inputArgs.Skip(1).ToArray();
                try
                {
                    switch (command)
                    {
                        case "Create":
                            {
                                string entity = inputArgs[0];
                                inputArgs = inputArgs.Skip(1).ToArray();
                                if (entity == "Clinic")
                                {
                                    ac.CreateClinic(inputArgs);
                                }
                                else if (entity == "Pet")
                                {
                                    ac.CreatePet(inputArgs);
                                }
                                break;
                            }
                        case "Add": Console.WriteLine(ac.AddPetToClinic(inputArgs)); break;
                        case "Release": Console.WriteLine(ac.Release(inputArgs)); break;
                        case "HasEmptyRooms": Console.WriteLine(ac.HasEmptyRooms(inputArgs)); break;
                        case "Print": ac.Print(inputArgs); break;


                    }
                }
                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine(ioe.Message);
                }
            }

        }
    }
}