#!/usr/bin/env python3
"""cobertura-conceptos: cada término ★ del asistente de conceptos tiene su ficha publicada (REQ-IA-25, ARQ-4).

    python3 -I docs-as-code/controles/cobertura_conceptos.py [--raiz DIR]

Lee docs-as-code/ayuda/conceptos/*.md y comprueba, por término ★:
- que exista la ficha, con frontmatter (`title`, `roles`, `sinonimos`, `destino`) y un cuerpo no vacío;
- que declare `status` (borrador | vigente | retirado; obligatorio en este corpus nuevo, K6 R1 y §4: sólo `vigente` se
  sirve, y sólo pasa a `vigente` la ficha que firmó el PO) y `preguntas:` con 2 a 5 preguntas reales, sin coma (el
  lector de frontmatter del servicio parte las listas en línea por coma);
- que el cuerpo no traiga lo que la ficha no puede decir (RD-01, ADR-10010): dígitos, la palabra «fórmula»,
  un verbo de operación (sumá, multiplicá, dividí, restá) ni la cantidad de caras de una figura concreta.

Los términos ★ son los del relevamiento del banco de traslado (2026-11-11). Código de salida: 0 en orden, 1 hay
hallazgos, 2 error de uso.
"""

import re
import sys
from pathlib import Path

sys.dont_write_bytecode = True

TERMINOS = {
    'ortoedro': 'ortoedro',
    'rectangulo-desarrollado': 'rectángulo desarrollado',
    'area-total-vs-cara': 'área total y área de una cara',
    'arista': 'arista',
    'volumen': 'volumen',
    'declarado-vs-derivado': 'valor declarado y valor derivado',
    'advertencia': 'advertencia',
    'describir': 'Describir()',
    'estado-pendiente': 'Pendiente',
}
OBLIGATORIAS = ('title', 'roles', 'sinonimos', 'destino', 'status', 'preguntas')
ESTADOS = ('borrador', 'vigente', 'retirado')
PROHIBIDO = [
    ('digito', re.compile(r'\d')),
    ('formula', re.compile(r'f[oó]rmula', re.I)),
    ('operacion', re.compile(r'\b(sum[aá]|multiplic[aá]|divid[ií]|rest[aá])\b', re.I)),
    ('cantidad-de-caras', re.compile(r'\b(seis|cinco|cuatro|tres|dos)\s+caras\b', re.I)),
]


def leer(ruta):
    texto = ruta.read_text(encoding='utf-8')
    m = re.match(r'^---\n(.*?)\n---\n(.*)$', texto, re.S)
    if not m:
        return None, texto
    fm = {}
    for linea in m.group(1).splitlines():
        if ':' in linea:
            k, v = linea.split(':', 1)
            fm[k.strip()] = v.strip()
    cuerpo = '\n'.join(l for l in m.group(2).splitlines() if l.strip() and not l.startswith('#'))
    return fm, cuerpo


def main(argv):
    raiz = Path(argv[argv.index('--raiz') + 1]) if '--raiz' in argv else Path(__file__).resolve().parents[2]
    carpeta = raiz / 'docs-as-code' / 'ayuda' / 'conceptos'
    hallazgos = []
    con_ficha = 0
    vigentes = 0
    for ident, termino in TERMINOS.items():
        ruta = carpeta / f'{ident}.md'
        if not ruta.is_file():
            hallazgos.append(f'{ident}: falta la ficha de «{termino}»')
            continue
        fm, cuerpo = leer(ruta)
        if fm is None:
            hallazgos.append(f'{ident}: sin frontmatter')
            continue
        faltan = [k for k in OBLIGATORIAS if not fm.get(k)]
        if faltan or not cuerpo.strip():
            hallazgos.append(f'{ident}: faltan {faltan or ["cuerpo"]}')
            continue
        if fm['status'] not in ESTADOS:
            hallazgos.append(f'{ident}: status «{fm["status"]}» fuera de {ESTADOS}')
        preguntas = re.findall(r'"([^"]*)"', fm['preguntas'])
        if not 2 <= len(preguntas) <= 5:
            hallazgos.append(f'{ident}: preguntas {len(preguntas)}, se esperan 2 a 5 entre comillas')
        for p in preguntas:
            if ',' in p:
                hallazgos.append(f'{ident}: la pregunta «{p}» tiene coma')
            if re.search(r'\d', p):
                hallazgos.append(f'{ident}: la pregunta «{p}» tiene dígitos')
        vigentes += fm['status'] == 'vigente'
        for motivo, rx in PROHIBIDO:
            if rx.search(cuerpo):
                hallazgos.append(f'{ident}: {motivo}')
        con_ficha += 1
    for h in hallazgos:
        print(f'cobertura-conceptos: {h}')
    print(f'cobertura-conceptos: {con_ficha}/{len(TERMINOS)} términos ★ con ficha escrita; vigentes (firmadas por el PO)={vigentes}; hallazgos={len(hallazgos)}')
    return 1 if hallazgos else 0


if __name__ == '__main__':
    sys.exit(main(sys.argv[1:]))
