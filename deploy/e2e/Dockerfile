# ============================================================================================
# IMAGEN DE LA CORRIDA DE EXTREMO A EXTREMO. No despliega nada: sólo sabe correr un navegador
# de verdad y compilar .NET, que es lo que la suite necesita y el anfitrión NO TIENE —el host de
# esta casa no lleva kit de desarrollo, por decisión de la etapa `a`—.
#
# POR QUE EXISTE ESTE ARCHIVO, Y ES LA RAZON PRINCIPAL: la primera corrida de la suite, el
# 2026-09-02, se hizo con una imagen `gf-e2e:local` ARMADA A MANO Y NUNCA VERSIONADA. La suite
# daba verde y el modo de correrla vivía en la memoria de una sesión: nadie más podía repetirlo,
# y una prueba que sólo una persona puede correr no es una compuerta, es una anécdota.
#
# LA BASE ES LA IMAGEN OFICIAL DE PLAYWRIGHT DE LA MISMA VERSION QUE EL PAQUETE. Trae los tres
# navegadores y —lo que de verdad cuesta reponer— las bibliotecas de sistema que piden. Si la
# versión de `Microsoft.Playwright.NUnit` en `GeometriaFactory.E2ETests.csproj` cambia, ESTA
# ETIQUETA CAMBIA CON ELLA: con versiones cruzadas el binding descarga su propia copia de los
# navegadores y la imagen deja de servir para lo único que se la trajo.
FROM mcr.microsoft.com/playwright:v1.49.0-noble

# El kit de desarrollo no viene en la imagen de Playwright y no hay paquete oficial para noble
# que traiga .NET 10: se instala con el guion de Microsoft, anclado al canal.
RUN apt-get update \
 && apt-get install -y --no-install-recommends curl ca-certificates \
 && rm -rf /var/lib/apt/lists/* \
 && curl -fsSL https://dot.net/v1/dotnet-install.sh -o /tmp/dotnet-install.sh \
 && bash /tmp/dotnet-install.sh --channel 10.0 --install-dir /usr/share/dotnet \
 && ln -sf /usr/share/dotnet/dotnet /usr/local/bin/dotnet \
 && rm /tmp/dotnet-install.sh

ENV DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    DOTNET_NOLOGO=1 \
    PLAYWRIGHT_BROWSERS_PATH=/ms-playwright
