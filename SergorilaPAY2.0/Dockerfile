FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SergorilaPAY2.0/SergorilaPAY2.0.csproj", "SergorilaPAY2.0/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["DAL/DAL.csproj", "DAL/"]
COPY ["BLL/BLL.csproj", "BLL/"]
RUN dotnet restore "SergorilaPAY2.0/SergorilaPAY2.0.csproj"
COPY . .
WORKDIR "/src/SergorilaPAY2.0"
RUN dotnet build "SergorilaPAY2.0.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SergorilaPAY2.0.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SergorilaPAY2.0.dll"]
