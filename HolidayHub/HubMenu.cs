using Npgsql;

namespace HolidayHub;

public class HubMenu
{
    private readonly QueryHandler _queryHandler;

    public HubMenu(QueryHandler queryHandler)
    {
        _queryHandler = queryHandler;
    }

    public void PrintMenu()
    {
        bool exitProgram = false;
        while (!exitProgram)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════════╗");
            Console.WriteLine("║                     Hub Menu                     ║");
            Console.WriteLine("╚══════════════════════════════════════════════════╝");  
            Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("1. ");
        Console.ResetColor();
        Console.WriteLine("Manage Customers");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("2. ");
        Console.ResetColor();
        Console.WriteLine("Manage Bookings");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("3. ");
        Console.ResetColor();
        Console.WriteLine("List All Customers");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("0. ");
        Console.WriteLine(" Exit");
        Console.ResetColor();


        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Choose an option: ");
        Console.ResetColor();

            string input = Console.ReadLine();


            switch (input)
            {
                case "1":
                    ManageCustomers();
                    break;
                case "2":
                    ManageBookings();
                    break;
                case "3":
                    _queryHandler.ListAllCustomers();
                    break;
                case "0":
                    Console.WriteLine("Exiting... Have a nice day!");
                    exitProgram = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    public void ManageCustomers()
    {
        bool backToMain = false;
        while (!backToMain)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
             Console.Write("\n");
;
            Console.WriteLine("╔════════════════════════╗");
            Console.WriteLine("║    Manage Customers    ║");
            Console.WriteLine("╚════════════════════════╝"); 
            Console.Write("\n"); 
            Console.Write("1. ");
            Console.ResetColor();
            Console.WriteLine("Register new customer");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("2. ");
            Console.ResetColor();
        	Console.WriteLine("Update existing customer");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("0. Returning to Main Menu...");
            Console.ResetColor(); 
            Console.Write("\n");  
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Choose an option: ");
            Console.ResetColor();


            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("\n");
                    Console.WriteLine("Register new customer:");
                    QueryViewer newCustomer = new QueryViewer();

                    Console.Write("Enter first name: ");
                    newCustomer.CustomerDetails.FirstName = Console.ReadLine();

                    Console.Write("Enter last name: ");
                    newCustomer.CustomerDetails.LastName = Console.ReadLine();

                    Console.Write("Enter email: ");
                    newCustomer.CustomerDetails.Email = Console.ReadLine();

                    Console.Write("Enter phone number: ");
                    newCustomer.CustomerDetails.PhoneNr = Console.ReadLine();

                    Console.Write("Enter date of birth (yyyy-MM-dd): ");
                    DateTime dob;
                    while (!DateTime.TryParse(Console.ReadLine(), out dob))
                    {
                        Console.Write("Invalid date format. Please enter again (yyyy-MM-dd): ");
                    }
                    newCustomer.CustomerDetails.DateOfBirth = dob;

                    _queryHandler.RegisterCustomer(newCustomer).Wait(); // Synchronous wait for simplicity
                    Console.WriteLine("New customer has been registered.");
                    break;

                case "2":
                    Console.Write("\n");
                    Console.WriteLine("Update existing customer:");
                    _queryHandler.ListAllCustomers();
                    break;

                case "0":
                    Console.WriteLine("Returning to Main Menu...");
                    backToMain = true;
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    public void ManageBookings()
    {
        bool backToMain = false;
        while (!backToMain)
        {
			 Console.ForegroundColor = ConsoleColor.Blue;
             Console.Write("\n");

            Console.WriteLine("╔════════════════════════╗");
            Console.WriteLine("║    Manage Bookings     ║");
            Console.WriteLine("╚════════════════════════╝");
            Console.Write("\n");  
            Console.Write("1. ");
            Console.ResetColor();            
            Console.WriteLine("Search Rooms");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("2. ");
            Console.ResetColor();
            Console.WriteLine("Add new booking");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("3. ");
            Console.ResetColor();

            Console.WriteLine("Change booking");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("4. ");
            Console.ResetColor();

            Console.WriteLine("Remove booking");
            Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("5. ");
            Console.ResetColor();
            Console.WriteLine("List All Bookings");

			Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("0. Returning to Main Menu...");
            Console.ResetColor(); 
            Console.Write("\n");
  
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Choose an option: ");
            Console.ResetColor(); 

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
 					Console.WriteLine("\n");          		 
     				Console.ForegroundColor = ConsoleColor.Blue;
					Console.WriteLine("\nSearch Rooms:");
            		Console.Write("1. ");
            		Console.ResetColor();
       	 			Console.WriteLine("Search Rooms by Rating");
        
        			Console.ForegroundColor = ConsoleColor.Blue;
        			Console.Write("2. ");
        			Console.ResetColor();
        			Console.WriteLine("Search Rooms by Price");
        
        			Console.ForegroundColor = ConsoleColor.Blue;
        			Console.Write("3. ");
        			Console.ResetColor();
        			Console.WriteLine("Search Rooms by Specifications");
        
        			Console.ForegroundColor = ConsoleColor.Blue;
        			Console.WriteLine("0. Return to Manage Bookings");
                    Console.WriteLine("\n");
            		Console.Write("Choose an option: ");
        			Console.ResetColor();


                    string searchInput = Console.ReadLine();

                    switch (searchInput)
                    {
                        case "1":
                            _queryHandler.SearchAvailableRoomOrderByRating().Wait();
                            break;
                        case "2":
                            _queryHandler.SearchAvailableRoomOrderByPrice().Wait();
                            break;
                        case "3":
                            _queryHandler.SearchBySpecifications();
                            break;
                        case "0":
                            Console.WriteLine("Returning to Manage Bookings...");
                            break;
                        default:
                            Console.WriteLine("Invalid input. Please try again.");
                            break;
                    }
                    break;

                case "2":
                    Console.WriteLine("\nAdding a new booking...");
					_queryHandler.CreateBooking();                
                    break;

                case "3":
                    Console.WriteLine("Enter the Booking ID to search:");
                    string bookingId = Console.ReadLine();
                    QueryViewer booking = _queryHandler.SearchBookingById(bookingId).Result;
                    _queryHandler.UpdateBookingById(booking);
                    break;

                case "4":
                    Console.WriteLine("Enter the Booking ID to remove:");
                    if (int.TryParse(Console.ReadLine(), out int idToRemove))
                    {
                        _queryHandler.RemoveBookingById(idToRemove).Wait();
                    }
                    else
                    {
                        Console.WriteLine("Invalid booking ID. Please try again.");
                    }
                    break;

                case "5":
                    _queryHandler.ListAllBookings().Wait();
                    break;

                case "0":
                    Console.WriteLine("Returning to Main Menu...");
                    backToMain = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
