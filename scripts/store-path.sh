#!/usr/bin/env bash
# ============================================================================
# store-path.sh — DÓNDE VIVE EL ALMACÉN DE DESARROLLO. Un solo lugar que lo
# resuelve, para que el guion que levanta el servicio y el guion que BORRA el
# almacén no puedan estar mirando archivos distintos.
#
# Este archivo SE SOURCEA, no se ejecuta:
#
#   . "$(dirname "${BASH_SOURCE[0]}")/store-path.sh"
#   gf_resolve_store   # deja GF_STORE_FILE y exporta ConnectionStrings__Store
#
# ---------------------------------------------------------------------------
# POR QUÉ EL ALMACÉN SALIÓ DE ADENTRO DEL ÁRBOL DEL REPOSITORIO
#
# Hasta acá el almacén de desarrollo era `src/GeometriaFactory.Api/geometriafactory.db`:
# adentro del árbol, al lado del código, y `scripts/reset-db.sh` lo borraba sin
# preguntar nada. El 2026-08-15 una corrida de guiones se llevó la cuenta de
# administrador del Product Owner. La pérdida fue chica porque todavía no hay
# trabajos; en la etapa `e` el mismo archivo va a tener las entregas de una comisión
# y el mismo descuido se las lleva.
#
# EL DEFECTO ERA DE DISEÑO, NO DE QUIEN CORRIÓ EL GUION: no existía ninguna
# separación entre «la base con la que el docente está trabajando» y «la ruta sobre
# la que operan los guiones». Eran la misma ruta, y una de las dos cosas era
# destructiva. Sacar el archivo del árbol no lo vuelve indestructible —para eso está
# la confirmación de `reset-db.sh`—, pero le saca dos propiedades que lo hacían
# frágil: deja de estar donde `git clean` y los borrados de árbol pasan, y deja de
# ser el valor por omisión que cualquier `dotnet run` a secas encuentra solo.
#
# ---------------------------------------------------------------------------
# LA UBICACIÓN ELEGIDA, Y POR QUÉ ÉSA
#
#   ${XDG_DATA_HOME:-$HOME/.local/share}/geometria-factory/geometriafactory.db
#
# La convención es la XDG Base Directory Specification, que es la que el sistema ya
# usa y la que la clave de firma ya sigue en `~/.config/geometria-factory/access-token.key`.
# La especificación separa por ROL, y la elección entre sus dos candidatos no es
# indistinta:
#
#   `$XDG_CONFIGURATION` — `~/.config` — es para configuración. Ahí va la clave de
#   firma, que es un parámetro: se pierde, se genera otra y no se perdió nada.
#
#   `$XDG_STATE_HOME` — `~/.local/state` — es para estado que conviene conservar
#   entre ejecuciones pero que NO es lo bastante importante ni portable como para
#   `$XDG_DATA_HOME`: registros, historiales, disposición de ventanas. Cosas que se
#   pueden tirar.
#
#   `$XDG_DATA_HOME` — `~/.local/share` — es para DATOS DEL USUARIO. Es la que
#   corresponde: acá adentro hay cuentas y, desde la etapa `e`, los trabajos de los
#   alumnos. Es exactamente lo que alguien querría respaldar y lo que nadie querría
#   perder. Ponerlo en `$XDG_STATE_HOME` diría por escrito que se puede tirar, que es
#   la afirmación que nos costó una cuenta.
#
# El directorio de aplicación se llama igual que el de la clave —`geometria-factory`—
# para que las dos cosas del producto que viven fuera del árbol se encuentren juntas.
#
# ---------------------------------------------------------------------------
# CÓMO LLEGA LA RUTA AL SERVICIO: POR CONFIGURACIÓN, Y NUNCA ESCRITA EN EL CÓDIGO
#
# El servicio la lee de `ConnectionStrings:Store` y eso NO cambia. Lo que cambia es de
# dónde sale el valor. Tres candidatos, y por qué se descartaron dos:
#
#   (a) Ruta absoluta en `appsettings.json`. DESCARTADO: `appsettings.json` está
#       versionado y se hornea en la imagen de despliegue. Una ruta absoluta ahí es la
#       ruta de UNA máquina, y rompe a todos los demás.
#
#   (b) Valor por omisión calculado en el arranque, adentro del código. DESCARTADO por
#       dos motivos. El primero es la regla que este repositorio ya tiene: la ruta del
#       almacén llega por configuración y no está escrita en el código. El segundo es
#       concreto y peor: el contenedor de desarrollo `gf-back` corre con `HOME=/tmp`,
#       de modo que un valor por omisión calculado sobre `$HOME` resolvería
#       `/tmp/.local/share/...`, ADENTRO del contenedor y sobre un sistema de archivos
#       efímero. El almacén se perdería en cada relanzamiento y el defecto se vería
#       igual que hoy: datos que desaparecen sin que nadie los haya borrado.
#
#   (c) LA ELEGIDA: la ruta llega por la variable de entorno `ConnectionStrings__Store`,
#       que es el mecanismo que la configuración de .NET ya define para esta misma
#       llave y el que `deploy/Dockerfile` ya usa. Este archivo la calcula y la
#       exporta; los guiones de desarrollo lo sourcean.
#
# Y `appsettings.json` DEJA DE DECLARAR LA LLAVE. Eso es deliberado y es la mitad que
# hace que la solución sirva: sin valor por omisión, un `dotnet run` a secas ya no
# encuentra una ruta relativa y crea la base adentro del árbol. Se detiene en el
# arranque, con el mensaje de `CompositionRoot` diciendo qué llave falta. Es el mismo
# criterio que ya rige para la clave de firma: una pieza a la que le falta algo sin lo
# cual no puede cumplir su función se niega a arrancar, para que el defecto aparezca
# donde lo ve quien despliega.
#
# EFECTO EN EL CONTENEDOR Y EN EL DESPLIEGUE, dicho sin adornar:
#
#   · `deploy/Dockerfile` NO SE TOCA y no se rompe: ya trae
#     `ConnectionStrings__Store="Data Source=/datos/geometriafactory.db"` en su `ENV`,
#     sobre el volumen `/datos`. El despliegue nunca dependió de `appsettings.json`.
#
#   · El contenedor de desarrollo `gf-back` SÍ hay que relanzarlo, porque hoy corre
#     `dotnet run --no-build` sin esa variable y sin el directorio del anfitrión
#     montado. El comando exacto está en el informe de este cambio y en
#     `scripts/run-api.sh`.
#
#   · Las tres baterías de prueba no se enteran: cada una fija su propia cadena de
#     conexión (`DataServiceHarness`, `SigningKeyStartupTests`) y nunca leyeron
#     `appsettings.json` para esto.
# ============================================================================

