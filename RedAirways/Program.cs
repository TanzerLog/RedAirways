using System;
using RedAirways.Model;
using RedAirways.Data;

class Program
{
    static void Main(string[] args)
    {
        //Creates initial instances of necessary classes in lieu of database
        InitialData.Load();

        //Welcome message
        Console.WriteLine("Welcome to the Red Airways ticketing system!");

        //Initialises program navigation variables
        string state = "start";
        string input;
        int numberStorer = 0;

        while (true)
        {
            switch (state)
            {
                case "start":
                    Console.WriteLine();
                    Console.WriteLine("Enter 'A' to see a list of flights and to buy tickets.");
                    Console.WriteLine("Enter 'Z' to use admin controls.");
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "A":
                        case "a":
                            state = "flights";
                            break;
                        case "Z":
                        case "z":
                            state = "admin";
                            break;
                        default:
                            Console.WriteLine("Invalid input, please try again.");
                            Console.WriteLine();
                            break;
                    }
                    break;

                case "flights":
                    Console.WriteLine();
                    Console.WriteLine("RED AIRWAYS FLIGHTS:");
                    if (Flight.AllFlights.Count != 0)
                    {
                        Flight.PrintAllFlights();
                    } else
                    {
                        Console.WriteLine("We have either ceased to function as an airline or there is an error with our software as I cannot find any flights.");
                        state = "start";
                        break;
                    }
                    Console.WriteLine("Enter 'A' to purchase a ticket from one of the listed flights or enter 'B' to return to the navigation menu.");
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "A":
                        case "a":
                            state = "purchase";
                            break;
                        case "B":
                        case "b":
                            state = "start";
                            break;
                        default:
                            Console.WriteLine("Invalid input, please try again.");
                            break;
                    }
                    break;

                case "purchase":
                    Console.WriteLine();
                    Console.WriteLine("Please enter the listed number of the flight you wish to purchase a ticket for using an integer (e.g 1, not 'one'),");
                    Console.WriteLine(" or enter 'B' to return to the navigation menu.");
                    input = Console.ReadLine();
                    if (int.TryParse(input, out int result))
                    {
                        if ((result > 0) && (result <= Flight.AllFlights.Count))
                        {
                            state = "ticket";
                            numberStorer = result - 1;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Sorry, this is not a valid flight. Please try again.");
                            Console.WriteLine();
                            Flight.PrintAllFlights();
                            break;
                        }
                    }
                    else
                    {
                        switch (input)
                        {
                            case "B":
                            case "b":
                                state = "start";
                                break;
                            default:
                                Console.WriteLine("Invalid input, please try again.");
                                break;
                        }
                    }
                    break;

                case "ticket":
                    Flight flight = Flight.AllFlights[numberStorer];
                    Console.WriteLine();
                    Console.WriteLine("You have selected the following flight:");
                    flight.PrintFlight();
                    Console.WriteLine();
                    Console.WriteLine("The following seats are available on this flight:");
                    flight.PrintSeats();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Please enter the seat you want as displayed (e.g 'S4') or enter 'B' to return to the navigation menu.");
                    input = Console.ReadLine();
                    var seat = flight.GetSeat(input);
                    if (seat != null)
                    {
                        if (flight.CheckSeat(seat) == false)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Please enter your name:");
                            input = Console.ReadLine();
                            Ticket ticket = new Ticket(input, flight, seat);
                            Console.WriteLine();
                            Console.WriteLine("Thank you for getting a ticket with us, here are the details of your ticket:");
                            ticket.PrintTicket();
                            Console.WriteLine();
                            Console.WriteLine("The price will be $0 as we can't afford a programmer that can set up a payment system yet.");
                            Console.WriteLine();
                            Console.WriteLine("Please enter 'A' to buy another ticket for this flight or 'B' to return to the navigation menu.");
                            input = Console.ReadLine();
                            switch (input)
                            {
                                case "A":
                                case "a":
                                    break;
                                case "B":
                                case "b":
                                    state = "start";
                                    break;
                                default:
                                    Console.WriteLine("Invalid input, returning you to navigation menu.");
                                    state = "start";
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Sorry, that seat is already taken. Please try again.");
                            break;
                        }

                    }
                    else
                    {
                        switch (input)
                        {
                            case "B":
                            case "b":
                                state = "start";
                                break;
                            default:
                                Console.WriteLine("Invalid input, please try again.");
                                break;
                        }
                    }
                    break;


            }
        }
    }
}