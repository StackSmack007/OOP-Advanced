namespace WorkForce.Core
{
    using System;
    public class Engine
    {
        private ScheduleMaster scheduler;
        public Engine()
        {
            scheduler = new ScheduleMaster();
        }

        public void Run()
        {
            string input = Console.ReadLine();
            while (input != "End")
            {
                if (input == "Pass Week")
                {
                    scheduler.PassWeek();
                }
                else if (input=="Status")
                {
                    scheduler.Status();
                }
                else if (input.Substring(0,3)=="Job")
                {
                    scheduler.RegisterJob(input);
                }
                else if (input.Contains("Employee "))
                {
                    scheduler.RegisterEmployee(input);
                }
                input = Console.ReadLine();
            }
        }
    }
}