using AutoMapper;
using HiLoGame.Shared.DTO;
using HILoGame.Shared.Models;

namespace HILoGame.WebApi.Services
{
    public class RoomService : IBaseRoomService
    {
        private readonly IBaseRepository<Room> _roomRepository;
        private readonly IMapper _mapper;

        public RoomService(IBaseRepository<Room> roomRepository,
                            IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<RoomDTO> Create(RoomDTO createRcord)
        {
            Random rnd = new Random();

            var room = _mapper.Map<Room>(createRcord);
            
            room.SecretNumber = rnd.Next(createRcord.MinimumNumber, createRcord.MaximumNumber);

            return _mapper.Map<RoomDTO>(await _roomRepository.Create(room));
        }

        public async Task<IEnumerable<RoomDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<RoomDTO>>(
                                        await _roomRepository.GetAll());
        }

        public async Task<RoomDTO> GetById(string id)
        {
            return _mapper.Map<RoomDTO>(
                                    await _roomRepository.GetById(id));
        }

        public async Task<bool> Remove(string id)
        {
            return await _roomRepository.Remove(id);
        }

        public async Task Update(RoomDTO updateRecord)
        {
            var roomDb = await GetById(updateRecord.Id);

            updateRecord.SecretNumber = roomDb.SecretNumber;

            await _roomRepository.Update(updateRecord.Id,
                                    _mapper.Map<Room>(updateRecord));
        }
    }
}
