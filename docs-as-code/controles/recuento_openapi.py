#!/usr/bin/env python3
"""recuento-openapi: la instantánea OpenAPI contra la tabla de operaciones del contrato (expediente 0003, tramo T-6).

    python3 -I docs-as-code/controles/recuento_openapi.py [--contrato MD] [--instantanea JSON] [--elevados TXT]
        [--hoy AAAA-MM-DD]

Por defecto compara `docs-as-code/api/openapi-v1.json` con
`SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md`:

- `recuento-cifra` (error): la cifra en palabras del título de la §3 («los diecisiete puntos de acceso») no coincide
  con la cantidad de operaciones de la instantánea.
- `recuento-falta` (error): una ruta de la tabla de §3.1 («Ruta pública», `VERBO /ruta`) no está en la instantánea.
- `recuento-sobra` (error): una operación de la instantánea no está en la tabla de §3.1.
- `recuento-codigos` (aviso): un punto declara en la tabla de §3 códigos que la instantánea no documenta. Es lo que
  resuelve el PR de `ProducesProblem` (H-09); no cuenta como hallazgo que pinta de rojo.

El contrato es del SDD generado: lo que este control encuentra allí **se eleva, no se corrige** (AGENTS.md:61).
Un error ya elevado se registra en `docs-as-code/controles/recuento-elevados.txt` (o --elevados), una línea por
hallazgo: `regla | mensaje exacto | folio donde se elevó | vence AAAA-MM-DD`. Mientras está vigente, ese error exacto
cuenta como aviso (`elevado`); si el mensaje cambia (p. ej. otra cifra), vuelve a ser error. Vencida, la línea no se
aplica y se avisa; sin uso (el SDD ya se corrigió), se avisa para borrarla. --hoy fija la fecha (autoprueba).
Código de salida: 0 en orden, 1 hay errores, 2 error de uso. Arrancó en avisa (D-10); desde PR-P6 del portal
(folio 018 §7) la compuerta lo corre en bloquea.
"""

import datetime as dt
import json
import re
import sys
from pathlib import Path

sys.dont_write_bytecode = True  # no deja __pycache__ en el árbol
sys.path.insert(0, str(Path(__file__).resolve().parent))
import catalogo_pu as pu  # noqa: E402

CONTRATO = 'SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md'
INSTANTANEA = 'docs-as-code/api/openapi-v1.json'
ELEVADOS = 'docs-as-code/controles/recuento-elevados.txt'
VERBOS = ('get', 'post', 'put', 'patch', 'delete')
NUMEROS = {
    'uno': 1, 'dos': 2, 'tres': 3, 'cuatro': 4, 'cinco': 5, 'seis': 6, 'siete': 7, 'ocho': 8, 'nueve': 9, 'diez': 10,
    'once': 11, 'doce': 12, 'trece': 13, 'catorce': 14, 'quince': 15, 'dieciséis': 16, 'diecisiete': 17, 'dieciocho': 18, 'diecinueve': 19, 'veinte': 20,
    'veintiuno': 21, 'veintidós': 22, 'veintitrés': 23, 'veinticuatro': 24, 'veinticinco': 25,
}
RE_TITULO = re.compile(r'^##\s+3\.\s+Operaciones:\s+los\s+(\S+)\s+puntos', re.I)
RE_PUNTO = r'(A-\d{2}|IA-V\d+)'
RE_RUTA = re.compile(RE_PUNTO + r'\s*\|\s*`(GET|POST|PUT|PATCH|DELETE)\s+(/[^`\s]*)`')
RE_FILA_S3 = re.compile(r'^\|\s*' + RE_PUNTO + r'\s*\|\s*`(GET|POST|PUT|PATCH|DELETE)`\s*\|(.*)$')
RE_CODIGO = re.compile(r'`(\d{3})`')


def leer_contrato(texto):
    """Devuelve (cifra, línea de la cifra, rutas {(VERBO, ruta): (punto, línea)}, códigos {punto: (set, línea)})."""
    cifra, n_cifra, rutas, codigos = None, 0, {}, {}
    for n, linea in enumerate(texto.split('\n'), 1):
        m = RE_TITULO.match(linea)
        if m and cifra is None:
            cifra, n_cifra = m.group(1).lower(), n
            continue
        m = RE_FILA_S3.match(linea)
        if m:
            celdas = m.group(3).split('|')
            # Columnas tras el verbo: qué hace | papel | códigos | CU
            if len(celdas) >= 3:
                codigos[m.group(1)] = (set(RE_CODIGO.findall(celdas[2])), n)
            continue
        for p, verbo, ruta in RE_RUTA.findall(linea):
            rutas[(verbo, ruta)] = (p, n)
    return cifra, n_cifra, rutas, codigos


def leer_instantanea(texto):
    doc = json.loads(texto)
    ops = {}
    for ruta, item in (doc.get('paths') or {}).items():
        for verbo, op in item.items():
            if verbo in VERBOS:
                ops[(verbo.upper(), ruta)] = set((op.get('responses') or {}).keys())
    return ops


