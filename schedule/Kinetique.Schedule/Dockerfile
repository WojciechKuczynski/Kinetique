FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5197

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/scheduleapp
COPY ["./schedule/Kinetique.Schedule/*.csproj", "./schedule/Kinetique.Schedule/"]
RUN dotnet restore "./schedule/Kinetique.Schedule/Kinetique.Schedule.csproj"
COPY shared ./shared
COPY schedule ./schedule
RUN dotnet build "./schedule/Kinetique.Schedule/Kinetique.Schedule.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./schedule/Kinetique.Schedule/Kinetique.Schedule.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kinetique.Schedule.dll"]