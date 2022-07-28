using HiLoGame.Shared.DTO;
using HiLoGame.Shared.DTO.Http;
using HiLoGame.Tests.InitializeData;
using HILoGame.WebApi.Controllers;
using HILoGame.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HiLoGame.Tests
{
    public class PlayerControllerTest
    {
        internal PlayerTest playerTest { get; set; } = default!;
        internal Mock<IBasePlayerService> mockPlayerService = default!;

        [Fact]
        internal async Task PlayerControllerTest_RegisterNewPlayer_Return_IsNotNull()
        {
            // Arrange
            PlayerDTO newPlayerSend = ArranjeMockPlayerTest_With_New_Player();
            
            mockPlayerService
                .Setup(service => service.Create(It.IsAny<PlayerDTO>()))
                .ReturnsAsync(playerTest.Player)
                .Verifiable();

            var controllerPlayer = new PlayerController(mockPlayerService.Object);

            // Act
            var resultResponse = await controllerPlayer.RegisterPlayer(newPlayerSend);

            // Assert
            Assert.NotNull(resultResponse);
            
            Assert.IsType<OkObjectResult>(resultResponse.Result);
            
            Assert.IsType<ResponseModel<PlayerDTO>>(
                                                ((OkObjectResult)resultResponse.Result!)
                                                .Value);
        }

        [Fact]
        internal async Task PlayerControllerTest_RegisterNewPlayer_Return_Player_Is_Created()
        {
            // Arrange
            PlayerDTO newPlayerSend = ArranjeMockPlayerTest_With_New_Player();

            mockPlayerService
                .Setup(service => service.Create(It.IsAny<PlayerDTO>()))
                .ReturnsAsync(playerTest.Player)
                .Verifiable();

            var controllerPlayer = new PlayerController(mockPlayerService.Object);

            // Act
            var resultResponse = await controllerPlayer.RegisterPlayer(newPlayerSend);

            var resultPlayer = ((ResponseModel<PlayerDTO>)(
                                                           (OkObjectResult)resultResponse?.Result!)
                                                                                    .Value!)
                                                                                    .Data;

            // Assert
            Assert.NotNull(resultResponse);
            Assert.NotNull(resultResponse?.Result);
            Assert.IsType<ActionResult<ResponseModel<PlayerDTO>>>(resultResponse);
            Assert.NotNull(resultPlayer);
            Assert.Equal(resultPlayer, playerTest.Player);
        }


        internal void ArranjeMockPlayerService()
        {
            mockPlayerService = new Mock<IBasePlayerService>();
        }

        internal PlayerDTO ArranjeMockPlayerTest_With_New_Player()
        {
            ArranjeMockPlayerService();

            playerTest = new("Player");

            PlayerDTO playerSendDTO = playerTest.Player;

            playerSendDTO.Id = null;

            return playerSendDTO;
        }
    }
}