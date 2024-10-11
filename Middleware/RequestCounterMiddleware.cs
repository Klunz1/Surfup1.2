namespace SurfsupEmil.Middleware
{
    public class RequestCounterMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly Dictionary<string, int> RequestCounts = new Dictionary<string, int>();

        public RequestCounterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userIp = context.Connection.RemoteIpAddress.ToString();

            if (RequestCounts.ContainsKey(userIp))
            {
                RequestCounts[userIp]++;
            }
            else
            {
                RequestCounts[userIp] = 1;
            }

            // Log or display the count (you could use a logger here as well)
            Console.WriteLine($"User {userIp} has made {RequestCounts[userIp]} requests.");

            // Add header to response for demonstration purposes
            context.Response.Headers.Add("X-Request-Count", RequestCounts[userIp].ToString());

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }

}
