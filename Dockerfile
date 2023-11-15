FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY . .

RUN dotnet restore ./GameOfLifeAPI.sln
RUN dotnet test
RUN dotnet publish ./src/GameOfLifeKata.API/GameOfLifeKata.API.csproj -c Release -o published

# ASP.NET Core Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
EXPOSE 80
WORKDIR /app
COPY --from=build /app/published .

ENTRYPOINT ["dotnet", "./GameOfLifeKata.API.dll"]