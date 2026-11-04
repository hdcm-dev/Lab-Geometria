#!/usr/bin/env python3
"""referencia-api: la referencia de API del portal desde la instantánea OpenAPI (expediente 0003, folio 018 §6, PR-P6).

    python3 -I docs-as-code/portal/referencia_api.py [--raiz DIR] CONTENIDO

Corre ANTES de `mkdocs build` (scripts/docs-portal.sh), después de preparar.py, y escribe en el directorio de
contenido CONTENIDO:

- `api/index.md`: portada de la referencia (versión de la instantánea, grupos con su cantidad de operaciones y enlaces
  al JSON y a la vista Redoc);
- `api/<grupo>.md`: una página por grupo de operaciones. El grupo es el primer segmento de la ruta después del prefijo
  de versión (`/v1/cuentas/{id}` -> `cuentas`; `/salud` -> `salud`), en el orden en que aparece en la instantánea.
  Cada operación es un `## \\`VERBO /ruta\\``, con su resumen, `operationId`, seguridad declarada, parámetros, cuerpo y
  respuestas, tal como los trae la instantánea (no se agrega nada que ella no diga);
- `api/openapi-v1.json`: la instantánea, byte a byte (mkdocs la copia sin tocar);
- `api/redoc.html` y `assets/vendor/redoc/`: la vista Redoc, con el bundle vendorizado de docs-as-code/portal/vendor/redoc
  (versión exacta, sin CDN). Antes de copiarlo se verifica su huella contra vendor/redoc/VENDOR.

El Markdown crudo de esas páginas (con enlaces absolutos del portal) lo escribe agentes.py después del build con
`paginas(doc, enlace)`, la misma función. La cantidad de operaciones renderizadas tiene que ser la de
docs-as-code/controles/recuento_openapi.py; lo vigila portal_salida.py (`api-recuento`).
Sin dependencias. Código de salida: 0 escrito, 1 instantánea ilegible o bundle con otra huella, 2 error de uso.
"""

import hashlib
import json
import re
import shutil
import sys
from pathlib import Path

sys.dont_write_bytecode = True

AQUI = Path(__file__).resolve().parent
INSTANTANEA = 'docs-as-code/api/openapi-v1.json'
VENDOR = AQUI / 'vendor' / 'redoc'
VENDOR_SITIO = 'assets/vendor/redoc'
VERBOS = ('get', 'post', 'put', 'patch', 'delete')  # los mismos que cuenta recuento_openapi.py
RE_VERSION = re.compile(r'^v\d+$')
RE_OPERACION = re.compile(r'^## `(GET|POST|PUT|PATCH|DELETE) (/[^`]*)`$', re.M)


def operaciones(doc):
    """[(VERBO, ruta, op)] en el orden de la instantánea."""
    salida = []
    for ruta, item in (doc.get('paths') or {}).items():
        for verbo, op in (item or {}).items():
            if verbo in VERBOS and isinstance(op, dict):
                salida.append((verbo.upper(), ruta, op))
    return salida


def grupo(ruta):
    """Primer segmento después del prefijo de versión: `/v1/cuentas/{id}` -> `cuentas`; `/salud` -> `salud`."""
    partes = [p for p in ruta.split('/') if p]
    if len(partes) > 1 and RE_VERSION.match(partes[0]):
        partes = partes[1:]
    return re.sub(r'[^a-z0-9-]', '-', partes[0].lower()) if partes else 'raiz'


def prefijo(ruta):
    partes = [p for p in ruta.split('/') if p]
    if len(partes) > 1 and RE_VERSION.match(partes[0]):
        return '/' + '/'.join(partes[:2])
    return '/' + (partes[0] if partes else '')


def grupos(doc):
    """{grupo: (prefijo, [(VERBO, ruta, op)])} en el orden de primera aparición."""
    salida = {}
    for verbo, ruta, op in operaciones(doc):
        g = grupo(ruta)
        salida.setdefault(g, (prefijo(ruta), []))[1].append((verbo, ruta, op))
    return salida


def rutas(doc):
    """Rutas de las páginas, relativas al contenido: `api/index.md` y `api/<grupo>.md`."""
    return ['api/index.md'] + [f'api/{g}.md' for g in grupos(doc)]


def contar(textos):
    """Operaciones renderizadas en los textos de las páginas de grupo: {(VERBO, ruta)}."""
    return {(m.group(1), m.group(2)) for t in textos for m in RE_OPERACION.finditer(t)}


def celda(texto):
    return str(texto).replace('|', '\\|').replace('<', '&lt;').replace('\n', ' ').strip() or '—'


