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
        Console.WriteLine("Main Menu: ");
        Console.WriteLine("1. Manage Customers: ");
        Console.WriteLine("2. Manage Bookings: ");
        Console.WriteLine("3. List All Customers");
        Console.WriteLine("0. Exit");
        AskUser();
    }

    public void AskUser()
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
					case "3":
						Console.WriteLine("\n");
						_queryHandler.ListAllCustomers();
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
		Console.WriteLine("\n");
        Console.WriteLine("Manage Customers: ");
        Console.WriteLine("1. Register new customer:  ");
        Console.WriteLine("2. Update existing customer: ");
        Console.WriteLine("0. Return to Main Menu: ");
		Console.WriteLine("Choose an option: ");

		string input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input))
        {
            switch (input)  
            {
                case "1":
					Console.WriteLine("\n");
                    System.Console.WriteLine("Register new customer");
                    QueryViewer newCustomer = new QueryViewer();// inititerar QueryViewer(objekt)
					Console.WriteLine("\n");
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
                    
                    /*string dateBirth = Console.ReadLine();
                    
                    if (DateTime.TryParse(inputDate, out DateTime dateOfBirth))
                    { 
                        newCustomer.CustomerDetails.DateOfBirth = DateTime.Parse(dateOfBirth);
                        break;
                        
                    }
                    
                    else{ Console.WriteLine("Invalid format!");}*/
                   					
                    await _queryHandler.RegisterCustomer(newCustomer);
					Console.WriteLine("\n");
                    Console.WriteLine("New customer has been registered");
                    break;
                case "2":
					Console.WriteLine("\n");
                    System.Console.WriteLine("Update existing customer");
                    _queryHandler.ListAllCustomers(); // Objectname.Methodname();
                    break;
                case "0":
                    Console.WriteLine("Returning to Main Menu...");
					Console.WriteLine("\n");
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
		bool backToMain = false;
		while (!backToMain) {
    	Console.WriteLine("\nManage Bookings:");
    	Console.WriteLine("1. Search Rooms:");
    	Console.WriteLine("2. Add new booking:");
    	Console.WriteLine("3. Change booking:");
    	Console.WriteLine("4. Remove booking:");
		Console.WriteLine("5. List All Bookings: ");
    	Console.WriteLine("0. Return to Main Menu:");
    	Console.WriteLine("Choose an option:");

	    string input = Console.ReadLine();

	    if (!string.IsNullOrWhiteSpace(input))
  	 	{
        	switch (input)
        	{
            	case "1": // Search Rooms submenu
                	Console.WriteLine("\nSearch Rooms:");
                	Console.WriteLine("1. Search Rooms by Rating:");
                	Console.WriteLine("2. Search Rooms by Price:");
                	Console.WriteLine("3. Search Rooms by Specifications:");
                	Console.WriteLine("0. Return to Manage Bookings:");
                	Console.WriteLine("Choose an option:");

                	string searchInput = Console.ReadLine();

                	if (!string.IsNullOrWhiteSpace(searchInput))
                	{
                    	switch (searchInput)
                    	{
                        	case "1":
                            	await _queryHandler.SearchAvailableRoomOrderByRating();
                            	break;

                        	case "2":
                            	await _queryHandler.SearchAvailableRoomOrderByPrice();
                            	break;

                        	case "3":
                            	_queryHandler.SearchBySpecifications();
                            	break;

                        	case "0":
                            	Console.WriteLine("Returning to Manage Bookings...");
                            	ManageBookings(); // Go back to ManageBookings menu
                            	break;

                        	default:
                            	Console.WriteLine("Invalid input. Please try again.");
                            	ManageBookings(); // Restart ManageBookings
                            	break;
                    	}
                	}

                	ManageBookings(); // Go back to ManageBookings after Search Rooms submenu
                	break;

            	case "2":
                	Console.WriteLine("\nAdding a new booking...");
                	ManageBookings(); // Go back to ManageBookings after adding a booking
                	break;

            	case "3":
                	Console.WriteLine("\nEnter the Booking ID to search:");
                	string? idInput = Console.ReadLine();
                	QueryViewer bookingData = await _queryHandler.SearchBookingById(idInput);
                	_queryHandler.UpdateBookingById(bookingData);
                	ManageBookings(); // Go back to ManageBookings after updating a booking
                	break;

            	case "4":
                	Console.WriteLine("\nEnter the Booking ID to remove:");
                	string bookingIdInput = Console.ReadLine();
                	if (int.TryParse(bookingIdInput, out int bookingId))
                	{
                    	await _queryHandler.RemoveBookingById(bookingId);
                	}
                	else
                	{
                    	Console.WriteLine("Invalid booking ID. Please enter a valid integer.");
                	}

                	ManageBookings(); // Go back to ManageBookings after removing a booking
                	break;
			
				case "5":
					_queryHandler.ListAllBookings();
					break;
            	case "0":
                	Console.WriteLine("Returning to Main Menu...");
					backToMain = true;
                	break; // Exit ManageBookings and return to the main menu

            	default:
                	Console.WriteLine("Invalid input. Please try again.");
                	ManageBookings(); // Restart ManageBookings on invalid input
                	break;	
				}		
			}
    	}
	}
}
