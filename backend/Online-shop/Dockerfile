FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
RUN useradd -ms /bin/bash app
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Compose
WORKDIR /src
COPY . .

RUN dotnet build "./Shop.API/Shop.API.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Compose
RUN dotnet publish "./Shop.API/Shop.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.API.dll"]