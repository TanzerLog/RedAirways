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
    }
}
