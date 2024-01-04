using JsonFlatFileDataStore;
using MongoDB.Driver;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text;
using TestProject.Models;

namespace TestProject.Services;

public interface IUserService
{
    public Task CreateAsync(User user);
    public Task<UpdateResult> UpdateAsync(string id, User user);
    public Task<DeleteResult> DeleteAsync(string id);
    public User GetAsync(string id);
    public List<User> GetAllAsync();
}

public class UserService : IUserService
{
    private readonly IMongoCollection<User> _Collection;

    public UserService(IConfiguration configuration)
    {
        // Get MongoDB database and collection names from ConfigMap
        string databaseName = configuration.GetValue("databaseName", "");
        string collectionName = configuration.GetValue("collectionName", "");

        // Create connection string
        string connectionString = $"mongodb://admin:adminpassword@my-mongodb-mongo-svc:27017/?authSource=admin&authMechanism=SCRAM-SHA-256";


        // Creating MongoClient
        MongoClient client = new MongoClient(connectionString);

        // Get a reference to the database
        var database = client.GetDatabase(databaseName);

        // Get a reference to the collection
        _Collection = database.GetCollection<User>(collectionName);

    }

    public async Task CreateAsync(User user) => await _Collection.InsertOneAsync(user);


    public async Task<DeleteResult> DeleteAsync(string id) => await _Collection.DeleteOneAsync(x => x.Id == id);

    public List<User> GetAllAsync() => _Collection.AsQueryable().AsEnumerable().ToList();

    public User GetAsync(string id) => _Collection.AsQueryable().FirstOrDefault(x => x.Id == id);

    public async Task<UpdateResult> UpdateAsync(string id, User user)
    {
        var update = Builders<User>.Update
                        .Set(u => u.FirstName, user.FirstName)
                        .Set(u => u.LastName, user.LastName)
                        .Set(u => u.Email, user.Email)
                        .Set(u => u.Phone, user.Phone)
                        .Set(u => u.UserName, user.UserName);
        return await _Collection.UpdateOneAsync(x => x.Id == id, update);

    }

}
