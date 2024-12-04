using Npgsql;

namespace HolidayHub;

public class QueryHandler
{
    NpgsqlDataSource _db;
    
     public QueryHandler(NpgsqlDataSource db) 
      {
        _db = db;
      }

     public async void ListAllCustomers()
	{ 
	 	await using (var cmd = _db.CreateCommand("SELECT * FROM customers"))
		await using (var reader = await cmd.ExecuteReaderAsync())
		{
			while (await reader.ReadAsync())
			{
				Console.WriteLine($"id: {reader.GetInt32(0)} \t first_name: {reader.GetString(1)}  \t last_name: {reader.GetString(2)} \t email: {reader.GetString(3)} \t phone_nr: {reader.GetString(4)} \t date_of_birth: {reader.GetDateTime(5)}");//check DateTime Type
			}
		}

	}
     //Metod för register new customer.
	 /*public async Task RegisterCustomer(QueryViewer customer) // INSERT
	 {
		 await using (var cmd = _db.CreateCommand("INSERT INTO customers (first_name, last_name, email, phone_nr, date_of_birth) VALUES ($1, $2, $3, $4, $5)"))
		 {
			 cmd.Parameters.AddWithValue("$1", customer.CustomerDetails.FirstName);
			 cmd.Parameters.AddWithValue("$2", customer.CustomerDetails.LastName);
			 cmd.Parameters.AddWithValue("$3", customer.CustomerDetails.Email);
			 cmd.Parameters.AddWithValue("$4", customer.CustomerDetails.PhoneNr);
			 cmd.Parameters.AddWithValue("$5", customer.CustomerDetails.DateOfBirth);
			 await cmd.ExecuteNonQueryAsync();			 
		 }
		
		
		
		
		
		
		
		
		

	 }*/
	 public async Task RegisterCustomer(QueryViewer customer) // INSERT

