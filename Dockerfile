# Etapa base para ejecución
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar solo el archivo de proyecto y restaurar dependencias
COPY WebApplication4.csproj ./
RUN dotnet restore "WebApplication4.csproj"

# Copiar el resto del código fuente
COPY . .

# Publicar en modo Release
RUN dotnet publish "WebApplication4.csproj" -c Release -o /app/publish

# Etapa final: imagen optimizada
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Cadena de conexión como variable de entorno
ENV ConnectionStrings__DefaultConnection="Server=sqlserver;Database=PRUEBA;User Id=Manu;Password=2022.Tgram2;TrustServerCertificate=True;"

ENTRYPOINT ["dotnet", "WebApplication4.dll"]
