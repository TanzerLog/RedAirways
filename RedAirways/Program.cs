using RedAirways.Model;
using RedAirways.Data;
using System.Globalization;

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

                case "admin":
                    Console.WriteLine();
                    Console.WriteLine("Welcome to the Admin menu. As the login system is not implemented, we are using an honour system.");
                    Console.WriteLine("Please enter 'A' to add a new airport, 'B' to add a new plane, 'C' to add a new flight or 'D' to return to the navigation menu.");
                    Console.WriteLine("You will be able to list all existing instances of each class in their relevant menu.");
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "A":
                        case "a":
                            state = "airportMenu";
                            break;
                        case "B":
                        case "b":
                            state = "planeMenu";
                            break;
                        case "C":
                        case "c":
                            state = "flightMenu";
                            break;
                        case "D":
                        case "d":
                            state = "start";
                            break;
                        default:
                            Console.WriteLine("Invalid input, please try again.");
                            break;
                    }
                    break;

                case "airportMenu":
                    Console.WriteLine();
                    Console.WriteLine("To view a list of existing airports enter 'A', to add a new airport enter 'B' or to return to the admin menu, enter 'C'.");
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "A":
                        case "a":
                            Console.WriteLine();
                            Airport.PrintAirports();
                            break;
                        case "B":
                        case "b":
                            state = "addAirport";
                            break;
                        case "C":
                        case "c":
                            state = "admin";
                            break;
                        default:
                            Console.WriteLine("Invalid input, please try again.");
                            break;
                    }
                    break;

                case "addAirport":
                    Console.WriteLine();
                    Console.WriteLine("Please enter the name of the airport you would like to add or enter no input to back out:");
                    input = Console.ReadLine();
                    if (input != null)
                    {
                        bool match = false;
                        foreach (Airport airport in Airport.Airports)
                        {
                            if (airport.Name == input)
                            {
                                match = true;
                            }
                        }
                        if (!match)
                        {
                            new Airport(input);
                            Console.WriteLine($"{input} has been added to the list of airports.");
                            state = "airportMenu";
                            break;
                        } else
                        {
                            Console.WriteLine("An airport with that name already exists.");
                            state = "airportMenu";
                            break;
                        }
                    }
                    break;

                case "planeMenu":
                    Console.WriteLine();
                    Console.WriteLine("To view a list of all existing planes enter 'A', to add a new plane enter 'B', or to return to the admin menu enter 'C'.");
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "A":
                        case "a":
                            Console.WriteLine();
                            Plane.PrintPlanes();
                            break;
                        case "B":
                        case "b":
                            Console.WriteLine();
                            Console.WriteLine("Enter the unique serial for the new plane:");
                            string serial = Console.ReadLine();
                            if (serial == null)
                            {
                                Console.WriteLine("You must enter a value, try again.");
                                break;
                            } else if (Plane.CheckSerial(serial))
                            {
                                Console.WriteLine("You must enter a unique serial");
                                break;
                            }
                            Console.WriteLine();
                            Console.WriteLine("Enter the model of the plane:");
                            string model = Console.ReadLine();
                            if (model == null)
                            {
                                Console.WriteLine("You must enter a value, try again.");
                                break;
                            }
                            Console.WriteLine();
                            Console.WriteLine("Enter the number of seats as an integer:");
                            input = Console.ReadLine();
                            if (input == null)
                            {
                                Console.WriteLine("You must enter a value, try again.");
                                break;
                            }
                            else if (!int.TryParse(input, out result))
                            {
                                Console.WriteLine("You must enter an integer value, try again.");
                                break;
                            }
                            else
                            {
                                Plane plane = new Plane(serial, model, result);
                                Console.WriteLine();
                                Console.WriteLine($"The following plane was added:");
                                Console.WriteLine($"Serial: {plane.Serial}  Model: {plane.Model}  Number of Seats: {plane.Seats.Count}");
                                break;
                            }
                        case "C":
                        case "c":
                            state = "admin";
                            break;
                        default:
                            Console.WriteLine("Invalid input, please try again.");
                            break;
                    }
                    break;

                case "flightMenu":
                    Console.WriteLine();
                    Console.WriteLine("To view a list of all existing flights enter 'A', to add a new flight enter 'B', or to return to the admin menu enter 'C'.");
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "A":
                        case "a":
                            Flight.PrintAllFlights();
                            break;

                        case "B":
                        case "b":
                            Console.WriteLine("You will now be asked to make a series of choices, being shown the options available when relevant. A new flight cannot start and end at the same airport, and the flight time period cannot overlap with an existing flight for the selected plane.");
                            Console.WriteLine("Please select a plane from this list by entering the initial integer for that plane (e.g 1 not one):");
                            Plane.PrintPlanes();
                            input = Console.ReadLine();
                            if (!int.TryParse(input, out result))
                            {
                                Console.WriteLine("You must enter an integer value matching one of the listed planes, try again.");
                                break;
                            } else if (result < 1 || result > Plane.Planes.Count)
                            {
                                Console.WriteLine("You must choose from one of the listed planes, try again.");
                                break;
                            }
                            Plane plane = Plane.Planes[result - 1];
                            Console.WriteLine();
                            DateTime departure;
                            try
                            {
                                Console.WriteLine("Enter a departure date and time in the format 'yyyy-MM-dd HH:mm:ss':");
                                input = Console.ReadLine();
                                departure = DateTime.ParseExact(input, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("Invalid format. Please enter a valid date and time.");
                                break;
                            }
                            DateTime arrival;
                            try
                            {
                                Console.WriteLine("Enter an arrival date and time in the format 'yyyy-MM-dd HH:mm:ss':");
                                input = Console.ReadLine();
                                arrival = DateTime.ParseExact(input, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("Invalid format. Please enter a valid date and time.");
                                break;
                            }
                            Console.WriteLine();
                            Console.WriteLine("Please select a departure airport using its listed integer value (e.g 1 not one):");
                            Airport.PrintAirports();
                            input = Console.ReadLine();
                            if (!int.TryParse(input, out result))
                            {
                                Console.WriteLine("You must enter an integer value matching one of the listed airports, try again.");
                                break;
                            }
                            else if (result < 1 || result > Airport.Airports.Count)
                            {
                                Console.WriteLine("You must choose from one of the listed airports, try again.");
                                break;
                            }
                            Airport departureAirport = Airport.Airports[result - 1];
                            Console.WriteLine();
                            Console.WriteLine("Please select an arrival airport using its listed integer value (e.g 1 not one), ensuring it is not the same airport as the departure airport:");
                            Airport.PrintAirports();
                            input = Console.ReadLine();
                            if (!int.TryParse(input, out result))
                            {
                                Console.WriteLine("You must enter an integer value matching one of the listed airports, try again.");
                                break;
                            }
                            else if (result < 1 || result > Airport.Airports.Count)
                            {
                                Console.WriteLine("You must choose from one of the listed airports, try again.");
                                break;
                            }
                            Airport arrivalAirport = Airport.Airports[result - 1];
                            try
                            {
                                Flight newFlight = new Flight(plane, departure, arrival, departureAirport, arrivalAirport);
                                Console.WriteLine("You have created the following flight:");
                                newFlight.PrintFlight();
                                break;
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.Write("Please try again.");
                                break;
                            }
                        case "C":
                        case "c":
                            state = "admin";
                            break;
                        default:
                            Console.WriteLine("Invalid input, please try again.");
                            break;
                    }
                    break;
            }       

        }
    }
}