FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out .

ENV DEBIAN_FRONTEND=noninteractive
RUN apt update && apt install -y openjdk-17-jdk && rm -rf /var/lib/apt/lists/*

ENTRYPOINT ["dotnet", "ShortDev.JLab.dll"]