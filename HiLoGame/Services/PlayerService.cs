using HiLoGame.Model;
using HiLoGame.Network.Http;
using HiLoGame.Shared.DTO;
using HiLoGame.Shared.DTO.Http;

namespace HiLoGame.Services
{
    public class PlayerService : IServiceModelClient<ResponseModel<PlayerDTO>, GamePlayer>
    {
        private readonly PlayerRequestClient _requestData;

        public PlayerService(PlayerRequestClient requestData)
        {
            _requestData = requestData;
        }

        public async Task<ResponseModel<PlayerDTO>> Create(GamePlayer player)
        {
            PlayerDTO roomSend = new(player.Name, player.RoomId, player.Interaction, player.IsHoster);

            return await _requestData.RegisterPlayer(roomSend);
        }

        public Task<ResponseModel<PlayerDTO>> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<PlayerDTO>> Update(GamePlayer game)
        {
            throw new NotImplementedException();
        }
    }
}
