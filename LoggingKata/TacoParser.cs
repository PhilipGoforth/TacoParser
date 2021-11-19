namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            var cells = line.Split(',');

            // If array.Length is less than 3, something went wrong
            if (cells.Length < 3)
            {
                logger.LogInfo("Length not 3");
                return null;
            }

            //the latitude from array at index 0
            var latitude = double.Parse(cells[0]);
            //the longitude from array at index 1
            var longitude = double.Parse(cells[1]);
            //the name from array at index 2
            var name = cells[2];

            //instance of the TacoBell class
            var newTaco = new TacoBell() { Name = name};
            Point location = new Point() { Latitude = latitude, Longitude = longitude};
            newTaco.Location = location;
            
            return newTaco;
            
        }
    }
}