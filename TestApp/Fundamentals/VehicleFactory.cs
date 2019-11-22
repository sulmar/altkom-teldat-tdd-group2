using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Fundamentals
{
    public abstract class Platform
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Country { get; set; }
    }



    public class Friend : Platform
    {
        public string Unit { get; set; }
    }

    public class Foe : Platform
    {
      
    }


    // | ICAO | Country | IsFriend

    public static class PlatformFactory
    {
        // https://pl.wikipedia.org/wiki/Oznakowania_statk%C3%B3w_powietrznych
        public static Platform Create(string symbolIdentifier)
        {
            Platform platform;

            bool isFriend = false;

            if (isFriend)
            {
                platform = new Friend();
            }
            else
            {
                platform = new Foe();
            }

            // platform.Country = 

            throw new NotImplementedException();

        }
    }

   
}
