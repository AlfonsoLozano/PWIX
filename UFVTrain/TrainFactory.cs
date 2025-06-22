using System;

namespace UFVTrain
{
    public static class TrainFactory
    {
        public static Train? CreateTrainFromCsv(string csvLine)
        {
            
            if (string.IsNullOrWhiteSpace(csvLine))
                return null;

      
            string[] parts = csvLine.Split(',');

         
            if (parts.Length < 5)
                return null;

            string id = parts[0].Trim();
            int arrivalTime;
      
            if (!int.TryParse(parts[1], out arrivalTime))
                return null;

            string type = parts[2].Trim().ToLower();

 
            if (type == "passenger")
            {
                int numberOfCarriages;
                int capacity;
                if (!int.TryParse(parts[3], out numberOfCarriages)) return null;
                if (!int.TryParse(parts[4], out capacity)) return null;
                return new PassengerTrain(id, arrivalTime, numberOfCarriages, capacity);
            }
    
            else if (type == "freight")
            {
                int maxWeight;
                if (!int.TryParse(parts[3], out maxWeight)) return null;
                string freightType = parts[4].Trim();
                return new FreightTrain(id, arrivalTime, maxWeight, freightType);
            }

    
            return null;
        }
    }
}