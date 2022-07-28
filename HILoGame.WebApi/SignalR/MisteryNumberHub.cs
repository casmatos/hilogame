using HiLoGame.Shared.Enums;
using HILoGame.WebApi.Services;
using HILoGameWebApi.PersistenceData.InMemory;
using Microsoft.AspNetCore.SignalR;

namespace HILoGameWebApi.SignalR
{
    public class MisteryNumberHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
        private readonly IBasePlayerService _playerService;
        private readonly IBaseRoomService _roomService;

        public MisteryNumberHub(IBasePlayerService playerService,
                                IBaseRoomService roomService)
        {
            _playerService = playerService;
            _roomService = roomService;

        }

        public async Task Subscribe(string roomId, string playerId)
        {
            var room = await _roomService.GetById(roomId);
            var player = await _playerService.GetById(playerId);

            var otherPlayers = await _playerService.GetPlayersByFilter(filtro => filtro.RoomId == roomId);

            if (room is not null && playerId is not null)
            {
                await Clients.Caller.SendAsync("Subscribed", false);

                await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

                await Clients.Caller.SendAsync("Subscribed", otherPlayers.Select(player => player.Name));

                await SendPackageToGroupExeptCaller("PlayerSubscribed", roomId, player.Name);
            }
        }

        public async Task StartGame(string roomId, DateTime startGame)
        {
            var room = await _roomService.GetById(roomId);

            if (room is not null)
            {
                await SendPackageToGroupExeptCaller("GameStated", roomId, startGame);
            }
        }

        public async Task TryGuessNumber(string roomId, string playerId, int guessNumber)
        {
            var player = await _playerService.GetById(playerId);
            var room = await _roomService.GetById(roomId);

            await _playerService.IncreseInteractions(player);

            if (room.SecretNumber == guessNumber)
            {
                await Clients.Caller.SendAsync("PlayerWon");

                await SendPackageToGroupExeptCaller("PlayerLoose", roomId, player.Name);
            }
            else
            {
                ProposalMisteryNumber proposal = ProposalMisteryNumber.Unknown;

                if (room.SecretNumber > guessNumber)
                    proposal = ProposalMisteryNumber.High;
                else if (room.SecretNumber < guessNumber)
                {
                    proposal = ProposalMisteryNumber.Low;
                }

                await Clients.Caller.SendAsync("PlayerTryAgain", proposal);
            }
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionPlayer = _connections.Remove(Context.ConnectionId);

            if (connectionPlayer is not null)
            {
                await SendPackageToGroupExeptCaller("PlayerUnsubscribed", connectionPlayer.Group, connectionPlayer.Name);
            }
        }

        internal async Task SendPackageToGroupExeptCaller<T>(string method, string group, T message) =>
            await Clients.GroupExcept(group, new[] { Context.ConnectionId })
                                .SendAsync(method, message);
    }
}
