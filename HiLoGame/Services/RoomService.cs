using HiLoGame.Model;
using HiLoGame.Network.Http;
using HiLoGame.Shared.DTO;
using HiLoGame.Shared.DTO.Http;

namespace HiLoGame.Services
{
    public class RoomService : IServiceModelClient<ResponseModel<RoomDTO>, GameRoom>
    {
        private readonly RoomRequestClient _requestData;

        public RoomService(RoomRequestClient requestData)
        {
            _requestData = requestData;
        }

        public async Task<ResponseModel<RoomDTO>> Create(GameRoom game)
        {
            RoomDTO roomSend = new(game.MisteryNumberMin, game.MisteryNumberMax, game.IsMultiplayer, game.CanJoinPlayer);

            return await _requestData.RegisterRoom(roomSend);
        }

        public async Task<ResponseModel<RoomDTO>> Get(string id)
        {
            var data = await _requestData.GetInfoRoom(id);

            return data;
        }

        public async Task<ResponseModel<RoomDTO>> Update(GameRoom game)
        {
            RoomDTO roomSend = new(game.MisteryNumberMin, game.MisteryNumberMax, game.IsMultiplayer, game.CanJoinPlayer);

            roomSend.SetRoomId(game.RoomId);

            return await _requestData.UpdateRoom(roomSend);
        }
    }
}
