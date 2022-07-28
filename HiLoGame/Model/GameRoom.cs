namespace HiLoGame.Model
{
    public class GameRoom : GameBase
    {
        public int MisteryNumberMin { get; set; }
        public int MisteryNumberMax { get; set; }
        public bool IsMultiplayer { get; set; }
        public bool CanJoinPlayer { get; set; }

        public GameRoom() { }

        public GameRoom(string id, bool isMultiplayer, bool canJoinPlayer) : base()
        {
            RoomId = id;
            IsMultiplayer = isMultiplayer;
            CanJoinPlayer = canJoinPlayer;
        }
    }
}
