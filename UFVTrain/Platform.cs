using System;

namespace UFVTrain
{
    public enum PlatformStatus
    {
        Free,
        Occupied
    }

    public class Platform
    {
        public string ID { get; set; }
        public PlatformStatus Status { get; set; }
        public Train CurrentTrain { get; set; }
        public int DockingTime { get; set; } = 2; 
        public int RemainingDockingTicks { get; set; } = 0;

        public Platform(string id)
        {
            ID = id;
            Status = PlatformStatus.Free;
            CurrentTrain = null;
        }

        public void AssignTrain(Train train)
        {
            CurrentTrain = train;
            Status = PlatformStatus.Occupied;
            RemainingDockingTicks = DockingTime;
        }

        public void AdvanceTick()
        {
            if (Status == PlatformStatus.Occupied && RemainingDockingTicks > 0)
            {
                RemainingDockingTicks--;
                if (RemainingDockingTicks == 0 && CurrentTrain != null)
                {
                    CurrentTrain.Status = TrainStatus.Docked;
                    CurrentTrain = null;
                    Status = PlatformStatus.Free;
                }
            }
        }
    }
}