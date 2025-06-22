using System;

namespace UFVTrain
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("||||||||||||||||||||||||||||||||||||||||||||");
            Console.WriteLine("|                                          |");
            Console.WriteLine("| Welcome to UFV Train Station Simulation! |");
            Console.WriteLine("|                                          |");
            Console.WriteLine("||||||||||||||||||||||||||||||||||||||||||||");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.Write("Enter the number of platfors for the UFV Train Station Simulation: ");
            int platformCount = 0;
            platformCount = int.Parse(Console.ReadLine());

            Station station = new Station(platformCount);

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("|                  Menu                    |");
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("||||||||||||||||||||||||||||||||||||||||||||");
                Console.WriteLine("|                                          |");
                Console.WriteLine("|      1. Load trains from file            |");
                Console.WriteLine("|      2. Start simulation                 |");
                Console.WriteLine("|      3. Display system state             |");
                Console.WriteLine("|      4. Exit                             |");
                Console.WriteLine("|                                          |");
                Console.WriteLine("|      Choose an option:                   |");
                Console.WriteLine("|                                          |");
                Console.WriteLine("||||||||||||||||||||||||||||||||||||||||||||");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");

                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Write("Enter the file name: ");
                        string fileName = Console.ReadLine();
                        int loaded = 0;
                        string basePath = "/Users/alfonsolozano/P1/UFVTrain/UFVTrain/";
                        string fileNameLocation = basePath + fileName;
                         try
                        {
                            using (var reader = new System.IO.StreamReader(fileNameLocation))
                            {
                                string? line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    var train = TrainFactory.CreateTrainFromCsv(line);
                                    if (train != null)
                                    {
                                        station.Trains.Add(train);
                                        loaded++;
                                    }
                                }
                            }
                            Console.WriteLine($"{loaded} trains loaded from {fileNameLocation}.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error loading file: {ex.Message}");
                        }
                        break;

                    case "2":
                        if (station.Trains.Count == 0)
                        {
                            Console.WriteLine("No trains loaded. Please load trains first");
                            break;
                        }
                        Console.WriteLine("Starting simulation...");
                        while (!station.AllTrainsDocked())
                        {
                            station.AdvanceTick();
                            station.DisplayStatus();
                            Console.WriteLine("Press Enter to advance to the next tick");
                            Console.ReadLine();
                        }
                        Console.WriteLine("Simulation complete. All trains are docked");
                        break;

                    case "3":
                        station.DisplayStatus();
                        break;

                    case "4":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Select from 1-4");
                        break;
                }
            }
        }
    }
}