FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
#EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
#COPY "DM/DM.csproj" ./DM/
#COPY "DM.DAL/DM.DAL.csproj" ./DM.DAL/
#COPY "DM.Domain/DM.Domain.csproj" ./DM.Domain/
#COPY "DM.sln" .

RUN dotnet restore "DM.sln"

COPY . .
COPY "DM/appsettings.dev.test.json" "DM/appsettings.json"

RUN dotnet build "DM.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DM.sln" -c Release -o /app/publish
#
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DM.dll"]