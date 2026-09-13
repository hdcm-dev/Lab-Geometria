# Derivación del ciclo de origen, v5 (respuesta a MN-REF-07, sobre v4).
# Base fija b9675d8. Por fila (documento, sección de proyecto, Id):
#  1. La SECCIÓN de la fila: el último encabezado «### N.M `GeometriaFactory-X`» anterior a la tabla da el proyecto dueño.
#  2. Linaje: archivos con el basename vigente o su antecesor, en la misma categoría, del MISMO proyecto
#     (ruta Proyectos/<proyecto>/... o Unidades-Entrega/<unidad>/...), excluidos _legacy/ y _fusion/.
#  3. git log b9675d8 --reverse -S"<fragmento>" --name-only: el primer commit que toca un archivo del linaje.
#  4. Preexistencia: si en el padre de ese commit, EN ESE MISMO ARCHIVO, ya había una fila de tabla de ítems
#     diferidos (cabecera con «Quién lo cierra») con el mismo Id → el texto se reescribió: no derivable.
#  5. Si ningún fragmento resuelve → no derivable. Terna (Master-Prompt.md §8.2): fase = asunto literal del commit;
#     unidad de trabajo = el PROYECTO DE CÓDIGO en curso en ese ciclo (todos los ciclos derivados son anteriores a la 8.0,
#     cuando la unidad de trabajo era el proyecto); base = hash corto. La unidad de entrega que hoy lo compone se anota aparte.
import re,subprocess,json,sys
B="b9675d8"
LIN={"Arquitectura-Unidad-Entrega.md":["Arquitectura-Unidad-Entrega.md","Arquitectura-Proyecto-Codigo.md"],"Product-Backlog.md":["Product-Backlog.md"],"Pipeline-CI-CD.md":["Pipeline-CI-CD.md"],"Supply-Chain-Seguridad.md":["Supply-Chain-Seguridad.md"],"Estrategia-Versionado.md":["Estrategia-Versionado.md"]}
DOCS=[l.strip() for l in open(sys.argv[2])] if len(sys.argv)>2 else []
HDR="| Id | Punto abierto | Quién lo cierra | En qué evento se cierra (artefacto y sección) | Estado |"
def git(*a): return subprocess.run(["git",*a],capture_output=True,text=True).stdout
def fragments(cell):
    c=re.sub(r'\[([^\]]*)\]\([^)]*\)',r'\1',cell)
    vivos=re.sub(r'~~.*?~~','',c); tach=" ".join(re.findall(r'~~(.*?)~~',c)); out=[]
    for src in (vivos,tach):
        ps=sorted([p.strip() for p in re.split(r'\*\*|`|—|«|»|\(|\)|;|\.|:|\|',src) if len(p.strip())>=14],key=len,reverse=True)
        out+=[p[:60].strip() for p in ps[:4]]
    return out
def fila_diferida_en(texto,rid):
    L=texto.split("\n"); en=False
    for l in L:
        if l.startswith("|") and "Quién lo cierra" in l: en=True; continue
        if en and not l.startswith("|"): en=False
        if en and re.match(r"^\|\s*`?"+re.escape(rid)+r"`?\s*\|",l): return True
    return False
def derivar(cell,doc,rid,proy):
    cat="/"+doc.split("/")[-2]+"/"; bases=LIN[doc.split("/")[-1]]
    for f in fragments(cell):
        out=git("log",B,"--reverse","--format=@@%h\t%ad\t%s","--date=short","--name-only","-S",f)
        for bl in out.split("@@")[1:]:
            ls=bl.strip().split("\n"); cab=ls[0]
            hit=[x for x in ls[1:] if x.split("/")[-1] in bases and cat in x and "/_legacy/" not in x and "/_fusion/" not in x and ("/"+proy+"/" in x)]
            if not hit: continue
            h=cab.split("\t")[0]; arch=hit[0]
            padre=git("show",f"{h}^:{arch}")
            cmd=f'git log {B} --reverse --name-only -S"{f}"  →  {h} en {arch}'
            if padre and fila_diferida_en(padre,rid):
                return dict(frag=f,commit=None,archivo=arch,cmd=cmd+f"; git show {h}^:{arch} ya tiene la fila {rid}",motivo="no derivable — anterior al mecanismo (la fila ya existía en ese archivo: texto reescrito)")
            return dict(frag=f,commit=cab,archivo=arch,cmd=cmd,motivo="alta de la fila en el linaje del proyecto")
    return dict(frag=None,commit=None,archivo=None,cmd="ningún fragmento con alta en el linaje del proyecto",motivo="no derivable — anterior al mecanismo")
UE={"GeometriaFactory-Api":"GeometriaFactory-Api","GeometriaFactory-Domain":"GeometriaFactory-Api","GeometriaFactory-Application":"GeometriaFactory-Api","GeometriaFactory-Infrastructure":"GeometriaFactory-Api","GeometriaFactory-Web":"GeometriaFactory-Web","GeometriaFactory-Visor":"GeometriaFactory-Web","GeometriaFactory-Contracts":"GeometriaFactory-Api y GeometriaFactory-Web"}
rows=[]
for d in DOCS:
    L=open(d,encoding='utf-8').read().split("\n"); proy=None; i=0
    while i<len(L):
        m=re.match(r"^#{2,4} .*`(GeometriaFactory-[A-Za-z]+)`",L[i])
        if m: proy=m.group(1)
        if L[i].strip()==HDR:
            j=i+2
            while j<len(L) and L[j].startswith("|"):
                c=[x.strip() for x in L[j].strip().strip("|").split("|")]
                p=proy or ("GeometriaFactory-Api" if "/GeometriaFactory-Api/" in d else "GeometriaFactory-Web")
                r=derivar(c[1],d,c[0],p); r.update(doc=d,line=j+1,id=c[0],seccion=p,unidad=UE.get(p,"?"),superficie="PM-06"); rows.append(r); j+=1
            i=j
        else: i+=1
json.dump(rows,open(sys.argv[1],"w"),ensure_ascii=False,indent=1)
from collections import Counter
print("filas",len(rows),"derivadas",sum(1 for r in rows if r["commit"]),"no derivables",sum(1 for r in rows if not r["commit"]))
print(Counter(r["motivo"].split(" (")[0] for r in rows))
print(Counter(r["seccion"] for r in rows))
