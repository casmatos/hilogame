using MongoDB.Bson.Serialization.Attributes;

namespace HILoGame.Shared.Models
{
    public class Player : BaseModel
    {
        [BsonElement("Name")]
        [BsonRequired]
        public string Name { get; set; }
        [BsonElement("Number")]
        public int MisteryNumber { get; set; }
        public int Interactions { get; set; } = 0;
        public string RoomId { get; set; }
        public bool IsHoster { get; set; }

    }
}
