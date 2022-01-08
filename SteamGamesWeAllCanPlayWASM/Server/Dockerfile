#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SteamGamesWeAllCanPlayWASM/Server/SteamGamesWeAllCanPlayWASM.Server.csproj", "SteamGamesWeAllCanPlayWASM/Server/"]
COPY ["SteamGamesWeAllCanPlayWASM/Shared/SteamGamesWeAllCanPlayWASM.Shared.csproj", "SteamGamesWeAllCanPlayWASM/Shared/"]
COPY ["SteamGamesWeAllCanPlayWASM/Client/SteamGamesWeAllCanPlayWASM.Client.csproj", "SteamGamesWeAllCanPlayWASM/Client/"]
COPY ["SteamGamesWeAllCanPlayWASM.Data/SteamGamesWeAllCanPlayWASM.Data.csproj", "SteamGamesWeAllCanPlayWASM.Data/"]
RUN dotnet restore "SteamGamesWeAllCanPlayWASM/Server/SteamGamesWeAllCanPlayWASM.Server.csproj"
COPY . .
WORKDIR "/src/SteamGamesWeAllCanPlayWASM/Server"
RUN dotnet build "SteamGamesWeAllCanPlayWASM.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SteamGamesWeAllCanPlayWASM.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SteamGamesWeAllCanPlayWASM.Server.dll"]