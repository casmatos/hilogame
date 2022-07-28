using HiLoGame.Shared.Enums;
using HILoGame.Shared.Models;

namespace HiLoGame.Shared.DTO
{
    public class PlayerDTO : BaseDTO
    {
        public string Name { get; set; }
        public string RoomId { get; set; }
        public int MisteryNumber { get; set; }
        public int Interactions { get; set; } = 0;
        public bool IsHoster { get; set; }
        public ProposalMisteryNumber Proposal { get; set; } = ProposalMisteryNumber.Unknown;

        public PlayerDTO() { }

        public PlayerDTO(string name, string roomId, int interactions, bool isHoster, int misteryNumber = 0, ProposalMisteryNumber proposal = ProposalMisteryNumber.Unknown) : base()
        {
            Name = name;
            RoomId = roomId;
            Interactions = interactions;
            MisteryNumber = misteryNumber;
            IsHoster = isHoster;
            Proposal = proposal;
        }

    }
}