	 {

		 try

		 {

			 // Ensure connection is available

			 await using (var cmd = _db.CreateCommand("INSERT INTO customers (first_name, last_name, email, phone_nr, date_of_birth) VALUES ($1, $2, $3, $4, $5)"))

			 {

				 // Add parameters with proper names and values

				 cmd.Parameters.AddWithValue(customer.CustomerDetails.FirstName);
				 cmd.Parameters.AddWithValue(customer.CustomerDetails.LastName);
				 cmd.Parameters.AddWithValue(customer.CustomerDetails.Email);
				 cmd.Parameters.AddWithValue(customer.CustomerDetails.PhoneNr);
				 cmd.Parameters.AddWithValue(customer.CustomerDetails.DateOfBirth);
 
				 // Execute the query and capture the result

				 int rowsAffected = await cmd.ExecuteNonQueryAsync();

				 if (rowsAffected > 0)        
				 {           
					 Console.WriteLine("Customer successfully registered.");
				 }        
				 else        
				 {           
					 Console.WriteLine("Customer registration failed.");
				 }     
			 } 
		 } 
		 catch (Exception ex) 
		 {     
			 Console.WriteLine($"An error occurred while registering the customer: {ex.Message}");
		 }
	 }
	 public async Task SearchAvailableRoomOrderByRating()  // SELECT QueryViewer AvailableRoomByRating 
	 /*Skulle kunna lägga ihop ByRating och ByPrice i en metod som sedan ger valet hur det ska visas.*/
	 {
		 Console.WriteLine("Enter city destination:");
		 string destination = Console.ReadLine();
		 DateTime checkInDate;
		 while (true)
		 {
			 Console.WriteLine("Enter check in date (yyyy-MM-dd):");
			 string input = Console.ReadLine();
			 if (DateTime.TryParse(input, out checkInDate))
			 {
				 break;
			 }
			 else
			 {
				 Console.WriteLine("Invalid format!");
			 }
		 } 
		 DateTime checkOutDate;
		 while (true)
		 {
			 Console.WriteLine("Enter check out date (yyyy-MM-dd):");
			 string input = Console.ReadLine();
			 if (DateTime.TryParse(input, out checkOutDate))
			 {
				 break;
			 }
			 else
			 {
				 Console.WriteLine("Invalid format!");
			 }
		 }
	     Console.WriteLine("Showing available rooms sorted by rating:");
			try
			{
			    await using (var cmd = _db.CreateCommand(@"SELECT DISTINCT r.id, h.rating, h.city, r.room_type, r.price_per_night, h.hotel_name, h.dist_to_city_center, h.dist_to_beach, r.window_view, r.balcony, r.floor, h.pool, h.night_club, h.kids_zone, h.restaurant, h.gym
	               FROM bookings_with_rooms bwr --Bookings_X_Rooms?
	               JOIN bookings b ON bwr.booking_id = b.id
	               JOIN rooms r ON bwr.rooms_id = r.id
	               JOIN hotels h ON r.hotel_id = h.id
	               WHERE NOT (check_in_date < $1 /*Här behöver vi lägga till datumet som kunden säger är CheckInDate*/ AND check_out_date > $2 /*Här behöver vi lägga till datumet som kunden säger är CheckOutDate*/)
	               AND h.city = $3 /*DestinationVariable*/
	               ORDER BY h.rating"))
	                {
		                cmd.Parameters.AddWithValue(checkInDate);
		                cmd.Parameters.AddWithValue(checkOutDate);
		                cmd.Parameters.AddWithValue(destination);
		               
	               
	                    await using (var reader = await cmd.ExecuteReaderAsync())
	                    {
	                        while (await reader.ReadAsync())                     
	                            
	                        {
		                        Console.WriteLine(
		                            $"Room ID: {reader.GetInt32(0)} \t " + 
		                            $"Rating: {reader.GetDouble(1)} \t " +
		                            $"City: {reader.GetString(2)} \t " +
		                            $"Room type: {reader.GetString(3)} \t " +
		                            $"Price per night: {reader.GetDouble(4)} \t " +
		                            $"Hotel name: {reader.GetString(5)} \t " +
		                            $"Distance to city center: {reader.GetDouble(6)} \t " +
		                            $"Distance to beach: {reader.GetDouble(7)} \t " +
		                            $"Window view: {reader.GetBoolean(8)} \t " +
		                            $"Balcony: {reader.GetBoolean(9)} \t " +
		                            $"Floor: {reader.GetInt32(10)} \t " +
		                            $"Pool: {reader.GetBoolean(11)} \t " +
		                            $"Night club: {reader.GetBoolean(12)} \t " +
		                            $"Kids Zone: {reader.GetBoolean(13)} \t " +
		                            $"Restaurant: {reader.GetBoolean(14)} \t " +
		                            $"Gym: {reader.GetBoolean(15)} \n " +
		                            $"{new string('-', 200)}"
		                            );
	                        }
	                    }
	                }
	            
			}
			catch (Exception ex)
			{
				Console.WriteLine("Something went wrong: {ex.Message}");
				Console.WriteLine($"Exceptions Details: {ex}");
			}
			//REturn too menu-- Hur?
     }

	public async Task SearchAvailableRoomOrderByPrice()
	{

		await using (var cmd = _db.CreateCommand("SELECT * FROM rooms ORDER BY price_per_night"))
		{
			await using (var reader = await cmd.ExecuteReaderAsync())
			{
				while (await reader.ReadAsync())

					Console.WriteLine(
						$"id: {reader.GetInt32(0)}\t price_per_night: {reader.GetDouble(3)} \t hotel_id: {reader.GetInt32(1)} \t room_type: {reader.GetString(2)} \t window_view: {reader.GetBoolean(4)}\t balcony: {reader.GetBoolean(5)}\t floor: {reader.GetInt32(6)}");
			}

		}



	 }


public async void SearchBySpecifications()
{
    Console.WriteLine("Enter city destination:");
    string destination = Console.ReadLine();

    Console.WriteLine("Would you like to include a pool? (yes/no):");
    bool includePool = Console.ReadLine().ToLower() == "yes";

    Console.WriteLine("Would you like to include night club? (yes/no):");
    bool includeNightClub = Console.ReadLine().ToLower() == "yes";

    Console.WriteLine("Would you like to include a kids club? (yes/no):");
    bool includeKidsZone = Console.ReadLine().ToLower() == "yes";

    Console.WriteLine("Would you like to include a restaurant? (yes/no):");
    bool includeRestaurant = Console.ReadLine().ToLower() == "yes";

    Console.WriteLine("Showing available rooms based on your specifications:");

    try
    {
        var query = @"SELECT DISTINCT r.id, h.rating, h.city, r.room_type, r.price_per_night, h.hotel_name, 
                      h.dist_to_city_center, h.dist_to_beach, r.window_view, r.balcony, r.floor, h.pool, 
                      h.night_club, h.kids_zone, h.restaurant, h.gym
                      FROM bookings_with_rooms bwr
                      JOIN bookings b ON bwr.booking_id = b.id
                      JOIN rooms r ON bwr.rooms_id = r.id
                      JOIN hotels h ON r.hotel_id = h.id
                      WHERE h.city = $1"; 

        if (includePool) query += " AND h.pool = TRUE";
        if (includeNightClub) query += " AND h.night_club = TRUE";
        if (includeKidsZone) query += " AND h.kids_zone = TRUE";
        if (includeRestaurant) query += " AND h.restaurant = TRUE";

       

        await using (var cmd = _db.CreateCommand(query))
        {
            cmd.Parameters.AddWithValue(destination);

            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Console.WriteLine(
                        $"Room ID: {reader.GetInt32(0)} \t " +
                        $"Rating: {reader.GetDouble(1)} \t " +
                        $"City: {reader.GetString(2)} \t " +
                        $"Room type: {reader.GetString(3)} \t " +
                        $"Price per night: {reader.GetDouble(4)} \t " +
                        $"Hotel name: {reader.GetString(5)} \t " +
                        $"Distance to city center: {reader.GetDouble(6)} \t " +
                        $"Distance to beach: {reader.GetDouble(7)} \t " +
                        $"Window view: {reader.GetBoolean(8)} \t " +
                        $"Balcony: {reader.GetBoolean(9)} \t " +
                        $"Floor: {reader.GetInt32(10)} \t " +
                        $"Pool: {reader.GetBoolean(11)} \t " +
                        $"Night club: {reader.GetBoolean(12)} \t " +
                        $"Kids Zone: {reader.GetBoolean(13)} \t " +
                        $"Restaurant: {reader.GetBoolean(14)} \t " +
                        $"Gym: {reader.GetBoolean(15)} \n " +
                        $"{new string('-', 200)}"
                    );
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Something went wrong: {ex.Message}");
        Console.WriteLine($"Exception Details: {ex}");
    }
}





	 public async Task CreateBooking() // For Individuals that hate life :) #Henke #NO_PAIN_NO_GAIN
    {
        try
        {
            QueryViewer bookingInfo = new QueryViewer();
            
            // Gather basic booking details from the user
            Console.WriteLine("Enter the Customer ID:");
			int custID;
            while (!int.TryParse(Console.ReadLine(), out custID))
            {
                Console.WriteLine("Invalid input. Please enter a valid Customer ID (positive integer).");
            }
			
			bookingInfo.BookingDetails.CustomerId = custID;
			
     
            Console.WriteLine("Enter Check-In Date (yyyy-MM-dd):");
			DateTime checkInDate;
            while (!DateTime.TryParse(Console.ReadLine(), out checkInDate))
            {
                Console.WriteLine("Invalid input. Please enter a valid Check-In Date (format: yyyy-MM-dd).");
            }
			bookingInfo.BookingDetails.CheckInDate = checkInDate;
     
            Console.WriteLine("Enter Check-Out Date (yyyy-MM-dd):");
			DateTime checkOutDate;
            while (!DateTime.TryParse(Console.ReadLine(), out checkOutDate))
            {
                Console.WriteLine("Invalid input. Check-Out Date must be later than Check-In Date (format: yyyy-MM-dd).");
            }
     		bookingInfo.BookingDetails.CheckOutDate = checkOutDate;
			
	        
	        Console.WriteLine("Enter number of guests :");
	        int nrOfGuests;
	        while (!int.TryParse(Console.ReadLine(), out nrOfGuests))
	        {
		        Console.WriteLine("Invalid input. Please enter a valid guest number (positive integer).");
	        }
	        bookingInfo.BookingDetails.NumberOfGuests = nrOfGuests;
	
	        Console.WriteLine("Enter number of adults :");
	        int nrOfAdults;
	        while (!int.TryParse(Console.ReadLine(), out nrOfAdults))
	        {
		        Console.WriteLine("Invalid input. Please enter a valid adult number (positive integer).");
	        }
	        bookingInfo.BookingDetails.NumberOfAdults = nrOfAdults;
	
	        Console.WriteLine("Enter number of children :");
	        int nrOfChildren;
	        while (!int.TryParse(Console.ReadLine(), out nrOfChildren))
	        {
		        Console.WriteLine("Invalid input. Please enter a valid children number (positive integer).");
	        }
	        bookingInfo.BookingDetails.NumberOfChildren = nrOfChildren;

	        bool validBoardType = false;
	        string boardType;
	        while (!validBoardType)
	        {
		        Console.WriteLine("Enter board type (none, half, full) :");
		        boardType = Console.ReadLine();
		        if (boardType != "none" && boardType != "half" && boardType != "full")
		        {
			        Console.WriteLine("Invalid input. Please enter a valid board type.");
		        }
		        else
		        {
			        bookingInfo.BookingDetails.BoardType = boardType;
					validBoardType = true;
		        }
	        }
	
	        Console.WriteLine("Do you want an extra bed? (yes/no): ");
	        bool extraBed = Console.ReadLine().ToLower() == "yes";
	        bookingInfo.BookingDetails.ExtraBed = extraBed;
	        
	        
	        Console.WriteLine("Enter the Room ID:");
	        int roomID;
	        while (!int.TryParse(Console.ReadLine(), out roomID))
	        {
		        Console.WriteLine("Invalid input. Please enter a valid Room ID (positive integer).");
	        }
			
	        bookingInfo.RoomDetails.RoomID = roomID;
	        /*
	        // Fetch available rooms for the selected destination
            var updatedBookingInfo = await GetAvailableRooms(bookingInfo.BookingDetails.CheckInDate, bookingInfo.BookingDetails.CheckOutDate, destination);
     
            if (updatedBookingInfo == null)
            {
                Console.WriteLine("Booking process terminated due to unavailability of rooms.");
                return;
            }
			*/
	        
	        
            // Update the bookingInfo with the selected room
            //bookingInfo.RoomDetails = updatedBookingInfo.RoomDetails;
     
            // Proceed to database insertion
            //await CreateBookingInDatabase(bookingInfo);
            // Using a parameterized query to avoid SQL injection
            await using (var cmd = _db.CreateCommand(@"
            WITH new_booking AS (
                INSERT INTO bookings (
                    customer_id,
                    check_in_date,
                    check_out_date,
                    nr_of_guests,
                    nr_of_adults,
                    nr_of_children,
                    board_type,
                    extra_bed
                )
                VALUES (
                    $1,  -- customer_id
                    $2,  -- check_in_date
                    $3,  -- check_out_date
                    $4,  -- nr_of_guests
                    $5,  -- nr_of_adults
                    $6,  -- nr_of_children
                    $7,  -- board_type
                    $8   -- extra_bed
                )
                RETURNING id  -- Return the generated id
            )
            INSERT INTO bookings_x_rooms (booking_id, rooms_id)
            SELECT new_booking.id, $9 
		    FROM new_booking;  -- Use the id from the WITH clause"))
            {
	            // Add parameters to the command in the same order as the placeholders in the SQL query
	            cmd.Parameters.AddWithValue(bookingInfo.BookingDetails.CustomerId);
	            cmd.Parameters.AddWithValue(bookingInfo.BookingDetails.CheckInDate);
	            cmd.Parameters.AddWithValue(bookingInfo.BookingDetails.CheckOutDate);
	            cmd.Parameters.AddWithValue(bookingInfo.BookingDetails.NumberOfGuests);
	            cmd.Parameters.AddWithValue(bookingInfo.BookingDetails.NumberOfAdults);
	            cmd.Parameters.AddWithValue(bookingInfo.BookingDetails.NumberOfChildren);
	            cmd.Parameters.AddWithValue(bookingInfo.BookingDetails.BoardType);
	            cmd.Parameters.AddWithValue(bookingInfo.BookingDetails.ExtraBed);

	            // Assuming you have room information in the QueryViewer for room ID (booking_x_rooms)
	            // If there's a way to retrieve the room_id, you can use it like this:
	            cmd.Parameters.AddWithValue(bookingInfo.RoomDetails.RoomID); // Or use a different value if needed
	            //cmd.Parameters.AddWithValue(1);  // Replace with the actual room ID, e.g., room 1

	            // Execute the command
	            await cmd.ExecuteNonQueryAsync();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while creating the booking: {ex.Message}");
        }
    }

	public async Task ListAllBookings() // SELECT * FROM bookings
	{

		await using (var cmd = _db.CreateCommand("SELECT * FROM bookings ORDER BY id"))
		await using (var reader = await cmd.ExecuteReaderAsync())
		{

			while (await reader.ReadAsync())
			{
				Console.WriteLine(
					$"Booking ID: {reader.GetInt32(0)} \t " +
					$"Customer ID: {reader.GetInt32(1)} \t " +
					$"Check-In Date: {reader.GetDateTime(2):yyyy-MM-dd} \t " +
					$"Check-Out Date: {reader.GetDateTime(3):yyyy-MM-dd} \t " +
					$"Number of Guests: {reader.GetInt32(4)} \t " +
					$"Number of Adults: {reader.GetInt32(5)} \t " +
					$"Number of Children: {reader.GetInt32(6)} \t " +
					$"Board Type: {reader.GetString(7)} \t " +
					$"Extra Bed: {reader.GetBoolean(8)}");
			}
		}
	}

	public async Task<QueryViewer> SearchBookingById(string bookingId) // SELECT 
	{
		QueryViewer bookingData = new QueryViewer();

		// Convert bookingId (string) to int
		if (int.TryParse(bookingId, out int parsedBookingId))
		{
			await using (var cmd = _db.CreateCommand("SELECT * FROM bookings WHERE id = $1"))
			{
				// Use the parsed integer as the parameter value
				cmd.Parameters.AddWithValue(parsedBookingId);

				await using (var reader = await cmd.ExecuteReaderAsync())
				{
					Console.WriteLine("Query executed. Now reading data...");

					if (await reader.ReadAsync())
					{
						bookingData.BookingDetails.BookingId = reader.GetInt32(reader.GetOrdinal("id"));
						bookingData.BookingDetails.CustomerId = reader.GetInt32(reader.GetOrdinal("customer_id"));
						bookingData.BookingDetails.CheckInDate = reader.GetDateTime(reader.GetOrdinal("check_in_date"));
						bookingData.BookingDetails.CheckOutDate =
							reader.GetDateTime(reader.GetOrdinal("check_out_date"));
						bookingData.BookingDetails.NumberOfGuests = reader.GetInt32(reader.GetOrdinal("nr_of_guests"));
						bookingData.BookingDetails.NumberOfAdults = reader.GetInt32(reader.GetOrdinal("nr_of_adults"));
						bookingData.BookingDetails.NumberOfChildren =
							reader.GetInt32(reader.GetOrdinal("nr_of_children"));
						bookingData.BookingDetails.BoardType = reader.GetString(reader.GetOrdinal("board_type"));
						bookingData.BookingDetails.ExtraBed = reader.GetBoolean(reader.GetOrdinal("extra_bed"));

						Console.WriteLine(
							$"Booking ID: {bookingData.BookingDetails.BookingId} \t " +
							$"Customer ID: {bookingData.BookingDetails.CustomerId} \t " +
							$"Check-In Date: {bookingData.BookingDetails.CheckInDate:yyyy-MM-dd} \t " +
							$"Check-Out Date: {bookingData.BookingDetails.CheckOutDate:yyyy-MM-dd} \t " +
							$"Number of Guests: {bookingData.BookingDetails.NumberOfGuests} \t " +
							$"Number of Adults: {bookingData.BookingDetails.NumberOfAdults} \t " +
							$"Number of Children: {bookingData.BookingDetails.NumberOfChildren} \t " +
							$"Board Type: {bookingData.BookingDetails.BoardType} \t " +
							$"Extra Bed: {bookingData.BookingDetails.ExtraBed}");
					}
					else
					{
						Console.WriteLine("No booking found with the provided ID.");
					}
				}
			}
		}
		else
		{
			Console.WriteLine("Invalid booking ID input. Please enter a valid number.");
		}

		return bookingData;
	}

	public async void UpdateBookingById(QueryViewer bookingData) // UPDATE
	{
		// Update data

		// Ask and update Check-In Date
        Console.WriteLine($"Current Check-In Date: {bookingData.BookingDetails.CheckInDate.ToString("yyyy-MM-dd")}");
        Console.WriteLine("What would you like your new Check-In Date to be? (yyyy-MM-dd)");
        string checkInInput = Console.ReadLine();
        
        if (DateTime.TryParse(checkInInput, out DateTime newCheckInDate))
        {
            bookingData.BookingDetails.CheckInDate = newCheckInDate;
        }
        else
        {
            Console.WriteLine("Invalid date format. The check-in date was not changed.");
        }
    
        // Ask and update Check-Out Date
        Console.WriteLine($"Current Check-Out Date: {bookingData.BookingDetails.CheckOutDate.ToString("yyyy-MM-dd")}");
        Console.WriteLine("What would you like your new Check-Out Date to be? (yyyy-MM-dd)");
        string checkOutInput = Console.ReadLine();
        
        if (DateTime.TryParse(checkOutInput, out DateTime newCheckOutDate))
        {
            bookingData.BookingDetails.CheckOutDate = newCheckOutDate;
        }
        else
        {
            Console.WriteLine("Invalid date format. The check-out date was not changed.");
        }
    
        // Ask and update Number of Guests
        Console.WriteLine($"Current Number of Guests: {bookingData.BookingDetails.NumberOfGuests}");
        Console.WriteLine("What would you like your new Number of Guests to be?");
        string guestsInput = Console.ReadLine();
        
        if (int.TryParse(guestsInput, out int newNumberOfGuests))
        {
            bookingData.BookingDetails.NumberOfGuests = newNumberOfGuests;
        }
        else
        {
            Console.WriteLine("Invalid number. The number of guests was not changed.");
        }
    
        // Ask and update Number of Adults
        Console.WriteLine($"Current Number of Adults: {bookingData.BookingDetails.NumberOfAdults}");
        Console.WriteLine("What would you like your new Number of Adults to be?");
        string adultsInput = Console.ReadLine();
        
        if (int.TryParse(adultsInput, out int newNumberOfAdults))
        {
            bookingData.BookingDetails.NumberOfAdults = newNumberOfAdults;
        }
        else
        {
            Console.WriteLine("Invalid number. The number of adults was not changed.");
        }
    
        // Ask and update Number of Children
        Console.WriteLine($"Current Number of Children: {bookingData.BookingDetails.NumberOfChildren}");
        Console.WriteLine("What would you like your new Number of Children to be?");
        string childrenInput = Console.ReadLine();
        
        if (int.TryParse(childrenInput, out int newNumberOfChildren))
        {
            bookingData.BookingDetails.NumberOfChildren = newNumberOfChildren;
        }
        else
        {
            Console.WriteLine("Invalid number. The number of children was not changed.");
        }
    
        // Ask and update Board Type
        Console.WriteLine($"Current Board Type: {bookingData.BookingDetails.BoardType}");
        Console.WriteLine("What would you like your new Board Type to be?");
        string boardTypeInput = Console.ReadLine();
        
        if (!string.IsNullOrWhiteSpace(boardTypeInput))
        {
            bookingData.BookingDetails.BoardType = boardTypeInput;
        }
        else
        {
            Console.WriteLine("Invalid board type. The board type was not changed.");
        }
    
        // Ask and update Extra Bed
        Console.WriteLine($"Current Extra Bed: {(bookingData.BookingDetails.ExtraBed ? "Yes" : "No")}");
        Console.WriteLine("Would you like an extra bed? (yes/no)");
        string extraBedInput = Console.ReadLine();
        
        if (extraBedInput.ToLower() == "yes")
        {
            bookingData.BookingDetails.ExtraBed = true;
        }
        else if (extraBedInput.ToLower() == "no")
        {
            bookingData.BookingDetails.ExtraBed = false;
        }
        else
        {
            Console.WriteLine("Invalid input. The extra bed status was not changed.");
        }
    
        // Display the updated booking details
        Console.WriteLine("\nUpdated Booking Details:");
        Console.WriteLine($"Check-In Date: {bookingData.BookingDetails.CheckInDate.ToString("yyyy-MM-dd")}");
        Console.WriteLine($"Check-Out Date: {bookingData.BookingDetails.CheckOutDate.ToString("yyyy-MM-dd")}");
        Console.WriteLine($"Number of Guests: {bookingData.BookingDetails.NumberOfGuests}");
        Console.WriteLine($"Number of Adults: {bookingData.BookingDetails.NumberOfAdults}");
        Console.WriteLine($"Number of Children: {bookingData.BookingDetails.NumberOfChildren}");
        Console.WriteLine($"Board Type: {bookingData.BookingDetails.BoardType}");
        Console.WriteLine($"Extra Bed: {(bookingData.BookingDetails.ExtraBed ? "Yes" : "No")}");
		
		
		
		
		
		await using (var cmd = _db.CreateCommand(
			             "UPDATE bookings SET id = $1, customer_id = $2, check_in_date = $3, check_out_date = $4, nr_of_guests = $5, nr_of_adults = $6, nr_of_children = $7, board_type = $8, extra_bed = $9  WHERE id = $1"))
		{
			cmd.Parameters.AddWithValue(bookingData.BookingDetails.BookingId);
			cmd.Parameters.AddWithValue(bookingData.BookingDetails.CustomerId);
			cmd.Parameters.AddWithValue(bookingData.BookingDetails.CheckInDate);
			cmd.Parameters.AddWithValue(bookingData.BookingDetails.CheckOutDate);
			cmd.Parameters.AddWithValue(bookingData.BookingDetails.NumberOfGuests);
			cmd.Parameters.AddWithValue(bookingData.BookingDetails.NumberOfAdults);
			cmd.Parameters.AddWithValue(bookingData.BookingDetails.NumberOfChildren);
			cmd.Parameters.AddWithValue(bookingData.BookingDetails.BoardType);
			cmd.Parameters.AddWithValue(bookingData.BookingDetails.ExtraBed);

			await cmd.ExecuteNonQueryAsync();
		}

	}

	public async Task RemoveBookingById(int bookingId)
	{
		try
		{		// Delete command
			await using (var cmd = _db.CreateCommand("DELETE FROM bookings WHERE id = $1"))
			{
				cmd.Parameters.AddWithValue(bookingId);
				int rowsAffected = await cmd.ExecuteNonQueryAsync();
				if (rowsAffected > 0)
				{
					Console.WriteLine($"Booking with ID {bookingId} was successfully removed.");
				}
				else
				{
					Console.WriteLine($"No booking found with ID {bookingId}. Nothing was removed.");
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred while removing the booking: {ex.Message}");
		}
	}
}

/* public async void FilterCustomerBy()
{

}

 public async void UpdateCustomerInformation()
{

}

  public async void ShowOne(string id)
{
    await using (var cmd = _db.CreateCommand("SELECT * FROM customers WHERE id = $1"))
    {
        cmd.Parameters.AddWithValue(int.Parse(id));

        await using (var reader = await cmd.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                Console.WriteLine(
                    $"id: {reader.GetInt32(0)} \t " +
                    $"first_name: {reader.GetString(1)} \t " +
                    $"last_name: {reader.GetString(2)}\t " +
                    $"email: {reader.GetString(3)} \t " +
                    $"phone_nr: {reader.GetString(4)} \t " +
                    $"date_of_birth: {reader.GetDateTime(5)}"
                );
            }
        }
    }
}




     public async void AddOne(string name, string? slogan) { }

     public async void UpdateOne(string id) { }

     public async void DeleteOne(string id) { }

*/



