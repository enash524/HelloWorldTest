using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HelloWorldTest.Infrastructure.Messages.Queries.GetHelloWorld;
using HelloWorldTest.Persistence.Extensions;
using Polly;

namespace HelloWorldTest.ConsoleApp
{
    internal static class Program
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async Task Main(string[] args)
        {
            try
            {
                _client.BaseAddress = new Uri("https://localhost:5001/");
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string message = await GetServerMessageAsync();
                Console.WriteLine($"Response From Server: {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
            finally
            {
                Console.WriteLine("Press any key to close this window ...");
                Console.ReadKey(true);
            }
        }

        private static async Task<string> GetServerMessageAsync()
        {
            HelloWorldViewModel model = null;
            Uri msgUrl = new Uri("api/Messages/GetHelloWorld", UriKind.Relative);
            PolicyResult<HttpResponseMessage> response = await Policy
                .HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
                .Or<Exception>()
                .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(2), (result, timeSpan, retryCount, context) =>
                {
                    if (result.Exception != null)
                    {
                        Console.WriteLine($"Request failed with an exception ({result.Exception.GetType().FullName}). Waiting {timeSpan} before next retry. Retry attempt {retryCount}.");
                    }
                    else
                    {
                        Console.WriteLine($"Request failed with {result.Result.StatusCode}. Waiting {timeSpan} before next retry. Retry attempt {retryCount}.");
                    }
                })
                .ExecuteAndCaptureAsync(() => _client.GetAsync(msgUrl));

            if (response?.Result?.IsSuccessStatusCode == true)
            {
                model = await response.Result.Content.ReadAsJsonAsync<HelloWorldViewModel>();
                return model?.MessageText;
            }
            else
            {
                return "Did not receive a valid response from the server. Retry count exceeded.";
            }
        }
    }
}
