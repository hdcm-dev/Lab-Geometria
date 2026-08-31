# Inventario de las marcas `[A VERIFICAR]` del corpus vivo

**Producto:** Fábrica de Geometría
**Documento:** Inventario-Marcas-A-Verificar-2026-08-31.md
**Versión:** 1.2
**Fecha:** 2026-08-31
**Instrumento:** clasificación previa a la edición, con el precedente de [`Inventario-Renombre-F03-2026-08-31.md`](Inventario-Renombre-F03-2026-08-31.md)
**Estado:** **Emitido.** Clasifica; **no toca una sola marca**

---

## 0. El resultado, primero

> **Setenta y una apariciones. CINCO incógnitas.** Y de las cinco, **dos ya no tienen pregunta que
> contestar** —una porque se midió el 2026-08-13, otra porque su objeto no existe— y **una no está
> registrada en la sección que declara las marcas**.
>
> El §8 del README declaraba **«ocho marcas `[A VERIFICAR]` vigentes»** y enumeraba **tres temas**.
> Ninguno de los dos números era el de las incógnitas: **el ocho salía de contar líneas del intake
> —cuatro de ellas del control de cambios— y el tres omitía dos.**

| | Incógnita | Apariciones | Estado, medido contra el árbol |
|---|---|---|---|
| **`I-1`** | La **versión de plataforma que soporta el hosting** | **18** | **RESUELTA el 2026-08-13, midiendo.** La declaraban abierta **10 apariciones en 9 documentos** |
| **`I-2`** | La **versión de la biblioteca de componentes de interfaz** | **12** | **SIN OBJETO — y CERRADA EN ORIGEN desde el 2026-08-20.** El desenlace nunca bajó a los diez lugares que la seguían pidiendo |
| **`I-3`** | La **construcción de la imagen en destino desde el repositorio** | **22** | **VIVA, y bien declarada.** Se mide en la fase `i`; `BT-00026` la prueba |
| **`I-4`** | La **disponibilidad de un dominio propio** para el túnel saliente | **1** | **VIVA.** Atada a `X-10`, que es una exclusión |
| **`I-5`** | El **volumen de la comisión** | **9** | **CERRADA POR INCOGNOSCIBLE el 2026-08-20**, y no figuraba en `PRODUCT-INTAKE` §22. Lo que sobrevive es su **consecuencia**: el caudal quedó provisorio |
| | *Prosa que habla de las marcas sin ser ninguna* | **9** | Recuentos, definiciones y filas que agregan varias |
| | **Total** | **71** | |

---

## 1. Por qué existe este documento

**Porque contar sin clasificar ya salió mal una vez en este producto, y fue hoy mismo.** El hallazgo
`I-03` del renombre `F-03` se estimó en **«~90 códigos, 731 ocurrencias, 132 documentos, cuatro o cinco
tandas»**; clasificadas las **723** candidatas del tramo, **722 no había que tocarlas y la población real
era una**. Eran registro histórico, el propio glosario y citas. Se cerró esta misma jornada, y su
inventario —[`Inventario-Renombre-F03-2026-08-31.md`](Inventario-Renombre-F03-2026-08-31.md)— es el
precedente directo de éste. La regla que salió de
ahí —`Migracion-Rules.md` §4.1, clasificar antes de editar— es la que este documento aplica **antes** de
tocar una marca.

**Y el riesgo acá era el mismo, con el mismo signo.** «Setenta y una apariciones» invita a planificar un
barrido de setenta y un puntos. **Cincuenta y siete de las setenta y una son citas**: documentos que
remiten correctamente a una incógnita declarada en otro lado. Editarlas sería propagar ruido, no cerrar
nada.

**Método.** `grep -rn 'A VERIFICAR'` sobre `SDD/`, excluyendo `_legacy/`, `PROMPTs/` —que es del Product
Owner— y las filas de control de cambios, que son registro histórico y no declaran nada vigente. Cada
aparición se leyó y se asignó a una incógnita **abriendo su línea completa**, no por palabra clave: la
primera pasada automática asignó mal **catorce** y se corrigieron a mano.

