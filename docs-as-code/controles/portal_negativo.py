#!/usr/bin/env python3
"""portal-negativo: lo que NO tiene que estar en el sitio del portal (expediente 0003, folio 018 §7, PR-P4).

    python3 -I docs-as-code/controles/portal_negativo.py [--raiz DIR] [--manifiesto ARCHIVO] [--privados ARCHIVO]
        SITIO

SITIO es la salida de `mkdocs build` (scripts/docs-portal.sh). Sin --manifiesto lee docs-as-code/portal/publicar.yml
de la raíz. Hallazgos (error):

- `fuera-del-manifiesto`: un archivo del sitio que no sale de una pieza del manifiesto ni de la infraestructura del
  generador. Una pieza `ruta/x.md` da `ruta/x/index.html` (un README.md o index.md da el índice de su carpeta) y, desde
  PR-P5, su Markdown crudo `ruta/x.md` o `ruta/x/index.md`. Infraestructura: `assets/`, `search/`, `404.html`,
  `sitemap.xml`, `sitemap.xml.gz`, `llms.txt` y `llms-full.txt`.
- `patron-prohibido`: en cualquier archivo de texto, una dirección de red privada (10/8, 172.16/12, 192.168/16,
  100.64/10), `jump-host`, una ruta de un anfitrión (`/home/…/`, `/root/…/`, …), una dirección de correo o una
  `SigningKey` con valor (los créditos de autor del código de terceros bajo `assets/` no cuentan como correo); y, en
  una URL (`href`, `src`, sitemap, `location` del índice de búsqueda o llms.txt), `SDD/Expedientes`, `SDD/Docs/Audit`
  o `_legacy`. Nombrar esas carpetas en el texto de una guía no es una fuga; enlazarlas, sí.
  Los nombres de máquinas y las rutas propias de la infraestructura NO se escriben en este repo público: van en un
  archivo de patrones privado, una expresión regular por línea, que se pasa con --privados o PORTAL_PATRONES_PRIVADOS.
- `urls-distintas`: el sitemap, el índice de búsqueda y (si existe) llms.txt no tienen el mismo conjunto de páginas.

Sin dependencias. Código de salida: 0 en orden, 1 hay hallazgos, 2 error de uso. Arranca en avisa (folio 018 §7).
"""

import importlib.util
import json
import os
import re
import sys
import xml.etree.ElementTree as ET
from pathlib import Path
from urllib.parse import urlparse

sys.dont_write_bytecode = True

RAIZ_REPO = Path(__file__).resolve().parents[2]
CONTROL_MANIFIESTO = Path(__file__).resolve().parent / 'portal_manifiesto.py'
INFRA_DIRS = ('assets/', 'search/')
INFRA_ARCHIVOS = {'404.html', 'sitemap.xml', 'sitemap.xml.gz', 'llms.txt', 'llms-full.txt'}
TEXTO = {'.html', '.md', '.txt', '.xml', '.json', '.js', '.css', '.map', '.svg'}
PROHIBIDOS = [
    ('red privada 10/8', re.compile(r'(?<![\d.])10\.\d{1,3}\.\d{1,3}\.\d{1,3}(?![\d.])')),
    ('red privada 172.16/12', re.compile(r'(?<![\d.])172\.(?:1[6-9]|2\d|3[01])\.\d{1,3}\.\d{1,3}(?![\d.])')),
    ('red privada 192.168/16', re.compile(r'(?<![\d.])192\.168\.\d{1,3}\.\d{1,3}(?![\d.])')),
    ('red 100.64/10', re.compile(r'(?<![\d.])100\.(?:6[4-9]|[7-9]\d|1[01]\d|12[0-7])\.\d{1,3}\.\d{1,3}(?![\d.])')),
    ('jump-host', re.compile(r'jump-host', re.I)),
    ('ruta de un anfitrión', re.compile(r'(?<![\w.:/-])/(?:home|root|srv|mnt|opt)/[\w.-]+/')),
    ('correo', re.compile(r'[A-Za-z0-9._%+-]+@[A-Za-z0-9-]+(?:\.[A-Za-z0-9-]+)*\.[A-Za-z]{2,}')),
    ('SigningKey con valor', re.compile(r'SigningKey["\']?\s*[:=]\s*["\']?[^\s"\'<>,}]{8,}')),
]
EN_URL = re.compile(r'(?:href|src)\s*=\s*["\']([^"\']*)["\']', re.I)
URL_PROHIBIDA = re.compile(r'SDD/Expedientes|SDD/Docs/Audit|_legacy')
RE_LOCATION = re.compile(r'"location"\s*:\s*"([^"]*)"')
ENLACE_MD = re.compile(r'\]\(([^)\s]+)\)')


