namespace HiLoGame.Model
{
    public abstract class GameBase
    {
        public string RoomId { get; set; }

        public void SetRoomIdentification(string identification) =>
            RoomId = identification;
    }
}
