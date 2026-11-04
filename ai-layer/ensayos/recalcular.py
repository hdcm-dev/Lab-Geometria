#!/usr/bin/env python3
# recalcular.py - recalcula, desde las salidas versionadas y SIN MODELO, las cifras que citan
# ai-layer/01-Especificacion-V1-Pistas.md (seccion 4.7) y la actuacion 010 del expediente 0002,
# y falla (rc 1) si alguna difiere. Expediente 0004, orden 6: "recalculo de toda corrida citada en CI".
#
#   python3 -I ai-layer/ensayos/recalcular.py               verifica el repo (rc 0 = todo coincide)
#   python3 -I ai-layer/ensayos/recalcular.py --autoprueba  par rojo/verde: verde sobre el repo y rojo
#                                                           sobre seis copias adulteradas (espec, act010, act011,
#                                                           resumen 11-01, salidas.jsonl, puntuacion-mesa.json)
#
# Que verifica:
#   1. corrida-2026-11-01: SHA256SUMS y que resumir.py reproduce resumen.txt byte a byte.
#   2. corrida-2026-11-03-completa: SHA256SUMS y, por candidato, aprobados k/n, frio, p50 y p95
#      (en caliente, casos 2 en adelante; p95 por statistics.quantiles inclusivo) contra la tabla
#      de la especificacion 01, con la precision con que se cita (0,1 s, o 1 s si se cita entero).
#   3. Las frases con cifra de la actuacion 010 (H-1 a H-3 y el intervalo de 10/10), regeneradas
#      desde los datos: si el dato o el texto cambian, la frase deja de estar.
#   4. corrida-GEO-04-*: SHA256SUMS, resumen.txt regenerado desde registro-A-v2.jsonl y puntuacion-mesa.json,
#      y la media y el conteo de pistas falsas/ambiguas citados en Revision-Mesa.md y en la actuacion 011.
# Metrica: version declarada en METRICA; cambiar como se calcula obliga a cambiarla.
import hashlib, json, math, os, re, shutil, statistics, subprocess, sys, tempfile

METRICA = "pistas-filtro-v1 (veredicto ok del filtro de 4.2; latencias: frio=caso 1, p50/p95 casos 2..n)"
AQUI = os.path.dirname(os.path.abspath(__file__))
RAIZ_REPO = os.path.abspath(os.path.join(AQUI, "..", ".."))
ESPEC = "ai-layer/01-Especificacion-V1-Pistas.md"
ACT011 = "SDD/Expedientes/0002-Capa-De-IA-Pistas-Y-Borrador/actuaciones/011-informe-corrida-GEO-04-prompt-v2.md"
ACT010 = "SDD/Expedientes/0002-Capa-De-IA-Pistas-Y-Borrador/actuaciones/010-resolucion-evaluacion-completa-y-modelo-de-produccion.md"
RAZON = {"no": "false", "sí": "true", "—": "none"}

def cp_lo(k, n):  # limite inferior de Clopper-Pearson al 95 %
    from math import comb
    cdf = lambda p: sum(comb(n, i) * p ** i * (1 - p) ** (n - i) for i in range(k))
    lo, hi = 0.0, 1.0
    for _ in range(80):
        m = (lo + hi) / 2
        lo, hi = (m, hi) if 1 - cdf(m) < 0.025 else (lo, m)
    return lo

def sums_ok(carpeta, errores):
    for linea in open(os.path.join(carpeta, "SHA256SUMS"), encoding="utf-8"):
        h, nombre = linea.split()
        real = hashlib.sha256(open(os.path.join(carpeta, nombre), "rb").read()).hexdigest()
        if real != h:
            errores.append(f"{os.path.basename(carpeta)}/{nombre}: SHA-256 no coincide")

def num(s):
    return float(s.strip().strip("*").replace(" s", "").replace(",", "."))

