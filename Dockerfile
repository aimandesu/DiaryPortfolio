# Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Install Chromium and necessary dependencies inside the container
#RUN apt-get update && apt-get install -y \
#    chromium \
#    fonts-ipafont-gothic \
#    fonts-wqy-zenhei \
#    fonts-thai-tlwg \
#    fonts-kacst \
#    fonts-freefont-ttf \
#    --no-install-recommends \
#    && rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Install Node.js so 'npx' is available for the Tailwind build target ---
RUN apt-get update && apt-get install -y curl ca-certificates gnupg \
    && curl -fsSL https://deb.nodesource.com/setup_20.x | bash - \
    && apt-get install -y openssl nodejs \
    && rm -rf /var/lib/apt/lists/*

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
