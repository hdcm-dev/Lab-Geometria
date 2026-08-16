# Informe de auditoría de migración — 8.6 → 8.11

**Producto:** Fábrica de Geometría
**Documento:** Informe-Migracion-8.6-a-8.11.md
**Versión:** 2.0
**Estado:** Emitido
**Fecha:** 2026-08-16
**Auditor:** Auditor independiente, invocado desde cero, sin participación en la generación ni en la migración
**Instrumento normativo:** `Master-Prompt.md` **7.7** §10, con los catorce criterios de aceptación de `Migracion-Rules.md` **3.4** §6
**Alcance:** la migración normativa 8.6 → 8.11 del destino `Lab-Geometria`, ejecutada el 2026-08-16
**Veredicto:** **APROBADO** — 0 P0, 0 P1, 2 P2, 1 P3 · **ronda 2**, después de la reparación de `N-01` y `N-04`

---

## Tabla de contenido

- [1. Qué se auditó](#1-qué-se-auditó)
- [2. Estado de las fases](#2-estado-de-las-fases)
- [3. Compuerta mecánica](#3-compuerta-mecánica)
- [4. Criterios de aceptación](#4-criterios-de-aceptación)
- [5. Hallazgos P0 propios de la migración](#5-hallazgos-p0-propios-de-la-migración)
- [6. Hallazgos](#6-hallazgos)
- [7. Estado final de cada fila del plan](#7-estado-final-de-cada-fila-del-plan)
- [8. Contenido sin destino](#8-contenido-sin-destino)
- [9. Migración completa o parcial](#9-migración-completa-o-parcial)
- [10. Veredicto](#10-veredicto)
- [11. Control de cambios](#11-control-de-cambios)

---

## 1. Qué se auditó

La migración 8.6 → 8.11, invocada desde la **salida B** del orquestador de reanudación con la
decisión del Product Owner del 2026-08-16, registrada en
[`Estado-Del-Destino-2026-08-16.md`](Estado-Del-Destino-2026-08-16.md) §5.

**Es una migración atípica y conviene decirlo antes que nada: su alcance documental es cero.** Ninguna
de las catorce reglas de categoría ni de las cuatro transversales cambió de versión entre 8.6 y 8.11,
de modo que ningún documento del árbol requería migración. Lo que la corrida hizo, en los hechos, fue
**verificar que eso era cierto y cerrar la procedencia**, más la reparación de ocho punteros colgados
que la verificación encontró de paso.

**Esa atipicidad es lo que esta auditoría vigila con más cuidado.** Una migración sin trabajo
documental es exactamente la forma que tendría una migración que no se hizo, y la diferencia entre
las dos **no está en el resultado sino en la verificación**: si el diff se hizo artefacto por artefacto
o si alguien miró el número de conjunto y supuso.

Insumos leídos: [`Plan-Migracion-8.6-a-8.11.md`](Plan-Migracion-8.6-a-8.11.md) 1.0,
[`Estado-Del-Destino-2026-08-16.md`](Estado-Del-Destino-2026-08-16.md) 1.0, el `CHANGELOG.md` del
framework en sus entradas 8.7 a 8.11, las versiones vivas de los veintidós artefactos del framework,
[`../../Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md`](../../Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md)
2.2 y [`../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md)
2.0, y el árbol vivo de `SDD/Docs/`.

**Todas las cifras de este informe se contaron sobre el instrumento**, no se heredaron del plan.

---

## 2. Estado de las fases

| Fase | Qué correspondía | Qué se hizo | Verificación del auditor |
| --- | --- | --- | --- |
| **M0** | Reconocer el destino | Intake y manifiesto resueltos con nombre vigente; procedencia 8.6; conjunto de origen disponible en `_legacy/8.6/`; 450 documentos vivos | **Cumple.** Los dos artefactos existen con su nombre vigente; `_legacy/8.6/` existe en el framework; el recuento de 450 se reprodujo con `find` |
| **M1** | Diff normativo | Verificado desde el informe de reanudación, no reconstruido, según `Master-Prompt-Migracion.md` 2.3 §5. Plan emitido y aprobado por el Product Owner | **Cumple.** Ver §3 |
| **M2** | Migración del intake | **Sin cambios.** El intake ya cumple la plantilla 3.1. No se escribió | **Cumple.** Verificado sobre §17.1, §17.2 y §13.2 del intake. El intake sigue en **2.0**, sin fila nueva de control de cambios, que es lo correcto: no se lo tocó |
| **M3** | Re-derivación del manifiesto | **No correspondía**: el intake no cambió | **Cumple.** Ninguna tabla de §2 a §5 del manifiesto cambió, verificado |
| **M4** | Migración de `SDD/Docs/` | **Sin trabajo.** 450 documentos `no tocar` | **Cumple con una salvedad de método**, en el hallazgo `N-02` |
| **M5** | Cierre de procedencia | Verificación de cadena completa, reparación de 8 punteros, y reescritura de §1.1 de 8.6 a 8.11 | **Cumple.** Ver §3 y §4 |
| **M6** | Esta auditoría | — | — |

**Sobre la detención de M5, que es lo que esta auditoría más valora de la corrida.** La verificación
mecánica encontró ocho enlaces rotos en documentos vivos **que contradecían el cierre de procedencia
anterior** —la M5 de la migración 6.0 → 8.6 declaró «0 enlaces rotos»—, y la corrida **se detuvo y lo
presentó** en lugar de escribir la procedencia sobre un árbol que no sostenía la afirmación. Es el
comportamiento que §9 del orquestador exige y que las seis rondas de la auditoría anterior tuvieron
que aprender: **declarar «COMPLETA» tres veces antes de que lo fuera** es el antecedente directo.

---

## 3. Compuerta mecánica

Corrida por el auditor sobre el árbol vivo, sin heredar ninguna cifra.

| Comprobación | Resultado |
| --- | --- |
| Enlaces relativos que resuelven, en documentos vivos | **4654 de 4658** (ronda 1: 4640 de 4644) |
| Enlaces rotos | **4**, los cuatro en `Audit/` — fuera de alcance (§6, `N-03`) |
| Enlaces rotos en documentos **no** `Audit/` | **0** |
| Documentos vivos en `SDD/Docs/` | **450** |
| Carpetas `_fusion/` | **0** — la fusión sigue cerrada |
| Filas del plan sin resolver | **0 de 450** |
| Secciones pendientes sin respuesta del humano | **0** |
| Documentos clasificados `regenerar` o `revisar` sin tocar | **0** — no hay ninguno de esa clase |

### 3.1 Verificación independiente del diff normativo

El criterio de `Master-Prompt.md` §10 dice que **un recuento que confirma una propiedad no confirma
las demás**, y que usarlo para afirmar lo que no decide es P1. Por eso el auditor **no dio por buena**
la tabla del plan: releyó la versión declarada por cada archivo del framework y la comparó con la
fila de la procedencia 8.6.

| Grupo | Artefactos | Coinciden con lo que el plan declara |
| --- | --- | --- |
| Reglas de categoría | 11 `Rules-*` más `Root-Rules` | **12 de 12**, ninguna cambió de versión |
| Reglas transversales aplicadas | `Intake-Rules`, `Vocabulario-Rules`, `Maqueta-Rules`, `Deriva-Rules` | **4 de 4**, ninguna cambió |
| Regla de migración | `Migracion-Rules` 3.2 → **3.4** | Coincide |
| Plantillas | `PRODUCT-INTAKE-template` 3.0 → **3.1**; `PRODUCT-MANIFEST-template` 5.0 sin cambio | Coincide |
| Orquestadores | `Master-Prompt` 7.4 → **7.7**; `Master-Prompt-Migracion` 2.0 → **2.3**; `Master-Prompt-Reanudacion` **1.1** nuevo | Coincide |

**Renombres de artefacto: cero, verificado por lectura y no por ausencia de noticia.** El bloque
«Impacto sobre destinos existentes» que `SDD-Development-Guide.md` §VI.4 exige en toda entrada
**major** aparece en 7.0, 6.0, 5.1 y 4.0 del `CHANGELOG`. Las cinco entradas del salto son **minor** y
ninguna lo lleva. La conclusión del plan se sostiene.

**El punto de contacto de la 8.7, comprobado a mano.** `PRODUCT-INTAKE-template` 3.1 exige dos tablas
de identidad con `tipo_unidad_entrega` y `redistribuible` **en la unidad de entrega**. El intake de
este destino las tiene así en §17.1 y §17.2, con la constancia explícita de por qué el proyecto de
código no los lleva, y §13.2 lo declara para todo el documento. **No requiere migración**, y la
afirmación del plan es verificable y no una conveniencia.

---

## 4. Criterios de aceptación

Los catorce de `Migracion-Rules.md` §6, en lo que aplican a un salto de alcance documental cero.

| # | Criterio | Resultado |
| --- | --- | --- |
| 1 | El plan existe y corresponde al par de versiones que M0 resolvió | **Cumple.** 8.6 → 8.11 |
| 2 | Cada documento del plan lleva su clasificación y su fuente de contenido | **Cumple con salvedad declarada.** La tabla es por categoría con recuento y no por documento; el apartamiento **está declarado en §4 del plan con su motivo**, no ejercido en silencio. Ver `N-02` |
| 3 | Ningún documento migrado contiene contenido sin fuente | **No aplica**: cero documentos migrados |
| 4 | Ninguna sección exigida quedó rellenada con contenido inferido | **Cumple.** Cero secciones tocadas |
| 5 | El estado previo de todo documento reescrito quedó archivado | **Cumple con declaración.** Los tres documentos tocados —`SDD/Docs/README.md`, el README de `10-Examples` de `Web` y el manifiesto— **no se archivaron**, y los tres lo declaran en su fila de control de cambios con el mismo fundamento: reconexión de punteros por `Migracion-Rules.md` §4.3.1 y cierre de procedencia, ninguno de los dos una reemisión de cuerpo. El criterio se cumple **por declaración y no por omisión**, que es la diferencia que importa |
| 6 | La procedencia no se reescribió con migración parcial | **Cumple.** Ver §9 |
| 7 | Ninguna corrección manual del usuario fue pisada | **Cumple.** El árbol no tenía trabajo en curso; `git status` limpio antes de la corrida |
| 8 | Ninguna fila del plan quedó sin resolver ni sin declararse | **Cumple.** 450 de 450 |
| 9 | La distinción entre «ningún documento se sobrescribió» y «la fusión terminó» está resuelta | **Cumple.** 0 carpetas `_fusion/`, verificado por el auditor y no heredado |
| 10 | La reconexión se hizo resolviendo destinos y no sustituyendo patrones | **Cumple.** Las ocho reparaciones se resolvieron una por una contra el árbol vivo; el control posterior no encontró daño colateral —4644 enlaces contra 4649 previos, la diferencia son los cinco enlaces que la reparación retiró al colapsar siete filas en dos— |
| 11 | Las citas ambiguas se declararon después de agotar los resolutores | **No aplica**: sin renumeración en este salto |
| 12 | El árbol de renumeración alcanza las familias acuñadas por el destino | **No aplica**: sin renumeración |
| 13 | El solapamiento se midió antes de elegir salida de consolidación | **No aplica**: sin consolidación |
| 14 | Toda marca de una comprobación se abrió antes de reportarla | **Cumple.** Las 12 marcas del control de enlaces se abrieron una por una: 8 eran defectos reales y 4 son `Audit/`, de las cuales 2 son literales de ejemplo y no enlaces. **Ninguna se contó sin mirar**, que es el criterio que la corrida anterior incorporó al framework después de que su verificador sobre-reportara cuatro de cinco veces |

---

## 5. Hallazgos P0 propios de la migración

Los seis que `Master-Prompt-Migracion.md` §10 enumera:

| P0 posible | Resultado |
| --- | --- |
| Contenido inventado en un documento migrado | **No.** Cero documentos migrados |
| Sección exigida rellenada con contenido inferido | **No** |
| Procedencia reescrita con migración parcial | **No.** §9 |
| Corrección manual pisada sin declarar | **No** |
| Estado previo sin archivar | **No como P0**: los tres documentos tocados lo declaran con fundamento; ver criterio 5 |
| Fila del plan sin resolver y sin declarar | **No.** 450 de 450 |

**Cero P0.**

---

## 6. Hallazgos

### N-01 · P2 · propio · por guion — El README raíz del corpus quedó fuera de la migración anterior y conserva contenido del modelo de siete proyectos de código · **CERRADO**

**Qué se encontró.** [`../README.md`](../README.md), README raíz de `SDD/Docs/`, tenía **siete
enlaces** en §4 y **tres rutas** en §8 apuntando a `Proyectos/<nombre>/`, un árbol que la migración
6.0 → 8.6 reemplazó por `Unidades-Entrega/` al mover el nivel de aplicación (framework 8.0). Los diez
estaban colgados.

**Qué se reparó en esta corrida.** Los diez punteros, con el documento subiendo a **1.5** y con su
fila de control de cambios. La verificación posterior da **0 enlaces rotos** fuera de `Audit/`.

**CERRADO en la ronda 2.** El Product Owner delegó la decisión, y la salida elegida fue **reemitir**
—no declararlo superado como `Handoff-Checkout.md`—, con este fundamento: un `Handoff-Checkout` es un
**inventario fechado** que `Master-Prompt.md` §12 exige antes del traspaso, y como tal **vale como
registro de lo que se entregó ese día**; un README raíz **no es un registro sino un índice**, y un
índice viejo no tiene valor residual que preservar. Declararlo superado habría dejado al corpus sin
puerta de entrada.

`README.md` pasa a **1.6**: §2 se rehace sobre los dos ejes —§2.1 las dos unidades de entrega con su
D8, §2.2 los siete proyectos de código sin él—, §7 se parte en tres y suma **7.2, el estado de
construcción por etapa**, y §7.3 declara que el documento **deja de replicar magnitudes** y remite a
quien las cuenta. La cabecera pasa a citar el manifiesto **2.2**.

**Lo que la ronda 1 dejó abierto, y que la 1.6 corrige una por una:**

| Dónde | Qué afirma | Qué es cierto hoy |
| --- | --- | --- |
| Cabecera y §2 | Refleja el `PRODUCT-MANIFEST` **1.3** | El vigente es **2.2** |
| §2 | Tabla con `Tipo D8` y `Redistribuible` **por proyecto de código** | Los proyectos de código **no llevan D8**: es atributo de la unidad de entrega |
| §7 | «El producto está **especificado y todavía no construido**» | El código está en la etapa **`e`** |
| §7 | Magnitudes: 71 casos de uso, 16 reglas de negocio | Anteriores a la consolidación; la unidad `Api` tiene **nueve** casos de uso y la `Web` **diez** |

**Por qué se reemitió entero y no a medias.** Es la lección del hallazgo **M-08** de la migración
anterior: un documento cuyos recuentos también están viejos **no se migra a medias**, porque quedaría
afirmando cosas que nunca fueron ciertas. La 1.5 había reconectado punteros **declarando** que el
contenido seguía viejo; la 1.6 lo corrige, y por eso el hallazgo cierra.

**Y una salida que la 1.6 toma y conviene registrar**: §7.3 **deja de replicar las magnitudes** en
lugar de actualizarlas. Actualizarlas las habría dejado correctas hoy y viejas la próxima vez que
alguien contara; no replicarlas quita la fuente del defecto. Es la aplicación de «contar sobre el
instrumento» al documento que más lo incumplía.

### N-02 · P2 · propio · sólo por lectura — El plan clasificó por categoría y no por documento

**Qué se encontró.** `Master-Prompt-Migracion.md` §5 pide **una fila por documento** en la tabla del
plan. El plan emitió **23 filas por categoría con su recuento**, sumando 450.

**Por qué no es P1.** El apartamiento **está declarado en §4 del plan, con su motivo**, y no ejercido
en silencio. Con salto de regla cero la clasificación es idéntica para los 450 por construcción, y el
recuento por categoría es reproducible con herramienta —el auditor lo reprodujo—, cosa que 450 filas
idénticas no harían mejor.

**Por qué se registra igual.** Un apartamiento declarado sigue siendo un apartamiento, y la próxima
migración de este destino **no debe tomarlo como precedente**: es válido porque el salto no clasifica
distinto a ningún documento, y deja de serlo en cuanto uno solo caiga en `regenerar` o `revisar`.

### N-03 · P2 · propio · por guion — Cuatro enlaces rotos en `Audit/`, dos de ellos ya declarados

**Qué se encontró.** [`E-08-Calidad-Siete-Proyectos-r2.md`](E-08-Calidad-Siete-Proyectos-r2.md)
enlaza a `Estrategia-Testing.md` y `Casos-Prueba-Referenciales.md` sin ruta resoluble —son los **dos
enlaces por nombre ambiguo** que el hallazgo **M-06** de la migración anterior ya dejó abierto—, y hay
dos literales de ejemplo (`ruta`, `destino`) escritos con sintaxis de enlace en ese mismo informe y en
[`Informe-Migracion-6.0-a-8.6.md`](Informe-Migracion-6.0-a-8.6.md).

**Por qué no se repara.** `Audit/` está fuera de alcance: son registros fechados de lo que se verificó
en su fecha, y reescribirlos falsea el registro. **M-06 sigue siendo el hallazgo titular**; éste sólo
lo confirma con la medición de hoy y suma los dos literales, que no son defecto sino sintaxis.

### N-04 · P3 · propio · por guion — Referencias que nombran el árbol anterior con el destino ya reconectado · **CERRADO, con cuatro citas que pasan a `N-05`**

> **Corrección de la cifra de la ronda 1, y de cómo se produjo.** La ronda 1 declaró **«105
> ocurrencias en 50 documentos»**. **Es falso.** La cifra salió de un `grep` cuyo filtro de exclusión
> de `Audit/` **no aplicó** —el patrón esperaba el prefijo `./` que esa invocación no emitía—, de modo
> que contó como documentos vivos los informes de auditoría, que están fuera de alcance. El recuento
> correcto, hecho recorriendo el árbol y excluyendo `_legacy/` y `Audit/`, es **55 ocurrencias en 29
> documentos**.
>
> **Es exactamente el defecto que este corpus ya tiene escrito como criterio**: `Master-Prompt.md`
> §10 exige **contar sobre el instrumento** y no heredar la cifra, y la ronda 1 heredó la suya de una
> herramienta cuyo resultado no verificó. Se declara acá en lugar de corregirse en silencio, porque
> una cifra publicada y después cambiada sin explicación es peor que la cifra equivocada.

**Qué se encontró.** **55 ocurrencias en 29 documentos vivos** de la cadena
`Proyectos/GeometriaFactory`, de las cuales **22 están dentro de la etiqueta de un enlace cuyo
destino sí resuelve**. La forma es:

```markdown
[`../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md`](../Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md)
```

**Es exactamente el error que la migración anterior aprendió y escribió**: `Migracion-Rules.md`
§4.3.1 lo registra como «M4 con la etiqueta y el destino», la primera de las tres veces que esa
corrida tropezó con lo mismo. El destino se reconectó y **la etiqueta quedó nombrando la carpeta que
ya no existe**.

**Por qué P3 y no P2.** Ningún enlace está roto y ninguna afirmación normativa depende de la etiqueta:
el daño es que **el lector ve una ruta que no existe** y puede deducir de ahí una estructura de árbol
equivocada. No compromete ninguna cadena de trazabilidad.

**Cómo se reparó, en la ronda 2.** Una pasada **resolviendo por destino y no sustituyendo patrones**,
que es la lección que `Migracion-Rules.md` §4.3.1 dejó escrita después de que un patrón rompiera 181
enlaces donde había 96:

| Caso | Regla aplicada | Resultado |
| --- | --- | --- |
| Ocurrencia **dentro de la etiqueta** de un enlace | La etiqueta pasa a decir lo que dice el destino, que ya estaba reconectado | Reconectadas |
| Ocurrencia en **texto**, con ruta que existe bajo la unidad de entrega | Se mapea el proyecto de código a la unidad que compone, **y se comprueba que el archivo exista** antes de escribir | Reconectadas |
| Ocurrencia en texto cuyo **destino no existe bajo ningún nombre** | **No se toca.** Ver `N-05` | 4, intactas |
| `Handoff-Checkout.md`, 11 ocurrencias | Fuera de alcance: documento declarado superado por `M-08` | Intactas, por decisión registrada |

**41 referencias reconectadas en 27 documentos**, con su registro en
[`Migracion-8.11-Registro-Reconexion-Etiquetas.json`](Migracion-8.11-Registro-Reconexion-Etiquetas.json),
que lleva el par origen–destino de cada una y la lista de las que no se tocaron con su motivo. La
comprobación posterior da **0 enlaces rotos fuera de `Audit/`** y **4658 enlaces verificados**, contra
4644 antes: **ninguno se rompió al reparar**, que es el control que la lección de la regla exige.

### N-05 · P2 · propio · sólo por lectura — Cuatro citas apuntan a documentos que la consolidación absorbió

**Qué se encontró.** Cuatro referencias de la unidad `GeometriaFactory-Api`, todas en la línea de
trazabilidad upstream de un documento de `05-Arquitectura-Tecnica/Operaciones-Internas/`, citan
documentos que **no existen bajo ningún nombre en el árbol vigente**: los casos de uso de la capa de
contratos y de infraestructura que la consolidación de la migración anterior absorbió al llevar los
32 casos de uso de la unidad a **nueve**.

| Documento que cita | Cita colgada |
| --- | --- |
| `CU-00009-Traducir-El-Motivo-Del-Contrato-A-Respuesta-De-Protocolo.md` | `…/Contracts/…/CU-00006-Contrato-De-Respuesta-De-Error.md` |
| `CU-00011-Arrancar-El-Servicio-Y-Dejar-El-Almacen-En-Condiciones.md` | `…/Infrastructure/…/CU-00010-Preparar-El-Almacen-Al-Arrancar.md` |
| `CU-06007-Producir-La-Contrasena-Provisoria-Del-Reseteo.md` | `…/Contracts/…/CU-06008-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md` |
| `CU-06008-Emitir-El-Acceso-Firmado.md` | `…/Contracts/…/CU-06001-Contrato-De-Canje-De-Credenciales-Y-Sesion.md` |

**Por qué no se repararon, y por qué eso es la decisión correcta.** Reescribirles la carpeta las
dejaría apuntando a `Unidades-Entrega/GeometriaFactory-Api/…` a un archivo **que tampoco existe ahí**:
convertiría un error visible —una carpeta que el lector reconoce como vieja— en uno invisible, con
forma de referencia sana. Resolverlas de verdad exige el **mapa de la consolidación** de
[`Migracion-8.5-Consolidacion-Decidida.md`](Migracion-8.5-Consolidacion-Decidida.md) §3, que dice qué
capacidad absorbió cada caso de uso, y **una de las cuatro no figura en ese mapa**.

**Se separa de `N-04` porque no es la misma clase de defecto.** `N-04` era una etiqueta vieja sobre un
destino sano; esto es una **cita a un documento absorbido**, que es la familia de `M-05` y `M-08` de
la migración anterior. Sube a **P2** por eso: no es cosmético, es una cadena de trazabilidad que no
cierra.

**Qué haría falta.** Una pasada de cuatro citas contra el mapa de consolidación, con la que no
resuelva **declarada ambigua después de agotar los resolutores** y no antes, que es la regla que la
corrida anterior incorporó al framework.

### Observación · fuera del alcance de la migración — La divergencia `D-01`, **reparada en la ronda 2**

El registro de cambios del producto declaraba la etapa **`b`** con el código en la **`e`**, según
[`Estado-Del-Destino-2026-08-16.md`](Estado-Del-Destino-2026-08-16.md) §2.

**No es hallazgo de esta migración** y no se computa: una migración normativa no toca el código ni su
registro, y el plan lo declaró fuera de alcance en §6. Se consigna porque el informe de estado la
declaró abierta.

**Reparada el 2026-08-16, fuera de la migración y con su propia declaración.** `changelog.md` suma las
entradas de las etapas **`c`, `d` y `e`**, escritas **desde los commits y desde el código** —no desde
la memoria de las sesiones que las construyeron— y **marcadas como repuestas después de la fusión**,
con un bloque de cabecera que declara qué regla se incumplió, cuántas veces, quién lo encontró y con
qué contraste. **No se reescribió ningún commit.**

**Por qué el marcado importa más que la reposición.** Un registro que se completa tarde y se presenta
como si se hubiera escrito a tiempo **queda diciendo la verdad sobre el avance y mintiendo sobre sí
mismo**, y es la segunda propiedad la que hace que el documento sirva para retomar. El bloque de
cabecera es lo que impide esa lectura.

---

## 7. Estado final de cada fila del plan

| Grupo de filas | Filas | Clasificación | Estado final |
| --- | --- | --- | --- |
| Nivel producto: `00-Contexto`, `01-Necesidades-Negocio`, `Producto/`, `Audit/`, raíz | 85 | `no tocar` | **Resueltas.** Evaluadas, sin cambio requerido |
| `GeometriaFactory-Api`, nueve categorías | 242 | `no tocar` | **Resueltas** |
| `GeometriaFactory-Web`, nueve categorías | 123 | `no tocar` | **Resueltas** |
| **Total** | **450** | | **450 resueltas, 0 pendientes** |

**Tres documentos se tocaron fuera de la clasificación `no tocar`, y los tres están declarados:**

| Documento | Qué se hizo | Dónde está declarado |
| --- | --- | --- |
| [`../README.md`](../README.md) 1.4 → 1.5 → **1.6** | 1.5 reconectó 10 punteros; **1.6 lo reemite** sobre los dos ejes y cierra `N-01` | Sus filas 1.5 y 1.6, y el hallazgo `N-01` |
| [`../Unidades-Entrega/GeometriaFactory-Web/10-Examples/README.md`](../Unidades-Entrega/GeometriaFactory-Web/10-Examples/README.md) 2.0 → **2.1** | Reconexión de 1 puntero al árbol del `Visor`, absorbido por M-10 | Su fila 2.1 |
| [`../../Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md`](../../Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md) 2.1 → **2.2** | Cierre de procedencia, fase M5 | Su fila 2.2 |
| **27 documentos** de nivel producto y de las dos unidades | Reconexión de **41 referencias** al árbol de unidades de entrega, sin cambio de cuerpo | [`Migracion-8.11-Registro-Reconexion-Etiquetas.json`](Migracion-8.11-Registro-Reconexion-Etiquetas.json) y el hallazgo `N-04` |

**Sólo uno de los cuatro grupos es una reescritura de contenido**: la reemisión del README (1.6), que
cierra un hallazgo de este informe. Los otros tres son reconexión de punteros —que
`Migracion-Rules.md` §4.3.1 declara que no modifica el cuerpo— y la fase M5, cuya escritura es el
objeto de esta corrida.

**Las 41 reconexiones no llevan fila de control de cambios documento por documento, y se declara.**
Llevan **registro JSON con el par origen–destino de cada una**, que es el instrumento con el que la
migración anterior registró sus 442 reconexiones. Veintisiete filas que dijeran todas lo mismo no
harían la reconexión más verificable; el registro sí, porque se puede recorrer contra el árbol.

---

## 8. Contenido sin destino

**Ninguno.** Cero documentos migrados implica cero contenido que la normativa vigente no ubique. No es
una omisión del informe: es la consecuencia de que ninguna regla cambiara de forma.

---

## 9. Migración completa o parcial

**COMPLETA Y CERRADA.**

| Condición de `Migracion-Rules.md` §4.6 | Estado |
| --- | --- |
| Ninguna fila del plan sin resolver | **0 de 450** |
| Ninguna sección pendiente sin respuesta del humano | **0** |
| Ningún documento `regenerar` o `revisar` sin tocar | **0**, no hay ninguno de esa clase |
| Ningún documento quedó sin migrar | **Ninguno lo requería** |

**La procedencia se reescribió, y le corresponde.** No por el número de conjunto —que es el
anti-patrón que `Master-Prompt-Reanudacion.md` §7 nombra— sino porque se verificó **artefacto por
artefacto** que el salto no obliga a reemitir nada, con la verificación escrita **antes** de tocar la
tabla, en `Plan-Migracion-8.6-a-8.11.md` §2, §3 y §5 y re-verificada de forma independiente en §3.1
de este informe.

**Una salvedad que este informe deja escrita, porque el veredicto sin ella se lee mal.** Que la
migración esté completa **no significa que el corpus esté al día**: `N-01` y `N-04` son residuo de la
migración **anterior** que el árbol sigue llevando, y ninguno de los dos es responsabilidad de este
salto. La distinción entre «este salto se hizo bien» y «el árbol no tiene deuda» es la misma que la
auditoría anterior confundió durante tres rondas, y por eso se enuncia en lugar de suponerse.

---

## 10. Veredicto

**APROBADO.** 0 P0, 0 P1, **2 P2 abiertos**, 0 P3 abiertos.

| Nivel | Abiertos | Cerrados en la ronda 2 |
| --- | --- | --- |
| P0 | — | — |
| P1 | — | — |
| P2 | `N-02` clasificación por categoría, con apartamiento declarado · `N-03` cuatro enlaces rotos en `Audit/`, dos ya titulados por `M-06` · **`N-05` cuatro citas a documentos absorbidos** | **`N-01`**, por la reemisión del README a 1.6 |
| P3 | — | **`N-04`**, por las 41 reconexiones, con las 4 citas que no resolvían promovidas a `N-05` |

Fuera de cómputo: la divergencia **`D-01`**, ajena a esta migración, **reparada** con la reposición de
las etapas `c`, `d` y `e` en `changelog.md`.

**Ningún P0 ni P1 abierto, de modo que la migración no está bloqueada y se declara cerrada.**

**Los dos P2 que quedan no son deuda de este salto**: `N-02` es un apartamiento de método declarado en
el plan, y `N-03` y `N-05` son residuo de la migración anterior que este informe deja localizado, con
su instrumento de resolución nombrado.

**Lo que esta auditoría quiere dejar dicho sobre la corrida.** El riesgo de una migración de alcance
cero es que se declare por el número y no por la verificación. Acá pasó lo contrario en el punto que
más importaba: la corrida **se detuvo antes de escribir la procedencia** porque su propia
comprobación encontró ocho enlaces rotos que contradecían el cierre anterior, y presentó la
contradicción en vez de escribir igual. Esa detención es lo que hace que la tabla de §1.1 del
manifiesto sea hoy una afirmación verificada, y no una repetición del optimismo de la corrida previa.

**Lo que la ronda 2 resolvió, y con qué criterio.** El Product Owner delegó las tres decisiones que la
ronda 1 le dejaba, y se tomaron así:

1. **`N-01`, el README raíz: reemitir.** Un `Handoff-Checkout` es un inventario fechado y vale como
   registro; un README es un índice, y un índice viejo no tiene valor residual que preservar.
2. **`N-04`, las referencias al árbol anterior: reparar las que resuelven, y sólo esas.** 41
   reconectadas por destino, 4 intactas porque su documento no existe bajo ningún nombre —promovidas
   a `N-05`— y 11 intactas en `Handoff-Checkout.md`, por su decisión ya registrada.
3. **`D-01`, el registro de cambios: reponer y marcar la reposición.** Las tres etapas escritas desde
   los commits, con el bloque que declara que se escribieron tarde.

**Lo que queda abierto, con su instrumento nombrado:** `N-05` necesita el mapa de consolidación de
`Migracion-8.5-Consolidacion-Decidida.md` §3; `N-03` está fuera de alcance por ser `Audit/`; y `N-02`
es un apartamiento de método que no requiere trabajo, sino no tomarse como precedente.

---

## 11. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 2.0 | 2026-08-16 | **Ronda 2**, después de que las tres decisiones que la ronda 1 dejaba al Product Owner se resolvieran y se ejecutaran. **`N-01` cierra** con la **reemisión** del README raíz a **1.6** —§2 sobre los dos ejes, §7 partido en tres con el estado de construcción por etapa, y §7.3 que deja de replicar magnitudes—, elegida sobre declararlo superado porque **un índice viejo no tiene el valor de registro que sí tiene un inventario fechado**. **`N-04` cierra** con **41 referencias reconectadas en 27 documentos, resolviendo por destino y no por patrón**, con su registro JSON; las **4 citas que no resolvían se promueven a `N-05` (P2)**, porque apuntan a documentos que la consolidación absorbió y reescribirles la carpeta habría convertido un error visible en uno invisible. **La divergencia `D-01` queda reparada** fuera del alcance de la migración: `changelog.md` suma las etapas `c`, `d` y `e` escritas desde los commits y **marcadas como repuestas después de la fusión**, sin reescribir ningún commit. **§6 corrige la cifra de `N-04` de la ronda 1**: decía 105 ocurrencias en 50 documentos y son **55 en 29**, porque el filtro de exclusión de `Audit/` de aquel recuento no aplicó —el mismo defecto de heredar una cifra sin verificarla que §10 del `Master-Prompt` tipifica—, y se declara en lugar de corregirse en silencio. Compuerta: **4654 de 4658 enlaces resuelven**, 0 rotos fuera de `Audit/`, **ninguno roto al reparar**. Veredicto **APROBADO**, ahora con **2 P2 abiertos y ningún P3**. | Auditor independiente |
| 1.0 | 2026-08-16 | Emisión inicial. Auditoría de la migración normativa **8.6 → 8.11**, de alcance documental cero: ninguna de las catorce reglas de categoría ni de las cuatro transversales cambió de versión, **450 documentos clasificados `no tocar` y 450 resueltos**. El diff normativo se **re-verificó de forma independiente artefacto por artefacto** (§3.1) en lugar de heredarse del plan, y los renombres de artefacto se comprobaron por lectura del `CHANGELOG` y no por ausencia de noticia: **cero**. Compuerta mecánica sobre el árbol vivo: **4640 de 4644 enlaces resuelven**, los 4 rotos en `Audit/`, fuera de alcance. **Cero P0 y cero P1.** Cuatro hallazgos: `N-01` (P2) el README raíz conserva el modelo de siete proyectos de código, con sus diez punteros ya reparados y su contenido no reemitido; `N-02` (P2) el plan clasificó por categoría con apartamiento declarado; `N-03` (P2) cuatro enlaces rotos en `Audit/`, dos ya titulados por `M-06`; `N-04` (P3) 105 etiquetas de enlace en 50 documentos nombrando el árbol anterior con el destino reconectado. Se consigna fuera de cómputo la divergencia `D-01` del registro de cambios, abierta y ajena a este salto. Veredicto **APROBADO**, migración **COMPLETA Y CERRADA**. | Auditor independiente |
