FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SensorManagerAPI/SensorManagerAPI.csproj", "SensorManagerAPI/"]
RUN dotnet restore "SensorManagerAPI/SensorManagerAPI.csproj"
COPY . .
WORKDIR "/src/SensorManagerAPI"
RUN dotnet build "SensorManagerAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SensorManagerAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SensorManagerAPI.dll"]
