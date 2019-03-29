using NUnit.Framework;
using StorageMaster.Entities.Products;
using StorageMaster.Entities.Storage;
using StorageMaster.Entities.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StorageMaster.BusinessLogic.Tests
{
    [TestFixture]
    public class BusinessLogicTests
    {
        private Type typeSM;
        private object instanceSM;

        [SetUp]
        public void SetUp()
        {
            typeSM = typeof(StartUp).Assembly.GetTypes().FirstOrDefault(x => x.Name == "StorageMaster");
            instanceSM = Activator.CreateInstance(typeSM);
        }

        [TestCase("Ram", 13.5)]
        [TestCase("Gpu", 10)]
        [TestCase("HardDrive", 10.5)]
        [TestCase("SolidStateDrive", 7.55)]
        public void TestAdding(string productName, double price)
        {

            var methodAddProduct = typeSM.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                        .FirstOrDefault(x => x.Name == "AddProduct"
                                        && x.ReturnType == typeof(string) &&
                                        x.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(string), typeof(double) }));
            Assert.NotNull(methodAddProduct, "Method named AddProduct(string x,double y) not found!");

            FieldInfo productPoolDictionary = typeSM.GetField("productsPool", BindingFlags.NonPublic | BindingFlags.Instance);
            IDictionary<string, Stack<Product>> currentDictionaryInfo = (IDictionary<string, Stack<Product>>)productPoolDictionary.GetValue(instanceSM);

            Assert.AreEqual(0, currentDictionaryInfo.Count, "Pool of Products is not null upon initialization");

            methodAddProduct.Invoke(instanceSM, new object[] { productName, price });

            Assert.AreEqual(1, currentDictionaryInfo.Count, "Pool of Products Dictionary is not increasing count");

            Assert.AreEqual(1, currentDictionaryInfo[productName].Count, "Pool of Products InnerStack is not increasing count");

            string message = (string)methodAddProduct.Invoke(instanceSM, new object[] { productName, price });
            string expectedMessage = $"Added {productName} to pool";
            Assert.AreEqual(expectedMessage, message, "Message not correct!");

            Assert.AreEqual(1, currentDictionaryInfo.Count, "Pool of Products Dictionary is increasing count adding same Key element");
            Assert.AreEqual(2, currentDictionaryInfo[productName].Count, "Pool of Products InnerStack is not increasing count");

        }


        [TestCase("AutomatedWarehouse", "Sklad1")]
        [TestCase("DistributionCenter", "Sklad1")]
        [TestCase("Warehouse", "Sklad1")]
        public void CheckStorageRegistering(string storageNameClass, string name)
        {
            FieldInfo storageRegistryField = typeSM.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == "storageRegistry");

            var storageRegistry = (IDictionary<string, Storage>)storageRegistryField.GetValue(instanceSM);
            Assert.That(storageRegistry.Count == 0, "There is initial values in storage registry");

            MethodInfo addStorageMethod = typeSM.GetMethods().FirstOrDefault(x => x.Name == "RegisterStorage" & x.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(string), typeof(string) }));
            string resultMessage = (string)addStorageMethod.Invoke(instanceSM, new object[] { storageNameClass, name });
            string expectedMessage = $"Registered {name}";
            Assert.AreEqual(expectedMessage, resultMessage, $"Return string is \"{resultMessage}\" instead of \"{expectedMessage}\"");
            Assert.AreEqual(1, storageRegistry.Count, "Adding storage does not increase inner container's counter!");
        }

        [TestCase("Sklad1", 0, typeof(Truck))]
        [TestCase("Sklad2", 0, typeof(Van))]
        [TestCase("Sklad2", 0, typeof(Van))]
        [TestCase("Sklad2", 1, typeof(Van))]
        [TestCase("Sklad3", 0, typeof(Semi))]
        [TestCase("Sklad3", 1, typeof(Semi))]
        [TestCase("Sklad3", 2, typeof(Semi))]
        public void CheckSelectVehicle(string storageName, int garageSlot, Type vehicleTypeExpected)
        {

            MethodInfo addStorageMethod = typeSM.GetMethods().FirstOrDefault(x => x.Name == "RegisterStorage" && x.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(string), typeof(string) }));
            addStorageMethod.Invoke(instanceSM, new object[] { "AutomatedWarehouse", "Sklad1" });
            addStorageMethod.Invoke(instanceSM, new object[] { "DistributionCenter", "Sklad2" });
            addStorageMethod.Invoke(instanceSM, new object[] { "Warehouse", "Sklad3" });
            MethodInfo selectVehicleMethod = typeSM.GetMethods().FirstOrDefault(x => x.Name == "SelectVehicle" && x.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(string), typeof(int) }));

            Assert.IsNull(typeSM.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == "currentVehicle").GetValue(instanceSM));

            string expectedMessage = $"Selected {vehicleTypeExpected.Name}";
            string actualMessage = (string)selectVehicleMethod.Invoke(instanceSM, new object[] { storageName, garageSlot });
            Assert.AreEqual(expectedMessage, actualMessage, "Return message was {0} instead of {1}", actualMessage, expectedMessage);
            var actualType = typeSM.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == "currentVehicle").GetValue(instanceSM).GetType();
            Assert.AreEqual(vehicleTypeExpected, actualType, "CurrentVehicle not properly set!");
        }

        [TestCase("Gpu", "HardDrive", "Ram")]
        public void CheckLoadVehicle(params string[] products)
        {
            string storageName = "Sklad1";
            string storageType = "AutomatedWarehouse";
            int vehicleSlotChosen = 0;
            //Adding product
            var methodAddProduct = typeSM.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                      .FirstOrDefault(x => x.Name == "AddProduct"
                                      && x.ReturnType == typeof(string) &&
                                      x.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(string), typeof(double) }));
            foreach (string productName in products)
            {
                double price = 10.42;
                methodAddProduct.Invoke(instanceSM, new object[] { productName, 10.42 });
            }
            //Adding product
            MethodInfo addStorageMethod = typeSM.GetMethods().FirstOrDefault(x => x.Name == "RegisterStorage" && x.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(string), typeof(string) }));
            addStorageMethod.Invoke(instanceSM, new object[] { storageType, storageName });

            MethodInfo selectVehicleMethod = typeSM.GetMethods().FirstOrDefault(x => x.Name == "SelectVehicle" && x.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(string), typeof(int) }));
            selectVehicleMethod.Invoke(instanceSM, new object[] { storageName, vehicleSlotChosen });

            MethodInfo loadVehicleMethod = typeSM.GetMethods().FirstOrDefault(x => x.Name == "LoadVehicle" && x.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(IEnumerable<string>) }));
            loadVehicleMethod.Invoke(instanceSM, new object[] { products });
            // CurrentVehicle is loaded already !

            Vehicle curentVehicle = (Vehicle)typeSM.GetField("currentVehicle", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instanceSM);
            //Checking if current 
            int expectedCountOfProducts = products.Length;
            int actualCountOfProducts = curentVehicle.Trunk.Count();
            Assert.AreEqual(expectedCountOfProducts, actualCountOfProducts);
        }

        [TestCase("Gpu", "Sklad1", "AutomatedWarehouse", 0)]
        public void CheckLoadNonExistingItemToVehicleThowsInvalidOperationException(string productName, string storageName, string storageType, int vehicleSlotChosen)
        {

            MethodInfo addStorageMethod = typeSM.GetMethod("RegisterStorage");
            addStorageMethod.Invoke(instanceSM, new object[] { storageType, storageName });

            MethodInfo selectVehicleMethod = typeSM.GetMethod("SelectVehicle");
            selectVehicleMethod.Invoke(instanceSM, new object[] { storageName, vehicleSlotChosen });

            MethodInfo loadVehicleMethod = typeSM.GetMethod("LoadVehicle");

            Assert.That(() => loadVehicleMethod.Invoke(instanceSM, new object[] { new[] { productName } }), Throws.Exception);
        }

        [TestCase("SkladInvalid", 0, "Sklad2")]
        // [TestCase("Sklad1",0,"SkladInvalid")]
        public void Check_Send_Vehicle_FromInvalidLocation_To_Throws_InvalidOperationException(string sourceName, int sourceGarageSlot, string destinationName)
        {

            MethodInfo addStorageMethod = typeSM.GetMethod("RegisterStorage");
            addStorageMethod.Invoke(instanceSM, new object[] { "AutomatedWarehouse", "Sklad1" });
            addStorageMethod.Invoke(instanceSM, new object[] { "DistributionCenter", "Sklad2" });

            MethodInfo sendVehicleToMethod = typeSM.GetMethod("SendVehicleTo");

            Assert.That(() => sendVehicleToMethod.Invoke(instanceSM, new object[] { sourceName, sourceGarageSlot, destinationName }), Throws.Exception.InnerException.Message.EqualTo("Invalid source storage!"));
        }

        [TestCase("Sklad1", 0, "SkladInvalid")]
        public void Check_Send_Vehicle_To_InvalidDestination_Throws_InvalidOperationException(string sourceName, int sourceGarageSlot, string destinationName)
        {
            Type typeSM = typeof(StartUp).Assembly.GetTypes().FirstOrDefault(x => x.Name == "StorageMaster");
            var instanceSM = Activator.CreateInstance(typeSM);

            MethodInfo addStorageMethod = typeSM.GetMethod("RegisterStorage");
            addStorageMethod.Invoke(instanceSM, new object[] { "AutomatedWarehouse", "Sklad1" });
            addStorageMethod.Invoke(instanceSM, new object[] { "DistributionCenter", "Sklad2" });

            MethodInfo sendVehicleToMethod = typeSM.GetMethod("SendVehicleTo");

            Assert.That(() => sendVehicleToMethod.Invoke(instanceSM, new object[] { sourceName, sourceGarageSlot, destinationName }), Throws.Exception.InnerException.Message.EqualTo("Invalid destination storage!"));
        }

        [TestCase("Sklad1", 0, "Sklad2")]
        [TestCase("Sklad2", 1, "Sklad1")]
        public void Check_SendVehicleToWorksProperly(string sourceName, int sourceGarageSlot, string destinationName)
        {

            MethodInfo addStorageMethod = typeSM.GetMethods().FirstOrDefault(x => x.Name == "RegisterStorage" && x.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(string), typeof(string) }));
            addStorageMethod.Invoke(instanceSM, new object[] { "AutomatedWarehouse", "Sklad1" });
            addStorageMethod.Invoke(instanceSM, new object[] { "DistributionCenter", "Sklad2" });


            var storages = (IDictionary<string, Storage>)typeSM.GetField("storageRegistry", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(instanceSM);
            Storage sourceStorage = storages[sourceName];

            Vehicle vehicle = ((Vehicle[])sourceStorage.GetType().BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == "garage").GetValue(sourceStorage))[sourceGarageSlot];

            //must take the garage slot where the vehicle goes to:
            Storage destinationStorage = storages[destinationName];
            Vehicle[] destinationGarage = (destinationStorage.GetType().BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == "garage").GetValue(destinationStorage) as Vehicle[]);

            int slotDestination = destinationGarage.Where(x => x != null).Count();
            string expectedMessage = $"Sent {vehicle.GetType().Name} to {destinationStorage.Name} (slot {slotDestination})";

            MethodInfo sendVehicleToMethod = typeSM.GetMethods(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(x => x.Name == "SendVehicleTo"
                                    && x.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(string), typeof(int), typeof(string) }));

            string resultMessage = (string)sendVehicleToMethod.Invoke(instanceSM, new object[] { sourceName, sourceGarageSlot, destinationName });
            //Return message Check!
            Assert.AreEqual(expectedMessage, resultMessage);

            //check if destination parked vehicle is the vehicle:
            Assert.AreSame(vehicle, destinationGarage[slotDestination], "Vehicle is not set on correct index in the destination garage field!");

            //string expectedMessage= $"Sent {vehicle.GetType().Name} to {destinationName} (slot {destinationGarageSlot})"
        }

    }
}