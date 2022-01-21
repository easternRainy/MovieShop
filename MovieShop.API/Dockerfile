FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MovieShop.API/MovieShop.API.csproj", "MovieShop.API/"]
RUN dotnet restore "MovieShop.API/MovieShop.API.csproj"
COPY . .
WORKDIR "/src/MovieShop.API"
RUN dotnet build "MovieShop.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieShop.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieShop.API.dll"]
