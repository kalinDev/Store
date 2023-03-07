FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/services/Store.Client.API/Store.Client.API.csproj", "Store.Client.API/"]
RUN dotnet restore "src/services/Store.Client.API/Store.Client.API.csproj"
COPY . .
WORKDIR "/src/Store.Client.API"
RUN dotnet build "Store.Client.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Store.Client.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Store.Client.API.dll"]
