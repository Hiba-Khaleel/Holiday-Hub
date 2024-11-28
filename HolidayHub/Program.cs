namespace HolidayHub;

using Npgsql;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to HolidayHub!");
        // Ensure QueryHandler is implemented
        Database database = new();
        var db=database.Connection();
         var queryHandler = new QueryHandler(db); 
        
        // Pass the dependency to HubMenu
        HubMenu menu = new HubMenu(queryHandler); //Lägg till queryHandler i parantesen
        
        // Display the menu
        menu.PrintMenu();
    }
}