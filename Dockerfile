# Etapa base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Corrigindo o caminho do .csproj
COPY ["Library.API.csproj", "./"]
RUN dotnet restore "./Library.API.csproj"

COPY . .
RUN dotnet build "Library.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Library.API.csproj" -c Release -o /app/publish

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Library.API.dll"]
