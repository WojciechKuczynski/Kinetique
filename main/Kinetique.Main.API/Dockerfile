FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/mainapp
COPY ["./main/Kinetique.Main.API/*.csproj", "./main/Kinetique.Main.API/"]
RUN dotnet restore "./main/Kinetique.Main.API/Kinetique.Main.API.csproj"
COPY shared ./shared
COPY main ./main
RUN dotnet build "./main/Kinetique.Main.API/Kinetique.Main.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./main/Kinetique.Main.API/Kinetique.Main.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kinetique.Main.API.dll"]