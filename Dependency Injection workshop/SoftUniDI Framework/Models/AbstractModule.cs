using SoftUniDI_Framework.Attributes;
using SoftUniDI_Framework.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftUniDI_Framework.Models
{
    public abstract class AbstractModule : IModule
    {
        private IDictionary<Type, Dictionary<string, Type>> implementations;//интерфейс{име на класове<->типове класове}
        private IDictionary<Type, object> instances;

        protected AbstractModule(IDictionary<Type, Dictionary<string, Type>> implementations, IDictionary<Type, object> instances)
        {
            this.implementations = implementations;
            this.instances = instances;
        }

        protected void CreateMapping<TInter, TImpl>()
        {
            if (!implementations.ContainsKey(typeof(TInter)))
            {
                implementations[typeof(TInter)] = new Dictionary<string, Type>();
            }
            implementations[typeof(TInter)].Add(typeof(TImpl).Name, typeof(TImpl));
        }

        public Type GetMapping(Type currentIntefrace, object attribute)
        {
            Dictionary<string, Type> currentImplementation = implementations[currentIntefrace];

            Type type = null;

            if (attribute is Inject)
            {
                if (currentImplementation.Count == 1)//if there are more than 1 what happens?
                {
                    type = currentImplementation.First().Value;
                }
                else
                {
                    throw new ArgumentException("No available mapping for class: {0}", currentIntefrace.FullName);
                }
            }
            else if (attribute is Named)
            {
                Named named = attribute as Named;

                string dependencyName = named.Name;
                type = currentImplementation[dependencyName];
            }
            return type;
        }

        public void SetInstance(Type implementation, object instance)
        {
            if (!instances.ContainsKey(implementation))
            {
                instances[implementation] = instance;
            }
        }

        public object GetInstance(Type implementation)
        {
            instances.TryGetValue(implementation, out var result);
            return result;
        }

        public abstract void Configure();

    }
}