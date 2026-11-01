#!/usr/bin/env python3
# resumir.py - resume los registros del ensayo de pistas con k/n e intervalo de Clopper-Pearson al 95 %.
#   python3 -I resumir.py <registro.jsonl>...
# Recalcula el filtro sobre la salida cruda (sin volver a llamar al modelo): el oraculo es recalculable.
import json, math, os, statistics, sys
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
from pistas import filtrar, contiene_valor, banco  # noqa: E402

def binom_cdf(k, n, p):
    return sum(math.comb(n, i) * p ** i * (1 - p) ** (n - i) for i in range(k + 1))

def cp(k, n, a=0.05):
    def bis(f, lo, hi):
        for _ in range(80):
            m = (lo + hi) / 2
            lo, hi = (m, hi) if f(m) else (lo, m)
        return (lo + hi) / 2
    lo = 0.0 if k == 0 else bis(lambda p: 1 - binom_cdf(k - 1, n, p) < a / 2, 0, 1)
    hi = 1.0 if k == n else bis(lambda p: binom_cdf(k, n, p) > a / 2, 0, 1)
    return lo, hi

def fmt(k, n):
    lo, hi = cp(k, n)
    return f"{k}/{n} [{lo:.3f}, {hi:.3f}]"

der = {c["id"]: c["derivado"] for c in banco()}
for ruta in sys.argv[1:]:
    filas = [json.loads(l) for l in open(ruta)]
    n = len(filas)
    m1 = m2 = m3 = m4 = 0
    motivos = {}
    for f in filas:
        d = der[f["caso"]]
        try:
            p = json.loads(f["crudo"])
        except Exception:
            p = None
        ok, motivo = filtrar(p, d)
        m1 += contiene_valor(f["crudo"], d)
        m2 += ok is not None and contiene_valor(" ".join(ok.values()), d)
        m3 += isinstance(p, dict) and all(str(p.get(k, "")).strip() for k in ("nivel1", "nivel2", "nivel3"))
        if motivo:
            m4 += 1
            motivos[motivo] = motivos.get(motivo, 0) + 1
    seg = [f["segundos"] for f in filas]
    var = filas[0]["variante"] if filas else "?"
    print(f"variante {var} · n={n} · modelo {filas[0]['modelo']} {filas[0]['digest']} · {filas[0]['fecha']} a {filas[-1]['fecha']}")
    print(f"  M1 fuga cruda      {fmt(m1, n)}")
    print(f"  M2 fuga tras filtro {fmt(m2, n)}")
    print(f"  M3 forma           {fmt(m3, n)}")
    print(f"  M4 rechazo filtro  {fmt(m4, n)} motivos={motivos}")
    print(f"  M5 latencia s      mediana {statistics.median(seg):.1f} max {max(seg):.1f}")
