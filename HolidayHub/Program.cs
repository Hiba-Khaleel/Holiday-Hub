namespace HolidayHub;

using Npgsql;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to HolidayHub!");
        // Ensure QueryHandler is implemented
        // QueryHandler queryHandler = new QueryHandler(); 
        
        // Pass the dependency to HubMenu
        HubMenu menu = new HubMenu(); //Lägg till queryHandler i parantesen
        
        // Display the menu
        menu.PrintMenu();
    }
}