# Etapa 1: Imagen base para ejecución
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

# Etapa 2: Imagen para compilación
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar archivos del proyecto
COPY ["WebApplication4.csproj", "./"]
RUN dotnet restore "./WebApplication4.csproj"

# Copiar el resto del código fuente
COPY . .

# Publicar en modo Release
RUN dotnet publish "./WebApplication4.csproj" -c Release -o /app/publish

# Etapa 3: Imagen final optimizada
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Variables de entorno para conexión SQL Server
ENV ConnectionStrings__DefaultConnection="Server=10.195.10.166;Database=PRUEBA;User Id=Manu;Password=2022.Tgram2;TrustServerCertificate=True;"

# Punto de entrada
ENTRYPOINT ["dotnet", "WebApplication4.dll"]
