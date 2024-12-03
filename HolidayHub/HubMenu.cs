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
                        break;
                }
            }
           
        }
    }

    public async Task ManageCustomers()
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
                    QueryViewer newCustomer = new QueryViewer();// inititerar QueryViewer(objekt)
                    Console.WriteLine("Enter first name:");
                    string firstName = Console.ReadLine();

                    Console.WriteLine("Enter last name:");
                    string lastName = Console.ReadLine();

                    Console.WriteLine("Enter email:");
                    string email = Console.ReadLine();

                    Console.WriteLine("Enter phone number:");
                    string phoneNr = Console.ReadLine();

                    Console.WriteLine("enter date of birth: ");
                    DateTime inputDate = DateTime.Parse (Console.ReadLine());
                    
                    newCustomer.CustomerDetails.FirstName = firstName;
                    newCustomer.CustomerDetails.LastName = lastName;
                    newCustomer.CustomerDetails.Email = email;
                    newCustomer.CustomerDetails.PhoneNr = phoneNr;
                    newCustomer.CustomerDetails.DateOfBirth = inputDate;
                    
                   
                    //string dateBirth = Console.ReadLine();
                    
                    /*if (DateTime.TryParse(inputDate, out DateTime dateOfBirth))
                    { 
                        newCustomer.CustomerDetails.DateOfBirth = DateTime.Parse(dateOfBirth);
                        break;
                        
                    }
                    
                    else{ Console.WriteLine("Invalid format!");}*/
                    
                    
					
                    await _queryHandler.RegisterCustomer(newCustomer);
                    System.Console.WriteLine("New customer has been registered");
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

    public void ManageBookings()
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
					_queryHandler.SearchBookingById(); // Objectname.Methodname();
                    break;
                case "3":
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
