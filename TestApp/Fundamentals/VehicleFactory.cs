﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Fundamentals
{
    public abstract class Vehicle
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class Friend : Vehicle
    {
        public string Unit { get; set; }
    }

    public class Foe : Vehicle
    {
        public string Country { get; set; }
    }

    public static class VehicleFactory
    {
        // https://pl.wikipedia.org/wiki/Oznakowania_statk%C3%B3w_powietrznych
        public static Vehicle Create(string symbolIdentifier)
        {
            // ICAO Code = SP   -> Friend

            // ICAO Code in ( ...  )  -> Foe

            throw new NotSupportedException();

            return new Friend();
        }
    }

   
}
