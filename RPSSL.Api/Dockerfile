FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RPSSL.Api/RPSSL.Api.csproj", "RPSSL.Api/"]
COPY ["RPSSL.Application/RPSSL.Application.csproj", "RPSSL.Application/"]
COPY ["RPSSL.Domain/RPSSL.Domain.csproj", "RPSSL.Domain/"]
COPY ["RPSSL.Infrastructure/RPSSL.Infrastructure.csproj", "RPSSL.Infrastructure/"]
RUN dotnet restore "RPSSL.Api/RPSSL.Api.csproj"
COPY . .
WORKDIR "/src/RPSSL.Api"
RUN dotnet build "RPSSL.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RPSSL.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RPSSL.Api.dll"]
