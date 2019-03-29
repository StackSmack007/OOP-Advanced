using System;

namespace Problem_2_Extended_Database
{
    public  interface IPerson: IEquatable<Person>
    {
        string Name { get; }
        long Id { get; }
    }
}
