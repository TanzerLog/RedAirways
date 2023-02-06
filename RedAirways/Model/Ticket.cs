using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedAirways.Model
{
    public class Ticket
    {
        public string Name { get; set; }
        public Flight Flight { get; set; }
        public Seat Seat { get; set; }

        public Ticket(string name, Flight flight, Seat seat)
        {
            Name = name;
            Flight = flight;
            Seat = seat;
            flight.Tickets.Add(this);
        }

        public void PrintTicket()
        {
            Console.WriteLine($"Name: {Name}, Seat: {Seat.Id}, Flight: {Flight.DepartureAirport.Name} at {Flight.Departure} to {Flight.ArrivalAirport.Name} at {Flight.Arrival}");
        }
    }
}
