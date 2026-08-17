# Imagen del servicio de datos. MULTIETAPA, con sólo el entorno de ejecución en la imagen final
# y SIN LINAJE con la imagen del contenedor de desarrollo (intake §17.5.P.8 y §17.5.P.9).
# Es lo que mide `PT-04`: se construye, arranca, aplica sus transformaciones sobre base vacía
# y responde salud.

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

# LA IMAGEN SE SELLA A SÍ MISMA, y sólo cae en el argumento si no puede.
#
# El argumento existía como única fuente, y eso obligaba a que alguien lo escribiera a mano y
# **lo mantuviera acompañando al código**. El día que las dos cosas se separan —se actualiza la
# rama y no el argumento— la imagen informa por `/salud` una revisión que no es la suya, y esa
# falla NO TIENE SÍNTOMA: el servicio anda perfecto mientras miente sobre qué está corriendo.
#
# Por eso ahora la revisión se deriva del propio árbol cuando el árbol la trae. Los tres caminos
# reales de construcción la traen: el despliegue desde la URL del repositorio —con
# `BUILDKIT_CONTEXT_KEEP_GIT_DIR=1`—, la composición de verificación de al lado, que construye
# sobre el árbol de trabajo, y la canalización, que deja el `.git` del checkout.
#
# LA COPIA ES TOLERANTE A LA AUSENCIA, y la forma `.gi[t]` es deliberada: es un patrón, y un
# patrón que no encuentra nada **no rompe la construcción**. Construir desde un tarball sin `.git`
# sigue funcionando y ahí sí manda el argumento. Con `COPY .git` a secas, ese caso fallaría.
ARG SOURCE_REVISION_ID=desconocida

COPY Directory.Build.props ./
COPY GeometriaFactory.sln ./
COPY src/ ./src/

RUN dotnet restore src/GeometriaFactory.Api/GeometriaFactory.Api.csproj

# Va DESPUÉS del restore para no invalidarle la caché en cada commit.
COPY .gi[t] ./.git/

RUN set -eu; \
    revision="$SOURCE_REVISION_ID"; \
    if [ -f .git/HEAD ]; then revision="$(git -c safe.directory='*' rev-parse HEAD)"; fi; \
    echo "Sellando la imagen con la revisión $revision"; \
    dotnet publish src/GeometriaFactory.Api/GeometriaFactory.Api.csproj \
      -c Release -o /publish --no-restore \
      -p:SourceRevisionId="$revision"

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
