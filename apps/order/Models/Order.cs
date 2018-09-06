namespace Order.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Order
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string EmailAddress { get; set; }

        public string PreferredLanguage { get; set; }

        public string Product { get; set; }

        public float Total { get; set; }

        public string Source { get; set; }

        public string Status { get; set; }
    }
}