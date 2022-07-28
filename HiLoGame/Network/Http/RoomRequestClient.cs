using HiLoGame.Services.Session;
using HiLoGame.Shared.DTO;
using HiLoGame.Shared.DTO.Http;
using MudBlazor;
using System.Net.Http.Json;

namespace HiLoGame.Network.Http
{
    public class RoomRequestClient
    {
        private readonly HttpClient _client;
        private readonly SessionGaming _sessionInfo;
        private readonly ISnackbar _snackbar;

        public RoomRequestClient(HttpClient client,
                         SessionGaming sessionInfo,
                         ISnackbar snackbar)
        {
            _client = client;
            _sessionInfo = sessionInfo;
            _snackbar = snackbar;
        }

        public async Task<ResponseModel<RoomDTO>> GetInfoRoom(string id)
        {
            var responseModel = await _client.GetFromJsonAsync<ResponseModel<RoomDTO>>($"api/room/getinforoom?id={(id is not null ? id : _sessionInfo.Player.PlayerId)}");

            if (responseModel?.Errors?.Count > 0)
            {
                foreach (var error in responseModel.Errors)
                {
                    _snackbar.Add(error, Severity.Error);
                }
            }

            return responseModel;
        }
            

        public async Task<ResponseModel<RoomDTO>> RegisterRoom(RoomDTO room)
        {
            var response = await _client.PostAsJsonAsync("api/room/registerroom", room);
            response.EnsureSuccessStatusCode();

            var responseModel = await response.Content.ReadFromJsonAsync<ResponseModel<RoomDTO>>();

            if (responseModel?.Errors?.Count > 0)
            {
                foreach (var error in responseModel.Errors)
                {
                    _snackbar.Add(error, Severity.Error);
                }
            }

            return responseModel;
        }

        public async Task<ResponseModel<RoomDTO>> UpdateRoom(RoomDTO room)
        {
            var response = await _client.PutAsJsonAsync("api/room/updateroom", room);
            response.EnsureSuccessStatusCode();

            var responseModel = await response.Content.ReadFromJsonAsync<ResponseModel<RoomDTO>>();

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
