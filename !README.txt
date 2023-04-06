dotnet new tool-manifest
dotnet tool install dotnet-ef
dotnet ef migrations add InitialDb
dotnet ef database update

dotnet ef migrations add SeedData

cd /d e:
cd E:\source\repos\aspnet\DutchTreat

set ASPNETCORE_ENVIRONMENT=Development
dotnet run

dotnet ef migrations add Identity
dotnet ef database drop
dotnet run /seed

--angular--

node --version
npm --version
npm install @angular/cli -g
npm uninstall @angular/cli -g npm cache clean --force 
ng version
ng new help
ng new client --skip-git --skip-tests --minimal --defaults

cd /d e:
cd E:\source\repos\aspnet\DutchTreat\client
ng build --output-hashing=none --watch

json2ts.com