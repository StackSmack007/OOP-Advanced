namespace WorkForce.Models
{
    public class PartTimeEmployee : Employee
    {
        private const int InitialWorkHours = 20;
        public PartTimeEmployee(string name) : base(name, workHoursPerWeek: InitialWorkHours) { }
    }
}