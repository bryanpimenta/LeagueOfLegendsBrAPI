FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["LeagueOfLegendsBrAPI/LeagueOfLegendsBrAPI.csproj", "LeagueOfLegendsBrAPI/"]
RUN dotnet restore "LeagueOfLegendsBrAPI/LeagueOfLegendsBrAPI.csproj"
COPY . .
WORKDIR "/src/LeagueOfLegendsBrAPI"
RUN dotnet build "LeagueOfLegendsBrAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LeagueOfLegendsBrAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LeagueOfLegendsBrAPI.dll"]
