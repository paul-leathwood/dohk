namespace Process.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MongoDB.Driver;

    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly ILogger logger;
        static readonly string mongoUrl = System.Environment.GetEnvironmentVariable("MONGO_URL");

        ProcessController(ILogger logger)
        {
            this.logger = logger;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Models.Order order)
        {
            RetrieveFromDatastore(order);
            return new OkResult();
        }

        public Models.Order RetrieveFromDatastore(Models.Order order)
        {
            this.logger.LogDebug($"Looking for {{ orderid: {order.Id}, status: \"Open\" }}");
            var stopWatch = Stopwatch.StartNew();
            var mongoDbClient = new MongoClient(mongoUrl);
            var mongoDbDatabase = mongoDbClient.GetDatabase("orders");
            var orderCollection = mongoDbDatabase.GetCollection<Models.Order>("Orders");
            var result = orderCollection.Find(o => o.Id == order.Id).FirstOrDefault();
            if (result == null)
            {
                this.logger.LogDebug("Not found (already processed) or error: ");
            }
            this.logger.LogDebug($"Retrieving from MongoDB retrieved from {stopWatch.ElapsedMilliseconds}ms");
            return result;
        }
    }
}
