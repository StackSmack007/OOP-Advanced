namespace _07InfernoInfinity.Contracts
{
    using Enumerations;
    using System.Collections.Generic;

    public interface IWeapon
    {
        string Name { get; }
        int MinDamage { get; }
        int MaxDamage { get; }
        byte Sockets { get; }
        Rarity Rarity { get; }
        IReadOnlyCollection<IGem> AppliedGems { get; } //Array wrapArround

       // /// <summary>
       // /// Adds a gem into the array! If previous gem exists 
       // /// overwrites it!
       // /// </summary>
       // /// <param name="socketIndex">index of the array</param>
       // /// <param name="gem">IGem instance!</param>
       // void Add(int socketIndex, IGem gem);
       //
       // /// <summary>
       // /// Removes a gem from the array! If no gem at the 
       // /// index nothing happens No exception trown!
       // /// </summary>
       // /// <param name="socketIndex">index of the array</param>
       // void Remove(int socketIndex);
       //
       // //TODO Overwrite string
    }
}