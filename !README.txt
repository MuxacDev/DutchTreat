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
ng build --watch

json2ts.com

--publish--
npm install gulp
npm i gulp-uglify
npm i gulp-concat
gulp

dotnet publish -o D:\Temp\publish\win --self-contained 
//if RuntimeIdentifier is set in .csproj

dotnet publish -o D:\Temp\publish\win --runtime win10-x64 --self-contained
dotnet publish -o D:\Temp\publish\linux --runtime linux-x64 --self-contained
dotnet publish -o D:\Temp\publish\win_lt --runtime win10-x64 --no-self-contained

dotnet DutchTreat.dll


