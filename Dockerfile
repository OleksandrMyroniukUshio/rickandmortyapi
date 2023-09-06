FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5191
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:5191

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RickAndMortyAPI.csproj", "./"]
RUN dotnet restore "RickAndMortyAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "RickAndMortyAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RickAndMortyAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RickAndMortyAPI.dll"]