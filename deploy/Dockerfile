# Imagen del servicio de datos. MULTIETAPA, con sólo el entorno de ejecución en la imagen final
# y SIN LINAJE con la imagen del contenedor de desarrollo (intake §17.5.P.8 y §17.5.P.9).
# Es lo que mide `PT-04`: se construye, arranca, aplica sus transformaciones sobre base vacía
# y responde salud.

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

COPY Directory.Build.props ./
COPY GeometriaFactory.sln ./
COPY src/ ./src/

RUN dotnet restore src/GeometriaFactory.Api/GeometriaFactory.Api.csproj
RUN dotnet publish src/GeometriaFactory.Api/GeometriaFactory.Api.csproj \
    -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# El almacén vive en un volumen, no en la imagen.
ENV ASPNETCORE_URLS=http://+:8080 \
    ConnectionStrings__Store="Data Source=/datos/geometriafactory.db"

# `curl` entra sólo porque el `healthcheck` de `compose.yaml` lo necesita: la imagen de
# ejecución no lo trae. Es la única cosa que se agrega sobre el entorno de ejecución.
RUN apt-get update \
    && apt-get install --yes --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*

RUN mkdir -p /datos
VOLUME ["/datos"]

COPY --from=build /publish ./
EXPOSE 8080

ENTRYPOINT ["dotnet", "GeometriaFactory.Api.dll"]
