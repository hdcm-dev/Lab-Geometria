#!/usr/bin/env python3
"""limite-didactico: la ayuda acompaña y no resuelve el trabajo (regla RD-01; expediente 0003, tramo T-3).

    python3 -I docs-as-code/controles/limite_didactico.py [--plantillas ARCHIVO] [--raiz DIR] RUTA...

Lleva a la práctica las prohibiciones de docs-as-code/reglas/RD-01-ayuda-no-resuelve.md sobre las filas de los
catálogos del punto de uso y sobre los bloques de código de los .md que recibe:

- `json-resuelto` (prohibición 1): un texto o un bloque de código que es JSON y trae `Tipo` junto con `Area` o
  `Volumen`, es decir, un envío completo.
- `codigo-resuelto` (prohibición 2): un bloque ```cs/```csharp, o un texto con una declaración de clase C#.
- `explica-discrepancia` (prohibición 4): una fila del catálogo que dice el valor correcto o por qué un valor está
  mal. Eso es de las pistas de la capa de IA (expediente 0002).
- `duplica-pista-ia` (prohibición 4 y frontera §3.3 de la resolución): un texto que repite una plantilla de respaldo
  de la capa de IA. Las plantillas se leen, sin tocarlas, de `--plantillas` (por defecto
  src/GeometriaFactory.Infrastructure/AI/TemplateHints.cs); si el archivo no está, el control lo dice y sigue.

La prohibición 3 (ejemplos no isomorfos al TP vigente) no es verificable desde el repo, que no declara el TP: la revisa
el Product Owner (H-10). Código de salida: 0 en orden, 1 hay hallazgos, 2 error de uso. Arranca en avisa (D-10).
"""

import json
import re
import sys
from pathlib import Path

sys.dont_write_bytecode = True  # no deja __pycache__ en el árbol
sys.path.insert(0, str(Path(__file__).resolve().parent))
import catalogo_pu as pu  # noqa: E402

PLANTILLAS = 'src/GeometriaFactory.Infrastructure/AI/TemplateHints.cs'
RE_CLASE = re.compile(r'\b(public|internal|sealed|abstract)?\s*class\s+[A-Z]\w*\s*(:|\{)')
RE_DISCREPANCIA = re.compile(
    r'\b(el|tu) valor correcto\b|\bdeber[ií]as? (dar|ser|valer)\b|\best[aá] mal porque\b'
    r'|\bla f[oó]rmula (correcta|es)\b|\bel resultado correcto\b', re.I)


def envio_resuelto(texto):
    try:
        dato = json.loads(texto)
    except (ValueError, TypeError):
        return False
    claves = set()

    def juntar(x):
        if isinstance(x, dict):
            claves.update(x.keys())
            for v in x.values():
                juntar(v)
        elif isinstance(x, list):
            for v in x:
                juntar(v)
    juntar(dato)
    return 'Tipo' in claves and bool(claves & {'Area', 'Volumen'})


def plantillas(camino):
    if not camino.is_file():
        return None
    return [pu.normalizar(s) for s in re.findall(r'"([^"\n]{20,})"', camino.read_text(encoding='utf-8'))]


def bloques(texto):
    """(línea de apertura, lenguaje, contenido) de cada bloque de código cercado."""
    abierto, lang, ini, buf = False, '', 0, []
    for n, l in enumerate(texto.split('\n'), 1):
        m = re.match(r'^\s*(```|~~~)\s*([\w#+-]*)', l)
        if m and not abierto:
            abierto, lang, ini, buf = True, m.group(2).lower(), n, []
        elif m and abierto:
            abierto = False
            yield ini, lang, '\n'.join(buf)
        elif abierto:
            buf.append(l)


def main(argv):
    dg = pu.cargar_dg()
    ops, rutas = pu.argumentos(argv, con_valor=('--raiz', '--plantillas'))
    if not rutas:
        print(__doc__)
        return 2
    raiz = dg.raiz_repo(ops.get('--raiz'))
    camino = Path(ops['--plantillas']) if '--plantillas' in ops else raiz / PLANTILLAS
    hints = plantillas(camino)
    if hints is None:
        print(f'  (sin plantillas de la capa de IA en {dg.rel(camino, raiz)}: duplica-pista-ia no se mira)')
    inf = dg.Informe('limite-didactico')
    textos = 0
    try:
        for f in dg.markdowns(rutas):
            texto = dg.leer(f)
            ruta = dg.rel(f, raiz)
            for n, lang, cont in bloques(texto):
                textos += 1
                if envio_resuelto(cont):
                    inf.error(ruta, n, 'json-resuelto', 'el bloque es un envío completo (Tipo con Area o Volumen)')
                if lang in ('cs', 'csharp', 'c#') or RE_CLASE.search(cont):
                    inf.error(ruta, n, 'codigo-resuelto', 'el bloque trae código C# de clases')
            if not pu.es_catalogo(texto):
                continue
            for n, contexto, capa, txt, _ in pu.filas(texto):
                textos += 1
                plano = pu.normalizar(txt)
                if envio_resuelto(plano):
                    inf.error(ruta, n, 'json-resuelto', f'{contexto}/{capa}: el texto es un envío completo')
                if RE_CLASE.search(plano):
                    inf.error(ruta, n, 'codigo-resuelto', f'{contexto}/{capa}: el texto declara una clase C#')
                m = RE_DISCREPANCIA.search(plano)
                if m:
                    inf.error(ruta, n, 'explica-discrepancia', f'{contexto}/{capa}: «{m.group(0)}» es de las pistas de IA')
                for h in hints or []:
                    if h and h in plano:
                        inf.error(ruta, n, 'duplica-pista-ia', f'{contexto}/{capa}: repite la plantilla «{h[:50]}»')
    except FileNotFoundError as e:
        print(f'ERROR: no existe {e}', file=sys.stderr)
        return 2
    return inf.cerrar(f' textos={textos}')


if __name__ == '__main__':
    sys.exit(main(sys.argv[1:]))
