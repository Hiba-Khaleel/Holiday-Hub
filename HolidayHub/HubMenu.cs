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
        Console.WriteLine("1. Manage Customers: ");
        Console.WriteLine("2. Manage bookings: ");
        Console.WriteLine("0. Exit");
        AskUser();
    }

    private void AskUser()
    {
        while (true)
        {
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
        Console.WriteLine("0. Return to Main Menu: ");
		Console.WriteLine("Enter your choice: ");

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
                    _queryHandler.ListAllCustomers(); // Objectname.Methodname();
                    break;
                case "0":
                    Console.WriteLine("Returning to Main Menu...");
					PrintMenu();
                    return;
                default:
                    System.Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
        
    }

    public async Task ManageBookings()
    {
        Console.WriteLine("Choose an option: ");
        Console.WriteLine("1. Add new booking: ");
        Console.WriteLine("2. Change booking: ");
        Console.WriteLine("3. Remove booking: ");
        Console.WriteLine("0. Return to Main Menu: ");
		Console.WriteLine("Enter your choice: ");

        string input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input))
        {
            switch (input)
            {
                case "1":
                    break;
                case "2":
					
					Console.WriteLine("Enter the Booking ID to search: ");
					string? idInput = Console.ReadLine();
					QueryViewer bookingData = await _queryHandler.SearchBookingById(idInput);
					_queryHandler.UpdateBookingById(bookingData);

					break;
                case "3":
                    Console.WriteLine("Enter the Booking ID to remove: ");
                    string bookingIdInput = Console.ReadLine();
                    if (int.TryParse(bookingIdInput, out int bookingId))
                    {
                        await _queryHandler.RemoveBookingById(bookingId);
                    }
                    else
                    {
                        Console.WriteLine("Invalid booking ID. Please enter a valid integer.");
                    }
                    break;
                case "0":
                    Console.WriteLine("Returning to Main Menu...");
					PrintMenu();
                    return;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }
}
