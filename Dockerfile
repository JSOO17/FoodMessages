#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7142

ENV ASPNETCORE_URLS=http://*:7142

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FoodMessages/FoodMessages.csproj", "FoodMessages/"]
RUN dotnet restore "FoodMessages/FoodMessages.csproj"
COPY . .
WORKDIR "/src/FoodMessages"
RUN dotnet build "FoodMessages.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoodMessages.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodMessages.dll"]