def prosa(texto):
    """Texto de la instantánea en un párrafo: sin saltos y sin `<` que el Markdown tome por HTML."""
    return ' '.join(str(texto).split()).replace('<', '&lt;') or '—'


def esquema(s):
    if not isinstance(s, dict):
        return '—'
    if '$ref' in s:
        return f'`{s["$ref"].rsplit("/", 1)[-1]}`'
    if s.get('type') == 'array':
        return f'lista de {esquema(s.get("items"))}'
    tipo = s.get('type')
    if isinstance(tipo, list):
        tipo = ' o '.join(str(t) for t in tipo)
    return f'`{tipo}`' + (f' (`{s["format"]}`)' if s.get('format') else '') if tipo else '—'


def seguridad(op, doc):
    reqs = op.get('security', doc.get('security'))
    if not reqs:
        return 'la instantánea no declara un esquema de seguridad para esta operación'
    nombres = sorted({n for r in reqs if isinstance(r, dict) for n in r})
    return ', '.join(f'`{n}`' for n in nombres)


def pagina_grupo(g, pref, ops, doc):
    lineas = [f'# Referencia de API · `{pref}`', '',
              f'Operaciones de la instantánea OpenAPI cuya ruta empieza por `{pref}`: {len(ops)}. '
              'Página generada en el build del portal; la fuente es la instantánea, no se edita a mano.', '']
    for verbo, ruta, op in ops:
        lineas += [f'## `{verbo} {ruta}`', '']
        if op.get('summary'):
            resumen = prosa(op['summary'])
            lineas += [resumen if resumen.endswith('.') else f'{resumen}.', '']
        if op.get('description'):
            lineas += [prosa(op['description']), '']
        lineas += [f'- Operación: `{op.get("operationId", "—")}`',
                   f'- Seguridad: {seguridad(op, doc)}']
        cuerpo = op.get('requestBody')
        if isinstance(cuerpo, dict):
            for tipo, c in (cuerpo.get('content') or {}).items():
                req = 'obligatorio' if cuerpo.get('required') else 'opcional'
                lineas.append(f'- Cuerpo ({req}): `{tipo}`, {esquema((c or {}).get("schema"))}')
        lineas.append('')
        params = [p for p in op.get('parameters') or [] if isinstance(p, dict)]
        if params:
            lineas += ['| Parámetro | En | Obligatorio | Tipo |', '| --- | --- | --- | --- |']
            for p in params:
                lineas.append(f'| `{celda(p.get("name"))}` | {celda(p.get("in"))} | '
                              f'{"sí" if p.get("required") else "no"} | {esquema(p.get("schema"))} |')
            lineas.append('')
        respuestas = op.get('responses') or {}
        if respuestas:
            lineas += ['| Respuesta | Descripción | Cuerpo |', '| --- | --- | --- |']
            for codigo in sorted(respuestas):
                r = respuestas[codigo] or {}
                cuerpos = [f'`{t}` {esquema((c or {}).get("schema"))}' for t, c in (r.get('content') or {}).items()]
                lineas.append(f'| `{codigo}` | {celda(r.get("description", ""))} | {celda("; ".join(cuerpos))} |')
            lineas.append('')
    return '\n'.join(lineas).rstrip('\n') + '\n'


def pagina_indice(doc, enlace):
    info = doc.get('info') or {}
    gs = grupos(doc)
    total = sum(len(ops) for _, ops in gs.values())
    lineas = ['# Referencia de API', '',
              f'Generada en el build del portal desde la instantánea OpenAPI del repositorio '
              f'(`{INSTANTANEA}`): «{prosa(info.get("title", "—"))}», versión `{info.get("version", "—")}`, '
              f'OpenAPI `{doc.get("openapi", "—")}`, {total} operaciones en {len(gs)} grupos.', '',
              f'- [Instantánea OpenAPI en JSON]({enlace("api/openapi-v1.json")}), idéntica byte a byte a la del '
              'repositorio.',
              f'- [Vista interactiva con Redoc]({enlace("api/redoc.html")}), servida desde el propio portal.', '',
              '## Grupos', '', '| Grupo | Operaciones |', '| --- | --- |']
    for g, (pref, ops) in gs.items():
        lineas.append(f'| [`{pref}`]({enlace(f"api/{g}.md")}) | {len(ops)} |')
    esquemas = (doc.get('components') or {}).get('securitySchemes') or {}
    if esquemas:
        lineas += ['', '## Seguridad', '']
        for nombre, s in esquemas.items():
            s = s or {}
            detalle = ', '.join(f'{k} `{s[k]}`' for k in ('type', 'scheme', 'bearerFormat') if s.get(k))
            lineas.append(f'- `{nombre}`: {detalle}.' + (f' {prosa(s["description"])}' if s.get('description') else ''))
    return '\n'.join(lineas) + '\n'


