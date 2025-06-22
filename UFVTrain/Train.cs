using System;

namespace UFVTrain
{
    public enum TrainStatus
    {
        EnRoute,
        Waiting,
        Docking,
        Docked
    }

    public abstract class Train
    {
        public string ID { get; set; }
        public TrainStatus Status { get; set; }
        public int ArrivalTime { get; set; } 
        public string Type { get; set; }

        protected Train(string id, int arrivalTime, string type)
        {
            ID = id;
            ArrivalTime = arrivalTime;
            Type = type;
            Status = TrainStatus.EnRoute;
        }

        public virtual void AdvanceTick(int minutes)
        {
            if (Status == TrainStatus.EnRoute && ArrivalTime > 0)
            {
                ArrivalTime -= minutes;
                if (ArrivalTime < 0) ArrivalTime = 0;
            }
        }
    }
}