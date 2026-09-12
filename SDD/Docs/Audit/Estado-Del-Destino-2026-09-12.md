# Estado del destino — Fábrica de Geometría · 2026-09-12

**Documento:** `Estado-Del-Destino-2026-09-12.md`
**Versión:** 2.0
**Estado:** Emitido
**Fecha:** 2026-09-12
**Autor:** Orquestador de reanudación SDD (`Master-Prompt-Reanudacion.md` **1.11**)
**Revisión leída:** `5c95dab` de `main`, árbol de trabajo limpio
**Trazabilidad upstream:** el árbol del destino y `IA.SDD` **13.10** en solo lectura
**Trazabilidad downstream:** [`Mesa-2026-09-12.md`](Mesa-2026-09-12.md); la salida que el Product Owner elija en §8

---

## 1. Qué es este documento

El estado del destino reconstruido **desde el árbol, sin memoria de sesiones anteriores**, con sus
divergencias declaradas, el resultado de la mesa de evaluación, la recomendación con su fundamento, la
decisión del Product Owner y el punto de continuación.

**Es la sexta reanudación de este destino.** La anterior, [`Estado-Del-Destino-2026-08-27.md`](Estado-Del-Destino-2026-08-27.md),
eligió `A` y después `C` con `D` a continuación, y dejó su punto de continuación en la **fase `i`**.
Este documento arranca ahí y encuentra que esa fase **está abierta y su entregable cambió**.

---

## 2. Paso 0 de R0 — la compuerta de arranque (`Master-Prompt.md` §12.1 T0)

```text
COMPUERTA DE ARRANQUE — hdcm-dev/Lab-Geometria
  Rama:            main   al día (0 detrás, 0 adelante)
  Árbol:           limpio (0 cambios, 0 borrados, 0 sin seguir)
  Entregas vivas:  ninguna (0 ramas remotas sin fusionar)
  Ramas a borrar:  ninguna
  Veredicto:       EN ORDEN, se puede empezar
```

**Se publica aunque esté todo en orden**, que es lo que distingue «no había nada que arreglar» de «no
se miró».

---

## 3. Las seis dimensiones (`Master-Prompt-Reanudacion.md` §1)

| # | Dimensión | Fuente declarativa | Lectura | Contraste observable | Resultado |
| --- | --- | --- | --- | --- | --- |
| 1 | ¿Hay documentación generada? | — | — | `SDD/Docs` con **506** documentos vivos fuera de `_legacy` | **Sí** |
| 2 | ¿Contra qué versión del framework? | `PRODUCT-MANIFEST` **6.0** §1.1 | SDD **13.7**, actualizada sin migrar el 2026-08-27 | `IA.SDD/CHANGELOG.md`: vigente **13.10** | **Desfasado en tres minor**, ninguno con impacto. Ver §6 |
| 3 | ¿La migración terminó? | `Informe-Migracion-10.0-a-13.3.md` | **APROBADO CON HALLAZGOS**, segunda ronda, 2026-08-25 | **0** carpetas `_fusion/` | **Sí**, coincide |
| 4 | ¿Qué quedó abierto? | `Reporte-Hallazgos-De-Los-Samples-2026-08-30.md` **11.0** §0 | 14 emitidos, 12 cerrados, 2 retirados, **cero vivos** | 10 enlaces no resuelven sobre 5.788, todos en `Audit/` y seis son literales de plantilla | **Coincide**, con un `P3` nuevo |
| 5 | ¿En qué etapa de construcción va? | `changelog.md` | Última entrada 2026-09-12; etapa `h` cerrada; l.894 «hasta la fase `i` no hubo despliegue real ni release público» | `git log`: PR **#186** (2026-09-06) sin entrada; el producto **corre en producción** desde ese día | **DIVERGEN** — `DIV-01`, `DIV-02` |
| 6 | ¿Qué falta para la siguiente? | `Roadmap-Producto.md` **1.9** §2.1 y §5.2 | Fase `i`, entregable «front publicado por FTP en el hosting, servicio de datos en el servidor propio», `PT-05` desde la facultad | La topología real es otra; `Medicion-PT-05.md` sigue en `SIN MEDIR` | **DIVERGEN** — `DIV-03`, `DIV-04` |