def paginas(doc, enlace):
    """{ruta relativa: texto}. `enlace(ruta del contenido)` da el destino de un enlace (relativo o absoluto)."""
    salida = {'api/index.md': pagina_indice(doc, enlace)}
    for g, (pref, ops) in grupos(doc).items():
        salida[f'api/{g}.md'] = pagina_grupo(g, pref, ops, doc)
    return salida


def huellas_vendor():
    """{archivo: sha256} declarados en vendor/redoc/VENDOR."""
    salida = {}
    for linea in (VENDOR / 'VENDOR').read_text(encoding='utf-8').split('\n'):
        if linea.startswith('sha256: '):
            h, nombre = linea[len('sha256: '):].split()
            salida[nombre] = h
    return salida


def version_vendor():
    for linea in (VENDOR / 'VENDOR').read_text(encoding='utf-8').split('\n'):
        if linea.startswith('version: '):
            return linea.split(':', 1)[1].strip()
    return '—'


REDOC_HTML = '''<!doctype html>
<html lang="es">
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<meta http-equiv="Content-Security-Policy" content="default-src 'self'; img-src 'self' data:; style-src 'self' 'unsafe-inline'; worker-src 'self' blob:">
<title>Referencia de API · Lab-Geometria</title>
<!-- Redoc {version} vendorizado (docs-as-code/portal/vendor/redoc); sin CDN ni fuentes de terceros: la CSP corta
     el logo remoto que el bundle pide para su pie. -->
</head>
<body>
<redoc spec-url="openapi-v1.json"></redoc>
<script src="../{vendor}/redoc.standalone.js"></script>
</body>
</html>
'''


def relativo(desde):
    """Enlaces para mkdocs: relativos a la carpeta de `desde` (mkdocs los resuelve)."""
    def enlace(ruta):
        return ruta[len('api/'):] if ruta.startswith('api/') and desde.startswith('api/') else ruta
    return enlace


def main(argv):
    raiz, contenido = None, None
    args = iter(argv)
    for a in args:
        if a == '--raiz':
            raiz = next(args, None)
            if raiz is None:
                print('referencia-api: falta el valor de --raiz', file=sys.stderr)
                return 2
        elif a.startswith('-') or contenido is not None:
            print(f'referencia-api: argumento no esperado: {a}', file=sys.stderr)
            return 2
        else:
            contenido = Path(a)
    if contenido is None or not contenido.is_dir():
        print('uso: referencia_api.py [--raiz DIR] CONTENIDO', file=sys.stderr)
        return 2
    raiz = Path(raiz).resolve() if raiz else AQUI.parents[1]
    fuente = raiz / INSTANTANEA
    try:
        doc = json.loads(fuente.read_text(encoding='utf-8'))
    except (OSError, ValueError) as e:
        print(f'referencia-api: no se puede leer la instantánea {INSTANTANEA}: {e}', file=sys.stderr)
        return 1
    huellas = huellas_vendor()
    for nombre, h in sorted(huellas.items()):
        real = hashlib.sha256((VENDOR / nombre).read_bytes()).hexdigest()
        if real != h:
            print(f'referencia-api: vendor/redoc/{nombre} no coincide con VENDOR ({real})', file=sys.stderr)
            return 1
    textos = paginas(doc, relativo('api/'))
    for ruta, texto in textos.items():
        destino = contenido / ruta
        destino.parent.mkdir(parents=True, exist_ok=True)
        with open(destino, 'w', encoding='utf-8', newline='\n') as f:
            f.write(texto)
    shutil.copyfile(fuente, contenido / 'api' / 'openapi-v1.json')
    with open(contenido / 'api' / 'redoc.html', 'w', encoding='utf-8', newline='\n') as f:
        f.write(REDOC_HTML.format(version=version_vendor(), vendor=VENDOR_SITIO))
    (contenido / VENDOR_SITIO).mkdir(parents=True, exist_ok=True)
    for nombre in sorted(huellas):
        shutil.copyfile(VENDOR / nombre, contenido / VENDOR_SITIO / nombre)
    n = len(contar(textos.values()))
    print(f'referencia-api: {len(textos)} páginas, {n} operaciones, Redoc {version_vendor()} en {contenido}')
    return 0


if __name__ == '__main__':
    sys.exit(main(sys.argv[1:]))
