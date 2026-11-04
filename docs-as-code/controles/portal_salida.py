#!/usr/bin/env python3
"""portal-salida: lo que SÍ tiene que estar, y bien, en la salida del portal (expediente 0003, folio 018 §7, PR-P5).

    python3 -I docs-as-code/controles/portal_salida.py [--raiz DIR] [--base URL] SITIO

SITIO es el sitio ya construido y completado por docs-as-code/portal/agentes.py (scripts/docs-portal.sh). La URL base
sale de --base o del sitemap (la URL más corta). Hallazgos (error):

- `sin-md-hermano`: una página HTML (`x/index.html`, fuera de `assets/` y `search/`) sin su Markdown crudo (`x.md` o
  `x/index.md`; la raíz, `index.md`).
- `enlace-no-publicado`: un `.md` crudo o `llms.txt` enlaza una URL del portal que no existe en el sitio, o deja un
  enlace relativo (agentes.py los reescribe todos como absolutos).
- `llms-full-tope`: falta `llms-full.txt` o supera 400 KB; falta `llms.txt` o no empieza por un H1.
- `linkset-invalido`: `.well-known/api-catalog` falta, no es JSON, no es un linkset de RFC 9727 (objeto `linkset` con
  una lista de contextos con `anchor` y relaciones con listas de `{href}`), le falta `service-desc` o `service-doc`, o
  una de sus URLs no es del portal (no puede apuntar a una instancia viva).
- `openapi-distinto`: el sitio sirve `api/openapi-v1.json` y no es idéntico byte a byte a la instantánea
  `docs-as-code/api/openapi-v1.json`.
- `api-recuento` (PR-P6): el repo tiene la instantánea y el sitio no sirve `api/openapi-v1.json` o `api/index.md`, o
  el conjunto de operaciones renderizadas en las páginas de grupo (`api/*.md`, un `## \`VERBO /ruta\`` cada una) no es
  el de la instantánea según docs-as-code/controles/recuento_openapi.py (la misma cantidad y las mismas rutas).
- `redoc-local` (PR-P6): con la instantánea, falta `api/redoc.html`, o carga un script que no es local (CDN) o que no
  está en el sitio.
- `anuncio-falso`: existe `mcp/` o `agent-card.json` (en cualquier lugar del sitio): no hay MCP ni agente A2A
  (folio 018 §5).

Sin dependencias. Código de salida: 0 en orden, 1 hay hallazgos, 2 error de uso. Arranca en avisa (folio 018 §7).
"""

import importlib.util
import json
import re
import sys
import xml.etree.ElementTree as ET
from pathlib import Path
from urllib.parse import urlparse

sys.dont_write_bytecode = True

RAIZ_REPO = Path(__file__).resolve().parents[2]
TOPE_FULL = 400 * 1024
RE_SCRIPT = re.compile(r'<script[^>]*\ssrc\s*=\s*["\']([^"\']+)["\']', re.I)
INFRA_DIRS = ('assets/', 'search/')
RE_ENLACE = re.compile(r'\]\((<[^>]*>|[^)\s]+)')
RE_REFERENCIA = re.compile(r'^\s{0,3}\[[^\]]+\]:\s*(\S+)')
RE_VALLA = re.compile(r'^\s{0,3}(```|~~~)')


def cargar_dg(nombre='dg', ruta=RAIZ_REPO / '.docs-guard' / 'dg.py'):
    spec = importlib.util.spec_from_file_location(nombre, ruta)
    mod = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(mod)
    return mod


def base_del_sitemap(sitio):
    try:
        locs = [e.text.strip() for e in ET.parse(sitio / 'sitemap.xml').iter() if e.tag.endswith('loc') and e.text]
    except (OSError, ET.ParseError):
        return None
    return min(locs, key=len) if locs else None


def enlaces(texto):
    valla = None
    for n, linea in enumerate(texto.split('\n'), 1):
        m = RE_VALLA.match(linea)
        if m:
            valla = None if valla == m.group(1) else (valla or m.group(1))
            continue
        if valla is not None:
            continue
        for d in RE_ENLACE.findall(linea):
            yield n, d.strip('<>')
        m = RE_REFERENCIA.match(linea)
        if m:
            yield n, m.group(1).strip('<>')


def existe_en_sitio(sitio, ruta):
    """`ruta` es el path relativo a la base (sin ancla). Acepta archivo, `ruta.html` o `ruta/index.html`."""
    if ruta == '' or ruta.endswith('/'):
        return (sitio / ruta / 'index.html').is_file()
    return (sitio / ruta).is_file() or (sitio / f'{ruta}.html').is_file() or (sitio / ruta / 'index.html').is_file()


