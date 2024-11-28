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
        Console.WriteLine("Choose an option: ");
        Console.WriteLine("1. Manage Customers: "); // Gör samma som Manage bookings
        Console.WriteLine("2. Manage bookings: ");
        Console.WriteLine("0. Exit");
        AskUser();
    }

    private void AskUser()
    {
        while (true)
        {
            Console.WriteLine("Enter your choice: ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                switch (input)
                {
                    case "1":
                        ManageCustomers();
                        break;
                    case "2":
                        ManageBookings();
                        break;
                    case "0":
                        Console.WriteLine("Exiting... Have a nice day!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        AskUser();
                        break;
                }
            }
           
        }
    }

    public void ManageCustomers()
    {
        Console.WriteLine("Choose an option: ");
        Console.WriteLine("1. Register new customer:  ");
        Console.WriteLine("2. Update existing customer: ");
        Console.WriteLine("0. Exit");

		string input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input))
        {
            switch (input)  
            {
                case "1":
                    System.Console.WriteLine("Register new customer");
                    break;
                case "2":
                    System.Console.WriteLine("Update existing customer");
                    ListAllCustomers();
                    break;
                case "0":
                    System.Console.WriteLine("Exiting... Have a nice day!");
                    break;
                default:
                    System.Console.WriteLine("Invalid choice. Please try again.");
                    ManageCustomers();
                    break;
            }
        }
        
        /*
        Console.WriteLine("Enter Email: ");
        var email = Console.ReadLine();  //  Kommenteras ut till QuerySelectors
        Console.WriteLine("Enter first name: ");
        var firstName = Console.ReadLine(); // Kommenteras ut till QuerySelectors
        Console.WriteLine("Enter last name: ");
        var lastName = Console.ReadLine(); // Kommenteras ut till QuerySelectors
        Console.WriteLine("Enter phone number: ");
        var phoneNumber = Console.ReadLine();  // Kommenteras ut till QuerySelectors

        // Add database logic here if required*/
    }

    public void ManageBookings()
    {
        Console.WriteLine("Choose an option: ");
        Console.WriteLine("1. Add new booking: ");
        Console.WriteLine("2. Change booking: ");
        Console.WriteLine("3. Remove booking: ");
        Console.WriteLine("0. Exit");

        string input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input))
        {
            string email, bookingID; // Variablerna skall vara genom QuerySelector
            switch (input)
            {
                case "1":
                    Console.WriteLine("Enter Email: ");
                   // email = Console.ReadLine();
                    break;
                case "2":
                    Console.WriteLine("Enter Email: ");
                    //email = Console.ReadLine();
                    Console.WriteLine("Enter Booking ID: ");
                    //bookingID = Console.ReadLine();
                    break;
                case "3":
                    Console.WriteLine("Enter Email: ");
                    //email = Console.ReadLine();
                    Console.WriteLine("Enter Booking ID: ");
                    //bookingID = Console.ReadLine();
                    break;
                case "0":
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    ManageBookings();
                    break;
            }
        }
    }
}
