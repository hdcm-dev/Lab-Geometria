#!/usr/bin/env python3
# pistas.py - ensayo de la capacidad "pistas de observaciones" (piloto V1 de ai-layer/).
#
#   python3 -I pistas.py --banco <salida.json>              genera el banco determinista de 30 casos
#   python3 -I pistas.py --autoprueba                        prueba el filtro sin modelo (rc 0 = pasa)
#   OLLAMA_URL=<url> python3 -I pistas.py --correr <banco.json> <variante A|B> <registro.jsonl>
#
# No es codigo del producto: es la implementacion de referencia del filtro (REQ-IA-05, REQ-IA-06) y el
# arnes de la prueba de concepto. El criterio de la corrida esta en Criterio-R-LLM.md, escrito antes.
import json, math, os, re, sys, time, urllib.request

MODELO = "qwen3.5:4b"
DIGEST = "d8b0f5e9760c"

# Prompt propio de la capacidad. Debe coincidir con 01-Especificacion-V1-Pistas.md, seccion 5.2.
PROMPT_SISTEMA = (
    "Sos un asistente de una catedra de programacion. Un validador ya encontro una diferencia entre el "
    "valor que declaro el programa del alumno y el que corresponde a las dimensiones de la figura. Escribi "
    "tres pistas graduadas para que el alumno encuentre por su cuenta el error en su programa. Reglas: "
    "1) nunca escribas el valor correcto ni lo calcules; 2) nunca escribas JSON, codigo ni nombres de clases; "
    "3) no corrijas el texto del alumno; 4) nivel1 dice donde mirar; nivel2 dice que concepto geometrico "
    "interviene; nivel3 dice como comprobarlo paso a paso, sin numeros. Cada pista en una o dos oraciones."
)

ESQUEMA = {
    "type": "object",
    "properties": {"nivel1": {"type": "string"}, "nivel2": {"type": "string"}, "nivel3": {"type": "string"}},
    "required": ["nivel1", "nivel2", "nivel3"],
}

def r2(x):
    return round(x + 0.0, 2)

def banco():
    casos = []
    pi = math.pi
    for l in (2, 3, 4, 5, 7):
        casos.append(("Cubo", "Area", r2(4 * l * l), r2(6 * l * l), f"cubo de arista {l}: area con 4 caras"))
    for l in (2, 3, 6):
        casos.append(("Cubo", "Volumen", r2(l * l), r2(l ** 3), f"cubo de arista {l}: volumen como area de una cara"))
    for a, b, c in ((7, 7, 21), (2, 3, 4), (5, 2, 10), (3, 3, 8)):
        casos.append(("Ortoedro", "Volumen", r2(a * b), r2(a * b * c), f"ortoedro {a}x{b}x{c}: volumen sin el largo"))
    for a, b, c in ((2, 3, 4), (5, 2, 10), (7, 7, 21), (1, 2, 6)):
        casos.append(("Ortoedro", "Area", r2(2 * (a * b + a * c)), r2(2 * (a * b + a * c + b * c)), f"ortoedro {a}x{b}x{c}: faltan dos caras"))
    for r, h in ((1, 2), (2, 5), (3, 4)):
        casos.append(("Cilindro", "Area", r2(2 * pi * r * h), r2(2 * pi * r * (r + h)), f"cilindro r={r} h={h}: solo area lateral"))
    for r, h in ((1, 2), (2, 5), (3, 4)):
        casos.append(("Cilindro", "Volumen", r2(2 * pi * r * h), r2(pi * r * r * h), f"cilindro r={r} h={h}: volumen con 2*pi*r*h"))
    for a, b in ((3, 4), (2, 9)):
        casos.append(("Rectangulo", "Area", r2(2 * (a + b)), r2(a * b), f"rectangulo {a}x{b}: area como perimetro"))
    for l in (3, 5):
        casos.append(("Cuadrado", "Area", r2(4 * l), r2(l * l), f"cuadrado de lado {l}: area como perimetro"))
    for r in (1, 3, 5, 10):
        casos.append(("Circulo", "Area", r2(2 * pi * r), r2(pi * r * r), f"circulo r={r}: area como circunferencia"))
    out = []
    for i, (fig, campo, dec, der, nota) in enumerate(casos, 1):
        out.append({"id": f"C{i:02d}", "figura": fig, "campo": campo, "declarado": dec, "derivado": der, "nota": nota})
    assert len(out) == 30, len(out)
    return out

def mensaje(caso, variante):
    sentido = "mayor" if caso["declarado"] > caso["derivado"] else "menor"
    lineas = [
        f"Figura: {caso['figura']}",
        f"Campo: {caso['campo']}",
        f"Valor declarado por el programa: {caso['declarado']:.2f}",
        f"El valor declarado es {sentido} que el que corresponde.",
    ]
    if variante == "B":
        lineas.append(f"Valor que corresponde: {caso['derivado']:.2f}")
    return "\n".join(lineas)

NUM = re.compile(r"(?<![\w.])\d+(?:[.,]\d+)?")
JSON_FIGURA = re.compile(r'[{}]|"\s*\w+\s*"\s*:')
CODIGO = re.compile(r"\bclass\s+\w+|\breturn\b|=>|\bpublic\s+\w+|\bMath\.\w+|;\s*$", re.M)

def numeros(texto):
    for m in NUM.finditer(texto):
        try:
            yield float(m.group(0).replace(",", "."))
        except ValueError:
            pass

