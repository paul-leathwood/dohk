namespace Order.Listener
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Flurl.Http;
    using Microsoft.Azure.EventHubs;
    using Microsoft.Azure.EventHubs.Processor;
    using Newtonsoft.Json;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    class Program
    {
        static readonly string rabbitUrl = Environment.GetEnvironmentVariable("RABBIT_URL");
        static readonly string processUrl = Environment.GetEnvironmentVariable("PROCESS_URL");
        private const string StorageContainerName = "{Storage account container name}";
        private const string StorageAccountName = "{Storage account name}";
        private const string StorageAccountKey = "{Storage account key}";

        private static readonly string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);

        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            try
            {
                ListenToEventHub();
                ListenToRabbitMQ();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);
            }
        }
        
        private static void ListenToEventHub()
        {
            // Console.WriteLine("Registering EventProcessor...");

            // var eventProcessorHost = new EventProcessorHost(
            //     System.Environment.GetEnvironmentVariable("EVENTHUB_NAME"),
            //     PartitionReceiver.DefaultConsumerGroupName,
            //     System.Environment.GetEnvironmentVariable("EVENT_URL"),
            //     StorageConnectionString,
            //     StorageContainerName);

            // // Registers the Event Processor Host and starts receiving messages
            // await eventProcessorHost.RegisterEventProcessorAsync<OrderEventProcessor>();

            // Console.WriteLine("Receiving. Press ENTER to stop worker.");
            // Console.ReadLine();

            // // Disposes of the Event Processor Host
            // await eventProcessorHost.UnregisterEventProcessorAsync();
        }

        private static void ListenToRabbitMQ()
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

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += async (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                        var order = JsonConvert.DeserializeObject(message);
                        await SendToProcess(order);
                    };
                    channel.BasicConsume(queue: "orders",
                                        autoAck: true,
                                        consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }

        private static async Task SendToProcess(object order)
        {
            try
            {
                var response = await processUrl.PostJsonAsync(order);
                Console.WriteLine(response.ToString());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
        }
    }
}
