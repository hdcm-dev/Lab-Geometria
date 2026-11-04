#!/usr/bin/env python3
"""agentes: la capa del portal para agentes (expediente 0003, folio 018 §3, §4 y §5, PR-P5).

    python3 -I docs-as-code/portal/agentes.py [--raiz DIR] [--manifiesto ARCHIVO] [--base URL] SITIO

Corre después de `mkdocs build` sobre el sitio ya construido (scripts/docs-portal.sh) y escribe en SITIO:

- el Markdown crudo de cada pieza del manifiesto: `ruta/x.md` da `ruta/x.md` (la página es `ruta/x/`); un README.md o
  index.md da `index.md` en su carpeta. Es el mismo archivo que ya es público en el repositorio con dos cambios: el
  frontmatter queda reducido a `doc_id`, `title` y `audience`, y cada enlace relativo se reescribe como URL absoluta:
  a una pieza publicada, su `.md` en el portal; a cualquier otro archivo del repo, su vista en el repositorio público
  (REPO_BLOB). Los bloques de código no se tocan.
- `llms.txt` (formato de llmstxt.org): H1, resumen en `>` y una sección con un enlace al `.md` de cada pieza con
  `llms: true`.
- `llms-full.txt`: la concatenación de esos `.md` crudos, en el orden del manifiesto. Tope: 400 KB (TOPE_FULL); si lo
  supera, sale con 1 y no lo escribe.
- `.well-known/api-catalog`: linkset de RFC 9727 (`application/linkset+json`, el content-type lo pone el servidor en
  PR-P7) con `service-desc` a `api/openapi-v1.json` y `service-doc` a `api/`, ambos del portal; ninguno apunta a una
  instancia viva. `mcp/server-card.json` y `agent-card.json` NO se escriben (folio 018 §5: no hay MCP ni agente A2A).

URL base: --base, o PORTAL_SITE_URL, o la del mkdocs.yml por defecto (http://localhost:8080/); el dominio lo decide el
PO. La única fuente es el manifiesto docs-as-code/portal/publicar.yml. Sin dependencias; la salida no depende de la
fecha ni del orden del disco. Código de salida: 0 escrito, 1 tope superado o manifiesto con hallazgos, 2 error de uso.
"""

import importlib.util
import json
import os
import re
import sys
from pathlib import Path, PurePosixPath
from urllib.parse import urlparse

sys.dont_write_bytecode = True

AQUI = Path(__file__).resolve().parent
CONTROL = AQUI.parent / 'controles' / 'portal_manifiesto.py'
BASE_DEFECTO = 'http://localhost:8080/'
REPO_BLOB = 'https://github.com/hdcm-dev/Lab-Geometria/blob/main/'
TOPE_FULL = 400 * 1024
CLAVES_FM = ('doc_id', 'title', 'audience')
TITULO = 'Lab-Geometria'
RESUMEN = ('Documentación pública de consulta de Lab-Geometria, el laboratorio de geometría de la cátedra: '
           'cómo intervenir, vocabulario, ayuda en el punto de uso y cambios de la API.')
# Enlace en línea `](destino)` o `](destino "título")`, y definición de referencia `[x]: destino`.
RE_ENLACE = re.compile(r'(\]\()(<[^>]*>|[^)\s]+)((?:\s+"[^"]*")?\))')
RE_REFERENCIA = re.compile(r'^(\s{0,3}\[[^\]]+\]:\s*)(\S+)(.*)$')
RE_VALLA = re.compile(r'^\s{0,3}(```|~~~)')


def cargar_control():
    spec = importlib.util.spec_from_file_location('portal_manifiesto', CONTROL)
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


def crudo(ruta):
    """Ruta del `.md` crudo en el sitio: `docs-as-code/glosario.md`; `README.md` -> `index.md`."""
    pag = pagina(ruta)
    return f'{pag}index.md' if pag == '' or ruta.rsplit('/', 1)[-1] in ('README.md', 'index.md') else f'{pag[:-1]}.md'


def reducir_frontmatter(texto):
    """Devuelve (frontmatter reducido o '', cuerpo, title). Conserva las líneas originales de las claves elegidas."""
    lineas = texto.split('\n')
    if not lineas or lineas[0].strip() != '---':
        return '', texto, None
    for fin in range(1, len(lineas)):
        if lineas[fin].strip() == '---':
            break
    else:
        return '', texto, None
    guardadas, titulo, clave = [], None, None
    for linea in lineas[1:fin]:
        m = re.match(r'^([A-Za-z_][\w-]*):', linea)
        if m:
            clave = m.group(1)
            if clave == 'title':
                titulo = linea.split(':', 1)[1].strip().strip('"\'')
        if clave in CLAVES_FM and linea.strip():
            guardadas.append(linea)
    fm = '---\n' + '\n'.join(guardadas) + '\n---\n' if guardadas else ''
    cuerpo = '\n'.join(lineas[fin + 1:])
    return fm, cuerpo, titulo


def reescribir(destino, origen, publicadas, base):
    """Destino de un enlace de la pieza `origen` -> URL absoluta (o el mismo destino si no es relativo)."""
    envuelto = destino.startswith('<') and destino.endswith('>')
    d = destino[1:-1] if envuelto else destino
    if not d or d.startswith('#') or urlparse(d).scheme or d.startswith('//') or d.startswith('/'):
        return destino
    ruta, sep, ancla = d.partition('#')
    partes = []
    for parte in (PurePosixPath(origen).parent / ruta).parts:
        if parte == '..':
            if not partes:
                return destino  # sale del repo: se deja como está y lo ve portal_salida.py
            partes.pop()
        elif parte != '.':
            partes.append(parte)
    objetivo = '/'.join(partes)
    if objetivo in publicadas:
        url = base + crudo(objetivo)
    elif not objetivo or ruta.endswith('/'):
        url = REPO_BLOB.replace('/blob/', '/tree/') + objetivo + ('/' if objetivo else '')
    else:
        url = REPO_BLOB + objetivo
    url += sep + ancla
    return f'<{url}>' if envuelto else url


