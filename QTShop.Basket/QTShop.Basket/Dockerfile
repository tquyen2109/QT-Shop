FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["QTShop.Basket/QTShop.Basket.csproj", "QTShop.Basket/"]
RUN dotnet restore "QTShop.Basket/QTShop.Basket.csproj"
COPY . .
WORKDIR "/src/QTShop.Basket"
RUN dotnet build "QTShop.Basket.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QTShop.Basket.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QTShop.Basket.dll"]
