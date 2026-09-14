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

# LA VERSIÓN TAMBIÉN SALE DEL ÁRBOL, y necesita más árbol que la revisión. `Directory.Build.props`
# adopta MinVer: la versión SemVer se calcula desde la última etiqueta `v*` alcanzable y la altura
# hasta ella. La revisión se lee con un solo commit; la versión no.
#
# Y EL CONTEXTO LLEGA RECORTADO. Cuando se construye desde la URL del repositorio, BuildKit hace el
# checkout con `--depth=1 --no-tags` aunque se pida conservar el `.git`: un commit, cero etiquetas.
# MinVer sobre eso calcula `0.0.0-alpha.0` SIN ADVERTIR —medido el 2026-09-12—, y `/salud` informaría
# una versión que no es la del producto, con la misma falla sin síntoma que motivó sellar la revisión.
#
# POR ESO, SI EL CLON ES SUPERFICIAL, SE COMPLETA ANTES DE PUBLICAR. `.git/shallow` existe sólo en
# ese caso; sobre un clon completo `--unshallow` falla y no hay nada que completar. El `origin` es el
# que dejó quien construyó —BuildKit apunta a la URL del repositorio— y la red ya se usó para el
# `restore`. Si igual no se puede completar, LA CONSTRUCCIÓN NO SE ROMPE: se avisa y la versión sale
# como `0.0.0-alpha.0`, que es una versión visiblemente falsa y no una silenciosamente parecida.
RUN set -eu; \
    revision="$SOURCE_REVISION_ID"; \
    if [ -f .git/HEAD ]; then \
      revision="$(git -c safe.directory='*' rev-parse HEAD)"; \
      if [ -f .git/shallow ]; then \
        echo "El clon es superficial: se completa el historial y las etiquetas para calcular la versión"; \
        git -c safe.directory='*' fetch --unshallow --tags origin \
          || echo "AVISO: no se pudo completar el historial; la versión saldrá como 0.0.0-alpha.0"; \
      fi; \
    fi; \
    echo "Sellando la imagen con la revisión $revision"; \
    dotnet publish src/GeometriaFactory.Api/GeometriaFactory.Api.csproj \
      -c Release -o /publish --no-restore \
      -p:SourceRevisionId="$revision"

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# El almacén vive en un volumen, no en la imagen.
ENV ASPNETCORE_URLS=http://+:8080 \
    ConnectionStrings__Store="Data Source=/datos/geometriafactory.db"

# DOS HERRAMIENTAS, Y CADA UNA CON SU MOTIVO. Son las únicas que se agregan sobre el
# entorno de ejecución, y la lista no crece sin que alguien escriba acá por qué.
#
#   `curl`    — el `healthcheck` de `compose.yaml` lo necesita y la imagen no lo trae.
#   `sqlite3` — para que la copia consistente del almacén se pueda tomar DESDE ADENTRO
#               del contenedor. Sin esto, `respaldo-almacen.sh --desde-contenedor`
#               dependería de lo que el host del destino tenga instalado, que es
#               justamente lo que no se puede saber de antemano: el destino es la
#               máquina del docente. `Entornos-Deploy.md` §11.1 declara el respaldo como
#               el único mecanismo de vuelta atrás del producto, y un mecanismo que
#               depende de una herramienta que puede no estar no es un mecanismo.
#
# NINGUNA DE LAS DOS LLEVA VERSIÓN ANCLADA, y es el precedente que `curl` ya sentó: son
# herramientas del sistema operativo base y no dependencias del producto. Si algún día se
# anclan, SE ANCLAN LAS DOS JUNTAS, y eso es otra decisión.
RUN apt-get update \
    && apt-get install --yes --no-install-recommends curl sqlite3 \
    && rm -rf /var/lib/apt/lists/*

# EL SERVICIO CORRE COMO EL USUARIO SIN PRIVILEGIOS DE LA IMAGEN BASE (`app`, `$APP_UID` = 1654), y
# no como root (mesa `SDD/Docs/Audit/Mesa-2026-09-14.md`, imágenes sin `USER`). El directorio del
# almacén es suyo desde la imagen, de modo que un volumen NUEVO nace con el dueño correcto.
#
# UN VOLUMEN QUE YA EXISTE CONSERVA SU DUEÑO, y si lo creó una versión anterior de esta imagen es
# root. Antes del primer despliegue de esta versión, quien despliega lo pasa al usuario del servicio:
#
#   docker run --rm -v <volumen>:/datos alpine chown -R 1654:1654 /datos
#
# SIN ESO EL SERVICIO NO ARRANCA: sale con código 78 y `attempt to write a readonly database`
# (medido el 2026-09-14 sobre un volumen con archivos de root). Los guiones de
# respaldo y restauración siguen andando: escriben en `/tmp` y en `/datos` del contenedor, y del
# montaje del anfitrión sólo leen la copia.
RUN mkdir -p /datos && chown "$APP_UID:$APP_UID" /datos
VOLUME ["/datos"]

COPY --from=build /publish ./
USER $APP_UID
EXPOSE 8080

ENTRYPOINT ["dotnet", "GeometriaFactory.Api.dll"]
