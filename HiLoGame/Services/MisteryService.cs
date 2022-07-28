using HiLoGame.Model;
using HiLoGame.Services.Session;
using HiLoGame.Shared.DTO;
using HiLoGame.Shared.DTO.Http;

namespace HiLoGame.Services
{
    public class MisteryService
    {
        private readonly IServiceModelClient<ResponseModel<PlayerDTO>, GamePlayer> _playerService;
        private readonly IServiceModelClient<ResponseModel<RoomDTO>, GameRoom> _roomService;

        public MisteryService(IServiceModelClient<ResponseModel<PlayerDTO>, GamePlayer> playerService,
                            IServiceModelClient<ResponseModel<RoomDTO>, GameRoom> roomService)
        {
            _playerService = playerService;
            _roomService = roomService;
        }

        public async Task<bool> StartNewMistery(SessionGaming game)
        {
            var roomResponse = await _roomService.Create(game.Room);

            if (roomResponse.Errors is not null && roomResponse.Errors.Count > 0)
                return false;

            if (roomResponse.Data is not null)
            {
                game.Room.SetRoomIdentification(roomResponse.Data.Id);
                game.Player.SetRoomIdentification(roomResponse.Data.Id);

                var playerResponse = await _playerService.Create(game.Player);

                if (playerResponse.Errors is not null && playerResponse.Errors.Count > 0)
                    return false;

                if (playerResponse.Data is not null)
                {
                    game.Player.PlayerId = playerResponse.Data.Id;
                    game.Player.IsHoster = true;
                }
            }

            return true;

        }

        public async Task<bool> JoinNewPlayer(SessionGaming game)
        {
            if (game.Room is null || string.IsNullOrEmpty(game.Room.RoomId))
                return false;
            
            game.Player.RoomId = game.Room.RoomId;

            var playerResponse = await _playerService.Create(game.Player);

            if (playerResponse.Errors is not null && playerResponse.Errors.Count > 0)
                return false;

            if (playerResponse.Data is not null)
            {
                game.Player.IsMultiplayer = game.Room.IsMultiplayer;
                game.Player.PlayerId = playerResponse.Data.Id;
                game.Player.IsHoster = false;
            }

            return true;

        }

        public async Task<GameRoom> GetInformationRoom(string id)
        {
            var roomResponse = await _roomService.Get(id);

            if (roomResponse.Data is not null)
            {
                var room = roomResponse.Data;

                return new GameRoom
                {
                    RoomId = room.Id,
                    IsMultiplayer = room.IsMultiplayer,
                    MisteryNumberMax = room.MaximumNumber,
                    MisteryNumberMin = room.MinimumNumber,
                    CanJoinPlayer = room.CanJoinPlayer
                };
            }

            return default!;
        }

        public async Task CloseRoomToJoin(GameRoom room)
        {
            var roomResponse = await _roomService.Get(room.RoomId);

            if (roomResponse.Data is not null)
            {
                room.CanJoinPlayer = false;
                await _roomService.Update(room);
            }
        }
    }
}
