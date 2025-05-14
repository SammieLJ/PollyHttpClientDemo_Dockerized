# PollyHttpClientDemo (.NET 8, Dockerized)

This project demonstrates how to use **Polly** in an ASP.NET Core 8 Web API to make resilient HTTP requests. It includes examples of:

- **Retry Policy** – for handling transient faults
- **Circuit Breaker Policy** – to avoid repeated calls to unstable services
- **HttpClientFactory** – integrating Polly policies with named clients
- **Docker support** – for running the app and mock API service locally

---

## 🎯 Goals

This project simulates a real-world scenario where:

- Your API consumes another external service
- That external service may become **temporarily unavailable**
- You want to **retry** failed calls a few times
- If the service is unstable, you want to **break the circuit** temporarily

All this is handled cleanly using Polly policies and modern .NET 8 best practices.

---

## 🛠 Technologies Used

- ASP.NET Core 8 Web API
- [Polly](https://github.com/App-vNext/Polly)
- IHttpClientFactory with named clients
- Docker and Docker Compose

---

## 📦 How to Load Polly in Your Project

1. Add Polly NuGet packages:
```bash
dotnet add package Microsoft.Extensions.Http.Polly

2. Register named HttpClient with policies in Program.cs:
```csharp
builder.Services.AddHttpClient("ExternalAPI", client =>
{
    client.BaseAddress = new Uri("http://mock-api:5001");
})
.AddTransientHttpErrorPolicy(policy => 
    policy.WaitAndRetryAsync(3, retry => TimeSpan.FromMilliseconds(200)))
.AddCircuitBreakerPolicy();

3. Inject IHttpClientFactory into your controller:
```csharp
public class TestController : ControllerBase
{
    private readonly HttpClient _client;

    public TestController(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("ExternalAPI");
    }

    [HttpGet("/test")]
    public async Task<IActionResult> Get() =>
        Ok(await _client.GetStringAsync("/ping"));
}

---

## 🐳 How to Run with Docker

1. Clone the repository:
```bash
git clone https://github.com/yourusername/PollyHttpClientDemo_Dockerized.git
cd PollyHttpClientDemo_Dockerized

2. Build and run the project:
```bash
docker-compose up --build

3. Test the endpoint:
```bash
curl http://localhost:8080/test

You’ll see the HttpClient call handled via Polly, including retry logic or circuit breaker behavior if the mock API is unstable.

---

## 📦 This Project Structure
```graphql
PollyHttpClientDemo/
│
├── PollyHttpClientDemo/          # Main ASP.NET Core app
│   ├── Controllers/
│   └── Program.cs
│
├── MockApi/                      # Minimal Web API to simulate failures
│   └── Program.cs
│
├── docker-compose.yml
└── README.md

---

## 📜 License
MIT

---
Created with ❤️ to demonstrate clean resilience handling using Polly in .NET.
