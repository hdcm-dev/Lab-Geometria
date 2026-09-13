# Derivación del ciclo de origen, v4 (suma MN-TRZ-01/02: preexistencia del Id en el padre, terna) (suma MN-FOR-01: categoría y proyectos de la unidad) (corrige MN-VER-01..03 y 05).
# Regla de elección declarada: por fila se corre
#   git log main --reverse --format=%h%x09%ad%x09%s --date=short --name-status -S"<fragmento>"
# y se conservan sólo los commits que tocan un archivo del LINAJE del documento (su basename vigente
# o el del antecesor anterior a la consolidación de 2026-08-16), excluidos _legacy/ y _fusion/.
# El primero de esa lista es necesariamente un ALTA del fragmento en el linaje (antes su recuento era 0):
# ése es el commit que introdujo la fila. Si ningún fragmento da un commit en el linaje: no derivable.
import re,subprocess,json,sys
BASE="b9675d8"
LINAJE={"Arquitectura-Unidad-Entrega.md":["Arquitectura-Unidad-Entrega.md","Arquitectura-Proyecto-Codigo.md"],
"Product-Backlog.md":["Product-Backlog.md"],"Pipeline-CI-CD.md":["Pipeline-CI-CD.md"],
"Supply-Chain-Seguridad.md":["Supply-Chain-Seguridad.md"],"Estrategia-Versionado.md":["Estrategia-Versionado.md"]}
DOCS=["SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md",
"SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/06-Backlog-Tecnico/Product-Backlog.md",
"SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/09-Devops/Pipeline-CI-CD.md",
"SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/09-Devops/Supply-Chain-Seguridad.md",
"SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md",
"SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/06-Backlog-Tecnico/Product-Backlog.md",
"SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/09-Devops/Pipeline-CI-CD.md",
"SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/09-Devops/Supply-Chain-Seguridad.md"]
HDR="| Id | Punto abierto | Quién lo cierra | En qué evento se cierra (artefacto y sección) | Estado |"
def fragments(cell):
    c=re.sub(r'\[([^\]]*)\]\([^)]*\)',r'\1',cell)
    vivos=re.sub(r'~~.*?~~','',c); tach=" ".join(re.findall(r'~~(.*?)~~',c))
    out=[]
    for src in (vivos,tach):
        parts=re.split(r'\*\*|`|—|«|»|\(|\)|;|\.|:|\|',src)
        parts=sorted([p.strip() for p in parts if len(p.strip())>=14],key=len,reverse=True)
        out+= [p[:60].strip() for p in parts[:4]]
    return out
def en_linaje(frag,bases):
    r=subprocess.run(["git","log",BASE,"--reverse","--format=@@%h\t%ad\t%s","--date=short","--name-only","-S",frag],capture_output=True,text=True).stdout
    for bloque in r.split("@@")[1:]:
        ls=bloque.strip().split("\n"); cab=ls[0]; fs=ls[1:]
        hit=[f for f in fs if f.split("/")[-1] in bases and "/_legacy/" not in f and "/_fusion/" not in f and CAT in f and any(("/"+p+"/") in f for p in PROYS)]
        if hit:
            h=cab.split("\t")[0]
            return cab,hit[0],preexiste(h,RID)

    return None,None,None
UE_PROY={"GeometriaFactory-Api":["GeometriaFactory-Api","GeometriaFactory-Domain","GeometriaFactory-Application","GeometriaFactory-Infrastructure","GeometriaFactory-Contracts"],
"GeometriaFactory-Web":["GeometriaFactory-Web","GeometriaFactory-Visor","GeometriaFactory-Contracts"]}
CAT="";PROYS=[]

RID=""
def preexiste(h,rid):
    # ¿en el padre del commit ya había, en algún archivo del linaje, una fila de tabla con ese Id?
    files=subprocess.run(["git","ls-tree","-r","--name-only",h+"^"],capture_output=True,text=True).stdout.split("\n")
    lin=[f for f in files if f.split("/")[-1] in BASES and "/_legacy/" not in f and "/_fusion/" not in f and CAT in f and any(("/"+p+"/") in f for p in PROYS)]
    for f in lin:
        c=subprocess.run(["git","show",h+"^:"+f],capture_output=True,text=True).stdout
        if re.search(r"^\|\s*`?"+re.escape(rid)+r"`?\s*\|",c,re.M): return f
    return None
