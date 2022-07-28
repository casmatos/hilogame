using HiLoGame.Shared.DTO;

namespace HiLoGame.Tests.InitializeData
{
    public class PlayerTest
    {
        public PlayerDTO Player { get; private set; }

        public PlayerTest(string name, bool isHoster = true, int interations = 0, string? roomId = null)
        {
            Player = new PlayerDTO
            {
                Id = Guid.NewGuid().ToString(),
                Interactions = interations,
                MisteryNumber = 0,
                IsHoster = isHoster,
                Name = name,
                RoomId = roomId
            };
        }
    }
}
