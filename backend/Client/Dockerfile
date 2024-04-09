FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY . .

RUN dotnet build "./Online-Shop.UI/Online-Shop.UI.csproj" -o /app/build


FROM build AS publish

RUN dotnet publish "./Online-Shop.UI/Online-Shop.UI.csproj" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Online-Shop.UI.dll"]