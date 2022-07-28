using HiLoGame.Shared.DTO;
using HiLoGame.Shared.DTO.Http;
using HILoGame.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HILoGame.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IBaseRoomService _roomService;

        public RoomController(ILogger<RoomController> logger,
                            IBaseRoomService roomService)
        {
            _logger = logger;
            _roomService = roomService;
        }

        private void AddErrors(ResponseModel<RoomDTO> responseModel, string message)
        {
            List<string> errors = responseModel.Errors is not null ? responseModel.Errors.ToList() : new();
            errors.Add(message);
        }

        [HttpGet("getinforoom")]
        public async Task<ActionResult<ResponseModel<RoomDTO>>> GetInfoRoom(string id)
        {
            ResponseModel<RoomDTO> responseModel = new ResponseModel<RoomDTO>();

            if (string.IsNullOrEmpty(id))
            {
                AddErrors(responseModel, "Room Unknown");
                return BadRequest();
            }

            try
            {
                var room = await _roomService.GetById(id);

                if (room is not null)
                {
                    responseModel.SetData(room);
                }
                else
                {
                    AddErrors(responseModel, "Room Unknown");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                AddErrors(responseModel, "Error on create Room");
            }

            return Ok(responseModel);
        }

        [HttpPost("registerroom")]
        public async Task<ActionResult<ResponseModel<RoomDTO>>> RegisterRoom([FromBody] RoomDTO newRoom)
        {
            if (newRoom is null)
                return BadRequest();

            ResponseModel<RoomDTO> responseModel = new ResponseModel<RoomDTO>();

            try
            {
                var room = await _roomService.Create(newRoom);

                responseModel.SetData(room);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                AddErrors(responseModel, "Error on create Room");
            }

            return Ok(responseModel);
        }

        [HttpPut("updateroom")]
        public async Task<ActionResult<ResponseModel<RoomDTO>>> UpdateRoom([FromBody] RoomDTO updateRoom)
        {
            if (updateRoom is null)
                return BadRequest();

            ResponseModel<RoomDTO> responseModel = new ResponseModel<RoomDTO>();

            try
            {
                await _roomService.Update(updateRoom);

                responseModel.SetData(updateRoom);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                AddErrors(responseModel, "Error on create Room");
            }

            return Ok(responseModel);
        }
    }
}