# Resuelve la ubicación del almacén de desarrollo y la deja disponible de dos formas:
#   GF_STORE_FILE              — la ruta desnuda, que es lo que un `rm` necesita
#   ConnectionStrings__Store   — la cadena de conexión, que es lo que el servicio lee
#
# PRECEDENCIA, de más específico a menos:
#   1. `GEOMETRIAFACTORY_STORE_FILE` — ruta desnuda puesta a mano. Ya existía en
#      `reset-db.sh` y se conserva.
#   2. `ConnectionStrings__Store` — si el entorno ya trae la cadena de conexión, se
#      respeta y se le extrae la ruta. Es lo que hace que `reset-db.sh` borre
#      exactamente el archivo que `run-api.sh` va a usar, y no otro.
#   3. La ubicación XDG de más arriba.
gf_resolve_store() {
  if [ -n "${GEOMETRIAFACTORY_STORE_FILE:-}" ]; then
    GF_STORE_FILE="$GEOMETRIAFACTORY_STORE_FILE"
  elif [ -n "${ConnectionStrings__Store:-}" ]; then
    # `Data Source=/ruta/al.db;Cache=Shared` -> `/ruta/al.db`
    GF_STORE_FILE="${ConnectionStrings__Store#*[Dd]ata [Ss]ource=}"
    GF_STORE_FILE="${GF_STORE_FILE%%;*}"
  else
    GF_STORE_FILE="${XDG_DATA_HOME:-$HOME/.local/share}/geometria-factory/geometriafactory.db"
  fi

  ConnectionStrings__Store="Data Source=$GF_STORE_FILE"
  export ConnectionStrings__Store
  export GF_STORE_FILE
}

# Crea el directorio que contiene el almacén. SQLite crea el archivo si no existe, pero
# NO crea el directorio: sin esto el primer arranque en una máquina limpia falla con un
# «unable to open database file» que no dice nada de lo que pasa.
gf_ensure_store_directory() {
  mkdir -p "$(dirname "$GF_STORE_FILE")"
}
