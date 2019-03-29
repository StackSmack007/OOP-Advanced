namespace StrategyPattern
{
    using System.Collections.Generic;
    public class NamesComparator : IComparer<Person>
    {

        public int Compare(Person x, Person y)
        {
            int nameLengthDiff = x.Name.Length - y.Name.Length;
            if (nameLengthDiff != 0) return nameLengthDiff;

            int nameCaseDiff = x.Name.ToLower()[0] - y.Name.ToLower()[0];
            return nameCaseDiff;
        }
    }
}