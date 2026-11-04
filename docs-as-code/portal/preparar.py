#!/usr/bin/env python3
"""preparar: copia las piezas del manifiesto del portal a un directorio de contenido (folio 018 §1 y §2, PR-P4).

    python3 -I docs-as-code/portal/preparar.py [--raiz DIR] [--manifiesto ARCHIVO] DESTINO

Lee docs-as-code/portal/publicar.yml con el lector de docs-as-code/controles/portal_manifiesto.py y copia cada `ruta`
a DESTINO/ruta, byte a byte. Lo que no está listado no se copia: ése es el filtro del portal. DESTINO tiene que no
existir o estar vacío. Si el manifiesto tiene hallazgos (p. ej. una ruta excluida o inexistente), no copia nada.
Sin dependencias. Código de salida: 0 copiado, 1 el manifiesto tiene hallazgos, 2 error de uso.
"""

import importlib.util
import shutil
import subprocess
import sys
from pathlib import Path

sys.dont_write_bytecode = True

AQUI = Path(__file__).resolve().parent
CONTROL = AQUI.parent / 'controles' / 'portal_manifiesto.py'


def cargar_control():
    spec = importlib.util.spec_from_file_location('portal_manifiesto', CONTROL)
    mod = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(mod)
    return mod


def main(argv):
    raiz, manifiesto, destino = None, None, None
    args = iter(argv)
    for a in args:
        if a in ('--raiz', '--manifiesto'):
            v = next(args, None)
            if v is None:
                print(f'preparar: falta el valor de {a}', file=sys.stderr)
                return 2
            if a == '--raiz':
                raiz = v
            else:
                manifiesto = v
        elif a.startswith('-') or destino is not None:
            print(f'preparar: argumento no esperado: {a}', file=sys.stderr)
            return 2
        else:
            destino = Path(a)
    if destino is None:
        print('uso: preparar.py [--raiz DIR] [--manifiesto ARCHIVO] DESTINO', file=sys.stderr)
        return 2
    if destino.exists() and any(destino.iterdir()):
        print(f'preparar: {destino} no está vacío', file=sys.stderr)
        return 2
    raiz = Path(raiz).resolve() if raiz else AQUI.parents[1]
    archivo = Path(manifiesto) if manifiesto else raiz / 'docs-as-code' / 'portal' / 'publicar.yml'
    # El control es la única validación: si da hallazgos, no se publica nada.
    rc = subprocess.run([sys.executable, '-I', str(CONTROL), '--raiz', str(raiz), str(archivo)]).returncode
    if rc != 0:
        print(f'preparar: el manifiesto no está en orden (portal-manifiesto dio {rc}); no se copia nada',
              file=sys.stderr)
        return 1 if rc == 1 else 2
    hallazgos = []
    piezas = cargar_control().leer_manifiesto(archivo.read_text(encoding='utf-8'), hallazgos)
    destino.mkdir(parents=True, exist_ok=True)
    for p in piezas:
        dst = destino / p['ruta']
        dst.parent.mkdir(parents=True, exist_ok=True)
        shutil.copyfile(raiz / p['ruta'], dst)
        print(f'  pieza {p["ruta"]}')
    print(f'preparar: {len(piezas)} piezas en {destino}')
    return 0


if __name__ == '__main__':
    sys.exit(main(sys.argv[1:]))
