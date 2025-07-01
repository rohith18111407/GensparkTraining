# Use .NET 9.0 Preview SDK image (official preview tag)
FROM mcr.microsoft.com/dotnet/nightly/sdk:9.0 AS build
WORKDIR /App

# Copy everything
COPY . ./

# Restore as distinct layers
RUN dotnet restore

# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/nightly/aspnet:9.0
WORKDIR /App
COPY --from=build /App/out .

EXPOSE 8080
ENTRYPOINT ["dotnet", "DoctorPatientAppointment.dll"]
