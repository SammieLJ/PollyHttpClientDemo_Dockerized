using Polly;
using Polly.Extensions.Http;
using System.Net;
using System.Text;
using PollyHttpClientDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Polly policies
var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(3, retryAttempt =>
        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

var circuitBreakerPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .CircuitBreakerAsync(2, TimeSpan.FromSeconds(10));

var fallbackPolicy = Policy<HttpResponseMessage>
    .Handle<Exception>()
    .FallbackAsync(
        new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(
                "{\"message\": \"This is a fallback response.\"}",
                Encoding.UTF8,
                "application/json")
        },
        onFallbackAsync: async (res, ctx) =>
        {
            Console.WriteLine("Fallback activated due to: " + res.Exception?.Message);
            await Task.CompletedTask;
        });

// Register HttpClient with policies
builder.Services.AddHttpClient("MyApiClient", client =>
{
    client.BaseAddress = new Uri("https://invalid-url.example.com"); // Simulate failure
})
.AddPolicyHandler(fallbackPolicy)
.AddPolicyHandler(retryPolicy)
.AddPolicyHandler(circuitBreakerPolicy);

// Register your service
builder.Services.AddScoped<ApiService>();

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
