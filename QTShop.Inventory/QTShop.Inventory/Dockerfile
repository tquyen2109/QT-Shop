FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["QTShop.Inventory/QTShop.Inventory.csproj", "QTShop.Inventory/"]
RUN dotnet restore "QTShop.Inventory/QTShop.Inventory.csproj"
COPY . .
WORKDIR "/src/QTShop.Inventory"
RUN dotnet build "QTShop.Inventory.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QTShop.Inventory.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QTShop.Inventory.dll"]
