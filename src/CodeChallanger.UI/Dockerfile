#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8085
ENV ASPNETCORE_URLS http://*:8085

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CodeChallanger.UI/CodeChallanger.UI.csproj", "CodeChallanger.UI/"]
COPY ["CodeChallenge.Domain.Interfaces/CodeChallenge.Domain.Interfaces.csproj", "CodeChallenge.Domain.Interfaces/"]
COPY ["CodeChallenge.Repository/CodeChallenge.Repository.csproj", "CodeChallenge.Repository/"]
COPY ["CodeExecutionContracts/CodeExecutionContracts.csproj", "CodeExecutionContracts/"]
RUN dotnet restore "./CodeChallanger.UI/./CodeChallanger.UI.csproj"
COPY . .
WORKDIR "/src/CodeChallanger.UI"
RUN dotnet build "./CodeChallanger.UI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CodeChallanger.UI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CodeChallanger.UI.dll"]