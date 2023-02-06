using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedAirways.Model
{
    public class Flight
    {
        public Plane Plane { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public Airport DepartureAirport { get; set; }
        public Airport ArrivalAirport { get; set; }
        public List<Ticket> Tickets = new List<Ticket>();
        public static List<Flight> AllFlights = new List<Flight>();

        public Flight(Plane plane, DateTime departure, DateTime arrival, Airport departureAirport, Airport arrivalAirport)
        {
            Plane = plane;
            Departure = departure;
            Arrival = arrival;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            Plane.Flights.Add(this);
            AllFlights.Add(this);
        }
        
        //Prints readable lines for each flight containing flight details, numbers each flight
        public static void PrintAllFlights()
        {
            int i = 1;
            foreach (Flight flight in AllFlights)
            {
                Console.WriteLine($"{i}: {flight.Plane.Serial} ({flight.Plane.Model}) departing {flight.DepartureAirport.Name} at {flight.Departure} and arriving at {flight.ArrivalAirport.Name} at {flight.Arrival}");
                i++;
            }
        }

        //Prints a readable line with flight information for this flight
        public void PrintFlight()
        {
            Console.WriteLine($"{Plane.Serial} ({Plane.Model}) departing {DepartureAirport.Name} at {Departure} and arriving at {ArrivalAirport.Name} at {Arrival}");
        }

        //Prints a list of all available seats in plane format, leaving blank spaces for already occupied seats
        public void PrintSeats()
        {
            int i = 0;
            int x = 0;
            foreach (Seat seat in Plane.Seats)
            {
                if (i == 0)
                {
                    Console.WriteLine();
                } else if (i == 3)
                {
                    Console.Write("   ");
                }
                if (!CheckSeat(seat))
                {
                    Console.Write($"{seat.Id} ");
                } else
                {
                    Console.Write("   ");
                }
                if (x < 9)
                {
                    Console.Write(" ");
                    x++;
                }
                if (i < 5)
                {
                    i++;
                } else
                {
                    i = 0;
                }
            }
        }

        //Checks if a ticket already exists for this seat on this flight, returns true if it is
        public bool CheckSeat(Seat seat)
        {
            bool taken = false;
            foreach (Ticket ticket in Tickets)
            {
                if (seat.Id == ticket.Seat.Id)
                {
                    taken = true;
                }
            }
            return taken;
        }

        public Seat GetSeat(string seat1)
        {
            foreach (Seat seat2 in Plane.Seats)
            {
                if (seat1 == seat2.Id)
                {
                    return seat2;
                }
            }
            return null;
        }
    }
}
