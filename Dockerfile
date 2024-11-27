FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["./src/Web/Web.csproj", "src/Web/"]
COPY ["./src/Application/Application.csproj", "src/Application/"]
COPY ["./src/Domain/Domain.csproj", "src/Domain/"]
COPY ["./src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]

RUN dotnet restore "src/Web/Web.csproj"
# Copy the rest of the files
COPY . .

# Build
WORKDIR "src/Web"
RUN dotnet build "Web.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]