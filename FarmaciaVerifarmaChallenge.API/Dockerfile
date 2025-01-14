# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
ENV DB_CONNECTION_STRING="Server=sqlserverdocker;Database=FarmaciaDB;User Id=sa;Password=MyPassword*1234;TrustServerCertificate=True;"

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FarmaciaVerifarmaChallenge.API/FarmaciaVerifarmaChallenge.API.csproj", "FarmaciaVerifarmaChallenge.API/"]
COPY ["FarmaciaVerifarmaChallenge.Application/FarmaciaVerifarmaChallenge.Application.csproj", "FarmaciaVerifarmaChallenge.Application/"]
COPY ["FarmaciaVerifarmaChallenge.Domain/FarmaciaVerifarmaChallenge.Domain.csproj", "FarmaciaVerifarmaChallenge.Domain/"]
COPY ["FarmaciaVerifarmaChallenge.Infrastructure/FarmaciaVerifarmaChallenge.Infrastructure.csproj", "FarmaciaVerifarmaChallenge.Infrastructure/"]
RUN dotnet restore "./FarmaciaVerifarmaChallenge.API/FarmaciaVerifarmaChallenge.API.csproj"
COPY . .
WORKDIR "/src/FarmaciaVerifarmaChallenge.API"
RUN dotnet build "./FarmaciaVerifarmaChallenge.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FarmaciaVerifarmaChallenge.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FarmaciaVerifarmaChallenge.API.dll"]