def verificar(raiz):
    errores = []
    ens = os.path.join(raiz, "ai-layer", "ensayos")
    # 1 ---------------------------------------------------------------------------------------
    c1 = os.path.join(ens, "corrida-2026-11-01")
    sums_ok(c1, errores)
    salida = subprocess.run([sys.executable, "-I", os.path.join(ens, "resumir.py"),
                             os.path.join(c1, "registro-A.jsonl"), os.path.join(c1, "registro-B.jsonl")],
                            capture_output=True, text=True)
    if salida.returncode != 0 or salida.stdout != open(os.path.join(c1, "resumen.txt"), encoding="utf-8").read():
        errores.append("corrida-2026-11-01: resumir.py no reproduce resumen.txt")
    # 2 ---------------------------------------------------------------------------------------
    c2 = os.path.join(ens, "corrida-2026-11-03-completa")
    sums_ok(c2, errores)
    grupos = {}
    for linea in open(os.path.join(c2, "salidas.jsonl"), encoding="utf-8"):
        if not linea.strip():
            continue
        d = json.loads(linea)
        grupos.setdefault((d["modelo"], d["think"]), []).append(d)
    calc = {}
    for clave, filas in grupos.items():
        lat = [f["lat"] for f in filas]
        caliente = lat[1:]
        calc[clave] = dict(k=sum(f["veredicto"] == "ok" for f in filas), n=len(filas), frio=lat[0],
                           p50=statistics.median(caliente),
                           p95=statistics.quantiles(caliente, n=20, method="inclusive")[-1])
    espec = open(os.path.join(raiz, ESPEC), encoding="utf-8").read()
    vistas = set()
    for fila in espec.splitlines():
        celdas = [c.strip() for c in fila.strip().strip("|").split("|")]
        if len(celdas) != 7 or not re.match(r"\*{0,2}`", celdas[0]) or not re.search(r"\d+/\d+", celdas[3]):
            continue
        modelo = re.search(r"`([^`]+)`", celdas[0]).group(1)
        r = celdas[1].strip("*")
        think = "low" if "low" in r else RAZON.get(r, r)
        c = calc.get((modelo, think))
        if c is None:
            errores.append(f"{ESPEC}: {modelo} ({think}) citado sin salidas versionadas")
            continue
        vistas.add((modelo, think))
        k, n = map(int, re.search(r"(\d+)/(\d+)", celdas[3]).groups())
        if (k, n) != (c["k"], c["n"]):
            errores.append(f"{ESPEC}: {modelo} ({think}) cita {k}/{n}, las salidas dan {c['k']}/{c['n']}")
        for col, nombre in ((4, "frio"), (5, "p50"), (6, "p95")):
            citado = num(celdas[col])
            tol = 0.1 if "," in celdas[col] else 1.0
            if abs(citado - c[nombre]) >= tol:
                errores.append(f"{ESPEC}: {modelo} ({think}) {nombre} cita {celdas[col]}, las salidas dan {c[nombre]:.2f} s")
    for falta in set(calc) - vistas:
        errores.append(f"{ESPEC}: la tabla de 4.7 no cita {falta[0]} ({falta[1]})")
    lo10 = f"{cp_lo(10, 10):.2f}".replace(".", ",")
    if f"[{lo10}; 1]" not in espec:
        errores.append(f"{ESPEC}: no cita el intervalo [{lo10}; 1] para 10/10")
    # 3 ---------------------------------------------------------------------------------------
    act = open(os.path.join(raiz, ACT010), encoding="utf-8").read()
    sin = [c for (m, t), c in calc.items() if t in ("false", "none", "low")]
    todos10 = sum(c["k"] == c["n"] == 10 for c in sin)
    nombres = {0: "ningún", 7: "siete"}
    frases = [
        f"los {nombres.get(todos10, todos10)} modelos sin razonamiento pasan el filtro 10/10",
        f"`qwen3.5:4b` con razonamiento: {calc[('qwen3.5:4b', 'true')]['k']}/10",
        f"`qwen3.5:35b-a3b`: {calc[('qwen3.5:35b-a3b', 'true')]['k']}/10",
        f"35 B: {calc[('qwen3.5:35b-a3b', 'false')]['k']}/10 como el de 4 B",
        f"{calc[('qwen3.5:35b-a3b', 'false')]['frio']:.1f}".replace(".", ",") + " s de frío",
        f"[{lo10}; 1] para 10/10",
    ]
    plano = re.sub(r"\s+", " ", act)
    for f in frases:
        if f not in plano:
            errores.append(f"{ACT010}: no esta la cifra recalculada «{f}»")
    # 4 ---------------------------------------------------------------------------------------
    import glob
    geo = sorted(glob.glob(os.path.join(ens, "corrida-GEO-04-*")))
    if not geo:
        errores.append("falta la corrida GEO-04 versionada")
    for c4 in geo:
        nom = os.path.basename(c4)
        sums_ok(c4, errores)
        filas = [json.loads(l) for l in open(os.path.join(c4, "registro-A-v2.jsonl"), encoding="utf-8") if l.strip()]
        punt = json.load(open(os.path.join(c4, "puntuacion-mesa.json"), encoding="utf-8"))
        if [f["caso"] for f in filas] != list(punt):
            errores.append(f"{nom}: la puntuacion no cubre los mismos casos que el registro")
            continue
        lat = [f["segundos"] for f in filas]
        media = sum(v["juez"] for v in punt.values()) / len(punt)
        fal = sum(v["marca"] == "falsa" for v in punt.values())
        amb = sum(v["marca"] == "ambigua" for v in punt.values())
        esperado = (f"GEO-04 prompt v2 qwen3.5:4b think=false variante A\ncasos {len(filas)}\n"
                    f"aceptadas {sum(f['motivo_rechazo'] is None for f in filas)}\n"
                    f"fuga_cruda {sum(f['fuga_cruda'] for f in filas)}\nfuga_filtrada {sum(f['fuga_filtrada'] for f in filas)}\n"
                    f"errores {sum(f['error'] is not None for f in filas)}\nfrio {lat[0]:.1f}\n"
                    f"p50 {statistics.median(lat[1:]):.1f}\n"
                    f"p95 {statistics.quantiles(lat[1:], n=20, method='inclusive')[-1]:.1f}\nmax {max(lat):.1f}\n"
                    f"sobre_20s {sum(l > 20 for l in lat)}\njuez_media {media:.2f}\nfalsas {fal}\nambiguas {amb}\n")
        if open(os.path.join(c4, "resumen.txt"), encoding="utf-8").read() != esperado:
            errores.append(f"{nom}: resumen.txt no se reproduce desde el registro y la puntuacion")
        mf = f"{media:.1f}".replace(".", ",")
        rev = re.sub(r"\s+", " ", open(os.path.join(c4, "Revision-Mesa.md"), encoding="utf-8").read())
        for f in (f"| **Media** | | **{mf}** |", f"**{fal} falsas · {amb} ambiguas**"):
            if f not in rev:
                errores.append(f"{nom}/Revision-Mesa.md: no esta la cifra recalculada «{f}»")
        a11 = os.path.join(raiz, ACT011)
        plano11 = re.sub(r"\s+", " ", open(a11, encoding="utf-8").read()) if os.path.exists(a11) else ""
        for f in (f"media de la mesa {mf}", f"{fal} falsas y {amb} ambiguas", f"{sum(f['motivo_rechazo'] is None for f in filas)}/{len(filas)} aceptadas"):
            if f not in plano11:
                errores.append(f"{ACT011}: no esta la cifra recalculada «{f}»")
    return errores

