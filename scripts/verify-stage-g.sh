#!/usr/bin/env bash
# ============================================================================
# verify-stage-g.sh — Puerta de los SIETE criterios de transición de la etapa
# `g` (`Roadmap-Producto.md` §5.2, `g` → `h`):
#
#   G-1  Las tres figuras del escenario semilla se dibujan, ORTOEDRO INCLUIDO.
#   G-2  Navegar entre trabajos ida y vuelta diez veces no degrada la
#        visualización.
#   G-3  Procesar el mismo trabajo dos veces produce la MISMA DISPOSICIÓN,
#        predicada de la posición de cada pieza derivada de su índice y no de
#        su orientación en un instante.
#   G-4  Durante la interacción tridimensional NO HAY NI UNA SOLA PETICIÓN
#        originada por la visualización.
#   G-5  El árbol y la escena se sincronizan POR ÍNDICE DE PIEZA.
#   G-6  El administrador abre cualquier trabajo que ve y encuentra
#        EXACTAMENTE LO MISMO que vio el alumno.
#   G-7  Los dos movimientos automáticos de `F-25` se gobiernan POR SEPARADO,
#        se detienen mientras se arrastra, y su estado inicial lo fija la pieza
#        pública pasando dos valores de verdad.
#
# POR QUÉ ESTE GUION EXISTE. Las etapas `c` y `b` tienen el suyo; `d`, `e`, `f` y
# `g` no tenían ninguno, y el orquestador de reanudación lo dejó declarado como
# estado observado el 2026-08-17. Una puerta sin guion se verifica cuando alguien
# se acuerda, que es la forma en que este producto ya vio degradarse otras cosas.
#
# DÓNDE SE MIDE CADA UNO, Y POR QUÉ NO TODOS EN EL MISMO LADO. Los criterios se
# reparten según de qué se predican, no por comodidad:
#
#   * CUATRO SE PREDICAN DE LO QUE PASA DENTRO DE LA ESCENA —G-1, G-3, G-4 y
#     G-5— y ninguna prueba de integración puede afirmarlos: la prueba ve el
#     marcado que se sirve, no la escena que el navegador construye. Van a
#     `visor/verification/stage-g.mjs`, CON NAVEGADOR DE VERDAD, midiendo por la
#     fachada pública y por los píxeles del lienzo.
#   * G-7 se mide EN LOS DOS LADOS y es correcto que así sea: que los dos
#     movimientos se gobiernen por separado y se detengan al arrastrar es de la
#     escena; que la pieza pública les pase DOS VALORES DE VERDAD —y no uno— es
#     del marcado, y lo mide la batería.
#   * G-2 ya tiene su medición: es la puerta técnica `PT-02`, y este guion la
#     invoca en lugar de reescribirla.
#   * G-6 es de la superficie servida, no de la escena: se mide en la batería.
#
# NO SE LE AGREGA AL PAQUETE NINGUNA FUNCIÓN DE MEDICIÓN. Las seis funciones de
# la fachada las fijó el Product Owner; un banco que necesitara una séptima para
# poder medir estaría midiendo otro producto.
# ============================================================================
set -uo pipefail

raiz="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$raiz"

fails=0
ok()  { printf '  \033[32mOK\033[0m   %s\n' "$1"; }
bad() { printf '  \033[31mFALLA\033[0m %s\n' "$1"; fails=$((fails + 1)); }

echo "== Puerta de la etapa \`g\` → \`h\` · siete criterios =="
echo

# ---------------------------------------------------------------------------
# El paquete se mide CONSTRUIDO, que es lo que se sirve.
# ---------------------------------------------------------------------------
if [ ! -f visor/dist/geometriafactory-visor.js ]; then
  printf 'El paquete del visor no está construido. Corré primero: bash scripts/build-visor.sh\n' >&2
  exit 1
fi

# ---------------------------------------------------------------------------
# G-1, G-3, G-4, G-5 y la mitad de escena de G-7 · con navegador
# ---------------------------------------------------------------------------
echo "-- G-1, G-3, G-4, G-5 y la mitad de escena de G-7 · con navegador --"

imagen="mcr.microsoft.com/playwright:v1.48.0-jammy"

if docker run --rm -v "$raiz/visor":/v -w /v "$imagen" \
     bash -c 'npm install --no-save playwright@1.48.0 >/dev/null 2>&1 && node verification/stage-g.mjs'; then
  ok "los cinco criterios con navegador pasan"
else
  bad "algún criterio con navegador falla, ver el detalle de arriba"
fi

echo

# ---------------------------------------------------------------------------
# G-2 · la puerta técnica PT-02, que ya tiene su medición
# ---------------------------------------------------------------------------
echo "-- G-2 · diez navegaciones sin degradación (PT-02) --"

if bash scripts/verify-viewer-lifecycle.sh; then
  ok "PT-02 pasa: crear y liberar diez veces no degrada"
else
  bad "PT-02 falla"
fi

echo

# ---------------------------------------------------------------------------
# G-6 y la mitad de marcado de G-7 · la batería
# ---------------------------------------------------------------------------
echo "-- G-6 y la mitad de marcado de G-7 · la batería --"

if dotnet test tests/GeometriaFactory.Integration.Tests --configuration Release \
     --filter "FullyQualifiedName~TheAdministratorOpensTheWorkAndFindsExactlyWhatTheStudentSaw|FullyQualifiedName~TheWorkViewBringsTheSceneTheTreeAndTheTwoMotionControls|FullyQualifiedName~PreviewingDrawsWithoutSavingAndWithoutTheBrowserCallingTheDataService" \
     >/tmp/etapa-g-bateria.log 2>&1; then
  ok "$(grep -oP 'Passed:\s+\K\d+' /tmp/etapa-g-bateria.log | tail -1) pruebas de la batería pasan"
else
  bad "la batería falla; ver /tmp/etapa-g-bateria.log"
  tail -20 /tmp/etapa-g-bateria.log
fi

echo
[ "$fails" -eq 0 ] && { echo "CONFORME · los siete criterios de transición de la etapa \`g\` se verifican"; exit 0; }
echo "NO CONFORME · $fails comprobacion(es) fallan"; exit 1