def leer_elevados(texto, nombre, hoy):
    """Devuelve ({(regla, mensaje): folio} vigentes, [avisos]). ValueError si una línea no tiene la forma pedida."""
    vigentes, avisos = {}, []
    for n, linea in enumerate(texto.split('\n'), 1):
        linea = linea.strip()
        if not linea or linea.startswith('#'):
            continue
        partes = [p.strip() for p in linea.split(' | ')]
        m = re.fullmatch(r'vence (\d{4}-\d{2}-\d{2})', partes[3]) if len(partes) == 4 else None
        if not m or not partes[0].startswith('recuento-') or not partes[1] or not partes[2]:
            raise ValueError(f'{nombre}:{n}: se espera «regla | mensaje exacto | folio | vence AAAA-MM-DD»')
        if dt.date.fromisoformat(m.group(1)) < hoy:
            avisos.append((n, f'elevado vencido el {m.group(1)}: {partes[0]} «{partes[1]}»; vuelve a ser error'))
        else:
            vigentes[(partes[0], partes[1])] = (partes[2], n)
    return vigentes, avisos


class Filtro:
    """Pasa los errores al informe, salvo los elevados vigentes, que quedan como aviso."""

    def __init__(self, inf, elevados, nombre):
        self.inf, self.elevados, self.nombre, self.usados = inf, elevados, nombre, set()

    def error(self, ruta, n, regla, texto):
        clave = (regla, texto)
        if clave in self.elevados:
            self.usados.add(clave)
            self.inf.aviso(ruta, n, 'elevado', f'{regla}: {texto} (elevado en {self.elevados[clave][0]})')
        else:
            self.inf.error(ruta, n, regla, texto)

    def sin_uso(self):
        for clave, (folio, n) in self.elevados.items():
            if clave not in self.usados:
                self.inf.aviso(self.nombre, n, 'elevado', f'sin uso, borrarlo: {clave[0]} «{clave[1]}» ({folio})')


def main(argv):
    dg = pu.cargar_dg()
    ops, resto = pu.argumentos(argv, con_valor=('--contrato', '--instantanea', '--raiz', '--elevados', '--hoy'))
    if resto:
        print(__doc__)
        return 2
    raiz = dg.raiz_repo(ops.get('--raiz'))
    contrato = Path(ops.get('--contrato') or raiz / CONTRATO)
    instantanea = Path(ops.get('--instantanea') or raiz / INSTANTANEA)
    try:
        cifra, n_cifra, rutas, codigos = leer_contrato(dg.leer(contrato))
        operaciones = leer_instantanea(dg.leer(instantanea))
    except FileNotFoundError as e:
        print(f'ERROR: no existe {e.filename}', file=sys.stderr)
        return 2
    except json.JSONDecodeError as e:
        print(f'ERROR: la instantánea no es JSON: {e}', file=sys.stderr)
        return 2
    archivo_el = Path(ops.get('--elevados') or raiz / ELEVADOS)
    try:
        hoy = dt.date.fromisoformat(ops['--hoy']) if '--hoy' in ops else dt.date.today()
        texto_el = dg.leer(archivo_el) if archivo_el.is_file() or '--elevados' in ops else ''
        elevados, avisos_el = leer_elevados(texto_el, dg.rel(archivo_el, raiz), hoy)
    except FileNotFoundError as e:
        print(f'ERROR: no existe {e.filename}', file=sys.stderr)
        return 2
    except ValueError as e:
        print(f'ERROR: {e}', file=sys.stderr)
        return 2
    if cifra is None or not rutas:
        print('ERROR: el contrato no trae el título de la §3 o la tabla de rutas de §3.1', file=sys.stderr)
        return 2

    rc_ = dg.rel(contrato, raiz)
    ri = dg.rel(instantanea, raiz)
    inf = dg.Informe('recuento-openapi')
    re_ = dg.rel(archivo_el, raiz)
    for n, texto in avisos_el:
        inf.aviso(re_, n, 'elevado', texto)
    filtro = Filtro(inf, elevados, re_)
    declarada = int(cifra) if cifra.isdigit() else NUMEROS.get(cifra)
    if declarada != len(operaciones):
        filtro.error(rc_, n_cifra, 'recuento-cifra',
                  f'el título declara «{cifra}» puntos y la instantánea trae {len(operaciones)} operaciones')
    for (verbo, ruta), (p, n) in sorted(rutas.items(), key=lambda x: x[1][1]):
        if (verbo, ruta) not in operaciones:
            filtro.error(rc_, n, 'recuento-falta', f'{p} `{verbo} {ruta}` no está en {ri}')
    for clave in sorted(operaciones):
        if clave not in rutas:
            filtro.error(ri, 1, 'recuento-sobra', f'`{clave[0]} {clave[1]}` no está en la tabla de §3.1 de {rc_}')
    sin_documentar = 0
    orden = lambda x: codigos.get(x[1][0], (None, 0))[1]  # noqa: E731 — en el orden de la tabla de §3
    for (verbo, ruta), (p, _) in sorted(rutas.items(), key=orden):
        if (verbo, ruta) not in operaciones or p not in codigos:
            continue
        declarados, n = codigos[p]
        faltan = sorted(declarados - operaciones[(verbo, ruta)])
        if faltan:
            sin_documentar += 1
            inf.aviso(rc_, n, 'recuento-codigos', f'{p} `{verbo} {ruta}` declara {", ".join(faltan)} y la instantánea no los documenta')
    filtro.sin_uso()
    return inf.cerrar(f' contrato={len(rutas)} instantanea={len(operaciones)} cifra={cifra} '
                      f'puntos-con-codigos-sin-documentar={sin_documentar}')


if __name__ == '__main__':
    sys.exit(main(sys.argv[1:]))
