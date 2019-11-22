using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NGeoHash;
using System;
using System.IO;
using System.Linq;

namespace TestApp.Mocking
{
    // dotnet add package NGeoHash


    public interface IFileReader
    {
        string ReadAllText(string path);
    }

    

    public class TrackingService
    {
        private readonly IFileReader fileReader;

        public TrackingService(IFileReader fileReader = null)
        {
            this.fileReader = fileReader;
        }

        public Location Get()
        {
            string json = fileReader.ReadAllText("tracking.json");
            try
            {
                Location location = JsonConvert.DeserializeObject<Location>(json);

                if (location == null)
                    throw new ApplicationException("Error parsing the location");

                return location;
            }
            catch(JsonReaderException e)
            {
                throw new ApplicationException("Error parsing the location", e);
            }

        }

        // geohash.org
        public string GetPathAsGeoHash()
        {
            TrackingContext context = new TrackingContext();

            var locations = context.Trackings
                .Where(t => t.ValidGPS)
                .Select(t => t.Location)
                .ToList();

            var path = locations.Select(l => GeoHash.Encode(l.Latitude, l.Longitude));

            return string.Join(",", path);
        }
    }

    public class TrackingContext : DbContext
    {
        public DbSet<Tracking> Trackings { get; set; }
    }

    public class Location
    {
        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString()
        {
            return $"{Latitude} {Longitude}";
        }
    }

    public class Tracking
    {
        public Location Location { get; set; }
        public byte Satellites { get; set; }
        public bool ValidGPS { get; set; }
    }
}
