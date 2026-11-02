#!/usr/bin/env python3
"""delivery-publico: extensión de la compuerta de docs de Lab-Geometria (expediente 0003, tramo T-2).

Regla (D-18 y K-06 de la resolución del expediente 0003): en estos repositorios públicos todo `delivery:` vale
`publico`. No se crean piezas `producto`, `restringido` ni `interno`.

    python3 -I docs-as-code/controles/delivery_publico.py [--exigir] [--raiz DIR] RUTA...

- Un frontmatter que declara `delivery` con otro valor que `[publico]` da hallazgo `delivery-no-publico`.
- Con `--exigir`, un documento con `doc_id` que no declara `delivery` da hallazgo `delivery-falta` (lo usa el ámbito
  `docs-as-code/**`, donde el esquema de la carpeta lo pide; ver docs-as-code/esquema-frontmatter.md).

Código de salida igual que la copia de docs-guard: 0 en orden, 1 hay hallazgos, 2 error de uso. Reutiliza el lector de
frontmatter y el informe de la copia con huella (.docs-guard/dg.py), sin modificarla.
"""

import importlib.util
import sys
from pathlib import Path

RAIZ_REPO = Path(__file__).resolve().parents[2]


def cargar_dg():
    spec = importlib.util.spec_from_file_location('dg', RAIZ_REPO / '.docs-guard' / 'dg.py')
    mod = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(mod)
    return mod


def main(argv):
    dg = cargar_dg()
    exigir, raiz, rutas = False, None, []
    i = 0
    while i < len(argv):
        if argv[i] == '--exigir':
            exigir = True
        elif argv[i] == '--raiz':
            raiz = argv[i + 1]
            i += 1
        else:
            rutas.append(argv[i])
        i += 1
    if not rutas:
        print(__doc__)
        return 2
    raiz = dg.raiz_repo(raiz)
    inf = dg.Informe('delivery-publico')
    vistos = 0
    try:
        for f in dg.markdowns(rutas):
            fm, _ = dg.frontmatter(dg.leer(f))
            if fm is None:
                continue
            ruta = dg.rel(f, raiz)
            if 'delivery' not in fm:
                if exigir and fm.get('doc_id'):
                    inf.error(ruta, 1, 'delivery-falta', 'el documento no declara delivery; en este repo es [publico]')
                continue
            vistos += 1
            v = fm['delivery']
            valores = v if isinstance(v, list) else [v]
            if [str(x) for x in valores] != ['publico']:
                inf.error(ruta, 1, 'delivery-no-publico',
                          f'delivery {valores}: en estos repos públicos el único valor es publico (D-18)')
    except FileNotFoundError as e:
        print(f'ERROR: no existe {e}', file=sys.stderr)
        return 2
    return inf.cerrar(f' con-delivery={vistos}')


if __name__ == '__main__':
    sys.exit(main(sys.argv[1:]))
