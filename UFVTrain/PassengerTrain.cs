using System;

namespace UFVTrain
{
    public class PassengerTrain : Train
    {
        public int NumberOfCarriages { get; set; }
        public int Capacity { get; set; }

        public PassengerTrain(string id, int arrivalTime, int numberOfCarriages, int capacity)
            : base(id, arrivalTime, "passenger")
        {
            NumberOfCarriages = numberOfCarriages;
            Capacity = capacity;
        }
    }
}