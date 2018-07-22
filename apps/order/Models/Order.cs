namespace Order.Models
{
    using MongoDB.Bson;

    public class Order
    {
        public ObjectId Id { get; set; }

        public string EmailAddress { get; set; }

        public string PreferredLanguage { get; set; }

        public string Product { get; set; }

        public float Total { get; set; }

        public string Source { get; set; }

        public string Status { get; set; }
    }
}