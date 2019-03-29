namespace WorkForce.Core
{
    using WorkForce.Models;
    using Contracts;
    using Factories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ScheduleMaster
    {
        private EmployeeFactory employeeFactory;
        private List<IEmployee> employees;
        private List<IJob> tasks;
        public ScheduleMaster()
        {
            employees = new List<IEmployee>();
            tasks = new List<IJob>();
            employeeFactory = new EmployeeFactory();
        }

        internal void RegisterJob(string input)
        {
            string[] args = input.Split();
            string name = args[1];
            string employeeName = args[3];
            IEmployee employee = employees.FirstOrDefault(x => x.Name == employeeName);
            int hoursRequired = int.Parse(args[2]);
            IJob newJob = new Job(name, employee, hoursRequired);
            newJob.JobDone += On_Job_Completed;
            tasks.Add(newJob);
        }

        internal void RegisterEmployee(string input)
        {
            IEmployee newEmployee = employeeFactory.CreateEmployee(input);
            employees.Add(newEmployee);
        }

        internal void PassWeek()
        {
            IJob[] jobsTemporary = tasks.ToArray();
           
            foreach (var task in jobsTemporary)
            {
                task.Update();
            }
        }

        internal void Status()
        {
            foreach (var job in tasks)
            {
                Console.WriteLine($"Job: {job.Name} Hours Remaining: {job.HoursRemaining}");
            }
        }

        private void On_Job_Completed(IJob job)
        {
            Console.WriteLine($"Job {job.Name} done!");
            tasks.Remove(job);
        }
    }
}
