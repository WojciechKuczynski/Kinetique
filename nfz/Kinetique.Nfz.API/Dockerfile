FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/nfzapp
COPY ["./nfz/Kinetique.Nfz.API/*.csproj", "./nfz/Kinetique.Nfz.API/"]
RUN dotnet restore "./nfz/Kinetique.Nfz.API/Kinetique.Nfz.API.csproj"
COPY shared ./shared
COPY nfz ./nfz
RUN dotnet build "./nfz/Kinetique.Nfz.API/Kinetique.Nfz.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./nfz/Kinetique.Nfz.API/Kinetique.Nfz.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kinetique.Nfz.API.dll"]