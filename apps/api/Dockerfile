FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY apps/api/Mepsan.Api/Mepsan.Api.csproj apps/api/Mepsan.Api/
RUN dotnet restore apps/api/Mepsan.Api/Mepsan.Api.csproj

COPY apps/api/ apps/api/
WORKDIR /src/apps/api/Mepsan.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5190
EXPOSE 5190
ENTRYPOINT ["dotnet", "Mepsan.Api.dll"]
