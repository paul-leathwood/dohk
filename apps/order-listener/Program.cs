namespace Order.Listener
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Azure.EventHubs;
    using Microsoft.Azure.EventHubs.Processor;
    using Newtonsoft.Json;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    class Program
    {
        static readonly string rabbitUrl = Environment.GetEnvironmentVariable("RABBIT_URL");
        static readonly string eventHubName = Environment.GetEnvironmentVariable("EVENTHUB_NAME");
        static readonly string eventHubUrl = Environment.GetEnvironmentVariable("EVENTHUB_URL");
        static readonly string storageName = Environment.GetEnvironmentVariable("STORAGE_NAME");
        static readonly string storageAccountName = Environment.GetEnvironmentVariable("STORAGE_ACCOUNT");
        static readonly string storageAccountKey = Environment.GetEnvironmentVariable("STORAGE_KEY");

        static readonly string storageUrl = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", storageAccountName, storageAccountKey);

        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            try
            {
                await ListenToEventHub();
                ListenToRabbitMQ();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);
            }
        }
        
        private static async Task ListenToEventHub()
        {
            if (string.IsNullOrEmpty(eventHubUrl))
            {
                return;
            }

            Console.WriteLine("Registering EventProcessor...");

            var eventProcessorHost = new EventProcessorHost(
                eventHubName,
                PartitionReceiver.DefaultConsumerGroupName,
                eventHubUrl,
                storageUrl,
                storageName);

            // Registers the Event Processor Host and starts receiving messages
            await eventProcessorHost.RegisterEventProcessorAsync<OrderEventProcessor>();

            Console.WriteLine("Receiving. Press ENTER to stop worker.");
            Console.ReadLine();

            // Disposes of the Event Processor Host
            await eventProcessorHost.UnregisterEventProcessorAsync();
        }

        private static void ListenToRabbitMQ()
        {
            if(string.IsNullOrEmpty(rabbitUrl))
            {
                return;
            }

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

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += async (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                        var order = JsonConvert.DeserializeObject(message);
                        await Process.SendAsync(order);
                    };
                    channel.BasicConsume(queue: "orders",
                                        autoAck: true,
                                        consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
