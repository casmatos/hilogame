namespace HiLoGame.Model
{
    public class GamePlayer : GameBase
    {
        public string PlayerId { get; set; }
        public string Name { get; set; }
        public bool IsHoster { get; set; }
        public bool IsMultiplayer { get; set; }
        public int MisteryNumber { get; set; } = 0;
        public int Interaction { get; set; } = 0;

        public GamePlayer() { }

        public GamePlayer(string NameOfPlayer, string RoomIdentification, bool isMultiplayer, bool isHoster) : base()
        {
            Name = NameOfPlayer;
            RoomId = RoomIdentification;
            IsMultiplayer = isMultiplayer;
            IsHoster = isHoster;
        }

        public void IncrementInterations() =>
            Interaction++;
    }
}