---

## 2. Las dos incógnitas que ya no tienen pregunta

### 2.1 `I-1` · La versión de plataforma del hosting — **resuelta el 2026-08-13, y nueve documentos no se enteraron**

**La medición existe, tiene fecha, tiene evidencia y está en tres documentos vivos:**

| Fuente | Qué declara |
|---|---|
| [`Reporte-Despliegue-Somee.md`](Reporte-Despliegue-Somee.md) §2 | **`PT-01.a` PASA**: `https://www.aplicada.somee.com/estado` → **200**. «**RESUELTA: el hosting soporta `net10.0`**» |
| [`../00-Contexto/Compatibilidad-Plataformas.md`](../00-Contexto/Compatibilidad-Plataformas.md) | «**Verificada sobre el hosting real el 2026-08-13**». **Confirmado además desde el panel de la cuenta** |
| [`../Unidades-Entrega/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md`](../Unidades-Entrega/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md) | «**RESUELTA el 2026-08-13** […] no por decisión, sino midiendo» |

**No hizo falta bajar la versión objetivo del front**, que era la salida declarada para el caso
contrario. **La incógnita está contestada del modo exacto en que su propia declaración pedía que se
contestara.**

**Y aun así nueve documentos la declaran abierta**, `PRODUCT-INTAKE` §17.2.P.9 entre ellos, junto con
`A3` `D6`, que el **2026-08-20** la presentó al Product Owner diciendo «**la fase `i` la hace sola**»
—siete días después de que la fase `a` la hubiera hecho—.

**Lo más caro no es eso, y conviene nombrarlo con precisión.** El **2026-08-27**, la mesa de evaluación
detectó que tres de esas filas estaban **vencidas por un evento que no podía cerrarlas** —`H-05`, con
evidencia `E4` y voto `PROCEDE 5-0`— y aplicó el parche `P-06`: **les movió el evento de cierre de la
etapa `a` a la fase `i`**, con el fundamento de que «esas marcas se resuelven midiendo, no decidiendo».
El fundamento es correcto. **La conclusión no**: la medición ya existía desde hacía **catorce días**.
Las tres filas pasaron de `VENCIDO` a `Vigente` y **el corpus registró como saneamiento el haberle dado
más plazo a una pregunta ya contestada**.

