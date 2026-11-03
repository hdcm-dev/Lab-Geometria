#!/usr/bin/env python3
"""anclas: el catálogo del punto de uso y los .razor no se separan (expediente 0003, tramo T-3).

    python3 -I docs-as-code/controles/anclas.py [--raiz DIR] RUTA...

Recorre los catálogos (.md con la tabla entre `<!-- catalogo:inicio -->` y `<!-- catalogo:fin -->`) y, por fila:

- `contexto-invalido`: el contexto no tiene la forma `web/<id-del-elemento>`.
- `capa-invalida`: la capa no es una de etiqueta, pista, info, ejemplo, validacion.
- `fuente-ausente` / `linea-fuera`: la `ruta[:línea]` no existe en el repositorio.
- `id-ausente`: la fuente es un .razor y no tiene un elemento con `id="<id>"` (el ancla es el id, no el texto) ni un
  `AyudaDeCampo` con `Contexto="web/<id>"`, que el componente resuelve a ese mismo id (tramo T-11).
- `texto-ausente`: la fuente es un .razor y el texto de la fila no está contenido en él (sin etiquetas HTML, sin
  comillas invertidas y con los blancos normalizados en los dos lados).
- `linea-desplazada`: el texto está en el archivo, pero no empieza en la línea citada ni en las tres siguientes.

Código de salida: 0 en orden, 1 hay hallazgos, 2 error de uso. Arranca en avisa (D-10).
"""

import re
import sys
from pathlib import Path

sys.dont_write_bytecode = True  # no deja __pycache__ en el árbol
sys.path.insert(0, str(Path(__file__).resolve().parent))
import catalogo_pu as pu  # noqa: E402

RE_CONTEXTO = re.compile(r'^web/([a-z0-9]+(?:-[a-z0-9]+)*)$')


def main(argv):
    dg = pu.cargar_dg()
    ops, rutas = pu.argumentos(argv, con_valor=('--raiz',))
    if not rutas:
        print(__doc__)
        return 2
    raiz = dg.raiz_repo(ops.get('--raiz'))
    inf = dg.Informe('anclas')
    total = 0
    cache = {}
    try:
        for f in dg.markdowns(rutas):
            texto = dg.leer(f)
            if not pu.es_catalogo(texto):
                continue
            ruta = dg.rel(f, raiz)
            for n, contexto, capa, txt, fuente in pu.filas(texto):
                total += 1
                m = RE_CONTEXTO.match(contexto)
                if not m:
                    inf.error(ruta, n, 'contexto-invalido', f'«{contexto}» no es web/<id-del-elemento>')
                if capa not in pu.CAPAS:
                    inf.error(ruta, n, 'capa-invalida', f'«{capa}» no es una de {", ".join(pu.CAPAS)}')
                camino, _, linea = fuente.partition(':')
                destino = raiz / camino
                if not destino.is_file():
                    inf.error(ruta, n, 'fuente-ausente', f'{fuente}: no existe {camino}')
                    continue
                if destino not in cache:
                    cache[destino] = dg.leer(destino).split('\n')
                lineas = cache[destino]
                num = int(linea) if linea.isdigit() else None
                if linea and (num is None or not 1 <= num <= len(lineas)):
                    inf.error(ruta, n, 'linea-fuera', f'{fuente}: {camino} tiene {len(lineas)} líneas')
                    continue
                if destino.suffix != '.razor':
                    continue
                todo = '\n'.join(lineas)
                if m and f'id="{m.group(1)}"' not in todo and f'Contexto="web/{m.group(1)}"' not in todo:
                    inf.error(ruta, n, 'id-ausente', f'{camino} no tiene un elemento con id="{m.group(1)}"')
                buscado = pu.normalizar(txt)
                if buscado not in pu.normalizar('\n'.join(lineas)):
                    inf.error(ruta, n, 'texto-ausente', f'«{buscado[:60]}» no está en {camino}')
                    continue
                if num is not None:
                    ventana = pu.normalizar('\n'.join(lineas[num - 1:num + 3]))
                    if buscado[:30] not in ventana:
                        inf.error(ruta, n, 'linea-desplazada', f'el texto no empieza en {fuente} ni en las 3 líneas siguientes')
    except FileNotFoundError as e:
        print(f'ERROR: no existe {e}', file=sys.stderr)
        return 2
    return inf.cerrar(f' filas={total}')


if __name__ == '__main__':
    sys.exit(main(sys.argv[1:]))