def es_del_portal(url, base):
    return url.startswith(base)


def revisar_linkset(sitio, base, inf):
    ruta = '.well-known/api-catalog'
    f = sitio / ruta
    if not f.is_file():
        inf.error(ruta, 1, 'linkset-invalido', 'falta el api-catalog (RFC 9727)')
        return
    try:
        datos = json.loads(f.read_text(encoding='utf-8'))
    except ValueError as e:
        inf.error(ruta, 1, 'linkset-invalido', f'no es JSON: {e}')
        return
    contextos = datos.get('linkset') if isinstance(datos, dict) else None
    if not isinstance(contextos, list) or not contextos:
        inf.error(ruta, 1, 'linkset-invalido', 'falta `linkset` como lista no vacía de contextos')
        return
    rels = set()
    for i, c in enumerate(contextos):
        if not isinstance(c, dict) or not isinstance(c.get('anchor'), str):
            inf.error(ruta, 1, 'linkset-invalido', f'el contexto {i} no tiene `anchor`')
            continue
        urls = [c['anchor']]
        for rel, objetivos in c.items():
            if rel == 'anchor':
                continue
            if not isinstance(objetivos, list) or not all(isinstance(o, dict) and isinstance(o.get('href'), str)
                                                         for o in objetivos):
                inf.error(ruta, 1, 'linkset-invalido', f'la relación `{rel}` no es una lista de objetos con `href`')
                continue
            rels.add(rel)
            urls += [o['href'] for o in objetivos]
        for u in urls:
            if not es_del_portal(u, base):
                inf.error(ruta, 1, 'linkset-invalido', f'URL fuera del portal: «{u}»')
    for rel in ('service-desc', 'service-doc'):
        if rel not in rels:
            inf.error(ruta, 1, 'linkset-invalido', f'falta la relación `{rel}`')


def revisar_api(sitio, instantanea, inf):
    recuento = cargar_dg('recuento_openapi', RAIZ_REPO / 'docs-as-code' / 'controles' / 'recuento_openapi.py')
    ref = cargar_dg('referencia_api', RAIZ_REPO / 'docs-as-code' / 'portal' / 'referencia_api.py')
    try:
        esperadas = set(recuento.leer_instantanea(instantanea.read_text(encoding='utf-8')))
    except ValueError as e:
        inf.error('api/openapi-v1.json', 1, 'api-recuento', f'la instantánea no es JSON: {e}')
        return
    for rel in ('api/openapi-v1.json', 'api/index.md'):
        if not (sitio / rel).is_file():
            inf.error(rel, 1, 'api-recuento', 'el repo tiene la instantánea y el sitio no sirve la referencia de API')
    grupos = sorted(f for f in (sitio / 'api').glob('*.md') if f.name != 'index.md') if (sitio / 'api').is_dir() else []
    renderizadas = ref.contar(f.read_text(encoding='utf-8', errors='replace') for f in grupos)
    if renderizadas != esperadas:
        faltan = sorted(f'{v} {r}' for v, r in esperadas - renderizadas)
        sobran = sorted(f'{v} {r}' for v, r in renderizadas - esperadas)
        inf.error('api/index.md', 1, 'api-recuento',
                  f'{len(renderizadas)} operaciones renderizadas y recuento-openapi cuenta {len(esperadas)}; '
                  f'faltan {faltan or "—"}; sobran {sobran or "—"}')
    redoc = sitio / 'api' / 'redoc.html'
    if not redoc.is_file():
        inf.error('api/redoc.html', 1, 'redoc-local', 'falta la vista Redoc')
        return
    srcs = RE_SCRIPT.findall(redoc.read_text(encoding='utf-8', errors='replace'))
    if not srcs:
        inf.error('api/redoc.html', 1, 'redoc-local', 'no carga ningún script')
    for src in srcs:
        destino = (sitio / 'api' / src).resolve()
        if urlparse(src).scheme or src.startswith('//') or src.startswith('/') or not destino.is_file() \
                or not destino.is_relative_to(sitio.resolve()):
            inf.error('api/redoc.html', 1, 'redoc-local', f'script no local o ausente: «{src}»')


