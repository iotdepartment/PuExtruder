# Etapa 1: Imagen base para ejecución
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:8080

# Copiar solo el archivo .csproj primero
COPY WebApplication4.csproj ./

# Restaurar dependencias
RUN dotnet restore "WebApplication4.csproj"

# Copiar el resto del proyecto
COPY . .

# Publicar en modo Release
RUN dotnet publish "WebApplication4.csproj" -c Release -o /app/publish

# Etapa 3: Imagen final optimizada
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Variables de entorno para conexión SQL Server
ENV ConnectionStrings__DefaultConnection="Server=10.195.10.166;Database=PRUEBA;User Id=Manu;Password=2022.Tgram2;TrustServerCertificate=True;"

# Punto de entrada
ENTRYPOINT ["dotnet", "WebApplication4.dll"]
