#!/usr/bin/env python3
"""recall-conceptos: recall@3 por rol de las fichas del asistente de conceptos, sin modelo (K6 §7, K7 R12.5).

    python3 -I docs-as-code/controles/recall_conceptos.py [--raiz DIR] [--autoprueba]

Conjunto de medición, por rol (Student, Administrator):
- todas las `preguntas:` del frontmatter de las fichas que el rol ve (se suman solas: una ficha nueva trae sus casos);
- los casos de `docs-as-code/controles/medicion-conceptos.jsonl` (preguntas de la batería y no respondibles, con
  esperada «ninguna»; `rol: ambos` cuenta para los dos).

El recuperador es el léxico del enrutador (`ConceptRouter.MatchCard`): pregunta del frontmatter idéntica primero;
después el sinónimo más largo que aparece como palabra entera. Se mide sobre TODAS las fichas escritas, cualquiera sea
su `status`: la firma decide qué se sirve, no qué se mide (la batería previa a la firma, K3 R2.6). La misma medición
corre con el enrutador real en `ConceptRecallTests` (ci.yml); si el puerto y el C# divergen, uno de los dos queda rojo.

Umbral (K6 §7.3, sin otro umbral en la especificación): recall@3 >= 0,9 por rol y 100 % de «ninguna» en las no
respondibles. Código de salida: 0 en orden, 1 bajo el umbral, 2 error de uso.
`--autoprueba`: par rojo/verde. Verde sobre el repo; rojo con una ficha que roba preguntas y con una que contesta
«perímetro».
"""

import json
import re
import shutil
import sys
import tempfile
from pathlib import Path

sys.dont_write_bytecode = True

ROLES = ('Student', 'Administrator')
UMBRAL_RECALL = 0.9
PLIEGUE = str.maketrans('áàäâéèëêíìïîóòöôúùüûñ', 'aaaaeeeeiiiioooouuuun')


def normalizar(texto):
    t = ''.join(c if c.isalnum() or c in '/-' else ' ' for c in texto.lower().translate(PLIEGUE))
    return ' ' + ' '.join(t.split()) + ' '


def lista(valor):
    return [v.strip().strip('"') for v in valor.strip().strip('[]').split(',') if v.strip().strip('"')]


def fichas(carpeta):
    salida = []
    for ruta in sorted(carpeta.glob('*.md')):
        m = re.match(r'^---\n(.*?)\n---\n', ruta.read_text(encoding='utf-8'), re.S)
        if not m:
            continue
        fm = {}
        for linea in m.group(1).splitlines():
            if ':' in linea:
                k, v = linea.split(':', 1)
                fm[k.strip()] = v.strip()
        salida.append({'id': ruta.stem, 'roles': lista(fm.get('roles', '')), 'sinonimos': lista(fm.get('sinonimos', '')),
                       'preguntas': re.findall(r'"([^"]*)"', fm.get('preguntas', ''))})
    return salida


def palabra(texto, aguja):
    a = aguja.strip()
    return bool(a) and any(f' {a}{s} ' in texto for s in ('', 's', 'es'))


def rango(pregunta, visibles):
    """Fichas candidatas en orden: pregunta idéntica primero, después por largo del sinónimo que aparece."""
    texto = normalizar(pregunta)
    puntaje = {}
    for f in visibles:
        if any(normalizar(q) == texto for q in f['preguntas']):
            puntaje[f['id']] = 10_000
            continue
        largo = max((len(normalizar(s).strip()) for s in f['sinonimos'] if palabra(texto, normalizar(s))), default=0)
        if largo:
            puntaje[f['id']] = largo
    orden = {f['id']: i for i, f in enumerate(visibles)}
    return sorted(puntaje, key=lambda i: (-puntaje[i], orden[i]))


