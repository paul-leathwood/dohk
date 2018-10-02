namespace Order.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.EventHubs;
    using MongoDB.Driver;
    using Newtonsoft.Json;
    using RabbitMQ.Client;

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        static readonly string mongoUrl = Environment.GetEnvironmentVariable("MONGO_URL");
        static readonly string rabbitUrl = Environment.GetEnvironmentVariable("RABBIT_URL");
        static readonly string eventHubUrl = Environment.GetEnvironmentVariable("EVENTHUB_URL");
        static readonly string eventHubName = Environment.GetEnvironmentVariable("EVENTHUB_NAME");

        // POST api/order
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.Order order)
        {
            Console.WriteLine(order);
            await SaveToDatastore(order);
            if (string.IsNullOrEmpty(eventHubUrl))
            {
                SendToRabbitMQ(order);
            }
            else
            {
                await SendToEventHub(order);
            }
            
            return new OkObjectResult(order);
        }

        private static async Task SaveToDatastore(Models.Order order)
        {
            var mongoDbClient = new MongoClient(mongoUrl);
            var mongoDbDatabase = mongoDbClient.GetDatabase("orders");
            var mongoDbCollection = mongoDbDatabase.GetCollection<Models.Order>("Orders");
            await mongoDbCollection.InsertOneAsync(order);
        }

        private static async Task SendToEventHub(Models.Order order)
        {
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(eventHubUrl)
            {
                EntityPath = eventHubName
            };

            var eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());

            var @event = JsonConvert.SerializeObject(order);
            await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(@event)));
            await eventHubClient.CloseAsync();
        }

        private static void SendToRabbitMQ(Models.Order order)
        {
            var factory = new ConnectionFactory() { Uri = new Uri(rabbitUrl) };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "orders",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                    string message = JsonConvert.SerializeObject(order);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                        routingKey: "orders",
                                        basicProperties: null,
                                        body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }
    }
}