def cargar(nombre, ruta):
    spec = importlib.util.spec_from_file_location(nombre, ruta)
    mod = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(mod)
    return mod


def pagina(ruta):
    """`docs-as-code/glosario.md` -> `docs-as-code/glosario/`; `README.md` -> ``; `x/README.md` -> `x/`."""
    p = ruta[:-3]
    padre, _, nombre = p.rpartition('/')
    if nombre in ('README', 'index'):
        return f'{padre}/' if padre else ''
    return f'{p}/'


def permitidos(piezas):
    paginas, archivos = set(), set()
    for p in piezas:
        pag = pagina(p['ruta'])
        paginas.add(pag)
        archivos.add(f'{pag}index.html')
        archivos.add(f'{pag}index.md')
        if pag:
            archivos.add(f'{pag[:-1]}.md')
        else:
            archivos.add('index.md')
    return paginas, archivos


def normalizar(url, base):
    path = urlparse(url).path
    if base and path.startswith(base):
        path = path[len(base):]
    path = path.lstrip('/')
    if path.endswith('index.md') or path.endswith('index.html'):
        path = path.rsplit('/', 1)[0] + '/' if '/' in path else ''
    elif path.endswith('.md'):
        path = path[:-3] + '/'
    return path


def main(argv):
    raiz, manifiesto, sitio, privados = None, None, None, None
    args = iter(argv)
    for a in args:
        if a in ('--raiz', '--manifiesto', '--privados'):
            v = next(args, None)
            if v is None:
                print(f'portal-negativo: falta el valor de {a}', file=sys.stderr)
                return 2
            if a == '--raiz':
                raiz = v
            elif a == '--manifiesto':
                manifiesto = v
            else:
                privados = v
        elif a.startswith('-') or sitio is not None:
            print(f'portal-negativo: argumento no esperado: {a}', file=sys.stderr)
            return 2
        else:
            sitio = Path(a)
    if sitio is None or not sitio.is_dir():
        print('uso: portal_negativo.py [--raiz DIR] [--manifiesto ARCHIVO] [--privados ARCHIVO] SITIO',
              file=sys.stderr)
        return 2
    dg = cargar('dg', RAIZ_REPO / '.docs-guard' / 'dg.py')
    raiz = Path(raiz).resolve() if raiz else RAIZ_REPO
    archivo = Path(manifiesto) if manifiesto else raiz / 'docs-as-code' / 'portal' / 'publicar.yml'
    try:
        texto = archivo.read_text(encoding='utf-8')
    except OSError as e:
        print(f'portal-negativo: no se puede leer {archivo}: {e}', file=sys.stderr)
        return 2
    hallazgos_m = []
    piezas = cargar('portal_manifiesto', CONTROL_MANIFIESTO).leer_manifiesto(texto, hallazgos_m)
    if piezas is None or hallazgos_m:
        print(f'portal-negativo: el manifiesto {archivo} no se puede leer (correr portal_manifiesto.py)',
              file=sys.stderr)
        return 2
    piezas = [p for p in piezas if isinstance(p.get('ruta'), str) and p['ruta'].endswith('.md')]
    prohibidos = list(PROHIBIDOS)
    externos = privados or os.environ.get('PORTAL_PATRONES_PRIVADOS')
    if externos:
        try:
            for linea in Path(externos).read_text(encoding='utf-8').split('\n'):
                if linea.strip() and not linea.lstrip().startswith('#'):
                    prohibidos.append(('patrón privado', re.compile(linea.strip(), re.I)))
        except (OSError, re.error) as e:
            print(f'portal-negativo: patrones privados ilegibles ({externos}): {e}', file=sys.stderr)
            return 2
    paginas, archivos_ok = permitidos(piezas)
    inf = dg.Informe('portal-negativo')
    n_archivos = 0

    # (a) y (b): cada archivo del sitio.
    for f in sorted(x for x in sitio.rglob('*') if x.is_file()):
        n_archivos += 1
        rel = f.relative_to(sitio).as_posix()
        if not (rel in archivos_ok or rel in INFRA_ARCHIVOS or rel.startswith(INFRA_DIRS)):
            inf.error(rel, 1, 'fuera-del-manifiesto', 'el archivo no sale de ninguna pieza de publicar.yml')
        if f.suffix not in TEXTO:
            continue
        contenido = f.read_text(encoding='utf-8', errors='replace')
        for n, linea in enumerate(contenido.split('\n'), 1):
            for nombre, rx in prohibidos:
                if nombre == 'correo' and rel.startswith('assets/'):
                    continue  # créditos de autor del código de terceros del generador (licencias), no una fuga
                m = rx.search(linea)
                if m:
                    inf.error(rel, n, 'patron-prohibido', f'{nombre}: «{m.group(0)[:60]}»')
            urls = EN_URL.findall(linea)
            if rel in ('sitemap.xml', 'llms.txt'):
                urls.append(linea)
            elif rel.startswith('search/'):
                urls += RE_LOCATION.findall(linea)
            for u in urls:
                m = URL_PROHIBIDA.search(u)
                if m:
                    inf.error(rel, n, 'patron-prohibido', f'URL a {m.group(0)}: «{u.strip()[:80]}»')

    # (c): el mismo conjunto de páginas en el sitemap, el índice de búsqueda y llms.txt.
    conjuntos = {}
    smap = sitio / 'sitemap.xml'
    if smap.is_file():
        try:
            locs = [e.text.strip() for e in ET.parse(smap).iter() if e.tag.endswith('loc') and e.text]
        except ET.ParseError as e:
            locs = []
            inf.error('sitemap.xml', 1, 'urls-distintas', f'sitemap ilegible: {e}')
        paths = [urlparse(u).path for u in locs]
        base = min(paths, key=len) if paths else '/'
        conjuntos['sitemap.xml'] = {normalizar(u, base) for u in locs}
    else:
        inf.error('sitemap.xml', 1, 'urls-distintas', 'falta el sitemap')
        base = '/'
    indice = sitio / 'search' / 'search_index.json'
    if indice.is_file():
        try:
            docs = json.loads(indice.read_text(encoding='utf-8')).get('docs', [])
            conjuntos['search/search_index.json'] = {d.get('location', '').split('#', 1)[0] for d in docs}
        except (ValueError, AttributeError) as e:
            inf.error('search/search_index.json', 1, 'urls-distintas', f'índice ilegible: {e}')
    else:
        inf.error('search/search_index.json', 1, 'urls-distintas', 'falta el índice de búsqueda')
    llms = sitio / 'llms.txt'
    if llms.is_file():
        conjuntos['llms.txt'] = {normalizar(u, base) for u in ENLACE_MD.findall(llms.read_text(encoding='utf-8'))}
    for nombre, conj in conjuntos.items():
        if conj != paginas:
            de_mas = sorted(conj - paginas)
            faltan = sorted(paginas - conj)
            inf.error(nombre, 1, 'urls-distintas',
                      f'no coincide con el manifiesto: de más {de_mas or "—"}; faltan {faltan or "—"}')
    return inf.cerrar(f' archivos={n_archivos} paginas={len(paginas)}')


if __name__ == '__main__':
    sys.exit(main(sys.argv[1:]))
