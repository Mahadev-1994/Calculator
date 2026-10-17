# ── Stage 1: Build ──────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2022 AS build
WORKDIR /src

# Copy ONLY the project file first — this layer is cached unless .csproj changes
COPY Calculator/Calculator.csproj ./Calculator/
RUN dotnet restore ./Calculator/Calculator.csproj --no-cache

# Copy the rest of the source
COPY Calculator/ ./Calculator/

# Publish in Release mode — self-contained=false keeps it smaller
RUN dotnet publish ./Calculator/Calculator.csproj -c Release -o /app/publish --no-restore

# ── Stage 2: Runtime ─────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0-windowsservercore-ltsc2022 AS runtime
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish .

# ASP.NET Core listens on port 5000
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 5000

ENTRYPOINT ["Calculator.exe"]
