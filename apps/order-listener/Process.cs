using System.Threading.Tasks;

namespace Order.Listener
{
    using System;
    using Flurl.Http;

    public static class Process
    {
        static readonly string processUrl = Environment.GetEnvironmentVariable("PROCESS_URL");

        public static async Task SendAsync(object order)
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
