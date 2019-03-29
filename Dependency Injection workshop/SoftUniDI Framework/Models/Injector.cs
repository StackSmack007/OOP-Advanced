using SoftUniDI_Framework.Attributes;
using SoftUniDI_Framework.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace SoftUniDI_Framework.Models
{
    public class Injector
    {
        private IModule module;

        public Injector(IModule module)
        {
            this.module = module;
        }

        private bool CheckForFieldInjection<TClass>()
        {
            bool IsthereSome = typeof(TClass).GetFields((BindingFlags)62).Any(f => f.GetCustomAttributes(typeof(Inject), true).Any());
            return IsthereSome;
        }

        private bool CheckForConstructorInjection<TClass>()
        {
            bool IsthereSome = typeof(TClass).GetConstructors(BindingFlags.Public | BindingFlags.Instance).Any(f => f.GetCustomAttributes(typeof(Inject), true).Any();
            return IsthereSome;
        }

        private TClass CreateConstructorInjection<TClass>()
        {
            Type desiredClass = typeof(TClass);
            if (desiredClass is null) return default(TClass);

            ConstructorInfo[] constructors = desiredClass.GetConstructors();

            foreach (ConstructorInfo constructor in constructors)
            {
                if (!CheckForConstructorInjection<TClass>()) continue;

                var inject = (Inject)constructor.GetCustomAttributes(typeof(Inject), true).FirstOrDefault();
                ParameterInfo[] parameterTypes = constructor.GetParameters();
                int i = 0;
                object[] constructorParams = new object[parameterTypes.Length];
                foreach (var parameterType in parameterTypes)
                {
                    var named = parameterType.GetCustomAttribute(typeof(Named));

                    Type dependency = null;

                    if (named == null)
                    {
                        dependency = module.GetMapping(parameterType.ParameterType, inject);
                    }
                    else
                    {
                        dependency = module.GetMapping(parameterType.ParameterType, named);
                    }
                    if (parameterType.ParameterType.IsAssignableFrom(dependency))
                    {
                        object instance = this.module.GetInstance(dependency);
                        if (instance != null)
                        {
                            constructorParams[i++] = instance;
                        }
                        else
                        {
                            instance = Activator.CreateInstance(dependency);
                            constructorParams[i++] = instance;
                            module.SetInstance(parameterType.ParameterType, instance);
                        }
                    }
                }
                    return (TClass)Activator.CreateInstance(desiredClass, constructorParams);
            }
            return default(TClass);
        }


        private TClass CreateFieldInjection<TClass>()
        {
            var desireClass = typeof(TClass);
            var desireClassInstance = this.module.GetInstance(desireClass);

            if (desireClassInstance is null)
            {
                desireClassInstance = Activator.CreateInstance(desireClass);
                module.SetInstance(desireClass, desireClassInstance);
            }
            var fields = desireClass.GetFields((BindingFlags)62);

            foreach (var field in fields)
            {
                if (field.GetCustomAttributes(typeof(Inject), true).Any())
                {
                    var injection = (Inject)field.GetCustomAttributes(typeof(Inject), true).FirstOrDefault();
                    Type dependency = null;

                    var named = field.GetCustomAttribute(typeof(Named), true);
                    var type = field.FieldType;
                    if (named is null)
                    {
                        dependency = module.GetMapping(type, injection);
                    }
                    else
                    {
                        dependency = module.GetMapping(type, named);
                    }
                    if (type.IsAssignableFrom(dependency))
                    {
                        object instance = this.module.GetInstance(dependency);
                        if (instance is null)
                        {
                            instance = Activator.CreateInstance(dependency);
                            module.SetInstance(dependency, instance);
                        }
                        field.SetValue(desireClassInstance, instance);
                    }
                }
            }
            return (TClass)desireClassInstance;
        }

        public TClass Inject<TClass>()
        {
            var hasConstructorAttribute = CheckForConstructorInjection<TClass>();
            var hasFieldAttribute = CheckForFieldInjection<TClass>();

            if (hasConstructorAttribute&&hasFieldAttribute)
            {
                throw new ArgumentException("There must be only field or constructor annotated with Inject attribute");
            }

            if (hasConstructorAttribute)
            {
                return CreateConstructorInjection<TClass>();
            }
            return CreateFieldInjection<TClass>();
        }
    }
}