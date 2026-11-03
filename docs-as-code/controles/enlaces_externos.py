#!/usr/bin/env python3
"""enlaces-externos: enlaces http/https de la documentación, con red (expediente 0003, tramo T-7; revisión externa R-G2).

Lo corre el carril programado (`.github/workflows/docs-programado.yml`) una vez por semana. Antes era un guion inline
del workflow, sin par rojo/verde; ahora vive acá y su par está en `autoprueba.sh` (hallazgo B4 de la revisión externa).

    python3 -I docs-as-code/controles/enlaces_externos.py [--raiz DIR] [--excepciones ARCHIVO] [--informe ARCHIVO]
                                                        [--hoy AAAA-MM-DD] [--estricto] [--permitir-local] [RUTA...]

- RUTA: archivos `.md` o carpetas (se recorren sus `.md`). Sin RUTA: `docs-as-code/**`, `README.md` y `SDD/Docs/**`.
- Se ignoran `localhost`, `127.0.0.1`, `0.0.0.0`, `example.com/.org`, los nombres sin dominio (internos de
  contenedor) y los ejemplos con marcadores (`{`, `<`, `...`, `…`). `--permitir-local` deja revisar `localhost` y
  `127.0.0.1`: sólo lo usa la autoprueba, que levanta un servidor local.
- Un 401, 403 o 429 no es un enlace caído: el sitio existe y no deja revisar.
- `--excepciones`: archivo versionado con líneas `URL | motivo | vence AAAA-MM-DD` (hallazgo B3). Una excepción
  vigente saca la URL del informe; una vencida no se aplica y se avisa, para que alguien la renueve o la quite.
- `--informe`: escribe la tabla en Markdown (el cuerpo del issue). Si existe `GITHUB_OUTPUT`, agrega `caidos=N`.

Código de salida: 0 (con caídos sólo avisa), 1 hay caídos y se pidió `--estricto`, 2 error de uso.
"""

import argparse
import concurrent.futures as cf
import datetime as dt
import os
import re
import sys
import urllib.error
import urllib.request
from pathlib import Path

RAIZ_REPO = Path(__file__).resolve().parents[2]
PATRON = re.compile(r'https?://[^\s)<>"`\]]+')
LOCALES = r'localhost|127\.0\.0\.1|0\.0\.0\.0'
NO_CAIDO = (401, 403, 429)


def archivos(raiz, rutas):
    if not rutas:
        rutas = ['docs-as-code', 'README.md', 'SDD/Docs']
    salida = set()
    for r in rutas:
        p = Path(r) if Path(r).is_absolute() else raiz / r
        if p.is_dir():
            salida.update(x for x in p.rglob('*.md') if x.is_file())
        elif p.is_file():
            salida.add(p)
    return sorted(salida)


def extraer(raiz, lista, permitir_local):
    locales = r'([^/]*\.)?example\.(com|org)' if permitir_local else rf'{LOCALES}|([^/]*\.)?example\.(com|org)'
    ignorar = re.compile(rf'^https?://({locales})([:/]|$)')
    usos = {}
    for ruta in lista:
        try:
            nombre = ruta.relative_to(raiz).as_posix()
        except ValueError:
            nombre = ruta.as_posix()
        with open(ruta, encoding='utf-8', errors='replace') as f:
            for n, linea in enumerate(f, 1):
                for url in PATRON.findall(linea):
                    url = url.rstrip('.,;:*_\'')
                    if ignorar.match(url) or any(m in url for m in ('{', '<', '...', '…')):
                        continue
                    host = re.sub(r'^https?://', '', url).split('/')[0].split(':')[0]
                    if '.' not in host and not (permitir_local and host == 'localhost'):
                        continue  # nombres internos de contenedor, no navegables desde afuera
                    lugar = f'{nombre}:{n}'
                    if lugar not in usos.setdefault(url, []):
                        usos[url].append(lugar)
    return usos


