using System;
using System.Collections.Generic;
using System.IO;

namespace UFVTrain
{
    public class Station
    {
        public List<Platform> Platforms { get; set; }
        public List<Train> Trains { get; set; }

        public Station(int platformCount)
        {
            Platforms = new List<Platform>();
            for (int i = 1; i <= platformCount; i++)
            {
                Platforms.Add(new Platform("PLAT-" + i.ToString("D2")));
            }
            Trains = new List<Train>();
        }

        public void DisplayStatus()
        {
            Console.WriteLine("\n--------------------- Platforms ---------------------");
            foreach (Platform platform in Platforms)
            {
                if (platform.Status == PlatformStatus.Free)
                {
                    Console.WriteLine(platform.ID + ": Free");
                }
                else
                {
                    string trainId = platform.CurrentTrain != null ? platform.CurrentTrain.ID : "None";
                    Console.WriteLine(platform.ID + ": Occupied by " + trainId + ", docking ticks left: " + platform.RemainingDockingTicks);
                }
            }

            Console.WriteLine("\n--------------------- Trains ---------------------");
            foreach (Train train in Trains)
            {
                Console.WriteLine(train.ID + " | " + train.Type + " | Status: " + train.Status + " | ArrivalTime: " + train.ArrivalTime + " min");
            }
        }

        public void AdvanceTick()
        {
     
            foreach (Train train in Trains)
            {
                train.AdvanceTick(15);
            }

         
            foreach (Train train in Trains)
            {
                if (train.Status == TrainStatus.EnRoute && train.ArrivalTime == 0)
                {
                    Platform freePlatform = null;
                    foreach (Platform platform in Platforms)
                    {
                        if (platform.Status == PlatformStatus.Free)
                        {
                            freePlatform = platform;
                            break;
                        }
                    }
                    if (freePlatform != null)
                    {
                        train.Status = TrainStatus.Docking;
                        freePlatform.AssignTrain(train);
                    }
                    else
                    {
                        train.Status = TrainStatus.Waiting;
                    }
                }
            }

            
            foreach (Train train in Trains)
            {
                if (train.Status == TrainStatus.Waiting)
                {
                    Platform freePlatform = null;
                    foreach (Platform platform in Platforms)
                    {
                        if (platform.Status == PlatformStatus.Free)
                        {
                            freePlatform = platform;
                            break;
                        }
                    }
                    if (freePlatform != null)
                    {
                        train.Status = TrainStatus.Docking;
                        freePlatform.AssignTrain(train);
                    }
                }
            }

     
            foreach (Platform platform in Platforms)
            {
                platform.AdvanceTick();
            }
        }

        public bool AllTrainsDocked()
        {
            foreach (Train train in Trains)
            {
                if (train.Status != TrainStatus.Docked)
                {
                    return false;
                }
            }
            return true;
        }

        public bool LoadTrainsFromFile(string filename)
        {
            try
            {
                string[] lines = File.ReadAllLines(filename);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length < 5) continue;

                    string id = parts[0];
                    int arrivalTime = int.Parse(parts[1]);
                    string type = parts[2].ToLower();

                    if (type == "passenger")
                    {
                        int numberOfCarriages = int.Parse(parts[3]);
                        int capacity = int.Parse(parts[4]);
                        Trains.Add(new PassengerTrain(id, arrivalTime, numberOfCarriages, capacity));
                    }
                    else if (type == "freight")
                    {
                        int maxWeight = int.Parse(parts[3]);
                        string freightType = parts[4];
                        Trains.Add(new FreightTrain(id, arrivalTime, maxWeight, freightType));
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}