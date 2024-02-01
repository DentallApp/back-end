FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ["src/Plugins/AppointmentReminders/*.csproj", "src/Plugins/AppointmentReminders/"]
COPY ["src/Plugins/ChatBot/*.csproj", "src/Plugins/ChatBot/"]
COPY ["src/Shared/*.csproj", "src/Shared/"]
COPY ["src/Infrastructure/*.csproj", "src/Infrastructure/"]
COPY ["src/Features/*.csproj", "src/Features/"]
COPY ["src/HostApplication/*.csproj", "src/HostApplication/"]
COPY *.props .
WORKDIR /app/src/Plugins/AppointmentReminders
RUN dotnet restore
WORKDIR /app/src/Plugins/ChatBot
RUN dotnet restore
WORKDIR /app/src/HostApplication
RUN dotnet restore

# Copy everything else and build app
COPY src/. ./src/
WORKDIR /app/src/Plugins/AppointmentReminders
RUN dotnet build -c Release --no-restore
WORKDIR /app/src/Plugins/ChatBot
RUN dotnet build -c Release --no-restore
WORKDIR /app/src/HostApplication
RUN dotnet publish -c Release -o /app/out --no-restore

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "DentallApp.HostApplication.dll"]