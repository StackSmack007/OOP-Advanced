namespace WorkForce.Models
{
    using Contracts;
    public class Job : IJob
    {
        private IEmployee employee;

        private int hoursOfWorkRequired;
        private int hoursRemaining;

        public Job(string name, IEmployee employee, int hoursOfWorkRequired)
        {
            this.employee = employee;
            this.Name = name;
            this.hoursOfWorkRequired = hoursOfWorkRequired;
            this.hoursRemaining = hoursOfWorkRequired;

        }

        public string Name { get; }

        public int HoursRemaining => hoursRemaining;

        public event JobHandler JobDone;

        public void Update()
        {
            hoursRemaining -= employee.WorkHoursPerWeek;
            if (hoursRemaining<=0) JobDone(this);
        }

    }
}
