# ---- Stage 1: Build ----
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy project files and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# ---- Stage 2: Runtime ----
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy the build output from the previous stage
COPY --from=build /app/out ./

# Expose ports (match your launchSettings.json)
EXPOSE 5106
EXPOSE 7049

# Run the app
ENTRYPOINT ["dotnet", "MyApiApp.dll"]
