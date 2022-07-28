using HiLoGame.Shared.DTO;
using HILoGame.Shared.Models;
using HILoGame.WebApi.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace HILoGame.WebApi.Services
{
    public class PlayerRepository : IBaseRepository<Player>
    {
        private readonly IMongoCollection<Player> _playersCollection;

        public PlayerRepository(IOptions<HiLoDatabaseSettings> hiloStoreDatabaseSettings)
        {
            var client = new MongoClient(hiloStoreDatabaseSettings.Value.ConnectionString);

            var database = client.GetDatabase(hiloStoreDatabaseSettings.Value.DatabaseName);

            _playersCollection = database.GetCollection<Player>(nameof(Player));
        }

        public async Task<IEnumerable<Player>> GetAll() =>
            await _playersCollection.Find(_ => true).ToListAsync();

        public async Task<IEnumerable<Player>> GetAllByFilter(Expression<Func<Player, bool>> filter)
        {
            return await _playersCollection.Find(filter).ToListAsync();
        }

        public async Task<Player> GetById(string id)
        {
            return await _playersCollection.Find(player => player.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Player> GetByFilter(Expression<Func<Player, bool>> filter)
        {
            return await _playersCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Player> Create(Player createRcord)
        {
            await _playersCollection.InsertOneAsync(createRcord);

            return createRcord;
        }

        public async Task Update(string id, Player updateRecord)
        {
            await _playersCollection.ReplaceOneAsync(rec => rec.Id == id, updateRecord);
        }

        public async Task<bool> Remove(string id)
        {
            var deleteResult = await _playersCollection.DeleteOneAsync(rec => rec.Id == id);

            return deleteResult.DeletedCount > 0;
        }

    }
}
