# Constancia — revisión externa, R-G2: enlaces externos con par rojo/verde y aviso que se cierra

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-03 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Origen.** La revisión externa de docs-as-code (sobre `main` en a1ff52d) dejó cuatro hallazgos de severidad baja
sobre el carril programado del tramo T-7 (folio 010), `.github/workflows/docs-programado.yml`:

- **B1.** El issue de aviso se buscaba por título (`gh issue list --search "\"$TITULO_ISSUE\" in:title"`, en el
  último paso del workflow en 83daf93): si alguien lo renombra, la corrida siguiente abre otro.
- **B2.** Sólo se avisaban altas: con los enlaces arreglados, el issue #251 no se cerraba solo.
- **B3.** La raíz de la Api (`https://api-geometria.aplicada.stream`) responde 404 y salía cada semana como caída.
- **B4.** La revisión era un guion de Python inline en el YAML, sin autoprueba: el único control sin par rojo/verde.

**Qué se hizo** (alcance R-G2 = B1 + B2 + B4, con B3):

- El guion pasa a `docs-as-code/controles/enlaces_externos.py`, con el mismo ámbito y las mismas reglas (401, 403 y
  429 no son caídos; se ignoran locales, `example.*` y marcadores). Suma `--estricto` (sale 1 con caídos),
  `--excepciones`, `--informe` y `--hoy`. El workflow lo llama sin `--estricto`: sigue avisando.
- `docs-as-code/controles/enlaces-excepciones.txt`: lista versionada `URL | motivo | vence AAAA-MM-DD`. Entra la raíz
  de la Api, con vencimiento el 2027-05-03. Vencida, la excepción no se aplica y se avisa.
- `autoprueba.sh` levanta un servidor local que da 200 en `/ok` y 404 en el resto, y exige: verde con el enlace que
  responde, rojo con el que da 404, verde con una excepción vigente, rojo con una vencida y error de uso con una
  línea sin vencimiento.
- El workflow busca el issue por la etiqueta fija `docs-enlaces` (la crea si falta al abrir uno) y, con `caidos=0`,
  comenta el informe y cierra el issue abierto.

**Qué no se hizo.** No se cambiaron los enlaces de `SDD/Docs/**` a `/salud`: se eleva al orquestador SDD, junto con
`/auth/token` (también 404, sin prefijo `/v1/`) y las direcciones del hosting anterior, que no responden.

**Qué no se tocó.** `src/`, `Contracts/`, `tests/`, `visor/`, `deploy/`, `ai-layer/`, `e2e.yml`,
`deploy-front-ftp.yml`, el expediente 0002 ni `SDD/Docs/**`.

**Efecto sobre el producto.** Ninguno: cambian un workflow programado, un control de docs y su autoprueba. Ninguna
fusión dispara un despliegue (`deploy-front-ftp.yml` es sólo `workflow_dispatch`).

Sigue: reetiquetar el issue #251 con `docs-enlaces` y disparar el carril a mano para ver el comentario; elevar al
orquestador SDD los enlaces caídos de `SDD/Docs` · mantenimiento de docs-as-code (agente)
