#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Antibiogram-Statistics-Api/Antibiogram-Statistics-Api.csproj", "Antibiogram-Statistics-Api/"]
RUN dotnet restore "Antibiogram-Statistics-Api/Antibiogram-Statistics-Api.csproj"
COPY . .
WORKDIR "/src/Antibiogram-Statistics-Api"
RUN dotnet build "Antibiogram-Statistics-Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Antibiogram-Statistics-Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV AntibiogramDatabase__ConnectionString mongodb://localhost:27017/
ENTRYPOINT ["dotnet", "Antibiogram-Statistics-Api.dll"]