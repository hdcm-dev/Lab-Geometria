#!/usr/bin/env python3
"""pista-sin-enlaces: la capa `pista` del catálogo no manda a otro lado (expediente 0003, tramo T-3).

    python3 -I docs-as-code/controles/pista_sin_enlaces.py [--raiz DIR] RUTA...

La pista va bajo la etiqueta y explica cómo se completa el campo. No lleva enlaces: un enlace en esa capa saca al
alumno del formulario y puede llevarlo a material que resuelve el trabajo (RD-01). Lo que hace falta ampliar va en la
capa `info` del catálogo o en una ficha de regla.

- `pista-con-enlace`: una fila de capa `pista` trae un enlace Markdown, una etiqueta `<a` o una URL con esquema.

Código de salida: 0 en orden, 1 hay hallazgos, 2 error de uso. Arranca en avisa (D-10).
"""

import re
import sys
from pathlib import Path

sys.dont_write_bytecode = True  # no deja __pycache__ en el árbol
sys.path.insert(0, str(Path(__file__).resolve().parent))
import catalogo_pu as pu  # noqa: E402

RE_ENLACE = re.compile(r'\[[^\]]*\]\([^)]*\)|<a[\s>]|\b[a-z][a-z0-9+.-]*://', re.I)


def main(argv):
    dg = pu.cargar_dg()
    ops, rutas = pu.argumentos(argv, con_valor=('--raiz',))
    if not rutas:
        print(__doc__)
        return 2
    raiz = dg.raiz_repo(ops.get('--raiz'))
    inf = dg.Informe('pista-sin-enlaces')
    pistas = 0
    try:
        for f in dg.markdowns(rutas):
            texto = dg.leer(f)
            if not pu.es_catalogo(texto):
                continue
            ruta = dg.rel(f, raiz)
            for n, contexto, capa, txt, _ in pu.filas(texto):
                if capa != 'pista':
                    continue
                pistas += 1
                m = RE_ENLACE.search(txt)
                if m:
                    inf.error(ruta, n, 'pista-con-enlace', f'{contexto}: la pista trae «{m.group(0)}»')
    except FileNotFoundError as e:
        print(f'ERROR: no existe {e}', file=sys.stderr)
        return 2
    return inf.cerrar(f' pistas={pistas}')


if __name__ == '__main__':
    sys.exit(main(sys.argv[1:]))
