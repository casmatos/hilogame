namespace HILoGame.Shared.Models
{
    public class Room : BaseModel
    {
        public int MinimumNumber { get; set; }
        public int MaximumNumber { get; set; }
        public int SecretNumber { get; set; }
        public bool IsMultiplayer { get; set; }
        public bool CanJoinPlayer { get; set; }
    }
}