def leer_excepciones(ruta, hoy):
    """Devuelve ({url: motivo} vigentes, [avisos]). Error de uso si una línea no tiene la forma pedida."""
    vigentes, avisos = {}, []
    if not ruta:
        return vigentes, avisos
    with open(ruta, encoding='utf-8') as f:
        for n, linea in enumerate(f, 1):
            linea = linea.strip()
            if not linea or linea.startswith('#'):
                continue
            partes = [p.strip() for p in linea.split('|')]
            m = re.fullmatch(r'vence (\d{4}-\d{2}-\d{2})', partes[2]) if len(partes) == 3 else None
            if not m or not partes[0].startswith(('http://', 'https://')) or not partes[1]:
                raise ValueError(f'{ruta}:{n}: se espera «URL | motivo | vence AAAA-MM-DD»')
            vence = dt.date.fromisoformat(m.group(1))
            if vence < hoy:
                avisos.append(f'excepción vencida el {vence} ({ruta}:{n}): {partes[0]}; se revisa como cualquier enlace')
            else:
                vigentes[partes[0]] = partes[1]
    return vigentes, avisos


def probar(url):
    cab = {'User-Agent': 'Lab-Geometria-docs-programado/1.0'}
    ultimo = ''
    for metodo in ('HEAD', 'GET'):
        try:
            with urllib.request.urlopen(urllib.request.Request(url, method=metodo, headers=cab), timeout=20):
                return url, None
        except urllib.error.HTTPError as e:
            if e.code in NO_CAIDO:
                return url, None
            ultimo = f'HTTP {e.code}'
        except Exception as e:  # noqa: BLE001 — cualquier falla de red cuenta como caído
            ultimo = type(e).__name__
    return url, ultimo


def main():
    ap = argparse.ArgumentParser(description=__doc__.splitlines()[0])
    ap.add_argument('rutas', nargs='*')
    ap.add_argument('--raiz', default=str(RAIZ_REPO))
    ap.add_argument('--excepciones')
    ap.add_argument('--informe')
    ap.add_argument('--hoy', help='fecha de referencia para el vencimiento (por omisión, hoy)')
    ap.add_argument('--estricto', action='store_true')
    ap.add_argument('--permitir-local', action='store_true')
    a = ap.parse_args()

    raiz = Path(a.raiz).resolve()
    try:
        hoy = dt.date.fromisoformat(a.hoy) if a.hoy else dt.date.today()
        exc, avisos = leer_excepciones(a.excepciones, hoy)
    except (OSError, ValueError) as e:
        print(f'enlaces-externos: {e}', file=sys.stderr)
        return 2

    lista = archivos(raiz, a.rutas)
    usos = extraer(raiz, lista, a.permitir_local)
    exceptuados = sorted(u for u in usos if u in exc)
    a_probar = sorted(u for u in usos if u not in exc)
    with cf.ThreadPoolExecutor(max_workers=8) as ex:
        caidos = [(u, err) for u, err in ex.map(probar, a_probar) if err]

    gha = os.environ.get('GITHUB_ACTIONS') == 'true'
    for av in avisos:
        print(f'::warning::{av}' if gha else f'AVISO {av}')
    print(f'{len(lista)} archivos, {len(usos)} enlaces externos, {len(exceptuados)} exceptuados, {len(caidos)} caídos.')
    for u, err in caidos:
        donde = ', '.join(usos[u][:5])
        print(f'::warning::enlace caído ({err}): {u} en {donde}' if gha else f'CAIDO {err}: {u} en {donde}')

    if a.informe:
        with open(a.informe, 'w', encoding='utf-8') as f:
            f.write(f'Revisión semanal de enlaces externos ({len(usos)} enlaces en {len(lista)} archivos, '
                    f'{len(exceptuados)} exceptuados).\n\n')
            if caidos:
                f.write('| Enlace | Error | Dónde |\n| --- | --- | --- |\n')
                for u, err in caidos:
                    f.write(f'| {u} | {err} | {", ".join(usos[u][:5])} |\n')
            else:
                f.write('No hay enlaces caídos.\n')
            for u in exceptuados:
                f.write(f'\nExceptuado: {u} ({exc[u]}).\n')
            for av in avisos:
                f.write(f'\nAviso: {av}.\n')
            f.write('\nLos enlaces de `SDD/Docs/**` se elevan al orquestador SDD, no se corrigen (`AGENTS.md:61`).\n')
    if os.environ.get('GITHUB_OUTPUT'):
        with open(os.environ['GITHUB_OUTPUT'], 'a', encoding='utf-8') as f:
            f.write(f'caidos={len(caidos)}\n')
    return 1 if (caidos and a.estricto) else 0


if __name__ == '__main__':
    sys.exit(main())