**La dimensión 5 y la 6 divergieron juntas, y es la misma causa.** No es que el código se haya
adelantado al registro: es que **el entregable de una etapa abierta cambió por decisión del Product
Owner y ningún documento del corpus lo absorbió**.

---

## 4. Ítems diferidos (`Root-Rules.md` §12.2)

**Contados sobre el instrumento, fila por fila**, en los ocho documentos que los llevan.

| Estado | Cantidad | Qué significa |
| --- | --- | --- |
| Cerrados | **87** | Incluye las variantes «cerrado en parte», «por lectura», «con un NO» y «por incognoscible» |
| Vigentes | **25** | Su evento no ocurrió, o su ocurrencia depende de `E-01` |
| `NO APLICA` | **6** | Con la figura de `ADR-14004` |
| **Total** | **118** | |

**Dos correcciones de recuento respecto de la reanudación anterior**, las dos verificadas contra el árbol:

1. `Estado-Del-Destino-2026-08-27.md` §4 declaraba **86 cerrados / 9 vencidos / 12 vigentes / 11 sin
   evento**. Las categorías «vencido» y «sin evento» **desaparecieron del vocabulario vivo**: de esas
   veinte filas, **seis** pasaron a `NO APLICA`, **trece** a `Vigente` y **una** a `Cerrado`. La
   aritmética cierra exacta y es consecuencia de las decisiones del Product Owner del 2026-08-26 y del
   2026-08-30 y de `ADR-14004`.
2. **Ninguna de las 118 filas está sin evento.** Es el cierre efectivo de la escalada `E-04` de la mesa
   del 2026-08-27.

**Lo que sí está en riesgo, y depende de `E-01`.** **Veintiuna** de las veinticinco vigentes nombran el
mismo evento —la fase `i`—, y **ocho de ellas** con la forma «punto de control de la fase `i`», que
`Root-Rules.md` §12.2 pone como ejemplo de lo que **no** es un evento abrible. Trece podrían haber
vencido con el despliegue del 2026-09-06: **vencieron o no según cómo se resuelva `E-01`**, y por eso
el barrido se difiere y no se ejecuta acá.

**Una fila venció por otro camino y no está en discusión**: `PD-05` de `Api/09 Pipeline-CI-CD.md`
esperaba «la próxima emisión de la 05», y la 05 emitió de **3.1 a 3.9** entre el 2026-08-20 y el
2026-08-31. Nueve veces vencida.

---

## 5. Divergencias

Se declaran; **no se resuelven acá** (`Master-Prompt-Reanudacion.md` §7).

### DIV-01 · El entregable de la etapa `i` cambió y el corpus no lo registra

