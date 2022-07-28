using HiLoGame.Shared.DTO;

namespace HiLoGame.Tests.InitializeData
{
    public class RoomTest
    {
        public RoomDTO Room { get; private set; }

        public RoomTest(string name, bool isMultiplayer = false, bool canJoinPlayer = false, int minimumNumber = 0, int maximumNumber = 3, int secretNumber = 1) : base()
        {
            Room = new RoomDTO
            {
                IsMultiplayer = isMultiplayer,
                CanJoinPlayer = canJoinPlayer,
                MaximumNumber = maximumNumber,
                MinimumNumber = minimumNumber,
                SecretNumber = secretNumber
            };
        }

        public RoomTest(string id, string name, bool isMultiplayer = false, bool canJoinPlayer = false, int minimumNumber = 0, int maximumNumber = 3, int secretNumber = 1) 
                                                                                                                                                            : this (name, isMultiplayer, canJoinPlayer, minimumNumber, maximumNumber, secretNumber)
        {
            this.Room.Id = id;
        }

    }
}