BASES=[]
def derivar(cell,doc,rid=''):
    global CAT,PROYS
    CAT="/"+doc.split("/")[-2]+"/"
    PROYS=UE_PROY["GeometriaFactory-Api" if "/GeometriaFactory-Api/" in doc else "GeometriaFactory-Web"]
    global BASES,RID
    bases=LINAJE[doc.split("/")[-1]]; BASES=bases; RID=rid
    for f in fragments(cell):
        cab,arch,pre=en_linaje(f,bases)
        if cab and pre and rid:
            return dict(frag=f,commit=None,archivo=arch,cmd=f'git log b9675d8 --reverse --name-only -S"{f}"; git show {cab.split(chr(9))[0]}^:{pre}',motivo=f"no derivable — anterior al mecanismo (la fila {rid} ya existía en {pre} antes de {cab.split(chr(9))[0]}: el texto fue reescrito)")
        if cab:
            proy=[p for p in PROYS if "/"+p+"/" in arch]
            return dict(frag=f,commit=cab,archivo=arch,unidad_ciclo=(proy[0] if proy else "?"),cmd=f'git log b9675d8 --reverse --name-only -S"{f}"',motivo="primer alta en el linaje, sin fila previa con el mismo Id")
    return dict(frag=None,commit=None,archivo=None,cmd="(ningún fragmento con alta en el linaje)",motivo="no derivable — anterior al mecanismo")
rows=[]
for d in DOCS:
    L=open(d,encoding='utf-8').read().split("\n"); i=0
    while i<len(L):
        if L[i].strip()==HDR:
            j=i+2
            while j<len(L) and L[j].startswith("|"):
                c=[x.strip() for x in L[j].strip().strip("|").split("|")]
                r=derivar(c[1],d,c[0]); r.update(doc=d,line=j+1,id=c[0],unit="GeometriaFactory-Api" if "/GeometriaFactory-Api/" in d else "GeometriaFactory-Web",superficie="PM-06")
                rows.append(r); j+=1
            i=j
        else: i+=1
# PM-07: PD-VER tablas verticales, fila «Qué falta»
ev="SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/09-Devops/Estrategia-Versionado.md"
L=open(ev,encoding='utf-8').read().split("\n")
for i,l in enumerate(L):
    m=re.match(r'\| Campo \| `(PD-VER-0\d)`',l)
    if m:
        qf=[x for x in L[i+2:i+4] if x.startswith("| Qué falta |")][0]
        cell=qf.split("|")[2]
        r=derivar(cell,ev,m.group(1)); r.update(doc=ev,line=i+1,id=m.group(1),unit="GeometriaFactory-Api",superficie="PM-07"); rows.append(r)
# PM-08: ADR de apartamiento, alta del archivo
for a in ["14001","14002","14003","14004"]:
    f=subprocess.check_output(["bash","-c",f"ls SDD/Docs/Producto/Adrs/ADR-{a}-*.md"]).decode().strip()
    cab=subprocess.check_output(["git","log",BASE,"--reverse","--diff-filter=A","--format=%h\t%ad\t%s","--date=short","--",f]).decode().strip().split("\n")[0]
    rows.append(dict(doc=f,line=0,id=f"ADR-{a}",unit="producto",superficie="PM-08",frag=None,commit=cab or None,archivo=f,cmd=f"git log main --reverse --diff-filter=A -- {f}",motivo="alta del archivo del ADR" if cab else "no derivable — anterior al mecanismo"))
json.dump(rows,open(sys.argv[1],"w"),ensure_ascii=False,indent=1)
from collections import Counter
print("total",len(rows),Counter(r["superficie"] for r in rows))
print("derivados",sum(1 for r in rows if r["commit"]),"no derivables",sum(1 for r in rows if not r["commit"]))
for r in rows:
    if not r["commit"]: print("ND",r["doc"].split("/")[-3:],r["line"],r["id"])
print(Counter(r["commit"].split("\t")[0]+" "+r["commit"].split("\t")[1] for r in rows if r["commit"]).most_common(25))
