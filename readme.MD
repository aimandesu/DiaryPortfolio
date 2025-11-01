mkdir DiaryPortfolio

dotnet new webapi -o DiaryPortfolio.Api

dotnet new classlib -n DiaryPortfolio.Application
dotnet new classlib -n DiaryPortfolio.Domain
dotnet new classlib -n DiaryPortfolio.Infrastructure

dotnet sln add "DiaryPortfolio.Api/DiaryPortfolio.Api.csproj"
dotnet sln add "DiaryPortfolio.Application/DiaryPortfolio.Application.csproj"
dotnet sln add "DiaryPortfolio.Domain/DiaryPortfolio.Domain.csproj"
dotnet sln add "DiaryPortfolio.Infrastructure/DiaryPortfolio.Infrastructure.csproj"
