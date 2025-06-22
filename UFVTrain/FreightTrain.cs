using System;

namespace UFVTrain
{
    public class FreightTrain : Train
    {
        public int MaxWeight { get; set; } 
        public string FreightType { get; set; }

        public FreightTrain(string id, int arrivalTime, int maxWeight, string freightType)
            : base(id, arrivalTime, "freight")
        {
            MaxWeight = maxWeight;
            FreightType = freightType;
        }
    }
}