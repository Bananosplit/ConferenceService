FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ConferenceService/ConferenceService.csproj", "ConferenceService/"]
COPY ["App/App.csproj", "App/"]
COPY ["DAL/DAL.csproj", "DAL/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "./ConferenceService/ConferenceService.csproj"
COPY . .
WORKDIR "/src/ConferenceService"
RUN dotnet build "./ConferenceService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ConferenceService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConferenceService.dll"]