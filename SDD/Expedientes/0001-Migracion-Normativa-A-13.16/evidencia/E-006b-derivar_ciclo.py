import os,re,subprocess,json,sys
BASE="main"
DOCS=["SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md",
"SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/06-Backlog-Tecnico/Product-Backlog.md",
"SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/09-Devops/Pipeline-CI-CD.md",
"SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/09-Devops/Supply-Chain-Seguridad.md",
"SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md",
"SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/06-Backlog-Tecnico/Product-Backlog.md",
"SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/09-Devops/Pipeline-CI-CD.md",
"SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/09-Devops/Supply-Chain-Seguridad.md"]
HDR="| Id | Punto abierto | Quién lo cierra | En qué evento se cierra (artefacto y sección) | Estado |"
def clean(s):
    s=re.sub(r'~~.*?~~','',s)            # lo tachado es texto posterior o reemplazado
    s=re.sub(r'\[([^\]]*)\]\([^)]*\)',r'\1',s)
    return s
def fragments(cell):
    c=clean(cell)
    parts=re.split(r'\*\*|`|—|«|»|\(|\)|;|\.|:',c)
    parts=[p.strip() for p in parts if len(p.strip())>=28]
    parts.sort(key=len,reverse=True)
    out=[]
    for p in parts[:4]:
        out.append(p[:60].strip())
    return out
def oldest(frag):
    r=subprocess.run(["git","log",BASE,"--format=%h\t%ad\t%s","--date=short","-S",frag],capture_output=True,text=True).stdout.strip().split("\n")
    r=[x for x in r if x]
    return r
rows=[]
for d in DOCS:
    L=open(d,encoding='utf-8').read().split("\n")
    i=0
    while i<len(L):
        if L[i].strip()==HDR:
            j=i+2
            while j<len(L) and L[j].startswith("|"):
                cells=[c.strip() for c in L[j].strip().strip("|").split("|")]
                rid=cells[0]; frs=fragments(cells[1]); res=None; used=None
                for f in frs:
                    o=oldest(f)
                    if o:
                        res=o[-1]; used=f; n=len(o); break
                unit="GeometriaFactory-Api" if "/GeometriaFactory-Api/" in d else "GeometriaFactory-Web"
                rows.append(dict(doc=d,line=j+1,id=rid,frag=used,commits=(n if res else 0),oldest=res,unit=unit))
                j+=1
            i=j
        else: i+=1
json.dump(rows,open(sys.argv[1],"w"),ensure_ascii=False,indent=1)
tot=len(rows); der=sum(1 for r in rows if r["oldest"])
print("filas",tot,"derivadas",der,"no derivables",tot-der)
from collections import Counter
print(Counter(r["oldest"].split("\t")[0]+" "+r["oldest"].split("\t")[1] for r in rows if r["oldest"]).most_common(20))
for r in rows:
    if not r["oldest"]: print("NO DERIVABLE",r["doc"],r["line"],r["id"])
