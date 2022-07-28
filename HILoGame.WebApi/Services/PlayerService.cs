using AutoMapper;
using HiLoGame.Shared.DTO;
using HILoGame.Shared.Models;
using System.Linq.Expressions;

namespace HILoGame.WebApi.Services
{
    public class PlayerService : IBasePlayerService
    {
        private readonly IBaseRepository<Player> _playerRepository;
        private readonly IMapper _mapper;

        public PlayerService(IBaseRepository<Player> playerRepository,
                            IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        public async Task<PlayerDTO> Create(PlayerDTO createRcord)
        {
            var player = await _playerRepository.Create(
                                                        _mapper.Map<Player>(createRcord));
            return _mapper.Map<PlayerDTO>(player);
        }

        public async Task<IEnumerable<PlayerDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<PlayerDTO>>(
                                        await _playerRepository.GetAll());
        }

        public async Task<PlayerDTO> GetById(string id)
        {
            return _mapper.Map<PlayerDTO>(
                                    await _playerRepository.GetById(id));
        }

        public async Task<bool> Remove(string id)
        {
            return await _playerRepository.Remove(id);
        }

        public async Task Update(PlayerDTO updateRecord)
        {
            await _playerRepository.Update(updateRecord.Id, 
                                    _mapper.Map<Player>(updateRecord));
        }

        public async Task<IEnumerable<PlayerDTO>> GetPlayersByFilter(Expression<Func<Player, bool>> filter)
        {
            var listPlayersDTO = await _playerRepository.GetAllByFilter(filter);

            return _mapper.Map<IEnumerable<PlayerDTO>>(listPlayersDTO);
        }

        public async Task IncreseInteractions(PlayerDTO player)
        {
            player.Interactions++;

            await Update(player);
        }

    }
}
