dotnet new tool-manifest
dotnet tool install dotnet-ef
dotnet ef migrations add InitialDb
dotnet ef database update

dotnet ef migrations add SeedData

cd /d e:
cd E:\source\repos\aspnet\DutchTreat
set ASPNETCORE_ENVIRONMENT=Development
dotnet run

