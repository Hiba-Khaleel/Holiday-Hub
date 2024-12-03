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
     
	 public async void RegisterCustomer() // INSERT
	 {
		
		
		
		
		
		
		
		
		

	 }
	 public async Task SearchAvailableRoomOrderByRating()  // SELECT QueryViewer AvailableRoomByRating
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
		    await using (var cmd = _db.CreateCommand(@"SELECT r.id, h.rating, h.rating, h.city, r.room_type, r.price_per_night, h.hotel_name, h.dist_to_city_center, h.dist_to_beach, r.window_view, r.balcony, r.floor, h.pool, h.night_club, h.kids_zone, h.restaurant, h.gym
               FROM bookings_with_rooms bwr
               JOIN bookings b ON bwr.booking_id = b.id
               JOIN rooms r ON bwr.rooms_id = r.id
               JOIN hotels h ON r.hotel_id = h.id
               WHERE (check_out_date <= '2024-12-01' /*Här behöver vi lägga till datumet som kunden säger är CheckOutDate*/ OR check_in_date >= '2024-12-16' /*Här behöver vi lägga till datumet som kunden säger är CheckInDate*/)
               AND h.city = 'Barcelona' /*DestinationVariable*/
               ORDER BY h.rating"))
                {
	                cmd.Parameters.AddWithValue("$1", checkOutDate");
	                cmd.Parameters.AddWithValue(2, QueryViewer.BookingInformation.CheckInDate);
	                cmd.Parameters.AddWithValue(3, QueryViewer.BookingInformation.Destination);
                
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())                     
                            
                        {
	                        Console.WriteLine(
	                            $"Room ID: {reader.GetInt32(0)} \t " + //fixa numbers
	                            $"Rating: {reader.GetDouble(1)} \t " +
	                            $"City: {reader.GetString(1)} \t " +
	                            $"Room type: {reader.GetString(2)} \t " +
	                            $"Price per night: {reader.GetDouble(3)} \t " +
	                            $"Hotel name: {reader.GetString(4)} \t " +
	                            $"Distance to city center: {reader.GetDouble(5)} \t " +
	                            $"Distance to beach: {reader.GetDouble(6)} \t " +
	                            $"Window view: {reader.GetBoolean(7)} \t " +
	                            $"Balcony: {reader.GetBoolean(8)} \t " +
	                            $"Floor: {reader.GetInt32(9)} \t " +
	                            $"Pool: {reader.GetBoolean(10)} \t " +
	                            $"Night club: {reader.GetBoolean(11)} \t " +
	                            $"Kids Zone: {reader.GetBoolean(12)} \t " +
	                            $"Restaurant: {reader.GetBoolean(13)} \t " +
	                            $"Gym: {reader.GetBoolean(14)} \t " 
	                            );
                        }
                    }
                }
            
		}
		catch
		{
			Console.WriteLine("Something went wrong");
		}
		
		
		
		
		
		
		
		

     }

	 public async void SearchAvailableRoomOrderByPrice() // SELECT 	
	 {
		
		
		
		
		
		
		
		
		

	 }

	 public async void SearchByCustomerSpecifications() // SELECT * FROM customers
	 {
		
		
		
		
		
		
		
		
		

	 }

	 public async void CreateBooking() // For Individuals that hate life :) 
	 {
		
		
		
		
		
		
		
		
		

	 }

	 public async void ListAllBookings() // SELECT * FROM bookings
	 {
		
		
		
		
		
		
		
		
		

	 }

	 public async void SearchBookingById() // SELECT 
	 {
		 Console.WriteLine("Enter the Booking ID to search: ");
		 string? input = Console.ReadLine();

		 if (!int.TryParse(input, out int bookingId))
		 {
			 Console.WriteLine("Invalid Booking ID. Please enter a numeric value.");
			 return;
		 }

		 try
		 {
			 await using (var cmd = _db.CreateCommand("SELECT * FROM bookings WHERE id = $1"))
			 {
				 cmd.Parameters.AddWithValue(bookingId);

				 await using (var reader = await cmd.ExecuteReaderAsync()) 
					// Since we are using a select => ExecuteReaderAsync()
				 {
					 if (await reader.ReadAsync())
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
					 else
					 {
						 Console.WriteLine("No booking found with the provided ID.");
					 }
				 }
			 }
		 }
		 catch (Exception ex)
		 {
			 Console.WriteLine($"An error occurred: {ex.Message}");
		 }
	 }

	 public async void UpdateBookingById() // UPDATE
	 {
		
		
		
		
		
		
		
		
		

	 }

	 public async void RemoveBookingById() // DELETE
	 {
		
		
		
		
		
		
		
		
		

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
}

