using HiLoGame.Shared.DTO;
using HILoGame.Shared.Models;
using HILoGame.WebApi.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace HILoGame.WebApi.Services
{
    public class RoomRepository : IBaseRepository<Room>
    {
        private readonly IMongoCollection<Room> _roomCollection;

        public RoomRepository(IOptions<HiLoDatabaseSettings> hiloStoreDatabaseSettings)
        {
            var client = new MongoClient(hiloStoreDatabaseSettings.Value.ConnectionString);

            var database = client.GetDatabase(hiloStoreDatabaseSettings.Value.DatabaseName);

            _roomCollection = database.GetCollection<Room>(nameof(Room));
        }

        public async Task<IEnumerable<Room>> GetAll() =>
            await _roomCollection.Find(_ => true).ToListAsync();

        public async Task<IEnumerable<Room>> GetAllByFilter(Expression<Func<Room, bool>> filter)
        {
            return await _roomCollection.Find(filter).ToListAsync();
        }

        public async Task<Room> GetById(string id)
        {
            return await _roomCollection.Find(room => room.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Room> GetByFilter(Expression<Func<Room, bool>> filter)
        {
            return await _roomCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Room> Create(Room createRcord)
        {
            await _roomCollection.InsertOneAsync(createRcord);

            return createRcord;
        }

        public async Task Update(string id, Room updateRecord)
        {
            await _roomCollection.ReplaceOneAsync(rec => rec.Id == updateRecord.Id, updateRecord);
        }

        public async Task<bool> Remove(string id)
        {
            var deleteResult = await _roomCollection.DeleteOneAsync(rec => rec.Id == id);

            return deleteResult.DeletedCount > 0;
        }
    }
}
