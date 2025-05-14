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


## 📦 How to Test out Polly in This local Project
- curl http://localhost:8080/test


## 📦 This Project Structure
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
