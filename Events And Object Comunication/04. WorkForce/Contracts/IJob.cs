using System;

namespace WorkForce.Contracts
{
    public delegate void JobHandler(IJob job);
    public interface IJob
    {
        string Name { get; }
        event JobHandler JobDone;
        int HoursRemaining { get; }
        void Update();
    }
}