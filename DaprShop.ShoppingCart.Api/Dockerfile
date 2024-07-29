FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy everything
COPY ./DaprShop.ShoppingCart.Api ./DaprShop.ShoppingCart.Api
COPY ./DaprShop.Contracts ./DaprShop.Contracts

# Restore as distinct layers
RUN dotnet restore ./DaprShop.ShoppingCart.Api
# Build and publish a release
RUN dotnet publish -c Release -o out ./DaprShop.ShoppingCart.Api

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "DaprShop.ShoppingCart.Api.dll"]