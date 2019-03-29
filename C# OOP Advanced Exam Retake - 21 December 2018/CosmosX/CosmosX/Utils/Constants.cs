namespace CosmosX.Utils
{
    public class Constants
    {
        public static int StartingId = 1;

        public static string ReactorCreateMessage = "Created {1} Reactor - {0}";

        public static string ModuleCreateMessage = "Added {0} - {1} to Reactor - {2}";

        public static string ModuleNullErrorMessage = "Can not add null module to collection";


        public static string ReactorToStringMessage= "{0} - {1}\nEnergy Output: {2}\nHeat Absorbing: {3}\nModules: {4}";

        public static string ModuleToStringMessage = "{0} Module - {1}";

        public static string ReactorTypeNotFound = "Reactor with type {0} does not exist in the database!";
        public static string ModuleTypeNotFound = "Module with type {0} does not exist in the database!";

        public static string IdNotFound = "Item with id {0} not found in the database!";
        public static string InvalidOperationMessage = "Unrecognised command {0}";


    }
}