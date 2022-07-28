using HiLoGame.Model;
using HiLoGame.Services.Session;
using HiLoGame.Shared.DTO;
using HiLoGame.Shared.DTO.Http;
using MudBlazor;
using System.Net.Http.Json;

namespace HiLoGame.Network.Http
{
    public class PlayerRequestClient
    {
        private readonly HttpClient _client;
        private readonly SessionGaming _sessionInfo;
        private readonly ISnackbar _snackbar;

        public PlayerRequestClient(HttpClient client,
                         SessionGaming sessionInfo,
                         ISnackbar snackbar)
        {
            _client = client;
            _sessionInfo = sessionInfo;
            _snackbar = snackbar;
        }

        public async Task<ResponseModel<PlayerDTO>> GetInfoPlayer() =>
            await _client.GetFromJsonAsync<ResponseModel<PlayerDTO>>($"api/player/GetInfoPlayer?id={_sessionInfo.Player.PlayerId}");

        public async Task<ResponseModel<PlayerDTO>> RegisterPlayer(PlayerDTO player)
        {
            var response = await _client.PostAsJsonAsync("api/player/registerplayer", player);
            response.EnsureSuccessStatusCode();
            
            var responseModel = await response.Content.ReadFromJsonAsync<ResponseModel<PlayerDTO>>();

            if (responseModel?.Errors?.Count > 0)
            {
                foreach (var error in responseModel.Errors)
                {
                    _snackbar.Add(error, Severity.Error);
                }
            }

            return responseModel;

        }
    }
}