> **Esto es, medido por segunda vez y dentro de este producto, el hallazgo `HM-02` que el
> [reporte 17](https://github.com/hdcm-dev/IA.SDD.Documentacion) elevó al framework el 2026-08-31: el
> diferimiento se contrasta contra el calendario y no contra lo que el producto hizo.** El primer caso
> medido fue `PA-01`, la unidad de estimación. Éste es el segundo, y es **peor en un aspecto**: no lo
> produjo el olvido, **lo produjo el mecanismo de saneamiento funcionando como está escrito**. La mesa
> hizo bien las cinco cosas que `Mesa-Rules.md` le pide y ninguna de las cinco es «contrastar el ítem
> contra los hechos».

### 2.2 `I-2` · La versión de la biblioteca de componentes — **sin objeto**

**No hay biblioteca de componentes en el producto.** `GeometriaFactory.Web.csproj` declara **una sola**
referencia de proyecto y lleva escrito el apartamiento: *«la etapa `b` decide NO INTRODUCIR MudBlazor»*,
con sus tres razones medidas sobre la maqueta aprobada.

**Y acá hay que corregir a este mismo inventario, que en su emisión 1.0 lo dijo de menos.** No es que
`I-2` *deba* cerrarse: **ya está cerrada, y desde el 2026-08-20**. `Web/05` §11 **`PA-01`** y
`Web/06` **`PA-03`** llevan escrito, con esa fecha, el desenlace exacto —*«Cerrado · A2b, por lectura:
no hay biblioteca, y es una decisión»*—, y `A3` §1 la clasificó como cerrable leyendo el mismo día.

**Lo que quedó abierto no es la incógnita: es su propagación.** El desenlace no bajó a `BT-10002`, ni a
`BT-02` y `V-5` del checkout, ni a la cláusula de `PD-03`, ni a `Estrategia-Versionado.md`, ni a las dos
filas de `Supply-Chain-Seguridad.md`, ni a la guía de publicación, ni —sobre todo— a
`PRODUCT-INTAKE` §17.2.P.1, que es **la fuente que acuñó la marca**. **Diez lugares siguieron pidiendo
anclar la versión de una biblioteca que el producto había decidido no tener, durante once días.**

**Es la tercera vez en la misma jornada que aparece este patrón**, y las tres con distinto disparador:
`D1` confirmada y no propagada al §22; `PT-01.a` medida y no propagada a nueve documentos; y ahora
`PA-01` cerrada y no propagada a diez. **Ninguna de las tres es un error de juicio: las tres son
decisiones correctas que no volvieron sobre quien había hecho la pregunta.**

**Una incógnita sobre la versión de algo que no existe no se resuelve midiendo: no tiene objeto.** Es la
figura de `ADR-14004`, y exige condición de reapertura declarada — que la hay: **si una etapa introduce
comportamiento sobre las superficies**, la pregunta vuelve, y vuelve mejor planteada que hoy.

---

## 3. Las tres que siguen vivas, y de quién es cada una

| Incógnita | Cómo se cierra | Quién | Estado del corpus |
|---|---|---|---|
| **`I-3`** · imagen en destino | **Midiendo**, en la fase `i`. `BT-00026` la prueba una vez antes de depender de ella | Se resuelve sola al desplegar | **Consistente.** Las 22 apariciones dicen lo mismo, y tres declaran expresamente que la categoría escribe *cómo se prueba* pero **no declara que funcione** |
| **`I-4`** · dominio propio | **Midiendo**, si aparece un dominio | Atada a `X-10`, exclusión vigente | **Consistente.** Una sola aparición dedicada |
| ~~**`I-5`** · volumen de la comisión~~ **Ya no viva** | **Decidiendo** — y **decidida el 2026-08-20 por INCOGNOSCIBLE** | El Product Owner, que ya la tomó | **Inconsistente por partida doble: no estaba en `PRODUCT-INTAKE` §22, y `A3` no registraba su cierre** |

### 3.1 `I-5`, la incógnita que el §22 nunca registró

**`PRODUCT-INTAKE` §22 enumera cuatro marcas heredadas y el volumen de la comisión no es ninguna de las
cuatro.** Pero está rotulada `[A VERIFICAR]` en **nueve** lugares del corpus, entre ellos `Web/05` §11
`PA-06`, `Web/06` `PA-08` y `BT-10022`, y `Handoff-Checkout.md` la registra como `A-19`.

**No es una omisión inocua, por tres motivos que se miden:**

1. **Condiciona una decisión de diseño ya tomada.** Los dos listados **no incorporan paginación**, y el
   fundamento escrito es que suponen decenas y no cientos. Si el número es otro, hay rediseño.
2. **Es la que destraba el último resto de `A-5`.** El caudal de 20 peticiones por minuto quedó fuera de
   la confirmación `D1` del 2026-08-26, y `A3` §3 declara que **sin saber cuántos alumnos, confirmar el
   caudal es adivinar**.
3. **`A3` §3 la pone PRIMERA en el orden recomendado**, por delante de `D1`. Se tomó `D1` y no se tomó
   ésta.

**Y es la única de las cinco que no se resuelve midiendo.** El §22 declara que las marcas *«se resuelven
midiendo, no decidiendo»*, y esa frase es verdadera para las cuatro que enumera. **Registrarla obliga a
matizarla**: hay una que es una decisión, y por eso no entró — la sección se organizó por *cómo se
cierra* el ítem, y el ítem que se cierra distinto se quedó afuera.

---

## 4. Qué se hace con esto, y qué no

**Este documento no toca ninguna marca.** Habilita la tanda siguiente, que hace exactamente esto:

| | Acción | Alcance |
|---|---|---|
| 1 | **`I-1` se cierra por lectura**, citando las tres fuentes de la medición | Las **10** apariciones, en 9 documentos, que la declaran abierta |
| 2 | **`I-2` propaga el cierre que `PA-01` tiene desde el 2026-08-20**, con la figura de `ADR-14004` y su condición de reapertura | Las **10** apariciones que la seguían pidiendo |
| 3 | **`I-5` se registra en `PRODUCT-INTAKE` §22** como quinta marca, con su naturaleza de decisión | El §22 |
| 4 | **`I-3` e `I-4` no se tocan**: están bien declaradas | — |

**Las 57 citas no se editan.** Una cita que remite correctamente a una incógnita cerrada en su fuente
**no es un defecto**: es cómo debe funcionar un corpus con una sola fuente por hecho. Se corrigen sólo
las apariciones que **declaran** el estado, no las que lo **refieren**.

**Y una cosa que este inventario no puede hacer:** decidir `I-5`. Es del Product Owner, y sigue abierta
después de esta tanda.

---

## 5. Las 71 apariciones, por incógnita

**I-1 — 18 apariciones**

- `00-Contexto/Compatibilidad-Plataformas.md:173`
- `00-Contexto/Compatibilidad-Plataformas.md:335`
- `Unidades-Entrega/GeometriaFactory-Web/08-Calidad-Y-Pruebas/Estrategia-Calidad.md:57`
- `Handoff-Checkout.md:597`
- `Producto/Pipeline-Producto.md:213`
- `Producto/Plan-Etapa-A.md:530`
- `Audit/F-09-Devops-Siete-Proyectos-r1.md:291`
- `Audit/A3-Decisiones-Del-Product-Owner.md:96`
- `Audit/Reporte-Despliegue-Somee.md:57`
- `Unidades-Entrega/GeometriaFactory-Web/09-Devops/Pipeline-CI-CD.md:209`
- `Unidades-Entrega/GeometriaFactory-Web/09-Devops/Pipeline-CI-CD.md:367`
- `Audit/Estado-Del-Destino-2026-08-27.md:254`
- `Unidades-Entrega/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md:49`
- `Unidades-Entrega/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md:92`
- `Unidades-Entrega/GeometriaFactory-Web/05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md:287`
- `Unidades-Entrega/GeometriaFactory-Web/05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md:556`
- `Unidades-Entrega/GeometriaFactory-Web/06-Backlog-Tecnico/Product-Backlog.md:664`
- `PRODUCT-INTAKE-Fabrica-De-Geometria.md:1293`

**I-2 — 12 apariciones**

- `Unidades-Entrega/GeometriaFactory-Web/06-Backlog-Tecnico/Backlog-Tecnico.md:156`
- `Handoff-Checkout.md:442`
- `Handoff-Checkout.md:598`
- `Unidades-Entrega/GeometriaFactory-Web/08-Calidad-Y-Pruebas/Estrategia-Testing.md:106`
- `Unidades-Entrega/GeometriaFactory-Web/09-Devops/Pipeline-CI-CD.md:365`
- `Unidades-Entrega/GeometriaFactory-Web/09-Devops/Estrategia-Versionado.md:136`
- `Unidades-Entrega/GeometriaFactory-Web/09-Devops/Supply-Chain-Seguridad.md:89`
- `Unidades-Entrega/GeometriaFactory-Web/09-Devops/Supply-Chain-Seguridad.md:188`
- `Unidades-Entrega/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md:50`
- `Unidades-Entrega/GeometriaFactory-Web/05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md:555`
- `Unidades-Entrega/GeometriaFactory-Web/06-Backlog-Tecnico/Product-Backlog.md:663`
- `PRODUCT-INTAKE-Fabrica-De-Geometria.md:1121`

**I-3 — 22 apariciones**

- `Unidades-Entrega/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Matriz-Cobertura-Pruebas.md:574`
- `Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md:590`
- `Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md:849`
- `Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md:1173`
- `Handoff-Checkout.md:599`
- `Producto/Pipeline-Producto.md:212`
- `Audit/F-09-Devops-Siete-Proyectos-r1.md:154`
- `Audit/B-02-03-GeometriaFactory-Api-r1.md:200`
- `Unidades-Entrega/GeometriaFactory-Api/06-Backlog-Tecnico/Backlog-Tecnico.md:327`
- `Unidades-Entrega/GeometriaFactory-Api/09-Devops/Guia-Publicacion-Image-Docker.md:32`
- `Unidades-Entrega/GeometriaFactory-Api/09-Devops/Guia-Publicacion-Image-Docker.md:50`
- `Unidades-Entrega/GeometriaFactory-Api/09-Devops/Guia-Publicacion-Image-Docker.md:79`
- `Unidades-Entrega/GeometriaFactory-Api/09-Devops/Pipeline-CI-CD.md:562`
- `Unidades-Entrega/GeometriaFactory-Api/09-Devops/Pipeline-CI-CD.md:568`
- `Unidades-Entrega/GeometriaFactory-Api/07-Plan-Sprint/Mini-Plan.md:574`
- `Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Especificacion-Funcional.md:856`
- `Unidades-Entrega/GeometriaFactory-Api/09-Devops/Entornos-Deploy.md:137`
- `Unidades-Entrega/GeometriaFactory-Api/09-Devops/Entornos-Deploy.md:244`
- `Unidades-Entrega/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Plan-Pruebas.md:205`
- `Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md:60`
- `Unidades-Entrega/GeometriaFactory-Api/06-Backlog-Tecnico/Product-Backlog.md:749`
- `PRODUCT-INTAKE-Fabrica-De-Geometria.md:1041`

**I-4 — 1 apariciones**

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md:323`

**I-5 — 9 apariciones**

- `Unidades-Entrega/GeometriaFactory-Web/03-UX-UI-DX/Experiencia-De-Uso.md:477`
- `Unidades-Entrega/GeometriaFactory-Web/06-Backlog-Tecnico/Backlog-Tecnico.md:176`
- `Handoff-Checkout.md:584`
- `Audit/A3-Decisiones-Del-Product-Owner.md:92`
- `Audit/B-02-03-GeometriaFactory-Web-r1.md:89`
- `Unidades-Entrega/GeometriaFactory-Web/05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md:560`
- `Unidades-Entrega/GeometriaFactory-Web/06-Backlog-Tecnico/historias-usuario/US-10022-Recorrer-La-Entrega-De-La-Comision-Agrupada-Y-Filtrada.md:96`
- `Unidades-Entrega/GeometriaFactory-Web/06-Backlog-Tecnico/Product-Backlog.md:668`
- `Unidades-Entrega/GeometriaFactory-Web/03-UX-UI-DX/Wireframes-Listado-De-La-Comision.md:141`

**META — 9 apariciones**

- `Audit/D1-Confirmacion-De-Asunciones.md:122`
- `README.md:211`
- `Handoff-Checkout.md:588`
- `Handoff-Checkout.md:590`
- `Audit/Mesa-2026-08-27.md:132`
- `Audit/Plan-Cierre-De-Pendientes.md:51`
- `Audit/Migracion-M2-Propuesta-Intake.md:211`
- `Unidades-Entrega/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md:52`
- `PRODUCT-INTAKE-Fabrica-De-Geometria.md:1847`

---

## 6. Control de cambios

| Versión | Fecha | Descripción | Autor |
|---|---|---|---|
| 1.2 | 2026-08-31 | **Tercera corrección del día, y la más importante: `I-5` NO está viva.** El volumen de la comisión se **cerró por INCOGNOSCIBLE el 2026-08-20** —el Product Owner declaró que el dato no se sabe ni se puede saber, y que no se fija número—, en el commit `b4a4804` y sobre **cinco documentos**. Las emisiones 1.0 y 1.1 la dieron por viva porque la leyeron de `A3-Decisiones-Del-Product-Owner.md`, **que nunca registró ese desenlace**. **Lo sustantivo del hallazgo se mantiene**: la marca faltaba de verdad en `PRODUCT-INTAKE` §22, y el motivo de su ausencia —es la única de las cinco que **se decide y no se mide**— sigue en pie. Lo que cambia es su estado y, sobre todo, **qué sobrevive de ella**: no la pregunta, sino su consecuencia — el **caudal** de `A-5` perdió el fundamento del que se derivaba y quedó **provisorio** hasta `PT-05`. **Y la lección es distinta de la de las otras dos correcciones.** Acá el error no fue dejar de mirar el árbol: fue **mirar el árbol y concluir al revés**. Se verificó que no hay `MaxRequestBodySize`, ni herramienta de versión, ni herramienta de mutación, y se tomaron esas ausencias por «sin decidir» — cuando **`D4` se decidió adoptando el valor por omisión y `D8` se decidió con un NO**. Son decisiones **cuyo cumplimiento consiste en que no haya nada**, y en el árbol **«decidido que no» y «sin decidir» son indistinguibles**. Para esa clase de decisión el registro es la única fuente posible. | Orquestador SDD |
| 1.1 | 2026-08-31 | **Dos correcciones a la emisión 1.0, las dos encontradas al ejecutar el cierre que este inventario habilitó.** **(a)** Decía que `I-1` la declaraban abiertas «nueve documentos»; son **10 apariciones en 9 documentos** —`Web/05` lleva dos—. **(b)** Presentaba a `I-2` como una incógnita **a cerrar**, y **ya estaba cerrada desde el 2026-08-20**: `PA-01` de `Web/05` §11 y `PA-03` de `Web/06` llevan el desenlace escrito con esa fecha. **Lo que estaba abierto no era la incógnita sino su propagación**: el desenlace no bajó a diez lugares, `PRODUCT-INTAKE` §17.2.P.1 —la fuente que acuñó la marca— entre ellos, y durante once días el corpus siguió pidiendo anclar la versión de una biblioteca que el producto había decidido no tener. **Con esto son tres los casos del mismo patrón en una sola jornada** —`D1` sin propagar al §22, `PT-01.a` sin propagar a nueve documentos, `PA-01` sin propagar a diez—, y ninguno es un error de juicio: **las tres son decisiones correctas que no volvieron sobre quien había hecho la pregunta.** | Orquestador SDD |
| 1.0 | 2026-08-31 | Emisión inicial. Clasifica las **71** apariciones vivas de `[A VERIFICAR]` en **cinco** incógnitas, aplicando `Migracion-Rules.md` §4.1 —clasificar antes de editar— con el precedente del hallazgo `I-03`, donde una estimación de ~731 ocurrencias tenía una población real de **una**. Encuentra que **`I-1` está resuelta desde el 2026-08-13** y nueve documentos la declaran abierta; que **`I-2` no tiene objeto**; y que **`I-5`, el volumen de la comisión, no figura en `PRODUCT-INTAKE` §22** pese a estar rotulada en nueve lugares y a ser la única de las cinco que **se decide y no se mide**. Registra además que el parche `P-06` de la mesa del 2026-08-27 **le dio más plazo a una pregunta ya contestada**, que es el hallazgo `HM-02` del reporte 17 al framework **medido por segunda vez**. **No toca ninguna marca.** | Orquestador SDD |
