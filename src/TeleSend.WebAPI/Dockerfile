# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY TeleSend.WebAPI.sln ./
COPY src/TeleSend.WebAPI/TeleSend.WebAPI.csproj ./src/TeleSend.WebAPI/

RUN dotnet restore

COPY . ./

WORKDIR /app/src/TeleSend.WebAPI
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "TeleSend.WebAPI.dll"]
