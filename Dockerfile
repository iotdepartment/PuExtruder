# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia el archivo de proyecto correcto
COPY ["WebApplication4.csproj", "./"]
RUN dotnet restore "WebApplication4.csproj"

# Copia el resto del código
COPY . .
RUN dotnet publish "WebApplication4.csproj" -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "WebApplication4.dll"]
