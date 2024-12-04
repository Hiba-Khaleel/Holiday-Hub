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
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. Manage Customers");
            Console.WriteLine("2. Manage Bookings");
            Console.WriteLine("3. List All Customers");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");

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
            Console.WriteLine("\nManage Customers:");
            Console.WriteLine("1. Register new customer");
            Console.WriteLine("2. Update existing customer");
            Console.WriteLine("0. Return to Main Menu");
            Console.Write("Choose an option: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
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
                    Console.WriteLine("Update existing customer:");
                    _queryHandler.ListAllCustomers();
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

    public void ManageBookings()
    {
        bool backToMain = false;
        while (!backToMain)
        {
            Console.WriteLine("\nManage Bookings:");
            Console.WriteLine("1. Search Rooms");
            Console.WriteLine("2. Add new booking");
            Console.WriteLine("3. Change booking");
            Console.WriteLine("4. Remove booking");
            Console.WriteLine("5. List All Bookings");
            Console.WriteLine("0. Return to Main Menu");
            Console.Write("Choose an option: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("\nSearch Rooms:");
                    Console.WriteLine("1. Search Rooms by Rating");
                    Console.WriteLine("2. Search Rooms by Price");
                    Console.WriteLine("3. Search Rooms by Specifications");
                    Console.WriteLine("0. Return to Manage Bookings");
                    Console.Write("Choose an option: ");

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
