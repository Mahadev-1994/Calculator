# ── Stage 1: Build ──────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2022 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY Calculator/Calculator.csproj ./Calculator/
RUN dotnet restore ./Calculator/Calculator.csproj

# Copy the rest of the source and publish
COPY Calculator/ ./Calculator/
WORKDIR /src/Calculator
RUN dotnet publish Calculator.csproj -c Release -o /app/publish --no-restore

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
