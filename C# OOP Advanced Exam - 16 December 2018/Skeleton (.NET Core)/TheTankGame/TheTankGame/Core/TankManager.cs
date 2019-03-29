namespace TheTankGame.Core
{
    using Contracts;
    using Entities.Parts.Contracts;
    using Entities.Vehicles.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TheTankGame.Entities.Parts;
    using TheTankGame.Entities.Parts.Factories.Contracts;
    using TheTankGame.Entities.Vehicles.Factories.Contracts;
    using Utils;

    public class TankManager : IManager
    {
        private readonly IDictionary<string, IVehicle> vehicles;
        private readonly IList<string> defeatedVehicles;

        private readonly IBattleOperator battleOperator;
        private readonly IVehicleFactory vehicleFactory;
        private readonly IPartFactory partFactory;

        public TankManager(IBattleOperator battleOperator, IVehicleFactory vehicleFactory, IPartFactory partFactory)
        {
            this.battleOperator = battleOperator;

            this.vehicles = new Dictionary<string, IVehicle>();
            this.defeatedVehicles = new List<string>();

            this.vehicleFactory = vehicleFactory;
            this.partFactory = partFactory;

        }

        public string AddVehicle(IList<string> arguments)
        {//Vehicle Vanguard SA-203 100 300 1000 450 2000
            string vehicleType = arguments[0];
            string model = arguments[1];
            double weight = double.Parse(arguments[2]);
            decimal price = decimal.Parse(arguments[3]);
            int attack = int.Parse(arguments[4]);
            int defense = int.Parse(arguments[5]);
            int hitPoints = int.Parse(arguments[6]);

            IVehicle vehicle = vehicleFactory.CreateVehicle(vehicleType, model, weight, price, attack, defense, hitPoints);
            //throws exception if unsuccessfull!
            if (vehicles.ContainsKey(model)) throw new ArgumentException("Vehicle already added!NSH");

            this.vehicles.Add(vehicle.Model, vehicle);
            return string.Format(GlobalConstants.VehicleSuccessMessage, vehicleType, vehicle.Model);
        }

        public string AddPart(IList<string> arguments)
        {
            // Part SA-203 Arsenal Cannon-KA2 300 500 450
            string vehicleModel = arguments[0];
            string partType = arguments[1];
            string modelPart = arguments[2];
            double weight = double.Parse(arguments[3]);
            decimal price = decimal.Parse(arguments[4]);
            int additionalParameter = int.Parse(arguments[5]);

            IPart part = partFactory.CreatePart(partType, modelPart, weight, price, additionalParameter);

            switch (partType)
            {
                case "Arsenal":
                    this.vehicles[vehicleModel].AddArsenalPart(part);
                    break;
                case "Shell":
                    this.vehicles[vehicleModel].AddShellPart(part);
                    break;
                case "Endurance":
                    this.vehicles[vehicleModel].AddEndurancePart(part);
                    break;
            }
            return string.Format(GlobalConstants.PartSuccessMessage, partType, part.Model, vehicleModel);
        }

        public string Inspect(IList<string> arguments)
        {
            string model = arguments[0];
            string result = string.Empty;
            IPart part = vehicles.SelectMany(x => x.Value.Parts).FirstOrDefault(x => x.Model == model);
            if (vehicles.ContainsKey(model))
            {
                result = this.vehicles[model].ToString();
            }
            else if (part != null)
            {
                result = part.ToString();
            }
            return result;
        }

        public string Battle(IList<string> arguments)
        {
            IVehicle[] contestants = new IVehicle[2] { vehicles[arguments[0]], vehicles[arguments[1]] };
            string victorModel = battleOperator.Battle(contestants[0], contestants[1]);
            string looserModel = contestants.FirstOrDefault(x => x.Model != victorModel).Model;
            vehicles.Remove(looserModel);
            defeatedVehicles.Add(looserModel);

            return string.Format(GlobalConstants.BattleSuccessMessage,
                                                 contestants[0].Model,
                                                 contestants[1].Model,
                                                 victorModel);
        }


        public string Terminate(IList<string> arguments)
        {
            StringBuilder finalResult = new StringBuilder();

            string vehiclesInGame = string.Join(", ", vehicles.Keys);
            if (vehiclesInGame == "") vehiclesInGame = "None";

            finalResult.AppendLine($"Remaining Vehicles: {vehiclesInGame}");

            string vehiclesDefeated = string.Join(", ", defeatedVehicles);
            if (vehiclesDefeated == "") vehiclesDefeated = "None";
            finalResult.AppendLine($"Defeated Vehicles: {vehiclesDefeated}");

            finalResult.AppendLine($"Currently Used Parts: { vehicles.SelectMany(x => x.Value.Parts).Count()}");

            return finalResult.ToString().Trim();
        }
    }
}