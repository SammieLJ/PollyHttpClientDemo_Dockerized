# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining source code
COPY . ./
RUN dotnet publish -c Release -o out

# Use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/out ./

# Expose port the app runs on
EXPOSE 5000
ENTRYPOINT ["dotnet", "PollyHttpClientDemo.dll"]
