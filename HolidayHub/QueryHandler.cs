using Npgsql;

namespace DefaultNamespace;

public class QueryHandler
{
    NpgsqlDataSource _db;
    
    public QueryHandlar(NpgsqlDataSource db)
    {
        _db = db;
    }
    
     public async void ListAll() { }

    public async void ShowOne(string id) { }

    public async void AddOne(string name, string? slogan) { }

    public async void UpdateOne(string id) { }
    
    public async void DeleteOne(string id) { }
}