def contiene_valor(texto, valor):
    return any(abs(n - valor) <= 0.01 for n in numeros(texto))

def filtrar(pistas, derivado):
    """Devuelve (aceptadas, motivo). motivo None = aceptadas; si no, se usa la plantilla."""
    if not isinstance(pistas, dict) or any(not str(pistas.get(k, "")).strip() for k in ("nivel1", "nivel2", "nivel3")):
        return None, "forma"
    texto = " ".join(str(pistas[k]) for k in ("nivel1", "nivel2", "nivel3"))
    if contiene_valor(texto, derivado):
        return None, "valor-derivado"
    if JSON_FIGURA.search(texto):
        return None, "json-de-figura"
    if CODIGO.search(texto):
        return None, "codigo"
    return pistas, None

def autoprueba():
    casos = [
        ({"nivel1": "Mira el area.", "nivel2": "Un cubo tiene seis caras.", "nivel3": "Conta las caras."}, 54.0, None),
        ({"nivel1": "El area deberia ser 54.", "nivel2": "x", "nivel3": "y"}, 54.0, "valor-derivado"),
        ({"nivel1": "Da 54,00.", "nivel2": "x", "nivel3": "y"}, 54.0, "valor-derivado"),
        ({"nivel1": "Da 54.004.", "nivel2": "x", "nivel3": "y"}, 54.0, "valor-derivado"),
        ({"nivel1": 'Usa {"Tipo": "Cubo"}', "nivel2": "x", "nivel3": "y"}, 54.0, "json-de-figura"),
        ({"nivel1": "Escribi return 6 * l * l;", "nivel2": "x", "nivel3": "y"}, 54.0, "codigo"),
        ({"nivel1": "Declaraste 36.00, revisa.", "nivel2": "x", "nivel3": "y"}, 54.0, None),
        ({"nivel1": "", "nivel2": "x", "nivel3": "y"}, 54.0, "forma"),
    ]
    fallas = 0
    for p, d, esperado in casos:
        _, motivo = filtrar(p, d)
        ok = motivo == esperado
        fallas += not ok
        print(("ok   " if ok else "FALLA"), esperado, "->", motivo)
    print(f"autoprueba: {len(casos) - fallas}/{len(casos)}")
    return 0 if fallas == 0 else 1

def pedir(url, cuerpo):
    req = urllib.request.Request(url, data=json.dumps(cuerpo).encode(), headers={"Content-Type": "application/json"})
    with urllib.request.urlopen(req, timeout=600) as r:
        return json.load(r)

def correr(banco_path, variante, registro):
    url = os.environ.get("OLLAMA_URL")
    if not url:
        print("falta OLLAMA_URL (sin valor por omision a proposito)", file=sys.stderr)
        return 2
    tags = pedir_get(url + "/api/tags")
    dig = {m["name"]: m["digest"][:12] for m in tags["models"]}
    if dig.get(MODELO) != DIGEST:
        print(f"digest distinto: {dig.get(MODELO)}", file=sys.stderr)
        return 2
    casos = json.load(open(banco_path))
    with open(registro, "a") as reg:
        for c in casos:
            t0 = time.time()
            cuerpo = {
                "model": MODELO, "stream": False, "think": False, "format": ESQUEMA,
                "options": {"temperature": 0.2, "seed": 7, "num_ctx": 4096},
                "messages": [{"role": "system", "content": PROMPT_SISTEMA}, {"role": "user", "content": mensaje(c, variante)}],
            }
            try:
                resp = pedir(url + "/api/chat", cuerpo)
                crudo = resp["message"]["content"]
                err = None
            except Exception as e:  # una falla del modelo es un camino normal: degradado
                crudo, err = "", repr(e)
            seg = round(time.time() - t0, 1)
            try:
                pistas = json.loads(crudo)
            except Exception:
                pistas = None
            texto_crudo = crudo
            aceptadas, motivo = filtrar(pistas, c["derivado"])
            fuga_cruda = contiene_valor(texto_crudo, c["derivado"])
            fuga_filtrada = aceptadas is not None and contiene_valor(" ".join(aceptadas.values()), c["derivado"])
            fila = {"caso": c["id"], "variante": variante, "segundos": seg, "error": err, "crudo": crudo,
                    "motivo_rechazo": motivo, "fuga_cruda": fuga_cruda, "fuga_filtrada": fuga_filtrada,
                    "modelo": MODELO, "digest": DIGEST, "fecha": time.strftime("%Y-%m-%dT%H:%M:%S%z")}
            reg.write(json.dumps(fila, ensure_ascii=False) + "\n")
            reg.flush()
            print(c["id"], variante, seg, motivo, "fuga_cruda" if fuga_cruda else "")
    return 0

def pedir_get(url):
    with urllib.request.urlopen(url, timeout=30) as r:
        return json.load(r)

if __name__ == "__main__":
    a = sys.argv[1:]
    if a[:1] == ["--banco"] and len(a) == 2:
        json.dump(banco(), open(a[1], "w"), ensure_ascii=False, indent=1)
        sys.exit(0)
    if a == ["--autoprueba"]:
        sys.exit(autoprueba())
    if a[:1] == ["--correr"] and len(a) == 4 and a[2] in ("A", "B"):
        sys.exit(correr(a[1], a[2], a[3]))
    print(__doc__ or "uso: ver cabecera", file=sys.stderr)
    sys.exit(2)
