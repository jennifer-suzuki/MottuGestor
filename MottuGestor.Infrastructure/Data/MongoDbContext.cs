using MongoDB.Driver;
using MottuGestor.Domain.Entities;

namespace MottuGestor.Infrastructure.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IMongoClient client, string dbName)
    {
        _database = client.GetDatabase(dbName);
    }

    public IMongoCollection<Usuario> Usuarios => _database.GetCollection<Usuario>("Usuarios");
    public IMongoCollection<Moto> Motos => _database.GetCollection<Moto>("Motos");
    public IMongoCollection<Patio> Patios => _database.GetCollection<Patio>("Patios");
}