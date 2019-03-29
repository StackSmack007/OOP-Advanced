namespace WorkForce.Models
{
    public class StandardEmployee : Employee
    {
        private const int InitialWorkHours = 40;
        public StandardEmployee(string name) : base(name, workHoursPerWeek: InitialWorkHours) { }
    }
}