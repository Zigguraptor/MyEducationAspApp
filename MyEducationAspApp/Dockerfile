FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MyEducationAspApp/MyEducationAspApp.csproj", "MyEducationAspApp/"]
RUN dotnet restore "MyEducationAspApp/MyEducationAspApp.csproj"
COPY . .
WORKDIR "/src/MyEducationAspApp"
RUN dotnet build "MyEducationAspApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyEducationAspApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN apt-get update && apt-get install -y procps
ENTRYPOINT ["dotnet", "MyEducationAspApp.dll"]
