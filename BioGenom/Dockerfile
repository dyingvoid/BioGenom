FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BioGenom/BioGenom.csproj", "BioGenom/"]
COPY ["Models/Models.csproj", "Models/"]
COPY ["Infrastructure.Persistence/Infrastructure.Persistence.csproj", "Infrastructure.Persistence/"]
COPY ["Business/Business.csproj", "Business/"]
COPY ["Infrastructure.Apis/Infrastructure.Apis.csproj", "Infrastructure.Apis/"]
RUN dotnet restore "BioGenom/BioGenom.csproj"
COPY . .
WORKDIR "/src/BioGenom"
RUN dotnet build "./BioGenom.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BioGenom.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BioGenom.dll"]