def medir(raiz):
    carpeta = raiz / 'docs-as-code' / 'ayuda' / 'conceptos'
    corpus = fichas(carpeta)
    extra = [json.loads(l) for l in (raiz / 'docs-as-code' / 'controles' / 'medicion-conceptos.jsonl')
             .read_text(encoding='utf-8').splitlines() if l.strip()]
    informe, ok = [], True
    for rol in ROLES:
        visibles = [f for f in corpus if rol in f['roles']]
        casos = [(q, f['id'], 'frontmatter') for f in visibles for q in f['preguntas']]
        casos += [(c['pregunta'], c['esperada'], c['origen']) for c in extra if c['rol'] in (rol, 'ambos')]
        resp = [c for c in casos if c[1] != 'ninguna']
        nores = [c for c in casos if c[1] == 'ninguna']
        hit1 = hit3 = rr = 0
        fallas = []
        for q, esperada, origen in resp:
            r = rango(q, visibles)
            if r[:1] == [esperada]:
                hit1 += 1
            if esperada in r[:3]:
                hit3 += 1
                rr += 1 / (r.index(esperada) + 1)
            else:
                fallas.append(f'  {rol}: «{q}» esperaba {esperada}, trajo {r[:3] or "ninguna"} ({origen})')
        ninguna = 0
        for q, _, origen in nores:
            r = rango(q, visibles)
            if not r:
                ninguna += 1
            else:
                fallas.append(f'  {rol}: «{q}» no respondible, trajo {r[:3]} ({origen})')
        recall = hit3 / len(resp) if resp else 0.0
        tasa = ninguna / len(nores) if nores else 0.0
        # EL PRODUCTO SIRVE UNA SOLA FICHA (el enrutador toma la primera): por eso hit@1 tiene el mismo umbral.
        bien = recall >= UMBRAL_RECALL and hit1 >= UMBRAL_RECALL * len(resp) and nores and ninguna == len(nores)
        ok = ok and bool(bien)
        informe.append(f'recall-conceptos: {rol}: recall@3 {hit3}/{len(resp)} = {recall:.2f}  hit@1 {hit1}/{len(resp)}  '
                       f'MRR {rr / len(resp) if resp else 0:.2f}  ninguna {ninguna}/{len(nores)} = {tasa:.0%}  '
                       f'{"en orden" if bien else "BAJO EL UMBRAL"}')
        informe.extend(fallas)
    vistas = {}
    for f in corpus:
        for q in f['preguntas']:
            vistas.setdefault(normalizar(q), []).append(f['id'])
    for q, ids in vistas.items():
        if len(ids) > 1:
            ok = False
            informe.append(f'recall-conceptos: la pregunta «{q.strip()}» está en {ids} (K6 §8.1: una pregunta, una ficha)')
    informe.append(f'recall-conceptos: léxico (sin embeddings: el producto no los tiene); {len(corpus)} fichas medidas; '
                   f'umbral recall@3 y hit@1 >= {UMBRAL_RECALL} por rol, ninguna 100 %, una pregunta en una sola ficha')
    return ok, informe


def autoprueba(raiz):
    fallas = 0
    ok, _ = medir(raiz)
    print(f'  {"ok   " if ok else "FALLA"} verde: el repo en orden')
    fallas += not ok
    plantas = {
        'aaa-ladrona': 'sinonimos: [es un, significa, que es, quiere decir, entiendo]\n'
                       'preguntas: ["¿Qué es un cuerpo?", "¿Qué es una figura?"]',
        'aab-duplicada': 'sinonimos: [cuerpo geometrico]\npreguntas: ["¿Qué es un ortoedro?", "¿Qué es una forma?"]',
        'perimetro': 'sinonimos: [perimetro]\npreguntas: ["¿Qué es el contorno?", "¿Qué es el borde de una figura plana?"]',
    }
    for nombre, cuerpo in plantas.items():
        with tempfile.TemporaryDirectory() as tmp:
            t = Path(tmp)
            shutil.copytree(raiz / 'docs-as-code' / 'ayuda', t / 'docs-as-code' / 'ayuda')
            (t / 'docs-as-code' / 'controles').mkdir(parents=True)
            shutil.copy(raiz / 'docs-as-code' / 'controles' / 'medicion-conceptos.jsonl', t / 'docs-as-code' / 'controles')
            (t / 'docs-as-code' / 'ayuda' / 'conceptos' / f'{nombre}.md').write_text(
                f'---\ntitle: "{nombre}"\nroles: [Student, Administrator]\n{cuerpo}\nstatus: borrador\n---\n\nTexto.\n',
                encoding='utf-8')
            ok, _ = medir(t)
            print(f'  {"FALLA" if ok else "ok   "} rojo: ficha «{nombre}» sembrada')
            fallas += ok
    print(f'recall-conceptos: autoprueba {"en orden" if not fallas else f"con {fallas} fallas"}')
    return 1 if fallas else 0


def main(argv):
    raiz = Path(argv[argv.index('--raiz') + 1]) if '--raiz' in argv else Path(__file__).resolve().parents[2]
    if '--autoprueba' in argv:
        return autoprueba(raiz)
    ok, informe = medir(raiz)
    print('\n'.join(informe))
    return 0 if ok else 1


if __name__ == '__main__':
    try:
        sys.exit(main(sys.argv[1:]))
    except (OSError, ValueError) as e:
        print(f'recall-conceptos: error de uso: {e}')
        sys.exit(2)
