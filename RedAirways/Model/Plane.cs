using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedAirways.Model
{
    public class Plane
    {
        public string Serial { get; set; }
        public string Model { get; set; }
        public List<Seat> Seats = new List<Seat>();
        public List<Flight> Flights = new List<Flight>();
        public static List<Plane> Planes = new List<Plane>();

        public Plane(string serial, string model, int seats)
        {
            if (seats < 1 || seats > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(seats), "Input must be between 1 and 100");
            }
            else
            {
                Serial = serial;
                Model = model;
                for (int i = 1; i <= seats; i++)
                {
                    var seat = new Seat
                    {
                        Id = $"S{i}"
                    };
                    Seats.Add(seat);
                }
                Planes.Add(this);
            }
        }

        public static void PrintPlanes()
        {
            int i = 1;
            foreach (Plane plane in Planes)
            {   
                Console.WriteLine($"{i}: {plane.Serial}, a {plane.Model} with {plane.Seats.Count} seats");
                i++;
            }
        }

        public void PrintPlaneFlights()
        {
            int i = 1;
            foreach (Flight flight in Flights)
            {
                Console.WriteLine($"{i}: {flight.Plane.Serial} ({flight.Plane.Model}) departing {flight.DepartureAirport.Name} at {flight.Departure} and arriving at {flight.ArrivalAirport.Name} at {flight.Arrival}");
                i++;
            }
        }

        public static bool CheckSerial(string serial)
        {
            bool match = false;
            foreach (Plane plane in Planes)
            {
                if (serial == plane.Serial)
                {
                    match = true;
                }
            }
            return match;
        }
    }
}
