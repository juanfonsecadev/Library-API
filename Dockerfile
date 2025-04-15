# Etapa de build
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["library/library.api/Library.Api.csproj", "library/library.api/"]
RUN dotnet restore "library/library.api/Library.Api.csproj"
COPY . .
WORKDIR "/src/library/library.api"
RUN dotnet build "Library.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Library.Api.csproj" -c Release -o /app/publish

# Etapa de produção
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Library.Api.dll"]
