using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HiLoGame.Shared.DTO
{
    public class RoomDTO : BaseDTO
    {
        public bool IsMultiplayer { get; set; }
        public int MinimumNumber { get; set; }
        public int MaximumNumber { get; set; }
        [JsonIgnore]
        public int SecretNumber { get; set; }
        public bool CanJoinPlayer { get; set; } = false;

        public RoomDTO() { }

        public RoomDTO(int minimumNumber, int maximumNumber, bool isMultiplayer, bool canJoinPlayer, int secretNumber = 0) : base()
        {
            MinimumNumber = minimumNumber;
            MaximumNumber = maximumNumber;
            IsMultiplayer = isMultiplayer;
            CanJoinPlayer = canJoinPlayer;

            if (secretNumber != 0)
            {
                SecretNumber = secretNumber;
            }
        }

        public void SetRoomId(string identifier) =>
            Id = identifier;

    }
}
