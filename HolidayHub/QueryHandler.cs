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
	 public async Task RegisterCustomer(QueryViewer customer) // INSERT
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
		
		
		
		
		
		
		
		
		

	 }
	 public async void SearchAvailableRoomOrderByRating()  // SELECT
     {
		
		
		
		
		
		
		
		
		

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

