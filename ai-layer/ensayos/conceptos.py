#!/usr/bin/env python3
"""Batería real del asistente de conceptos (rebanada del banco de traslado, 2026-11-11).

    GEO_API=http://127.0.0.1:<puerto> GEO_STORE=<ruta del .db> [GEO_IA_ENDPOINT=...] \
        python3 -I ai-layer/ensayos/conceptos.py --corrida N --salida ARCHIVO.jsonl [--sembrar E1.json]

Corre los 24 casos de `bateria-conceptos-v1.jsonl` (archivo de datos versionado, K7 R9.1) contra una instancia propia del servicio de datos (nunca contra el
despliegue del PO) y escribe una línea por caso con la forma, la intención, el destino, el origen y la latencia (registro crudo).
El veredicto lo calcula `veredicto_conceptos.py` desde el registro, con la lectura docente aparte. La dirección del servidor de modelos llega sólo por GEO_IA_ENDPOINT, y únicamente para
leer la composición (/api/ps: cuántos modelos hay en memoria y si usan aceleración); ni la dirección ni el nombre del
modelo se escriben en la salida (K2 R6). Las cifras de los casos 18 y 19 se comparan con una consulta SQL
independiente sobre GEO_STORE. Sin dependencias: urllib y sqlite3 de la biblioteca estándar.
"""
import json, os, re, sqlite3, sys, time, urllib.request, urllib.error

API = os.environ.get('GEO_API', '').rstrip('/')
STORE = os.environ.get('GEO_STORE', '')
EP = os.environ.get('GEO_IA_ENDPOINT', '').rstrip('/')
ADMIN = ('docente@ejemplo.edu', 'clave-de-prueba-del-docente')
ALUMNA = ('alumna@ejemplo.edu', 'clave-de-prueba-de-la-alumna')
OTRO = ('otro@ejemplo.edu', 'clave-de-prueba-del-otro')

AQUI = os.path.dirname(os.path.abspath(__file__))
# LA BATERÍA ES UN ARCHIVO DE DATOS VERSIONADO (K7 R9.1): este script la lee, corre y guarda el registro crudo; el
# veredicto lo recalcula `veredicto_conceptos.py` desde el registro, con la lectura docente aparte (K3 R2.2a).
BATERIA = os.path.join(AQUI, 'bateria-conceptos-v1.jsonl')
CASOS = [json.loads(l) for l in open(BATERIA, encoding='utf-8') if l.strip()]


def call(method, path, body=None, token=None):
    req = urllib.request.Request(API + path, method=method, data=None if body is None else json.dumps(body).encode(),
                                 headers={'Content-Type': 'application/json', **({'Authorization': 'Bearer ' + token} if token else {})})
    try:
        with urllib.request.urlopen(req, timeout=60) as r:
            raw = r.read()
            return r.status, (json.loads(raw) if raw else None)
    except urllib.error.HTTPError as e:
        raw = e.read()
        return e.code, (json.loads(raw) if raw and raw[:1] in b'{[' else None)


def token(email, password):
    st, b = call('POST', '/v1/auth/token', {'email': email, 'password': password})
    assert st == 200, (st, b)
    return b['accessToken']


def sembrar(e1):
    st, _ = call('POST', '/v1/cuentas/administrador', {'email': ADMIN[0], 'firstName': 'Docente', 'lastName': 'Prueba', 'password': ADMIN[1]})
    adm = token(*ADMIN)
    ids = {}
    for (email, pw), (fn, ln) in ((ALUMNA, ('Alumna', 'Prueba')), (OTRO, ('Otro', 'Prueba'))):
        st, b = call('POST', '/v1/cuentas', {'email': email, 'firstName': fn, 'lastName': ln})
        aid = b['accountId']
        st, b = call('POST', f'/v1/cuentas/{aid}/situacion', {'accountId': aid, 'intendedStatus': 'Enabled'}, adm)
        prov = b['provisionalPassword']
        st, _ = call('POST', '/v1/cuenta/contrasena', {'currentPassword': prov, 'newPassword': pw, 'email': email})
        tok = token(email, pw)
        st, b = call('POST', '/v1/trabajos', {'workId': None, 'name': 'Entrega de la batería', 'declaredDate': '2026-11-11',
                                              'description': None, 'originalJson': e1}, tok)
        assert st in (200, 201), (st, b)
        ids[email] = b['workId']
    json.dump({'own': ids[ALUMNA[0]], 'foreign': ids[OTRO[0]]}, open(os.path.join(os.path.dirname(STORE), 'mundo.json'), 'w'))


