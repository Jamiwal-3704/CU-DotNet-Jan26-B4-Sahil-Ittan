using MongoDB.Driver;
using VoltGearSystems.Models;
using VoltGearSystems.Settings;

namespace VoltGearSystems.Services
{
        public class LaptopService
        {
            private readonly IMongoCollection<Laptop> _laptops;

            public LaptopService(IConfiguration config)
            {
                var settings = config.GetSection("DatabaseSettings").Get<DatabaseSettings>();

                var client = new MongoClient(settings.ConnectionString);
                var database = client.GetDatabase(settings.DatabaseName);

                _laptops = database.GetCollection<Laptop>(settings.CollectionName);
            }

            public async Task<List<Laptop>> GetAsync()
            {
                return await _laptops.Find(_ => true).ToListAsync();
            }

            public async Task CreateAsync(Laptop newLaptop)
            {
                await _laptops.InsertOneAsync(newLaptop);
            }
    }
}
