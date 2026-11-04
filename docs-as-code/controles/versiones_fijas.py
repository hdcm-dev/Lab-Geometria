#!/usr/bin/env python3
"""versiones-fijas: workflows y Dockerfiles sin versiones flotantes (expediente 0003, folio 018 §7, PR-P2).

    python3 -I docs-as-code/controles/versiones_fijas.py [--raiz DIR] [RUTA...]

Sin RUTA revisa `.github/workflows/*.yml`, `deploy/Dockerfile*` y `docker/**/Dockerfile*` (PR-P7) de la raíz. Hallazgos (error):

- `accion-sin-parche`: `uses: dueño/accion@vN` o `@vN.M`; se exige `@vN.M.P` o un SHA de 40 caracteres.
- `version-flotante`: `*-version: '10.0.x'`, `'22'` o `'lts/*'`; se exige `N.M.P` (o más componentes).
- `imagen-sin-digest`: `FROM imagen:etiqueta` o `image: imagen:etiqueta` sin `@sha256:`. Las etapas de un
  Dockerfile multi-etapa (`FROM build`) y `scratch` no cuentan.

Las acciones locales (`uses: ./…`) y `docker://…@sha256:` no se miden por la regla de acciones.
Código de salida: 0 en orden, 1 hay hallazgos, 2 error de uso. Arranca en avisa (folio 018 §7).
"""

import os
import re
import sys
from pathlib import Path

sys.dont_write_bytecode = True

RE_USES = re.compile(r'^\s*(?:-\s*)?uses:\s*["\']?([^\s"\'#]+)')
RE_VERSION = re.compile(r'^\s*(?:-\s*)?([A-Za-z0-9_-]+-version):\s*["\']?([^\s"\'#]+)')
RE_FROM = re.compile(r'^\s*FROM\s+(?:--platform=\S+\s+)?(\S+)(?:\s+AS\s+(\S+))?', re.I)
RE_IMAGE = re.compile(r'^\s*(?:-\s*)?image:\s*["\']?([^\s"\'#]+)')
RE_REF_EXACTA = re.compile(r'^(v?\d+\.\d+\.\d+([.+-][0-9A-Za-z.-]+)?|[0-9a-f]{40})$')
RE_VERSION_EXACTA = re.compile(r'^\d+\.\d+\.\d+(\.\d+)?([+-][0-9A-Za-z.-]+)?$')


def revisar_uses(valor):
    if valor.startswith('./') or valor.startswith('docker://'):
        if valor.startswith('docker://') and '@sha256:' not in valor:
            return 'imagen-sin-digest', f'«{valor}» sin @sha256:'
        return None
    if '@' not in valor:
        return 'accion-sin-parche', f'«{valor}» sin versión'
    ref = valor.rsplit('@', 1)[1]
    if not RE_REF_EXACTA.match(ref):
        return 'accion-sin-parche', f'«{valor}»: la referencia «{ref}» no es vN.M.P ni un SHA'
    return None


def revisar(ruta, texto):
    hallazgos = []
    es_docker = Path(ruta).name.startswith('Dockerfile')
    etapas = set()
    for n, linea in enumerate(texto.split('\n'), 1):
        if linea.lstrip().startswith('#'):
            continue
        if es_docker:
            m = RE_FROM.match(linea)
            if m:
                imagen = m.group(1)
                if m.group(2):
                    etapas.add(m.group(2).lower())
                if imagen.lower() not in etapas and imagen != 'scratch' and '@sha256:' not in imagen \
                        and not imagen.startswith('$'):
                    hallazgos.append((n, 'imagen-sin-digest', f'«FROM {imagen}» sin @sha256:'))
            continue
        m = RE_USES.match(linea)
        if m:
            r = revisar_uses(m.group(1))
            if r:
                hallazgos.append((n, *r))
            continue
        m = RE_VERSION.match(linea)
        if m and not m.group(2).startswith('${{'):
            if not RE_VERSION_EXACTA.match(m.group(2)):
                hallazgos.append((n, 'version-flotante', f'«{m.group(1)}: {m.group(2)}» no es N.M.P'))
            continue
        m = RE_IMAGE.match(linea)
        if m and not m.group(1).startswith('${{') and '@sha256:' not in m.group(1):
            hallazgos.append((n, 'imagen-sin-digest', f'«image: {m.group(1)}» sin @sha256:'))
    return hallazgos


def main(argv):
    raiz = Path('.')
    rutas = []
    args = iter(argv)
    for a in args:
        if a == '--raiz':
            v = next(args, None)
            if v is None:
                print('uso: versiones_fijas.py [--raiz DIR] [RUTA...]', file=sys.stderr)
                return 2
            raiz = Path(v)
        elif a.startswith('-'):
            print(f'opción desconocida: {a}', file=sys.stderr)
            return 2
        else:
            rutas.append(Path(a))
    if not rutas:
        rutas = (sorted((raiz / '.github' / 'workflows').glob('*.yml')) + sorted((raiz / 'deploy').glob('Dockerfile*'))
                 + sorted(p for p in (raiz / 'docker').glob('**/Dockerfile*') if not p.name.endswith('.dockerignore')))
    if not rutas:
        print('versiones-fijas: no hay archivos que revisar', file=sys.stderr)
        return 2
    en_ci = os.environ.get('GITHUB_ACTIONS') == 'true'
    total = 0
    for ruta in rutas:
        try:
            texto = ruta.read_text(encoding='utf-8')
        except OSError as e:
            print(f'versiones-fijas: no se puede leer {ruta}: {e}', file=sys.stderr)
            return 2
        nombre = os.path.relpath(ruta, raiz)
        for n, regla, msg in revisar(nombre, texto):
            total += 1
            if en_ci:
                print(f'::error file={nombre},line={n}::versiones-fijas/{regla}: {msg}')
            else:
                print(f'{nombre}:{n}: {regla}: {msg}')
    print(f'versiones-fijas: {len(rutas)} archivos, {total} hallazgos')
    return 1 if total else 0


if __name__ == '__main__':
    sys.exit(main(sys.argv[1:]))
