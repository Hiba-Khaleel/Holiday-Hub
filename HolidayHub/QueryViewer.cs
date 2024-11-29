using Npgsql;

namespace HolidayHub;

public class QueryViewer
{

    /// <summary>
    /// För att lättare hålla koll på alla variabler har jag skapat några klasser för respektive informationsområde
    /// 
    /// 
    /// </summary>
        
    

    public class BookingInformation
    {
        // Grundkrav för att söka
        public int BookingId { get; set; }
        public string Destination { get; set; }
        public TimeDate CheckInDate { get; set; } // Kan behöva vara annan data typ - date ??
        public TimeDate CheckOutDate { get; set; }
        
        // Extra 
        public int NumberOfGuests { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public string BoardType { get; set; }
        public bool ExtraBed { get; set; }
    }

    public class BookingXRooms // Behövs denna? 
    {
        public int BookingId { get; set; } // Eller BookingXRoomsID
        public int BookingBookingID { get; set; } // NAMING!?!?
        public int RoomNumber { get; set; }
    }
    public class CustomerInformation
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNr { get; set; }
        public TimeDate DateOfBirth { get; set; }
    }
    public class HotelInformation
    {
        // Grundkrav
        public int HotelID { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string HotelName { get; set; }
        public double Rating { get; set; }        


        // Preferences
        public double DistanceToBeach { get; set; }
        public double DistanceToCityCenter { get; set; }
        public bool Pool { get; set; }
        public bool NightClub { get; set; }
        public bool KidsZone { get; set; }
        public bool Restaurant { get; set; }
        public bool Gym { get; set; }
    }
    public class RoomInformation
    {
        public int RoomID { get; set; }
        public int HotelID { get; set; } // Behövs nog inte?
        public string RoomType { get; set; }    // Kan behöva vara annan data typ - int ??
        public double PricePerNight { get; set; }
        public bool WindowView  { get; set; }
        public bool Balcony { get; set; }
        public int Floor { get; set; }
    }
    
    
    
    /*
    1. Hubmenu -> fråga kunden om id -> QueryHandler
        
    2. 
    public async Task<QueryView> ShowOne(string id)
    {
        await using (var cmd = _db.CreateCommand("SELECT * FROM customers WHERE id = $1"))
        {
            cmd.Parameters.AddWithValue(int.Parse(id));

            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    public Query.... customerView = new QueryView();
                    customerView.CustomerInformation.CustomerID = customerID;
                    customerView.CustomerInformation.FirstName = firstName;
                    
                    
                    
                    return QueryView;

}
            }
        }
    }
    
    3. HubMenu -> Skapa ett obj som är samma obj som QueryHandler
            Ändra kunduppgifter
            customerView.FirstName = input;
            customerView.Email = input;
            customerView.PhoneNr = input;
    
    4. HubMenu -> skickar obj till QueryHandler för att uppdatera Databasen
    
    
    
    alt 2
            
    1. HubMenu - skapar ett nytt obj av QueryView bookingView = new QueryView()
    
    2. HubMenu - fyller bookingView med relevant info. 
    
    3. HubMenu -> skickar obj till QueryHandler för att uppdatera Databasen
    
    */
    
    
    
    
    
    
    

}