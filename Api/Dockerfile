#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Api/CleanArch.Api.csproj", "Api/"]
COPY ["Core/CleanArch.Application/CleanArch.Application.csproj", "Core/CleanArch.Application/"]
COPY ["Core/CleanArch.Contracts/CleanArch.Contracts.csproj", "Core/CleanArch.Contracts/"]
COPY ["Core/CleanArch.Domain/CleanArch.Domain.csproj", "Core/CleanArch.Domain/"]
COPY ["Infrastructure/CleanArch.Identity/CleanArch.Identity.csproj", "Infrastructure/CleanArch.Identity/"]
COPY ["Infrastructure/CleanArch.Infrastructure/CleanArch.Infrastructure.csproj", "Infrastructure/CleanArch.Infrastructure/"]
COPY ["Infrastructure/CleanArch.Persistence/CleanArch.Persistence.csproj", "Infrastructure/CleanArch.Persistence/"]
COPY ["Infrastructure/CleanArch.IntegrationEvents/CleanArch.IntegrationEvents.csproj", "Infrastructure/CleanArch.IntegrationEvents/"]
RUN dotnet restore "./Api/CleanArch.Api.csproj"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "./CleanArch.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CleanArch.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArch.Api.dll"]