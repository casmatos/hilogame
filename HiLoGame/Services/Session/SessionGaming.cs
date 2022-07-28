using HiLoGame.Model;

namespace HiLoGame.Services.Session
{
    public class SessionGaming
    {
        public GamePlayer Player { get; private set; }
        public GameRoom Room { get; private set; }
        public DateTime? DataStart { get; set; }

        public void ResetSession(bool isMultiplayer)
        {
            ResetPlayer();

            Room = new();
            DataStart = null;
            Room.IsMultiplayer = isMultiplayer;
        }

        public void ResetPlayer()
        {
            Player = new();
        }

        public void RegisterRoom(string id, bool isMultiplayer, bool canJoinPlayer)
        {
            Room = new(id, isMultiplayer, canJoinPlayer);
        }

        public void RegisterPlayer(string NameOfPlayer, string RoomIdentification, bool isMultiplayer, bool isHoster)
        {
            Player = new(NameOfPlayer, RoomIdentification, isMultiplayer, isHoster);
        }

        public void SetRoom(GameRoom room) =>
            Room = room;
    }
}
