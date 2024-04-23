
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["acheipro-api/acheipro-api.csproj", "acheipro-api/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["Service.Contracts/Service.Contracts.csproj", "Service.Contracts/"]
COPY ["LoggerService/LoggerService.csproj", "LoggerService/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["AcheiProApi.Presentation/AcheiProApi.Presentation.csproj", "AcheiProApi.Presentation/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Service/Service.csproj", "Service/"]

RUN mkdir /acheipro-api/Resources

RUN dotnet restore "acheipro-api/acheipro-api.csproj"
COPY . .

WORKDIR "/src/acheipro-api"
RUN dotnet build "acheipro-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "acheipro-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "acheipro-api.dll"]
