using HiLoGame.Shared.DTO;
using HiLoGame.Shared.DTO.Http;
using HILoGame.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HILoGame.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IBasePlayerService _playerService;

        public PlayerController(IBasePlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost("registerplayer")]
        public async Task<ActionResult<ResponseModel<PlayerDTO>>> RegisterPlayer([FromBody] PlayerDTO newPlayer)
        {
            if (newPlayer is null)
                return BadRequest();

            ResponseModel<PlayerDTO> responseModel = new ResponseModel<PlayerDTO>();

            try
            {
                var player = await _playerService.Create(newPlayer);

                responseModel.SetData(player);

                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
