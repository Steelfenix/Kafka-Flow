FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

RUN dir
COPY ["Example.Common/Example.Common.csproj", "Example.Common/"]
COPY ["Kafka-Flow/Kafka-Flow.csproj", "Kafka-Flow/"]
RUN dotnet restore Kafka-Flow/Kafka-Flow.csproj
COPY . .
WORKDIR "/src/Kafka-Flow"
RUN dotnet build "Kafka-Flow.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kafka-Flow.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kafka-Flow.dll"]