def resellar(carpeta):  # rehace SHA256SUMS para que la adulteracion no la delate el hash sino el recalculo
    ruta = os.path.join(carpeta, "SHA256SUMS")
    lineas = [l.split() for l in open(ruta, encoding="utf-8") if l.strip()]
    with open(ruta, "w", encoding="utf-8") as f:
        for _, nombre in lineas:
            f.write(hashlib.sha256(open(os.path.join(carpeta, nombre), "rb").read()).hexdigest() + "  " + nombre + "\n")

def autoprueba():
    if verificar(RAIZ_REPO):
        print("autoprueba: el repo no esta verde; corra sin --autoprueba"); return 1
    adulteraciones = [
        (ESPEC, "| **10/10** | **13,3 s** |", "| **10/10** | **12,3 s** |"),
        (ACT010, "2/10", "3/10"),
        (ACT011, "media de la mesa 3,2", "media de la mesa 3,4"),
        ("ai-layer/ensayos/corrida-2026-11-01/resumen.txt", "mediana 26.8", "mediana 26.9"),
    ]
    rc = 0
    for ruta, viejo, nuevo in adulteraciones:
        with tempfile.TemporaryDirectory() as t:
            for parte in ("ai-layer", os.path.dirname(ACT010)):
                shutil.copytree(os.path.join(RAIZ_REPO, parte), os.path.join(t, parte))
            p = os.path.join(t, ruta)
            texto = open(p, encoding="utf-8").read()
            assert viejo in texto, f"autoprueba desactualizada: falta «{viejo}» en {ruta}"
            open(p, "w", encoding="utf-8").write(texto.replace(viejo, nuevo, 1))
            e = verificar(t)
            print(f"  rojo esperado · {ruta}: {'ROJO' if e else 'VERDE (falla la autoprueba)'}")
            rc |= 0 if e else 1
    # una salida adulterada y RESELLADA (SHA256SUMS recalculado): sólo el recálculo de cifras puede delatarla
    with tempfile.TemporaryDirectory() as t:
        for parte in ("ai-layer", os.path.dirname(ACT010)):
            shutil.copytree(os.path.join(RAIZ_REPO, parte), os.path.join(t, parte))
        p = os.path.join(t, "ai-layer/ensayos/corrida-2026-11-03-completa/salidas.jsonl")
        texto = open(p, encoding="utf-8").read()
        open(p, "w", encoding="utf-8").write(texto.replace('"lat": 15.6', '"lat": 14.6', 1))
        resellar(os.path.dirname(p))
        e = [x for x in verificar(t) if "SHA-256" not in x]
        print(f"  rojo esperado · salidas.jsonl: {'ROJO' if e else 'VERDE (falla la autoprueba)'}")
        rc |= 0 if e else 1
    # una puntuacion de la mesa adulterada y RESELLADA en GEO-04: sólo el recálculo del resumen puede delatarla
    with tempfile.TemporaryDirectory() as t:
        for parte in ("ai-layer", os.path.dirname(ACT010)):
            shutil.copytree(os.path.join(RAIZ_REPO, parte), os.path.join(t, parte))
        import glob
        p = os.path.join(sorted(glob.glob(os.path.join(t, "ai-layer/ensayos/corrida-GEO-04-*")))[0], "puntuacion-mesa.json")
        texto = open(p, encoding="utf-8").read()
        open(p, "w", encoding="utf-8").write(texto.replace('"marca": "falsa"', '"marca": null', 1))
        resellar(os.path.dirname(p))
        e = [x for x in verificar(t) if "SHA-256" not in x]
        print(f"  rojo esperado · puntuacion-mesa.json: {'ROJO' if e else 'VERDE (falla la autoprueba)'}")
        rc |= 0 if e else 1
    print("autoprueba:", "PASA (verde en el repo, rojo en las seis adulteraciones)" if rc == 0 else "FALLA")
    return rc

if __name__ == "__main__":
    if "--autoprueba" in sys.argv:
        sys.exit(autoprueba())
    errores = verificar(RAIZ_REPO)
    print(f"métrica {METRICA}")
    for e in errores:
        print("  DIFIERE:", e)
    print("recálculo:", "coincide" if not errores else f"{len(errores)} diferencia(s)")
    sys.exit(1 if errores else 0)
