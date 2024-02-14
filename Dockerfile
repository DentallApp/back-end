#
# Build stage/image
#
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY *.sln .
COPY *.props .

# Copy csproj and restore host application dependencies (entry point)
COPY ["src/Shared/*.csproj", "src/Shared/"]
COPY ["src/Infrastructure/*.csproj", "src/Infrastructure/"]
COPY ["src/Core/*.csproj", "src/Core/"]
COPY ["src/HostApplication/*.csproj", "src/HostApplication/"]
WORKDIR /app/src/HostApplication
RUN dotnet restore
WORKDIR /app

# Copy csproj and restore the dependencies of each plugin
COPY ["src/Plugins/AppointmentReminders/*.csproj", "src/Plugins/AppointmentReminders/"]
COPY ["src/Plugins/ChatBot/*.csproj", "src/Plugins/ChatBot/"]
COPY ["src/Plugins/SendGrid/*.csproj", "src/Plugins/SendGrid/"]
COPY ["src/Plugins/TwilioWhatsApp/*.csproj", "src/Plugins/TwilioWhatsApp/"]
COPY ["src/Plugins/*.props", "src/Plugins/"]
WORKDIR /app/src/Plugins/AppointmentReminders
RUN dotnet restore
WORKDIR /app/src/Plugins/ChatBot
RUN dotnet restore
WORKDIR /app/src/Plugins/SendGrid
RUN dotnet restore
WORKDIR /app/src/Plugins/TwilioWhatsApp
RUN dotnet restore

# Copy everything else and build plugins
COPY ["src/Shared/", "/app/src/Shared/"]
COPY ["src/Plugins/", "/app/src/Plugins/"]
WORKDIR /app/src/Plugins/AppointmentReminders
RUN dotnet build -c Release --no-restore
WORKDIR /app/src/Plugins/ChatBot
RUN dotnet build -c Release --no-restore
WORKDIR /app/src/Plugins/SendGrid
RUN dotnet build -c Release --no-restore
WORKDIR /app/src/Plugins/TwilioWhatsApp
RUN dotnet build -c Release --no-restore

# Copy everything else and build app
COPY ["src/", "/app/src/"]
WORKDIR /app/src/HostApplication
RUN dotnet publish -c Release -o /app/out --no-restore

#
# Final stage/image
#
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# The tzdata package (contains time zone data) is installed so that the "TZ" variable can be set at runtime
ARG DEBIAN_FRONTEND=noninteractive
RUN apt-get update && \
apt-get install -y --no-install-recommends tzdata \
&& rm -rf /var/lib/apt/lists/*

# Copy artifact from build stage to final image
COPY --from=build /app/out ./

# Run application
ENTRYPOINT ["dotnet", "DentallApp.HostApplication.dll"]