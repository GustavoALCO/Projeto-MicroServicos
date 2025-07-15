# Base para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8081

# SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["AdsService.WebApi/AdsService.WebApi.csproj", "AdsService.WebApi/"]
COPY ["AdsService.Infra/AdsService.Infra.csproj", "AdsService.Infra/"]
COPY ["AdsService.Dommain/AdsService.Dommain.csproj", "AdsService.Dommain/"]
COPY ["AdsSevice.IOC/AdsSevice.IOC.csproj", "AdsSevice.IOC/"]
COPY ["AdsService.Aplication/AdsService.Aplication.csproj", "AdsService.Aplication/"]

RUN dotnet restore "AdsService.WebApi/AdsService.WebApi.csproj"
COPY . .
WORKDIR "/src/AdsService.WebApi"
RUN dotnet build "AdsService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicação
FROM build AS publish
RUN dotnet publish "AdsService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdsService.WebApi.dll"]
