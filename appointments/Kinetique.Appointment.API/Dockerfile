FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5675

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/appointmentapp
COPY ["./appointments/Kinetique.Appointment.API/*.csproj", "./appointments/Kinetique.Appointment.API/"]
RUN dotnet restore "./appointments/Kinetique.Appointment.API/Kinetique.Appointment.API.csproj"
COPY shared ./shared
COPY appointments ./appointments
RUN dotnet build "./appointments/Kinetique.Appointment.API/Kinetique.Appointment.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./appointments/Kinetique.Appointment.API/Kinetique.Appointment.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kinetique.Appointment.API.dll"]