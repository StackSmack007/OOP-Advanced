using NUnit.Framework;
using StorageMaster;
using StorageMaster.Entities.Products;
using StorageMaster.Entities.Storage;
using StorageMaster.Entities.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StorageMester.Tests.Structure
{
    [TestFixture]
    public class StructureTests
    {
        [TestCase("Product", "Gpu", "HardDrive", "Ram", "SolidStateDrive")]
        [TestCase("Storage", "Warehouse", "DistributionCenter", "AutomatedWarehouse")]
        [TestCase("Vehicle", "Semi", "Truck", "Van")]
        public void ClassesExist(params string[] typesNames)
        {
            Type[] types = new Type[typesNames.Length];
            Type[] foundTypes = typeof(StartUp).Assembly.GetTypes().Where(x => typesNames.Contains(x.Name)).ToArray();
            string[] namesNotFound = typesNames.Where(x => foundTypes.All(y => y.Name != x)).ToArray();

            Assert.That(foundTypes.Length == typesNames.Length, "Class with name {0} not found", string.Join(", ", namesNotFound));
        }

        [TestCase("Vehicle", "Storage", "Product")]
        public void CheckAbstractClassesSet(params string[] typesNames)
        {
            Type[] types = new Type[typesNames.Length];
            Type[] foundTypes = typeof(StartUp).Assembly.GetTypes().Where(x => typesNames.Contains(x.Name) && x.IsAbstract).ToArray();
            string[] namesNotFound = typesNames.Where(x => foundTypes.All(y => y.Name != x)).ToArray();
            Assert.That(foundTypes.Length == typesNames.Length, "Abstract class with name {0} not found", string.Join(", ", namesNotFound));
        }


        [TestCase("Vehicle", "Semi", "Truck", "Van")]
        [TestCase("Storage", "Warehouse", "DistributionCenter", "AutomatedWarehouse")]
        [TestCase("Product", "Gpu", "HardDrive", "Ram", "SolidStateDrive")]

        public void CheckRelationStatus(string superClassName, params string[] subClassNames)
        {
            Type superClass = typeof(StartUp).Assembly.GetTypes().FirstOrDefault(x => x.Name == superClassName);
            Type[] subClasses = typeof(StartUp).Assembly.GetTypes().Where(x => subClassNames.Contains(x.Name)).ToArray();
            string[] notDerivedClasses = subClasses.Where(x => !x.IsSubclassOf(superClass)).Select(x => x.Name).ToArray();

            Assert.That(notDerivedClasses.Length == 0, "class with name {0} do not inherit {1}", string.Join(", ", notDerivedClasses), superClassName);
        }

        [TestCase("Vehicle", typeof(int))]
        [TestCase("Product", typeof(double), typeof(double))]
        [TestCase("Storage", typeof(string), typeof(int), typeof(int), typeof(IEnumerable<Vehicle>))]

        public void CheckProtectedConstructors(string className, params Type[] parameterTypes)
        {
            Type classType = typeof(StartUp).Assembly.GetTypes().FirstOrDefault(x => x.Name == className);

            var constructors = classType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.IsFamily);

            Assert.NotNull(constructors, "Constructor for class {0} not found!", className);

            var parameters = constructors.FirstOrDefault(x => x.GetParameters().Select(y => y.ParameterType).SequenceEqual(parameterTypes));
            
            Assert.NotNull(parameters, " Constructor with parameters {0} not found!", string.Join(", ", parameterTypes.Select(x => x.Name)));
        }

        [TestCase(new[] { "Price", "Weight", },
            new[] { typeof(double), typeof(double) },
            "Product")]

        [TestCase(new[] { "Name", "Capacity", "GarageSlots", "IsFull", "Garage", "Products" },
            new[] { typeof(string), typeof(int), typeof(int), typeof(bool), typeof(IReadOnlyCollection<Vehicle>), typeof(IReadOnlyCollection<Product>) },
            "Storage")]

        [TestCase(new[] { "Capacity", "Trunk", "IsFull", "IsEmpty" }, new[] { typeof(int), typeof(IReadOnlyCollection<Product>), typeof(bool), typeof(bool) },
            "Vehicle")]

        public void CheckPropertiesOfClassExistAndReturnProperTypes(string[] propNames, Type[] propReturnTypes, string className)
        {
            Assert.AreEqual(propNames.Length, propReturnTypes.Length, "Input Test incorrect arrays of names and types must match length!");
            Type type = typeof(StartUp).Assembly.GetTypes().FirstOrDefault(x => className == x.Name);
            var properties = type.GetProperties();
            for (int i = 0; i < propNames.Length; i++)
            {
                string propertyName = propNames[i];
                PropertyInfo prop = properties.FirstOrDefault(x => x.Name == propertyName);

                Assert.That(prop != null, $"{className} does not have {propertyName} property!");
                Type returnTypeExpected = propReturnTypes[i];
                Type returnTypeActual = prop.PropertyType;
                Assert.AreEqual(returnTypeExpected, returnTypeActual, $"{propertyName} is of type {returnTypeActual.Name} instead of {returnTypeExpected.Name}");
            }

        }

        private class Method
        {
            public Method(string name, Type returnType, params Type[] parameters)
            {
                Name = name;
                ReturnType = returnType;
                Parameters = parameters;
            }
            public string Name { get; }
            public Type ReturnType { get; }
            public Type[] Parameters { get; }
        }

        private readonly Dictionary<string, List<Method>> classesMethodsDataBank = new Dictionary<string, List<Method>>
        {
            ["Vehicle"] = new List<Method>
          {
             new Method("LoadProduct",typeof(void),typeof(Product)),
             new Method("Unload",typeof(Product))
          },
            ["Storage"] = new List<Method>
          {
             new Method("GetVehicle",typeof(Vehicle),typeof(int)),
             new Method("SendVehicleTo",typeof(int),typeof(int),typeof(Storage)),
             new Method("UnloadVehicle",typeof(int),typeof(int))
          },
            ["Product"] = new List<Method> { }
        };


        [TestCase("Vehicle")]
        [TestCase("Storage")]
        public void Test_Methods_In_Classes_Exist_Accept_Appropriate_Parameters_Return_Correct_Type(string className)
        {
            List<Method> methodsRequired = classesMethodsDataBank[className];
            MethodInfo[] methodsExisting = typeof(StartUp).Assembly.GetTypes().FirstOrDefault(x => x.Name == className).GetMethods();
            foreach (Method methodExpected in methodsRequired)
            {
                MethodInfo locatedMetod = null;
                foreach (MethodInfo methodFound in methodsExisting
                    .Where(x => x.Name == methodExpected.Name && x.ReturnType == methodExpected.ReturnType))
                {
                    Type[] FoundParameterTypes = methodFound.GetParameters().Select(x => x.ParameterType).ToArray();
                    Type[] ExpectedParameterTypes = methodExpected.Parameters;

                    if (FoundParameterTypes.SequenceEqual(ExpectedParameterTypes))
                    {
                        locatedMetod = methodFound;
                        break;
                    }
                }
                Assert.NotNull(locatedMetod, $"Public Method with:\nName: {methodExpected.Name}\nReturn Type: {methodExpected.ReturnType.Name}\nParameters: {string.Join(", ", methodExpected.Parameters.Select(x => x.Name))}\n Not Found!");
            }
        }

        [TestCase("Vehicle", "trunk", typeof(List<Product>))]
        [TestCase("Storage", "garage", typeof(Vehicle[]))]
        [TestCase("Storage", "products", typeof(List<Product>))]
        [TestCase("Product", "price", typeof(double))]
        public void CheckPrivateFields(string className, string fieldName, Type fieldType)
        {
            Type type = typeof(StartUp).Assembly.GetTypes().FirstOrDefault(x => x.Name == className);
            FieldInfo fieldFound = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(x => x.Name == fieldName && x.FieldType == fieldType);
            Assert.NotNull(fieldFound, $"Field {fieldName} representing {fieldType.Name} not found!");
        }





    }
}