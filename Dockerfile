# Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy .csproj and restore first (for caching)
COPY DiaryPortfolio.Api/*.csproj DiaryPortfolio.Api/
COPY DiaryPortfolio.Application/*.csproj DiaryPortfolio.Application/
COPY DiaryPortfolio.Infrastructure/*.csproj DiaryPortfolio.Infrastructure/
COPY DiaryPortfolio.Domain/*.csproj DiaryPortfolio.Domain/
RUN dotnet restore DiaryPortfolio.Api/DiaryPortfolio.Api.csproj

# Copy the rest and build
COPY . .
WORKDIR /src/DiaryPortfolio.Api
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DiaryPortfolio.Api.dll"]