def main(argv):
    raiz, base, sitio = None, None, None
    args = iter(argv)
    for a in args:
        if a in ('--raiz', '--base'):
            v = next(args, None)
            if v is None:
                print(f'portal-salida: falta el valor de {a}', file=sys.stderr)
                return 2
            if a == '--raiz':
                raiz = v
            else:
                base = v
        elif a.startswith('-') or sitio is not None:
            print(f'portal-salida: argumento no esperado: {a}', file=sys.stderr)
            return 2
        else:
            sitio = Path(a)
    if sitio is None or not sitio.is_dir():
        print('uso: portal_salida.py [--raiz DIR] [--base URL] SITIO', file=sys.stderr)
        return 2
    raiz = Path(raiz).resolve() if raiz else RAIZ_REPO
    base = base or base_del_sitemap(sitio)
    if not base or not urlparse(base).scheme:
        print('portal-salida: sin URL base (ni --base ni sitemap.xml legible)', file=sys.stderr)
        return 2
    if not base.endswith('/'):
        base += '/'
    inf = cargar_dg().Informe('portal-salida')
    archivos = sorted(x.relative_to(sitio).as_posix() for x in sitio.rglob('*') if x.is_file())

    # Páginas HTML con su .md hermano.
    for rel in archivos:
        if rel.startswith(INFRA_DIRS) or not rel.endswith('index.html'):
            continue
        carpeta = rel[:-len('index.html')]
        hermanos = [f'{carpeta}index.md'] + ([f'{carpeta[:-1]}.md'] if carpeta else [])
        if not any((sitio / h).is_file() for h in hermanos):
            inf.error(rel, 1, 'sin-md-hermano', f'falta {" o ".join(hermanos)}')

    # Enlaces de los .md crudos y de llms.txt.
    revisar = [r for r in archivos if r.endswith('.md') and not r.startswith(INFRA_DIRS)]
    if 'llms.txt' in archivos:
        revisar.append('llms.txt')
    for rel in revisar:
        for n, url in enlaces((sitio / rel).read_text(encoding='utf-8', errors='replace')):
            if url.startswith('#'):
                continue
            u = urlparse(url)
            if not u.scheme and not url.startswith('//'):
                inf.error(rel, n, 'enlace-no-publicado', f'enlace relativo sin reescribir: «{url}»')
            elif es_del_portal(url, base):
                destino = url[len(base):].split('#', 1)[0].split('?', 1)[0]
                if not existe_en_sitio(sitio, destino):
                    inf.error(rel, n, 'enlace-no-publicado', f'la URL no existe en el portal: «{url}»')

    # llms.txt y llms-full.txt.
    llms = sitio / 'llms.txt'
    if not llms.is_file():
        inf.error('llms.txt', 1, 'llms-full-tope', 'falta llms.txt')
    elif not llms.read_text(encoding='utf-8', errors='replace').startswith('# '):
        inf.error('llms.txt', 1, 'llms-full-tope', 'llms.txt no empieza por un H1 (llmstxt.org)')
    full = sitio / 'llms-full.txt'
    if not full.is_file():
        inf.error('llms-full.txt', 1, 'llms-full-tope', 'falta llms-full.txt')
    elif full.stat().st_size > TOPE_FULL:
        inf.error('llms-full.txt', 1, 'llms-full-tope', f'{full.stat().st_size} bytes, tope {TOPE_FULL}')

    revisar_linkset(sitio, base, inf)

    # OpenAPI servido idéntico a la instantánea (lo sirve PR-P6; si todavía no está, no hay nada que comparar).
    servido = sitio / 'api' / 'openapi-v1.json'
    instantanea_repo = raiz / 'docs-as-code' / 'api' / 'openapi-v1.json'
    if servido.is_file():
        instantanea = instantanea_repo
        if not instantanea.is_file() or servido.read_bytes() != instantanea.read_bytes():
            inf.error('api/openapi-v1.json', 1, 'openapi-distinto', 'no es idéntico byte a byte a la instantánea')

    # Referencia de API (PR-P6): las operaciones renderizadas son las que cuenta recuento_openapi.py.
    if instantanea_repo.is_file():
        revisar_api(sitio, instantanea_repo, inf)

    # Nada que anuncie un MCP o un agente A2A que no existen.
    for x in sorted(sitio.rglob('*')):
        rel = x.relative_to(sitio).as_posix()
        if (x.is_dir() and x.name == 'mcp') or x.name == 'agent-card.json':
            inf.error(rel, 1, 'anuncio-falso', 'no hay MCP ni agente A2A (folio 018 §5): tiene que dar 404')
    return inf.cerrar(f' archivos={len(archivos)} base={base}')


if __name__ == '__main__':
    sys.exit(main(sys.argv[1:]))
