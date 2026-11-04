#!/usr/bin/env python3
"""portal-manifiesto: la lista permitida del portal de documentación (expediente 0003, folio 018 §1 y §7, PR-P3).

    python3 -I docs-as-code/controles/portal_manifiesto.py [--raiz DIR] [MANIFIESTO]

Sin MANIFIESTO lee `docs-as-code/portal/publicar.yml` de la raíz. El manifiesto es un subconjunto de YAML:
`piezas: []` o una lista de entradas `- ruta: …` con `llms: true|false` y, opcional, `portal_docente_ok: true`.
Hallazgos (error):

- `formato`: línea que no se entiende, clave desconocida, `ruta` repetida o falta `llms` booleano.
- `ruta-no-permitida`: glob, ruta absoluta, `..`, o ruta bajo `SDD/`, `_legacy`, `ai-layer/`, `.docs-guard/` o
  `docs-as-code/controles/` (excluidos siempre).
- `ruta-inexistente`: la ruta no es un archivo `.md` del repo.
- `docente-sin-ok`: el `audience` de la pieza contiene `docente` y la entrada no lleva `portal_docente_ok: true`.

El filtro no es `delivery`: en este repo vale siempre `[publico]` (D-18). Código de salida: 0 en orden, 1 hay
hallazgos, 2 error de uso o manifiesto ilegible. Arranca en avisa (folio 018 §7).
"""

import importlib.util
import re
import sys
from pathlib import Path

sys.dont_write_bytecode = True

RAIZ_REPO = Path(__file__).resolve().parents[2]
MANIFIESTO = 'docs-as-code/portal/publicar.yml'
EXCLUIDOS = ('SDD/', 'ai-layer/', '.docs-guard/', 'docs-as-code/controles/')
CLAVES = {'ruta', 'llms', 'portal_docente_ok'}
RE_CLAVE = re.compile(r'^(\s*)(-\s+)?([A-Za-z_]\w*):\s*(.*?)\s*$')


def cargar_dg():
    spec = importlib.util.spec_from_file_location('dg', RAIZ_REPO / '.docs-guard' / 'dg.py')
    mod = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(mod)
    return mod


def valor(v):
    v = re.sub(r'\s+#.*$', '', v).strip()
    if len(v) >= 2 and v[0] == v[-1] and v[0] in '"\'':
        return v[1:-1]
    return {'true': True, 'false': False}.get(v, v)


def leer_manifiesto(texto, hallazgos):
    """Devuelve la lista de entradas ({clave: valor, '__linea__': n}) o None si falta `piezas:`."""
    piezas, actual, visto = None, None, False
    for n, linea in enumerate(texto.split('\n'), 1):
        if not linea.strip() or linea.lstrip().startswith('#'):
            continue
        m = RE_CLAVE.match(linea)
        if not m:
            hallazgos.append((n, 'formato', f'línea que no se entiende: «{linea.strip()}»'))
            continue
        sangria, guion, clave, v = m.groups()
        if not sangria and not guion:
            if clave != 'piezas' or visto:
                hallazgos.append((n, 'formato', f'clave de primer nivel «{clave}» desconocida o repetida'))
                continue
            visto = True
            piezas = []
            if valor(v) not in ('', '[]'):
                hallazgos.append((n, 'formato', '«piezas:» va vacío («[]») o seguido de entradas «- ruta: …»'))
            continue
        if piezas is None:
            hallazgos.append((n, 'formato', 'entrada antes de «piezas:»'))
            continue
        if guion:
            actual = {'__linea__': n}
            piezas.append(actual)
        elif actual is None:
            hallazgos.append((n, 'formato', 'clave fuera de una entrada «- ruta: …»'))
            continue
        if clave not in CLAVES or clave in actual:
            hallazgos.append((n, 'formato', f'clave «{clave}» desconocida o repetida en la entrada'))
            continue
        actual[clave] = valor(v)
    return piezas


def ruta_permitida(ruta):
    if any(c in ruta for c in '*?[]') or ruta.startswith('/') or '\\' in ruta:
        return 'glob o ruta absoluta: el manifiesto lista archivos exactos'
    partes = ruta.split('/')
    if '..' in partes or '.' in partes or '' in partes:
        return 'la ruta tiene «..», «.» o barras de más'
    if any(ruta.startswith(e) for e in EXCLUIDOS) or any(p.startswith('_legacy') for p in partes):
        return 'la ruta cae en lo excluido siempre (SDD, _legacy, ai-layer, .docs-guard, controles)'
    return None


def main(argv):
    raiz, manifiesto = None, None
    args = iter(argv)
    for a in args:
        if a == '--raiz':
            raiz = next(args, None)
            if raiz is None:
                print('uso: portal_manifiesto.py [--raiz DIR] [MANIFIESTO]', file=sys.stderr)
                return 2
        elif a.startswith('-') or manifiesto is not None:
            print(f'argumento no esperado: {a}', file=sys.stderr)
            return 2
        else:
            manifiesto = a
    dg = cargar_dg()
    raiz = dg.raiz_repo(raiz)
    archivo = Path(manifiesto) if manifiesto else raiz / MANIFIESTO
    try:
        texto = archivo.read_text(encoding='utf-8')
    except OSError as e:
        print(f'portal-manifiesto: no se puede leer {archivo}: {e}', file=sys.stderr)
        return 2
    nombre = dg.rel(archivo, raiz)
    inf = dg.Informe('portal-manifiesto')
    hallazgos = []
    piezas = leer_manifiesto(texto, hallazgos)
    if piezas is None:
        print(f'portal-manifiesto: {nombre} no tiene «piezas:»', file=sys.stderr)
        return 2
    vistas = set()
    for p in piezas:
        n = p['__linea__']
        ruta = p.get('ruta')
        if not isinstance(ruta, str) or not ruta:
            hallazgos.append((n, 'formato', 'la entrada no tiene «ruta»'))
            continue
        if ruta in vistas:
            hallazgos.append((n, 'formato', f'«{ruta}» repetida'))
            continue
        vistas.add(ruta)
        if not isinstance(p.get('llms'), bool):
            hallazgos.append((n, 'formato', f'«{ruta}»: falta «llms: true|false»'))
        ok_docente = p.get('portal_docente_ok', False)
        if not isinstance(ok_docente, bool):
            hallazgos.append((n, 'formato', f'«{ruta}»: «portal_docente_ok» va true o false'))
        motivo = ruta_permitida(ruta)
        if motivo:
            hallazgos.append((n, 'ruta-no-permitida', f'«{ruta}»: {motivo}'))
            continue
        f = raiz / ruta
        if f.suffix != '.md' or not f.is_file():
            hallazgos.append((n, 'ruta-inexistente', f'«{ruta}» no es un archivo .md del repo'))
            continue
        fm, _ = dg.frontmatter(dg.leer(f))
        audience = (fm or {}).get('audience') or []
        audience = audience if isinstance(audience, list) else [audience]
        if 'docente' in [str(x) for x in audience] and ok_docente is not True:
            hallazgos.append((n, 'docente-sin-ok',
                              f'«{ruta}» tiene audience docente y no lleva «portal_docente_ok: true»'))
    for n, regla, msg in hallazgos:
        inf.error(nombre, n, regla, msg)
    return inf.cerrar(f' piezas={len(piezas)}')


if __name__ == '__main__':
    sys.exit(main(sys.argv[1:]))
