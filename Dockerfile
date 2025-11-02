# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiar archivos del proyecto
COPY . ./

# Restaurar dependencias y compilar
RUN dotnet restore WebApplication4.csproj
RUN dotnet publish WebApplication4.csproj -c Release -o out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Copiar archivos publicados
COPY --from=build /app/out ./

# Exponer el puerto
EXPOSE 80

# Comando de inicio
ENTRYPOINT ["dotnet", "WebApplication4.dll"]
