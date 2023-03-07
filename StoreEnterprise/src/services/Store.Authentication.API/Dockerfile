FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/services/Store.Authentication.API/Store.Authentication.API.csproj", "Store.Authentication.API/"]
RUN dotnet restore "src/services/Store.Authentication.API/Store.Authentication.API.csproj"
COPY . .
WORKDIR "/src/Store.Authentication.API"
RUN dotnet build "Store.Authentication.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Store.Authentication.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Store.Authentication.API.dll"]
