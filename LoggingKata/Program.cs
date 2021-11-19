using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");

            // use File.ReadAllLines(path) to grab all the lines from csv file
            var lines = File.ReadAllLines(csvPath);
            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of TacoParser class
            var parser = new TacoParser();

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            var locations = lines.Select(parser.Parse).ToArray();

            //`ITrackable` variables with initial values of `null`.used to store two taco bells that are the farthest from each other.
            ITrackable store1 = null;
            ITrackable store2 = null;
            //`double` variable to store the distance
            double distance = 0.0;
            //loop locations to grab each location as the origin 
            for (int i = 0; i < lines.Length; i++)
            {
                GeoCoordinate locA = new GeoCoordinate(locations[i].Location.Latitude, locations[i].Location.Longitude);
                for (int j = 0; j < lines.Length; j++)
                {
                    GeoCoordinate locB = new GeoCoordinate(locations[j].Location.Latitude, locations[j].Location.Longitude);
                    if (locA.GetDistanceTo(locB) > distance)
                    {
                        distance = locA.GetDistanceTo(locB);
                        store1 = locations[i];
                        store2 = locations[j];
                    }
                }
            }
            Console.WriteLine(store2.Name);
            Console.WriteLine(store1.Name);
        }
    }
}
