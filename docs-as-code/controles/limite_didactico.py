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

- `ficha-dicta` (H3 del expediente 0004, actuación 003): el cuerpo de una ficha de concepto
  (`topic: concepto`) que dice un número (en cifras o en letras junto a una magnitud), una operación o un
  procedimiento («multiplicá», «sumá las caras», «primero tomá…», «tenés que poner…»). La ficha dice qué es,
  nunca cómo se obtiene el valor; es el mismo límite que `ConceptAnswerFilter` aplica a la redacción del modelo.

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


RE_FICHA_NUMERO = re.compile(r'\d')
RE_FICHA_LETRAS = re.compile(
    r'\b(dos|tres|cuatro|cinco|seis|siete|ocho|nueve|diez|once|doce|veinte|cien|mil|medio)\b.{0,40}'
    r'\b(area|volumen|perimetro|superficie|caras?|aristas?|lados?|radio|altura)\b'
    r'|\b(area|volumen|perimetro|superficie)\b.{0,40}\b(dos|tres|cuatro|seis|doce)\b')
RE_FICHA_OPERACION = re.compile(
    r'\b(sum|multiplic|divid|rest|elev|calcul|reemplaz|tom|pon|escrib|carg|complet)(á|é|í)(lo|la|los|las)?\b'
    r'|\b(sum|multiplic|divid|rest|elev)(ando|ar)\b'
    r'|\bal (cuadrado|cubo)\b|\bpor (dos|tres|cuatro|seis|el lado|la arista|la altura|el radio)\b'
    r'|\bformula\b|π|\bpi\b|\bprimero\b|\bten(e|es) que\b|\bhay que (poner|calcular|escribir|multiplicar|sumar)\b'
    r'|\b(el|tu) valor (correcto|es)\b|\bel resultado (es|da)\b')


def es_ficha(texto):
    m = re.match(r'^---\n(.*?)\n---\n', texto, re.S)
    return bool(m and re.search(r'^topic:\s*"?concepto"?\s*$', m.group(1), re.M))


def cuerpo_ficha(texto):
    """(línea, texto) de cada línea de prosa del cuerpo: sin frontmatter, sin títulos y sin bloques cercados."""
    lineas = texto.split('\n')
    fin = next((i for i in range(1, len(lineas)) if lineas[i].strip() == '---'), 0)
    cercado = False
    for n in range(fin + 1, len(lineas)):
        l = lineas[n]
        if re.match(r'^\s*(```|~~~)', l):
            cercado = not cercado
            continue
        if cercado or not l.strip() or l.lstrip().startswith('#'):
            continue
        yield n + 1, l


def plegar(texto):
    """Minúsculas y sin tildes, para números y magnitudes."""
    t = texto.lower()
    for a, b in zip('áéíóúü', 'aeiouu'):
        t = t.replace(a, b)
    return t


def ficha_dicta(texto):
    """El primer dictado del texto, o None. Los verbos se miran con tildes: el voseo imperativo («sumá») dicta y
    el indicativo («la app calcula») describe."""
    plano = plegar(texto)
    m = RE_FICHA_OPERACION.search(texto.lower())
    if m:
        return 'operacion', m.group(0)
    for nombre, rx in (('numero', RE_FICHA_NUMERO), ('numero-en-letras', RE_FICHA_LETRAS),
                       ('operacion', RE_FICHA_OPERACION)):
        m = rx.search(plano)
        if m:
            return nombre, m.group(0)
    return None


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
            if es_ficha(texto):
                for n, linea in cuerpo_ficha(texto):
                    textos += 1
                    hallado = ficha_dicta(pu.normalizar(linea))
                    if hallado:
                        inf.error(ruta, n, 'ficha-dicta', f'la ficha dice {hallado[0]} («{hallado[1][:40]}»): explica qué es, no cómo se obtiene')
                continue
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
