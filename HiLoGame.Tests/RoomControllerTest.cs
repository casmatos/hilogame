using HiLoGame.Shared.DTO;
using HiLoGame.Shared.DTO.Http;
using HiLoGame.Tests.InitializeData;
using HILoGame.WebApi.Controllers;
using HILoGame.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiLoGame.Tests
{
    public class RoomControllerTest
    {
        internal RoomTest roomTest { get; set; } = default!;
        internal Mock<IBaseRoomService> mockRoomService = default!;

        [Fact]
        internal async Task RoomControllerTest_GetInfoRoom_Return_IsNotNull_IsNotTypeOf_PlayerDTO_Is_Type_RoomDTO()
        {
            // Arrange
            var _ = ArranjeMockRoomTest_With_New_Room();

            mockRoomService
                .Setup(service => service.GetById(It.IsAny<string>()))
                .ReturnsAsync(roomTest.Room)
                .Verifiable();

            var controllerRoom = new RoomController(null, mockRoomService.Object);

            // Act
            var resultResponse = await controllerRoom.GetInfoRoom("1212");

            // Assert
            Assert.NotNull(resultResponse);

            Assert.IsType<OkObjectResult>(resultResponse.Result);

            Assert.IsNotType<ResponseModel<PlayerDTO>>(
                                                    ((OkObjectResult)resultResponse.Result!)
                                                    .Value);

            Assert.IsType<ResponseModel<RoomDTO>>(
                                                ((OkObjectResult)resultResponse.Result!)
                                                .Value);
        }

        [Fact]
        internal async Task RoomControllerTest_RegisterRoom_Return_IsNotNull_IsNotTypeOf_RoomDTO()
        {
            // Arrange
            var newRoomSend = ArranjeMockRoomTest_With_New_Room();

            mockRoomService
                .Setup(service => service.Create(It.IsAny<RoomDTO>()))
                .ReturnsAsync(roomTest.Room)
                .Verifiable();

            var controllerRoom = new RoomController(null, mockRoomService.Object);

            // Act
            var resultResponse = await controllerRoom.RegisterRoom(newRoomSend);

            // Assert
            Assert.NotNull(resultResponse);

            Assert.IsType<OkObjectResult>(resultResponse.Result);

            Assert.IsType<ResponseModel<RoomDTO>>(
                                                ((OkObjectResult)resultResponse.Result!)
                                                .Value);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        internal async Task RoomControllerTest_UpdateRoom_Is_Multiplayer(bool isMultiplayer)
        {
            // Arrange
            var updateRoomSend = ArranjeMockRoomTest_With_New_Room();
            updateRoomSend.IsMultiplayer = isMultiplayer;

            mockRoomService
                .Setup(service => service.Create(It.IsAny<RoomDTO>()))
                .ReturnsAsync(roomTest.Room)
                .Verifiable();

            var controllerRoom = new RoomController(null, mockRoomService.Object);

            // Act
            var resultResponse = await controllerRoom.UpdateRoom(updateRoomSend);
            var responseModelRoom = ((ResponseModel<RoomDTO>)
                                    (((OkObjectResult)resultResponse.Result!)
                                    .Value)!);

            var resultRoomUpdated = responseModelRoom?.Data;

            // Assert
            Assert.NotNull(responseModelRoom);
            Assert.NotNull(resultRoomUpdated);
            Assert.Equal(resultRoomUpdated!.IsMultiplayer, isMultiplayer);
        }

        internal void ArranjeMockRoomService()
        {
            mockRoomService = new Mock<IBaseRoomService>();
        }

        internal RoomDTO ArranjeMockRoomTest_With_New_Room(int roomNumber = 1)
        {
            ArranjeMockRoomService();

            roomTest = new($"Room {roomNumber}");

            RoomDTO newRoom = roomTest.Room;

            return newRoom;
        }

        internal RoomDTO ArranjeMockRoomTest_With_New_Room_With_Identification(string id, int roomNumber = 1)
        {
            ArranjeMockRoomService();

            roomTest = new(id ,$"Room {roomNumber}");

            RoomDTO newRoom = roomTest.Room;

            return newRoom;
        }

    }
}
