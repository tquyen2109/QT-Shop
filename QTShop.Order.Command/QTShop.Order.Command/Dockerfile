FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["QTShop.Order.Command/QTShop.Order.Command.csproj", "QTShop.Order.Command/"]
RUN dotnet restore "QTShop.Order.Command/QTShop.Order.Command.csproj"
COPY . .
WORKDIR "/src/QTShop.Order.Command"
RUN dotnet build "QTShop.Order.Command.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QTShop.Order.Command.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QTShop.Order.Command.dll"]