def markdown_crudo(texto, origen, publicadas, base):
    fm, cuerpo, titulo = reducir_frontmatter(texto)
    salida, valla = [], None
    for linea in cuerpo.split('\n'):
        m = RE_VALLA.match(linea)
        if m:
            valla = None if valla == m.group(1) else (valla or m.group(1))
            salida.append(linea)
            continue
        if valla is None:
            linea = RE_ENLACE.sub(lambda x: x.group(1) + reescribir(x.group(2), origen, publicadas, base) + x.group(3),
                                  linea)
            linea = RE_REFERENCIA.sub(lambda x: x.group(1) + reescribir(x.group(2), origen, publicadas, base)
                                      + x.group(3), linea)
        salida.append(linea)
    if titulo is None:
        h1 = next((l[2:].strip() for l in cuerpo.split('\n') if l.startswith('# ')), None)
        titulo = h1 or origen
    return fm + '\n'.join(salida), titulo


def api_catalog(base):
    return {
        'linkset': [{
            'anchor': base + 'api/',
            'service-desc': [{'href': base + 'api/openapi-v1.json', 'type': 'application/openapi+json'}],
            'service-doc': [{'href': base + 'api/', 'type': 'text/html'}],
        }]
    }


def escribir(ruta, texto):
    ruta.parent.mkdir(parents=True, exist_ok=True)
    with open(ruta, 'w', encoding='utf-8', newline='\n') as f:
        f.write(texto)


def main(argv):
    raiz, manifiesto, base, sitio = None, None, None, None
    args = iter(argv)
    for a in args:
        if a in ('--raiz', '--manifiesto', '--base'):
            v = next(args, None)
            if v is None:
                print(f'agentes: falta el valor de {a}', file=sys.stderr)
                return 2
            if a == '--raiz':
                raiz = v
            elif a == '--manifiesto':
                manifiesto = v
            else:
                base = v
        elif a.startswith('-') or sitio is not None:
            print(f'agentes: argumento no esperado: {a}', file=sys.stderr)
            return 2
        else:
            sitio = Path(a)
    if sitio is None or not sitio.is_dir():
        print('uso: agentes.py [--raiz DIR] [--manifiesto ARCHIVO] [--base URL] SITIO', file=sys.stderr)
        return 2
    base = base or os.environ.get('PORTAL_SITE_URL') or BASE_DEFECTO
    u = urlparse(base)
    if u.scheme not in ('http', 'https') or not u.netloc:
        print(f'agentes: la URL base no es absoluta: {base}', file=sys.stderr)
        return 2
    if not base.endswith('/'):
        base += '/'
    raiz = Path(raiz).resolve() if raiz else AQUI.parents[1]
    archivo = Path(manifiesto) if manifiesto else raiz / 'docs-as-code' / 'portal' / 'publicar.yml'
    try:
        texto_m = archivo.read_text(encoding='utf-8')
    except OSError as e:
        print(f'agentes: no se puede leer {archivo}: {e}', file=sys.stderr)
        return 2
    hallazgos = []
    piezas = cargar_control().leer_manifiesto(texto_m, hallazgos)
    if piezas is None or hallazgos:
        print(f'agentes: el manifiesto {archivo} tiene hallazgos (correr portal_manifiesto.py)', file=sys.stderr)
        return 1
    publicadas = {p['ruta'] for p in piezas}

    llms, full = [], []
    for p in piezas:
        ruta = p['ruta']
        try:
            texto = (raiz / ruta).read_text(encoding='utf-8')
        except OSError as e:
            print(f'agentes: no se puede leer la pieza {ruta}: {e}', file=sys.stderr)
            return 1
        md, titulo = markdown_crudo(texto, ruta, publicadas, base)
        if not md.endswith('\n'):
            md += '\n'
        escribir(sitio / crudo(ruta), md)
        print(f'  crudo {crudo(ruta)}')
        if p.get('llms') is True:
            llms.append(f'- [{titulo}]({base}{crudo(ruta)})')
            full.append(f'<!-- fuente: {base}{crudo(ruta)} -->\n\n{md}')

    texto_full = '\n'.join(full)
    tam = len(texto_full.encode('utf-8'))
    if tam > TOPE_FULL:
        print(f'agentes: llms-full.txt pesa {tam} bytes y el tope es {TOPE_FULL}; no se escribe', file=sys.stderr)
        return 1
    cabecera = f'# {TITULO}\n\n> {RESUMEN}\n\n'
    escribir(sitio / 'llms.txt', cabecera + '## Documentación\n\n' + ''.join(l + '\n' for l in llms))
    escribir(sitio / 'llms-full.txt', texto_full)
    escribir(sitio / '.well-known' / 'api-catalog',
             json.dumps(api_catalog(base), ensure_ascii=False, indent=2, sort_keys=False) + '\n')
    print(f'agentes: {len(piezas)} crudos, llms.txt con {len(llms)} enlaces, llms-full.txt de {tam} bytes, '
          f'.well-known/api-catalog en {sitio}')
    return 0


if __name__ == '__main__':
    sys.exit(main(sys.argv[1:]))
