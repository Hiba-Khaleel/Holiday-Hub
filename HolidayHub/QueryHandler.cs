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
     
	 public async void RegisterCustomer() 
	 {
		
		
		
		
		
		
		
		
		

	 }
	 public async void SearchAvailableRoomOrderByRating() 
     {
		
		
		
		
		
		
		
		
		

     }

	 public async void SearchAvailableRoomOrderByPrice() 
	 {
		
		
		
		
		
		
		
		
		

	 }

	 public async void SearchByCustomerSpecifications() 
	 {
		
		
		
		
		
		
		
		
		

	 }

	 public async void CreateBooking() 
	 {
		
		
		
		
		
		
		
		
		

	 }

	 public async void ListAllBookings() 
	 {
		
		
		
		
		
		
		
		
		

	 }

	 public async void SearchBookingById() 
	 {
		
		
		
		
		
		
		
		
		

	 }

	 public async void UpdateBookingById() 
	 {
		
		
		
		
		
		
		
		
		

	 }

	 public async void RemoveBookingById() 
	 {
		
		
		
		
		
		
		
		
		

	 }
	
	/* public async void FilterCustomerBy() 
	{
	
	} 
	
	 public async void UpdateCustomerInformation() 
	{
		
	}
     public async void ShowOne(string id) { }

     public async void AddOne(string name, string? slogan) { }

     public async void UpdateOne(string id) { }

     public async void DeleteOne(string id) { }

*/
}