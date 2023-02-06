﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedAirways.Model
{
    public class Airport
    {
        public string Name { get; set; }
        public static List<Airport> Airports = new List<Airport>();
        public Airport(string name)
        {
            Name = name;
            Airports.Add(this);
        }

    }
}