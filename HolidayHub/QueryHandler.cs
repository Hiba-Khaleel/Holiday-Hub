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
	 public async void SearchAvailableRoomOrderByRating()  // SELECT
     {
		
		
		
		
		
		
		
		
		

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

	 public async void SearchByCustomerSpecifications() // SELECT * FROM customers
	 {
		
		
		
		
		
		
		
		
		

	 }

	 public async void CreateBooking() // For Individuals that hate life :) 
	 {
		
		
		
		
		
		
		
		
		

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



