"""Lector del catálogo del punto de uso (expediente 0003, tramo T-3). Lo comparten `anclas`, `limite-didactico` y
`pista-sin-enlaces`; no es un control por sí mismo.

Un catálogo es un .md con una tabla de cuatro columnas `contexto | capa | texto | fuente` entre las marcas
`<!-- catalogo:inicio -->` y `<!-- catalogo:fin -->` (ver docs-as-code/punto-de-uso/catalogo.md).
"""

import importlib.util
import re
from pathlib import Path

RAIZ_REPO = Path(__file__).resolve().parents[2]
CAPAS = ('etiqueta', 'pista', 'info', 'ejemplo', 'validacion')
INICIO, FIN = '<!-- catalogo:inicio -->', '<!-- catalogo:fin -->'


def cargar_dg():
    spec = importlib.util.spec_from_file_location('dg', RAIZ_REPO / '.docs-guard' / 'dg.py')
    mod = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(mod)
    return mod


def argumentos(argv, banderas=(), con_valor=()):
    """Devuelve (opciones, rutas). `banderas` son booleanas; `con_valor` llevan un argumento."""
    ops, rutas, i = {}, [], 0
    while i < len(argv):
        a = argv[i]
        if a in banderas:
            ops[a] = True
        elif a in con_valor and i + 1 < len(argv):
            ops[a] = argv[i + 1]
            i += 1
        else:
            rutas.append(a)
        i += 1
    return ops, rutas


def sin_codigo(celda):
    return celda.strip().strip('`').strip()


def filas(texto):
    """(número de línea, contexto, capa, texto, fuente) por fila de la tabla del catálogo."""
    dentro = False
    for n, l in enumerate(texto.split('\n'), 1):
        s = l.strip()
        if s == INICIO:
            dentro = True
            continue
        if s == FIN:
            dentro = False
            continue
        if not dentro or not s.startswith('|'):
            continue
        celdas = [c.strip() for c in s.strip('|').split('|')]
        if len(celdas) != 4 or celdas[0] == 'contexto' or set(celdas[0]) <= set('-: '):
            continue
        yield n, sin_codigo(celdas[0]), celdas[1].strip(), celdas[2].strip(), sin_codigo(celdas[3])


def es_catalogo(texto):
    return INICIO in texto


def normalizar(texto):
    """Texto comparable: sin etiquetas HTML, sin comillas invertidas, sin ** y con los blancos colapsados."""
    t = re.sub(r'<[^>]+>', '', texto)
    t = t.replace('`', '').replace('**', '')
    return re.sub(r'\s+', ' ', t).strip()