def composicion():
    if not EP:
        return {'leida': False}
    try:
        with urllib.request.urlopen(EP + '/api/ps', timeout=10) as r:
            ps = json.load(r).get('models', [])
        return {'leida': True, 'modelos_en_memoria': len(ps), 'aceleracion': any(m.get('size_vram', 0) > 0 for m in ps)}
    except Exception as e:  # noqa: BLE001
        return {'leida': False, 'falla': type(e).__name__}


def sql(q):
    with sqlite3.connect(f'file:{STORE}?mode=ro', uri=True) as c:
        return c.execute(q).fetchone()[0]


def main(argv):
    if '--sembrar' in argv:
        sembrar(open(argv[argv.index('--sembrar') + 1], encoding='utf-8').read())
        return 0
    corrida = argv[argv.index('--corrida') + 1]
    salida = argv[argv.index('--salida') + 1]
    mundo = json.load(open(os.path.join(os.path.dirname(STORE), 'mundo.json')))
    toks = {'alumno': token(*ALUMNA), 'docente': token(*ADMIN)}
    st, salud = call('GET', '/salud')
    antes = composicion()
    with open(salida, 'w', encoding='utf-8') as out:
        out.write(json.dumps({'corrida': corrida, 'composicion_antes': antes, 'ia_del_asistente': salud.get('assistantAi'),
                              'version_servicio': salud.get('version')}, ensure_ascii=False) + '\n')
        for c in CASOS:
            n, rol, q = c['caso'], c['rol'], c['pregunta']
            wid = mundo.get(c['pantalla'])
            t0 = time.time()
            st, b = call('POST', '/v1/conceptos/consultas', {'question': q, 'workId': wid}, toks[rol])
            ms = round((time.time() - t0) * 1000)
            r = {'corrida': corrida, 'caso': n, 'rol': rol, 'pregunta': q, 'http': st, 'ms': ms}
            if st == 200:
                r.update(forma=b['form'], intencion=b['intent'], anillo=b['ring'], destino=(b.get('destination') or {}).get('id'),
                         ruta=(b.get('destination') or {}).get('path'), origen=b['origin'], texto=b['text'], nota=b.get('note'),
                         desglose=sum(x['count'] for x in (b.get('breakdown') or [])))
                # LA CIFRA DE REFERENCIA ES UNA CONSULTA SQL INDEPENDIENTE SOBRE EL ALMACÉN (K3 R7): se registra, no se juzga acá.
                if n == 18:
                    r['sql'] = sql(f"select count(*) from Observacion where upper(WorkId)=upper('{wid}') and Kind='Warning'")
                if n == 19:
                    r['sql'] = sql("select count(*) from Work w join Account a on a.Id=w.OwnerId where lower(a.NormalizedEmail)='alumna@ejemplo.edu'")
            out.write(json.dumps(r, ensure_ascii=False) + '\n')
        despues = composicion()
        out.write(json.dumps({'corrida': corrida, 'M': len(CASOS), 'composicion_despues': despues}, ensure_ascii=False) + '\n')
    print(f'corrida {corrida}: registro en {salida}; veredicto con veredicto_conceptos.py')
    return 0


if __name__ == '__main__':
    sys.exit(main(sys.argv[1:]))
