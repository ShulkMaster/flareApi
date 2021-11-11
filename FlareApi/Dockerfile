FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FlareApi/FlareApi.csproj", "FlareApi/"]
RUN dotnet restore "FlareApi/FlareApi.csproj"
COPY . .
WORKDIR "/src/FlareApi"
RUN dotnet build "FlareApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FlareApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FlareApi.dll"]