| | |
| --- | --- |
| **Lectura declarativa** | `Roadmap-Producto.md` **1.9** §2.1 y §5.2, e intake `F-14`: «el front publicado por FTP en el hosting y el servicio de datos en el servidor propio, con `PT-05` medida desde la red de la facultad» |
| **Lectura observable** | Desde el 2026-09-06 las dos piezas corren en contenedores del servidor propio, construidas desde el repositorio y publicadas por túnel con dominio propio; el hosting externo está declarado retirado por el Product Owner |
| **Evidencia declarativa** | `Roadmap-Producto.md:64`, `:160`; `PRODUCT-INTAKE` `:179` — **el intake se reemitió 4.0 y 4.1 el 2026-09-12 sin absorber la decisión del 2026-09-06** |
| **Evidencia observable** | `PROMPTs`-externo: `DESPLIEGUE.md` de la carpeta de despliegue, «el hosting externo queda retirado»; el servicio informa su revisión sellada |
| **Precisión que aporta la mesa** | **La etapa `i` está abierta desde el 2026-08-27** (PR #95, «arranca la etapa `i`»; `Estado-Del-Destino-2026-08-27.md` §10.3 declara la salida `D` arrancada con «Etapa siguiente: Fase `i`»). No es un despliegue fuera del roadmap ni una etapa cerrada en silencio: es **una etapa abierta cuyo entregable cambió** |

### DIV-02 · El registro de cambios no tiene el PR #186

| | |
| --- | --- |
| **Lectura declarativa** | `changelog.md` l.3-6: «se actualiza en la rama de la etapa, no después de la fusión»; y l.894: «hasta la fase `i` no hubo despliegue real ni release público» |
| **Lectura observable** | `git show --stat 89f3ab3`: PR #186, 2026-09-06, `deploy/Dockerfile.web`, 118 líneas. Ninguna entrada lo registra; la del 2026-09-12 lo nombra de pasada como si preexistiera |
| **Reincidencia** | Es la **tercera** vez: el propio documento narra el caso `D-01` (etapas `c`, `d`, `e`) y la reposición del 2026-08-18 |

### DIV-03 · El índice del corpus afirma un estado que ningún instrumento sostiene

| | |
| --- | --- |
| **Lectura declarativa** | `SDD/Docs/README.md` §7: «el código va por la **etapa `e`**»; «la séptima migración **está en curso**»; «10-Examples: pasada de diseño»; cabecera: «manifiesto **4.0**, intake **3.0**» |
| **Lectura observable** | Su propia tabla §7.2 dice `a`..`h` **Cerrada**; el informe de migración está cerrado desde el 2026-08-25; `samples/README.md` **2.0** declara dieciséis carpetas corriendo; el manifiesto es **6.0** y el intake **4.1** |
| **Nota** | Una de las seis afirmaciones **ya era falsa al emitirse la 2.5**: contradice la tabla del mismo §7 |

### DIV-04 · Dos canales de publicación vivos, y el corpus sólo describe el retirado

| | |
| --- | --- |
| **Lectura declarativa** | **214** líneas con «hosting» en 47 archivos y **42** con «FTP» en 15; **25** afirman hoy en presente algo falso, entre ellas `ADR-14003` (`Estado: Aceptado`, campo 5 `vigente`), `ADR-10007`, `Guia-Publicacion-Front-Ftp.md` («el despliegue ocurrió y está en línea»), `Pipeline-CI-CD.md` **3.8 —emitida el 2026-09-12—** y la matriz de publicación de `Pipeline-Producto.md` §3 |
| **Lectura observable** | `.github/workflows/deploy-front-ftp.yml` se dispara sobre `src/GeometriaFactory.Web/**`, `visor/**` y `src/GeometriaFactory.Contracts/**` —exactamente las rutas que tocó el PR #187— y publicó en el hosting el 2026-09-12. **El mismo `main` se publicó dos veces, a dos topologías, el mismo día** |
| **Matiz que aporta la mesa** | El hosting **no está retirado como hecho**: está retirado **como decisión con tres pasos pendientes**, ninguno técnico. Mientras tanto, la doble publicación es real y ningún documento la declara |

### DIV-05 · Procedencia 13.7 contra vigente 13.10

**Por diseño, y no es defecto.** Ver §6.

---

## 6. Diff normativo 13.7 → 13.10, artefacto por artefacto

| Salto | Artefacto que se movió | 13.7 | 13.10 | ¿Alcanza al destino? |
| --- | --- | --- | --- | --- |
| **13.8** | `Mesa-Rules` | 1.0 | **1.1** | **No.** Su §0.0 «rige para las convocatorias desde esta versión en adelante, y un registro de mesa anterior no queda no conforme» |
| **13.8** | `Master-Prompt-Reanudacion` | 1.10 | **1.11** | **No.** Es el prompt que produjo este documento; su §3.1.1 deja de excluir a la generación por categoría |
| **13.8** | `Catalogo-De-Criterios` | 1.14 | **1.15** | **No.** Índice: no define criterios |
| **13.9** | `Conocimiento/Knowledge-Conformacion-Pull-Request-Manual` + índice | — | 1.0 / 1.1 | **No.** Alta en la carpeta anexa; el intake no cita ningún alias |
| **13.10** | `Conocimiento/Knowledge-Template-HTML-SDD-Default` y `…-Blazor-Interactive-Server-…` + índice | — | 1.0 / 1.2 | **No.** Ídem; además la Fase B2 de `GeometriaFactory-Web` está cerrada |

**Los tres incrementos son minor y los tres declaran su bloque «Impacto sobre destinos existentes»
vacío.** **Cero major con impacto** entre la procedencia y la vigente: el umbral de continuidad de
`Master-Prompt-Reanudacion.md` §4.0.1 no se cruza, y la salida `C` sigue siendo barata y correcta.

**Ningún documento del corpus cambia de forma por este salto.** Es la misma verificación que §6 del
informe del 2026-08-27 hizo para 13.3 → 13.7, con la misma conclusión.

---

## 7. Resultado de la mesa de evaluación (R1.5)

Registro completo en [`Mesa-2026-09-12.md`](Mesa-2026-09-12.md).

| | |
| --- | --- |
| **Panel** | Núcleo de cuatro (requisitos, verificación, lector sin contexto, refutador) más cinco variables: arquitectura, operación y entrega, seguridad, formal y trazabilidad documental. Sin agentes ad hoc |
| **Descartados** | Datos y dominio, interfaz externa, concurrencia, rendimiento, cumplimiento y uso, cada uno con su motivo |
| **Hallazgos** | **55** detectados, **42** procedentes, **1** descartado por nivel `C` |
| **Parches listos** | **14**, en el roadmap, el guion de puerta, `Producto/Adrs`, `Web/05 Adrs`, `Web/09`, `Pipeline-Producto`, el changelog, los dos README, `Vista-Producto`, la categoría 10, los ítems diferidos, `AGENTS.md` y `Audit/` |
| **Deuda declarada** | **5**, con su evento de cierre |
| **Escaladas** | **5**, agrupadas y con default: `E-01` el entregable de la etapa `i`; `E-02` la API pública; `E-03` los secretos de FTP; `E-04` el criterio de etiquetado; `E-05` el estado durable del front |
| **Capas a revalidar** | Intake §14 y `F-14` (sujeto a `E-01`), `Api/05` §5.1, `Matriz-Sensado-Deriva.md`, `Medicion-PT-05.md` |
| **Cierre** | En **una** ronda, **por decisión y no por criterio**, declarado: doce de los hallazgos son escaladas y ninguna se resuelve leyendo más el árbol. Ciclo marcado **sospechoso de homogeneidad** (86 % de votos 5-0); el refutador revisó los descartes |

**Lo que la mesa estableció y R0 no podía ver.** Que la disyuntiva «la fase `i` ocurrió o no ocurrió»
era falsa: **está abierta**, y el defecto es más preciso y más barato de reparar que reescribir el
corpus como si la etapa hubiera pasado.

---

## 7.1 El ciclo 2 de la mesa, y lo que resuelve

**El Product Owner respondió a las escaladas con un testimonio que es evidencia de nivel E4**, y la
mesa abrió un segundo ciclo sobre él (registro §12). Lo que resuelve:

| Escalada del ciclo 1 | Desenlace |
|---|---|
| **`E-01`** · el entregable de la etapa `i` | **Respondida.** El cambio de topología es **una alternativa que el propio intake había evaluado** —`X-10`, con su condición «reevaluar si aparece un dominio propio»— y que se realizó porque el túnel con dominio propio **satisface mejor la razón que originó la partición**. La etapa `i` sigue abierta con su entregable reescrito; `P-01` y `P-03` quedan desbloqueados |
| **`E-03`** · los secretos y el flujo de FTP | **Respondida.** El canal se conserva **por decisión, como alternativa y antecedente de despliegue**. Lo que falta no es retirarlo: es **declararlo como tal y quitarle el disparador automático** (`P-16`) |
| **`E-02`** · la API públicamente alcanzable | **Abierta.** El testimonio no la menciona, y la exposición de la superficie sigue sin decidirse |
| **`E-04`** · el criterio de etiquetado · **`E-05`** · el estado durable del front | **Abiertas** |

**Y el planteo de fondo del Product Owner se elevó al framework.** Su observación —«el software como
producto evoluciona, y esa evolución no se registra como otro ciclo de ajustes y nuevas
características»— se verificó contra el conjunto normativo y resultó cierta, con la asimetría medida:
hay mecanismo para cuando **el framework** avanza y ninguno para cuando **el producto** avanza. Salió
como **reporte `25`** del framework, con su prompt de intervención. **No es un defecto de este
destino**: es lo que explica por qué sus cuatro divergencias existen.

**Dos parches nuevos para el destino**, que se suman a los catorce:

- **`P-15`** · `A3-Decisiones-Del-Product-Owner.md` recibe las dos decisiones con su fundamento. Hoy
  viven en el repositorio documental del framework, y el destino tiene que ser autosuficiente para su
  propia auditoría.
- **`P-16`** · el canal de FTP se declara **alternativa documentada, no productiva**, y pierde el
  disparador automático. Es lo que vuelve la decisión distinguible de un descuido.

**Y un tercer cambio de producto entra a la lista de trabajo**: la reestructuración de la solución bajo
un árbol nuevo, que la mesa calificó **cambio de alcance y no ajuste** —reordena la composición que el
roadmap y las épicas usan como unidad de planificación—.

---

## 8. Recomendación, y su fundamento

```text
RECOMENDACIÓN — A · Reparar primero, y después D · continuar la construcción

  Continuidad del origen: sostenible — cero major con impacto entre 13.7 y 13.10
  Alcance real del salto: 0 artefactos del destino, de 6 cambios del framework
  Volumen alcanzado:      506 documentos vivos
  Estado del repositorio: EN ORDEN (T0 limpio, sin entregas vivas)
  Divergencias abiertas:  4 reales y 1 por diseño; 42 hallazgos procedentes
                          (3 P0 de corpus, el resto P1 a P3)
  Costo de no hacerlo hoy: 21 ítems diferidos cuelgan de un evento que nadie
                          puede declarar ocurrido ni pendiente; cada fusión que
                          toque el front sigue publicando al hosting retirado;
                          y la puerta de la etapa `i` puede aprobarse en verde
                          sin haber medido nada
  Alternativa razonable:  D directo, sin reparar. Ganaría si lo único que
                          importara fuera medir PT-05 hoy; pierde porque la
                          puerta que mediría está escrita contra el canal que
                          se retiró

  DE LA MESA (§3.1)
  Hallazgos procedentes:  42 — P0: 3 (canal no documentado, README contradictorio,
                          topología sin ADR) · P1: 28 · P2: 8 · P3: 3
  Parches listos:         14, en 13 capas
  Deuda declarada:        5, con su evento de cierre
  Escaladas al humano:    5, agrupadas
```

**`B` no se recomienda, y conviene decir por qué**: el salto 13.7 → 13.10 movió tres artefactos de
proceso y dos altas en la carpeta anexa, y **ninguno toca un artefacto del destino**. Migrar por el
número sería trabajo sin resultado, exactamente como en la reanudación anterior.

**`C` no alcanza sola**: dejaría cuatro divergencias abiertas, y `Master-Prompt-Reanudacion.md` §4
declara que `A` es «la única salida que las demás dan por hecha».

**`E` no aplica**: no hay migración en vuelo.

### 8.1 Las salidas, y qué implica cada una

| Salida | Qué continúa en esta misma sesión | En qué estado te deja | ¿Vuelve a preguntar? | Qué **no** resuelve |
| --- | --- | --- | --- | --- |
| **A · Reparar primero** | Los 14 parches de la mesa, menos los bloqueados por `E-01` | El corpus diciendo lo que el producto es, y **el estado vuelto a leer** | **Sí**: vuelve a R0 y la recomendación se recalcula | Nada de lo que está en escalada |
| **B · Migrar a la vigente** | `Master-Prompt-Migracion.md` con el diff de §6 hecho | Procedencia 13.10 | No | Las cuatro divergencias, que no son de versión |
| **C · Seguir en la versión declarada** | `Master-Prompt.md` con la decisión tomada | Procedencia 13.7 declarada y verdadera | No | Las cuatro divergencias |
| **D · Continuar la construcción** | Nada que invocar: se sigue | Midiendo `PT-05` sobre una puerta escrita contra el canal retirado | No | Las cuatro divergencias |
| **E · Retomar una migración a medias** | — | — | — | No aplica: no hay ninguna en vuelo |

---

## 9. Decisión

| Campo | Valor |
| --- | --- |
| **Salida elegida** | *(pendiente del Product Owner)* |
| **Quién la eligió** | — |
| **Fecha** | — |
| **Escaladas respondidas** | `E-01` a `E-05`: *(pendientes)* |

**Mientras no haya decisión, este informe es la única escritura de la reanudación sobre el destino**,
junto con el registro de la mesa. Ningún parche se aplicó.

---

## 10. Punto de continuación

**Para la salida `A`** — la lista de trabajo es la tabla de parches de [`Mesa-2026-09-12.md`](Mesa-2026-09-12.md) §7,
en este orden, porque tres tienen dependencias:

1. **`P-03`** (el ADR de topología de nivel Producto) y **`P-01`** (el roadmap), que dependen de la
   respuesta a `E-01`.
2. **`P-04`**, **`P-05`**, **`P-06`**, que dependen de `P-03`.
3. **`P-11`** (el barrido de las 21 filas), que depende de `P-01`.
4. El resto —`P-07` a `P-10`, `P-12` a `P-14`— **no depende de ninguna escalada y se puede aplicar ya**.

**Para la salida `D`** — la construcción, que no tiene prompt:

| Campo | Valor |
| --- | --- |
| **Etapa** | **`i` · Despliegue real — ABIERTA desde el 2026-08-27**, no «planificada y no ejecutada» |
| **Qué ya ocurrió dentro de ella** | El despliegue real del 2026-09-06: las dos piezas en contenedores del servidor propio, sellados con su revisión, publicados por túnel con dominio propio; y la reinstalación del 2026-09-12 sobre `5c95dab` |
| **Qué falta, medido** | `I-1` **reescrito** contra el canal vigente (hoy es indecidible); `I-4` · **`PT-05` sin medir**; `I-5` · el circuito completo sobre el despliegue real, sin registrar; `I-7` · las ocho puertas anteriores sobre el árbol desplegado, sin registrar. `I-2`, `I-3` e `I-6` tienen evidencia, pero **vive fuera del corpus** |
| **Puerta de salida** | `scripts/verify-stage-i.sh`, siete criterios — **no la corras antes de `P-02`**: hoy aprueba `I-1` en verde midiendo el flujo equivocado |
| **Qué la bloquea** | `E-01` (qué es el entregable ahora) y, para `PT-05`, una persona en la red de la facultad |
| **Qué destraba** | **Veintiuna** filas de ítems diferidos |
| **Documentos que la gobiernan** | `Roadmap-Producto.md` **1.9** §2.1 y §5.2 · `scripts/verify-stage-i.sh` · `Medicion-PT-05.md` **1.1** · `Pipeline-CI-CD.md` (Web) **3.8** · el ADR de topología cuando exista |

**Para cualquier salida**, lo que no hay que volver a deducir: la etapa `i` **está abierta**; el
producto **corre en producción** desde el 2026-09-06; el canal vigente **no está descrito en ningún
documento del corpus**; y el criterio `I-1` **no mide lo que dice medir**.

---

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 2.0 | 2026-09-12 | **Suma §7.1, el ciclo 2 de la mesa**, convocado por el testimonio del Product Owner. `E-01` y `E-03` quedan respondidas —el cambio de topología es la realización de `X-10`, y el canal de FTP se conserva por decisión—; `E-02`, `E-04` y `E-05` siguen abiertas. El planteo de fondo se elevó al framework como **reporte `25`**. Suma `P-15` y `P-16`, y registra un tercer cambio de producto pendiente: la reestructuración de la solución, calificada **cambio de alcance**. Sube major. |
| 1.0 | 2026-09-12 | Emisión inicial. Sexta reanudación del destino. Compuerta de arranque en orden; seis dimensiones resueltas con las tres de contraste contrastadas; cuatro divergencias reales y una por diseño; recuento de ítems diferidos corregido sobre el instrumento (118 = 87 + 25 + 6) y explicada la desaparición de las categorías «vencido» y «sin evento» del informe anterior; diff normativo 13.7 → 13.10 artefacto por artefacto con cero major con impacto; mesa de evaluación con nueve roles y 42 hallazgos procedentes. **Establece que la etapa `i` está abierta desde el 2026-08-27 y que lo que cambió es su entregable**, corrigiendo la disyuntiva con la que R0 la había presentado. Recomienda `A` y después `D`, con cinco escaladas agrupadas. |
