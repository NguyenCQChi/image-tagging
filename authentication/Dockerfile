FROM mcr.microsoft.com/dotnet/aspnet:7.0.14-jammy-amd64 AS base
WORKDIR /app
EXPOSE 80/tcp
EXPOSE 8000/tcp
EXPOSE 443/tcp
EXPOSE 8001/tcp
EXPOSE 3306
EXPOSE 3307

RUN apt-get update \
    && apt-get install -y openssl

FROM mcr.microsoft.com/dotnet/sdk:7.0.404-1-jammy-amd64 AS build
WORKDIR /src
COPY ["authentication.csproj", "."]
RUN dotnet restore "authentication.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "authentication.csproj" -c Release -o /app/build

FROM build AS publish
COPY ["authentication.csproj", "."]
RUN dotnet publish "authentication.csproj" -c Release -o /app/publish
RUN dotnet dev-certs https -ep /app/publish/aspnetapp.pfx -p "${PASSWORD}"
RUN dotnet dev-certs https --trust

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /src/Templates /app/Templates
ARG SENDGRID_API_KEY
ARG ENVIRONMENT
ENV SendGridApiKey=$SENDGRID_API_KEY
ENV ASPNETCORE_URLS="https://+:443;http://+:80"
ENV ASPNETCORE_HTTPS_PORT=443
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="${PASSWORD}"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="/app/aspnetapp.pfx"
ENTRYPOINT ["dotnet", "authentication.dll"]
