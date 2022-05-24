FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

RUN dir
COPY ["Example.Common/Example.Common.csproj", "Example.Common/"]
COPY ["Consumer/Consumer.csproj", "Consumer/"]
RUN dotnet restore Consumer/Consumer.csproj
COPY . .
WORKDIR "/src/Consumer"
RUN dotnet build "Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Consumer.dll"]