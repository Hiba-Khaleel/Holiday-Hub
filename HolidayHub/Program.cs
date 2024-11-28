namespace HolidayHub;

using Npgsql;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to HolidayHub!");
        // anslut till databasen
        Database database = new();
        // hämta anslutningen (db) att göra queries med
        var db = database.Connection();
        // skapa actions och skicka in anslutningen, så att vi kan köra queries till databasen där
        var queryHandler = new QueryHandler(db);
        // Pass the dependency to HubMenu
        HubMenu menu = new HubMenu(queryHandler); //Lägg till queryHandler i parantesen
        // Display the menu
        menu.PrintMenu();
    }
}