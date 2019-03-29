namespace _07InfernoInfinity.Factories
{
    using _07InfernoInfinity.Contracts;
    using System;
    using System.Linq;
    using System.Reflection;

    public class WeaponFactory
    {
        public IWeapon CreateWeapon(string[] info)
        {
            string rarityType = info[0].Split()[0];
            string weaponType = info[0].Split()[1].ToLower();
            string name = info[1];
            var type = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(x => x.Name.ToLower() == weaponType);
            if (type is null) throw new ArgumentException("Invalid Weapon Type!");
            IWeapon newWeapon = (IWeapon)Activator.CreateInstance(type, new object[] { rarityType, name });
            return newWeapon;

        }
    }
}