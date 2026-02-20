# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY TemplateApi.slnx .
COPY Directory.Build.props ./
COPY Directory.Packages.props ./
COPY TemplateApi/TemplateApi.csproj TemplateApi/
RUN dotnet restore TemplateApi/TemplateApi.csproj

COPY TemplateApi/. TemplateApi/
RUN dotnet publish TemplateApi/TemplateApi.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "TemplateApi.dll"]
