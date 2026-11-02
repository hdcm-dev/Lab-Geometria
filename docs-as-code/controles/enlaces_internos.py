#!/usr/bin/env python3
"""enlaces-internos: enlaces relativos y anclas, sin red (expediente 0003, tramo T-2).

En docs-guard este control (PR-1) lo hace lychee `--offline --include-fragments`, un binario que se baja con versión y
SHA-256 fijos. La compuerta de Lab-Geometria corre sólo con el `python3` del runner y sin bajar herramientas, así que
el control se porta acá con la biblioteca estándar. Es un apartamiento parcial de la copia, avisado (K-13).

    python3 -I docs-as-code/controles/enlaces_internos.py [--raiz DIR] RUTA...

- `enlace-roto`: el destino relativo de un enlace Markdown no existe.
- `ancla-rota`: el fragmento `#…` no coincide con ningún encabezado (slug al estilo de GitHub) ni con un `id`/`name`
  explícito del archivo de destino `.md` (o del mismo archivo, si el enlace es sólo `#…`).
- No mira URL con esquema (`https:`, `mailto:`…) ni rutas absolutas: esas son del carril programado (ola 2).

Código de salida: 0 en orden, 1 hay hallazgos, 2 error de uso. Reutiliza la copia con huella (.docs-guard/dg.py).
"""

import importlib.util
import re
import sys
from pathlib import Path
from urllib.parse import unquote

RAIZ_REPO = Path(__file__).resolve().parents[2]
RE_ID_HTML = re.compile(r'<a\s[^>]*\b(?:id|name)="([^"]+)"', re.I)


def cargar_dg():
    spec = importlib.util.spec_from_file_location('dg', RAIZ_REPO / '.docs-guard' / 'dg.py')
    mod = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(mod)
    return mod


def slug(texto):
    """Aproximación del slug de encabezados de GitHub: texto visible en minúsculas, sin puntuación salvo - y _."""
    t = re.sub(r'!?\[([^\]]*)\]\([^)]*\)', r'\1', texto)  # enlaces: queda el texto
    t = re.sub(r'<[^>]+>', '', t)                          # etiquetas HTML
    t = t.replace('`', '').replace('*', '')
    t = t.strip().lower()
    t = re.sub(r'[^\w\- ]', '', t)
    return t.replace(' ', '-')


def anclas_de(dg, archivo, cache):
    if archivo not in cache:
        vistos, salida = {}, set()
        texto = dg.leer(archivo)
        for _, l, cod in dg.fuera_de_codigo(texto):
            if cod:
                continue
            m = re.match(r'^#{1,6}\s+(.*?)\s*#*\s*$', l)
            if m:
                s = slug(m.group(1))
                n = vistos.get(s, 0)
                vistos[s] = n + 1
                salida.add(s if n == 0 else f'{s}-{n}')
            for a in RE_ID_HTML.finditer(l):
                salida.add(a.group(1))
        cache[archivo] = salida
    return cache[archivo]


def main(argv):
    dg = cargar_dg()
    raiz, rutas = None, []
    i = 0
    while i < len(argv):
        if argv[i] == '--raiz':
            raiz = argv[i + 1]
            i += 1
        else:
            rutas.append(argv[i])
        i += 1
    if not rutas:
        print(__doc__)
        return 2
    raiz = dg.raiz_repo(raiz)
    inf = dg.Informe('enlaces-internos')
    total = 0
    cache = {}
    try:
        for f in dg.markdowns(rutas):
            ruta = dg.rel(f, raiz)
            for n, l, cod in dg.fuera_de_codigo(dg.leer(f)):
                if cod:
                    continue
                l = re.sub(r'`[^`]*`', '', l)  # un enlace dentro de un code span no es enlace
                for m in dg.RE_ENLACE.finditer(l):
                    destino = m.group(1)
                    if re.match(r'^[a-z][a-z0-9+.-]*:', destino, re.I) or destino.startswith('/'):
                        continue
                    total += 1
                    camino, _, frag = destino.partition('#')
                    objetivo = (f.parent / unquote(camino)).resolve() if camino else f.resolve()
                    if not objetivo.exists():
                        inf.error(ruta, n, 'enlace-roto', f'{destino}: no existe {dg.rel(objetivo, raiz)}')
                        continue
                    if frag and objetivo.is_file() and objetivo.suffix == '.md':
                        if unquote(frag).lower() not in {a.lower() for a in anclas_de(dg, objetivo, cache)}:
                            inf.error(ruta, n, 'ancla-rota', f'{destino}: #{frag} no es un encabezado de {dg.rel(objetivo, raiz)}')
    except FileNotFoundError as e:
        print(f'ERROR: no existe {e}', file=sys.stderr)
        return 2
    return inf.cerrar(f' enlaces={total}')


if __name__ == '__main__':
    sys.exit(main(sys.argv[1:]))
