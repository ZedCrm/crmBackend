# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy the project file and restore any dependencies (via dotnet restore)
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the source code and build the application
COPY . ./
RUN dotnet publish -c Release -o /out

# Use the official .NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Set the working directory in the runtime image
WORKDIR /app

# Copy the build output from the build stage
COPY --from=build /out .

# Expose the port the app will run on (usually 80 for web APIs)
EXPOSE 80

# Set the entry point for the container to run the application
ENTRYPOINT ["dotnet", "API.dll"]
