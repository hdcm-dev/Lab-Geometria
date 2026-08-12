# Observación: los identificadores de código quedaron en castellano sin que nadie lo decidiera

| Campo | Valor |
|---|---|
| Versión | 1.0 |
| Fecha | 2026-08-12 |
| Estado | **Aprobado** |
| Autor | Orquestador SDD |
| Origen | Observación del Product Owner, 2026-08-12: «El estándar trabaja con nombres en inglés … No sé por qué te saliste del estándar» |
| Relacionado | [`Observacion-Ejecucion-De-La-Orquestacion.md`](Observacion-Ejecucion-De-La-Orquestacion.md) 1.2 · [`Observacion-Ciclo-De-Correccion-Sin-Corte.md`](Observacion-Ciclo-De-Correccion-Sin-Corte.md) 1.2 |
| Instrumento emitido | [`../Producto/Norma-De-Nomenclatura.md`](../Producto/Norma-De-Nomenclatura.md) 1.0 |

---

## 1. La anomalía

**El producto nombra sus identificadores de código en castellano, y esa desviación del estándar nunca se decidió.** No hay ADR que la sostenga, no hay fila de intake que la declare, no hay punto abierto que la eleve. Se empezó a hacer, se repitió, y al aparecer en cinco grupos de identificadores **pareció una convención**.

La forma exacta del defecto está en [`Plan-Etapa-A.md`](../Producto/Plan-Etapa-A.md) §1.2, que la propone como decisión `D-01` y la funda así:

> «**Alternativa real `P-1b`: identificadores en inglés.** Costo: rompe la coincidencia con los cinco grupos de arriba, **que ya están declarados y no son propuesta** … Se descarta salvo decisión expresa.»

El razonamiento es circular y hay que verlo entero para entender por qué nadie lo frenó: **el plan descarta el inglés porque el corpus ya usa castellano**. Es decir, toma *lo que el corpus hace* como si fuera *lo que el corpus decidió*. Y como la práctica venía repetida desde el primer día, la fundamentación se leyó sólida.

## 2. Cuándo empezó

**El 2026-08-08, el primer día del producto, en la emisión del `PRODUCT-INTAKE`.**

La versión más antigua conservada del intake es la **1.1**, fechada **2026-08-08**, y ya trae **cuatro de los cinco grupos**:

| Grupo | Dónde, en el intake 1.1 | Origen que el propio intake cita |
| --- | --- | --- |
| Los tres puertos `IRepositorioTrabajos`, `IValidadorFiguras`, `IRelojDelSistema` | §13, §17.2.P.1 | **RT §4.1** |
| Los miembros `HashContrasena` y `JsonOriginal` | §17.1.P.5, §17.3.P.4 | **RT §7.1** |
| Las cinco entidades `ALUMNO`, `TRABAJO`, `PIEZA`, `COMPONENTE`, `OBSERVACION` | §17.3.P.4 | **RT §7.1** |
| Las cinco funciones de la fachada del visor | §13, §17.7.P.3 | **RT §8.4** |

El quinto grupo —los valores de los conjuntos cerrados— también está en 1.1: `Borrador`, `Pendiente` y `Finalizado` en §4 (capacidades F-07 y F-08), y `Pendiente` / `Habilitado` / `Bloqueado` en §17.1. Y la sexta función de la fachada, `establecerMovimiento`, entró en el intake **1.6**, del 2026-08-09, por decisión del Product Owner sobre la capacidad F-25.

**Y acá está el hecho que ordena todo lo demás: los identificadores del intake no los inventó el orquestador. Los transcribió.** Vienen del material del propio Product Owner —los Requerimientos Técnicos—, y el intake los cita con su sección de origen en cada caso. La transcripción fue correcta: el defecto no es haberlos transcripto.

**El defecto es lo que pasó después.** El mismo día, **2026-08-08**, las primeras categorías emitieron identificadores **acuñados**, no transcriptos. `GeometriaFactory-Contracts` emitió su `DX-Error-Messages.md` 1.0 y su control de cambios registra el alta de `CONTRATO_LISTADO_VACIO`; `GeometriaFactory-Visor` emitió su `Definicion-Contrato-De-Fachada.md` 1.0 con los **siete** códigos de condición de su §6 —`DIMENSION_NO_LEGIBLE`, `TIPO_NO_DIBUJABLE` y los otros cinco— y su `DX-Error-Messages.md` 1.0 derivada de ellos. **Ninguno de esos identificadores existe en el material del Product Owner: los acuñó el proceso, y los acuñó en castellano sin declararlo.** **Ahí la transcripción se convirtió en convención, y nadie lo declaró.**

El resto es propagación: los seis catálogos crecieron hasta **101 códigos distintos**, y el 2026-08-12 [`Plan-Etapa-A.md`](../Producto/Plan-Etapa-A.md) §1.2 leyó el resultado como fundamento.

### 2.1 Lo que la fuente decía y el plan leyó al revés

**Es el hallazgo más duro de esta observación, y no es de idioma: es de lectura de fuente.**

El intake §17.7.P.3 encabeza su tabla de la fachada así:

> «Contrato de la fachada, **con los nombres definitivos a fijar en la etapa que la implementa** (RT §8.4)»

Y [`Plan-Etapa-A.md`](../Producto/Plan-Etapa-A.md) §1.2 punto 5 afirma:

> «Las **seis funciones de la fachada del visor** están **fijadas** por el intake §17.7.P.3 en castellano y en `camelCase`»

**La fuente dice que los nombres están abiertos; el plan dice que están fijados, y cita la misma sección.** Lo mismo, en menor grado, con las entidades y los tipos: el intake §17.1.P.11 cierra con «Queda abierto para la etapa `a`: los nombres definitivos de tipos y espacios de nombres», y [`Handoff-Checkout.md`](../Handoff-Checkout.md) §6.2 `A-2` los declara abiertos en **seis de los siete** proyectos de código.

O sea: **el punto de control de la etapa `a` existe precisamente para fijar estos nombres**, y el plan que llegaba a ese punto de control declaró que ya estaban fijados por una fuente que dice lo contrario.

## 3. El alcance, contado

Medido el 2026-08-12 sobre **631 archivos** de `SDD/`, excluidos `_legacy/` y `Docs/Audit/`. El detalle por clase está en [`../Producto/Norma-De-Nomenclatura.md`](../Producto/Norma-De-Nomenclatura.md) §2; acá van las cifras que importan para dimensionar la anomalía.

| Clase | Distintos | Documentos | Ocurrencias |
| --- | --- | --- | --- |
| Valores de conjuntos cerrados | 10 | **396** | **4259** |
| Códigos de condición y de contrato | **101** | **334** | **2911** |
| Funciones de la fachada del visor | 6 | 52 | 593 |
| Interfaces y puertos | 5 | 12 | 61 |
| Entidades, tipos y miembros | 33 | 33 | 287 |

**Lo que estas cifras dicen sobre la anomalía, y no sobre el renombre:**

1. **La desviación creció por dos órdenes de magnitud sin cambiar de naturaleza.** Empezó con 3 puertos transcriptos en 1 documento y terminó con 101 códigos acuñados en 334.
2. **Lo acuñado pesa más que lo transcripto.** De los 155 identificadores contados, **101 son códigos de condición que ninguna fuente del Product Owner declara**. La parte que el orquestador produjo es la parte grande.
3. **Y lo que todavía es propuesta pesa casi nada.** Los 18 espacios de nombres, los 14 tipos y adaptadores y los 2 puertos alternativos de `Plan-Etapa-A.md` viven en **un solo documento**, con **41 ocurrencias**. La desviación es cara donde ya se declaró y gratis donde todavía se propone.

## 4. Cómo se volvió invisible

Cuatro mecanismos, y el orden importa porque cada uno habilita al siguiente.

### 4.1 La repetición se auto-justifica

**Es el mecanismo central y merece nombrarse con precisión: cuanto más se propaga una desviación, más parece deliberada.**

Un identificador castellano en un documento es un detalle. En cinco grupos es un patrón. En 334 documentos es, para cualquiera que llegue después, **una convención del producto**. Nada cambió en la calidad de la decisión —**no hubo ninguna**—; lo único que cambió fue la cantidad de evidencia de que «así se hace acá».

Y el corpus premia esa lectura: este producto tiene una regla explícita, correcta, de **no acuñar términos nuevos donde el producto ya tiene uno**. Aplicada al idioma de los identificadores, esa regla convierte cada repetición en un argumento para repetir.

### 4.2 La invariante de idioma existía, y era sobre la prosa

Las **33 auditorías** del corpus verifican una invariante `D1` que dice, con estas palabras, «idioma español rioplatense neutro técnico». **Se refiere al cuerpo del documento.** Ninguna invariante del framework habla del idioma de un identificador.

El resultado es el peor de los posibles: había un control de idioma, se ejecutó 33 veces, y **su existencia hizo parecer que el idioma estaba controlado**. Un hueco sin control se nota; un hueco tapado por un control vecino, no.

### 4.3 El punto abierto decía «los nombres», no «el idioma de los nombres»

`A-1` y `A-2` de [`Handoff-Checkout.md`](../Handoff-Checkout.md) §6.2 declaran abiertos el nombre del cuarto puerto y los nombres de tipos y espacios de nombres. **El idioma no era una de las dimensiones abiertas: era el suelo sobre el que se elegía entre nombres.**

Por eso el plan de la etapa `a` pudo abrir seis decisiones de nombres, listar para cada una su alternativa real con su costo —lo hizo bien— y **no notar que la primera pregunta estaba contestada de antemano por una práctica que nadie había decidido**.

### 4.4 El defecto no producía ningún síntoma

No rompe una compilación, no falsea un recuento, no desalinea dos documentos. Es del tipo de defecto que [`Observacion-Ejecucion-De-La-Orquestacion.md`](Observacion-Ejecucion-De-La-Orquestacion.md) §2 describe: **su producto es texto que nadie ejecuta**, y sólo se manifiesta cuando alguien lo lee y actúa sobre él. Lo detectó el Product Owner leyendo el plan de la etapa `a`, que es el primer documento donde la práctica se escribió como si fuera una decisión — y por eso mismo el primero donde se podía ver.

## 5. Quién debió detectarla y no lo hizo

### 5.1 Las auditorías: ninguna la reportó, y está verificado

`SDD/Docs/Audit/` tiene **36 archivos**: **33 informes de auditoría** y 3 documentos que no lo son —las dos observaciones previas y el reporte de primera aplicación del instrumento—.

**Verificación ejecutada el 2026-08-12 sobre los 33 informes**, antes de afirmar nada:

| Búsqueda | Resultado |
| --- | --- |
| La palabra «inglés» en los 33 informes | **0 apariciones** |
| «identificadores en castellano / en español / en inglés» en los 33 informes | **0 apariciones** |
| «nomenclatura» en los 33 informes | **13 apariciones, todas sobre consistencia de términos**: residuos de un renombre de «sellos», desincronizaciones internas de un catálogo, un patrón de filename heredado del framework anterior. **Ninguna sobre idioma de identificador** |
| La palabra «inglés» en todo el corpus vivo (631 archivos) | **2 apariciones**: la alternativa `P-1b` de `Plan-Etapa-A.md` §1.2, y la nota del glosario de `GeometriaFactory-Api` que prefiere «punto de acceso» a «endpoint» **en la prosa** |

La afirmación queda entonces acotada a lo que se comprobó: **ninguno de los 33 informes de auditoría planteó jamás la cuestión del idioma de los identificadores**, y la palabra que la nombraría aparece dos veces en todo el corpus vivo, ninguna de ellas en una auditoría.

### 5.2 Quién más

| Quién | Qué debió hacer | Por qué no lo hizo |
| --- | --- | --- |
| **El orquestador** | Declarar el idioma como decisión al emitir el primer identificador acuñado, el 2026-08-08 | Transcribió del material del Product Owner, y siguió acuñando en el mismo idioma por continuidad. Nunca hubo un momento en que la pregunta se planteara |
| **Las 33 auditorías** | Levantar la ausencia de una convención de nombrado declarada | Ninguna invariante lo pedía, y `D1` daba la apariencia de que el idioma estaba cubierto (§4.2) |
| **El plan de la etapa `a`** | Abrir `D-01` con las dos alternativas en pie de igualdad | Abrió las dos, y **descartó una fundándose en la práctica acumulada**. Es el mismo error de fondo con forma de rigor |
| **El punto de control** | Es el lugar correcto y **todavía no ocurrió**. La observación llega a tiempo | — |

**La constancia proporcionada, para no sobreextender el diagnóstico:** ninguna de las tres cosas que sí funcionaron se atenúa por esta observación. El plan de la etapa `a` **elevó** seis decisiones de nombres en vez de tomarlas, declaró alternativas con costo, y se emitió **antes de escribir código**. Que la desviación se detecte hoy, con cero líneas de código escritas, es consecuencia directa de que ese punto de control exista.

## 6. Qué instrumento la habría detectado

Ninguno de los que había. El que falta es **una norma de nomenclatura declarada, con verificación mecánica**, y se emite junto con esta observación: [`../Producto/Norma-De-Nomenclatura.md`](../Producto/Norma-De-Nomenclatura.md) 1.0.

Tres controles, y el segundo es el que ataca este defecto directamente:

| # | Control | Qué habría pasado |
| --- | --- | --- |
| `V-1` | Todo identificador declarado resuelve contra una fila del glosario de correspondencia | El primer código acuñado el 2026-08-08 no habría tenido fila, y la falta de fila es una pregunta, no un silencio |
| `V-2` | **Inspección de idioma de identificador** en cada emisión que declare uno | Habría fallado el 2026-08-08, con 3 identificadores en 1 documento en lugar de 155 en 459 |
| `V-3` | Todo valor de conjunto cerrado tiene identificador **y** etiqueta | Habría forzado la separación entre el dato y lo que la persona ve, que es la decisión `F-02` que hoy hay que tomar sobre 396 documentos |

**Y una regla de método, que es lo que generaliza:**

> **Una práctica repetida no es una decisión.** Antes de fundar una propuesta en «el corpus ya lo hace así», hay que encontrar el documento donde se decidió que se hiciera así. Si no existe, lo que hay es una desviación acumulada, y la propuesta correcta es declararla y elegir — no continuarla.

## 7. Por qué es la misma familia que las dos observaciones previas

Las tres tienen **la misma raíz: tomar lo que el corpus hace como si fuera lo que el corpus decidió.**

| Observación | Qué se tomó como decidido | Qué era en realidad |
| --- | --- | --- |
| [`Observacion-Ejecucion-De-La-Orquestacion.md`](Observacion-Ejecucion-De-La-Orquestacion.md) | El informe del subagente como si fuera el trabajo | Una **afirmación** sobre el trabajo, sistemáticamente optimista, verificada tarde |
| [`Observacion-Ciclo-De-Correccion-Sin-Corte.md`](Observacion-Ciclo-De-Correccion-Sin-Corte.md) | Las 47 filas de puntos abiertos como si fueran una lista de tareas | Un **mecanismo de diferimiento** para poder avanzar; el 76 % estaba correctamente abierto |
| **Ésta** | Los identificadores castellanos del corpus como si fueran una convención | Una **transcripción** del material de origen, continuada por inercia, jamás declarada |

En las tres, **el orquestador leyó un artefacto del proceso como si fuera una conclusión del proceso**, y en las tres la corrección vino de afuera: las dos primeras, del Product Owner; ésta también.

**Y en las tres el mecanismo de ocultamiento es el mismo**: había un control vecino que daba la sensación de cobertura. La auditoría cubría el contenido pero no la entrega; el criterio de corte cubría la calidad del hallazgo pero no la terminación del ciclo; la invariante `D1` cubría el idioma de la prosa pero no el de los identificadores. **Un hueco sin control se ve. Un hueco al lado de un control, no.**

Esta observación agrega, entonces, un criterio de diagnóstico a los que las dos anteriores dejaron:

> Cuando un control lleva muchas ejecuciones sin encontrar nada en su vecindad, la pregunta no es si el control funciona: es **qué está justo afuera de su alcance y se está confundiendo con lo que está adentro**.

## 8. Qué se hace ahora

| # | Acción | Estado |
| --- | --- | --- |
| 1 | Emitir la norma de nomenclatura con el alcance contado y las tres fronteras elevadas | **Hecho**: [`../Producto/Norma-De-Nomenclatura.md`](../Producto/Norma-De-Nomenclatura.md) 1.0, estado `Propuesto` |
| 2 | Emitir esta observación | **Hecho** |
| 3 | **Decidir `F-01`, `F-02` y `F-03`** — fachada, conjuntos cerrados, códigos de condición | **Del Product Owner**, en el punto de control de la etapa `a` |
| 4 | Corregir `Plan-Etapa-A.md` §1.2, cuyo punto 5 contradice al intake §17.7.P.3 (§2.1) | **Pendiente**, después de 3: la corrección depende de qué se decida |
| 5 | Renombrar según lo decidido, con recuento en las dos direcciones | **Pendiente**, después de 3. **Esta tanda no renombró nada** |
| 6 | Resolver los dos códigos `CONTRATO_*` que 3 casos de uso de `GeometriaFactory-Web` citan y ningún catálogo declara | **Pendiente**. Es un defecto de fondo preexistente, ajeno al idioma, hallado al contar |

## 9. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-12 | **Emisión inicial**, a pedido del Product Owner, que detectó al leer `Plan-Etapa-A.md` que el producto nombra sus identificadores de código en castellano contra el estándar. Fija **cuándo empezó** —el 2026-08-08, con el `PRODUCT-INTAKE` 1.1, y por **transcripción** del material del propio Product Owner (RT §4.1, §7.1, §8.4), no por invención—, y **dónde se volvió desviación**: el mismo día, cuando las primeras categorías 03 empezaron a **acuñar** códigos de condición en el mismo idioma sin declararlo. Declara el hallazgo de §2.1: el intake §17.7.P.3 dice que los nombres de la fachada están «a fijar en la etapa que la implementa» y `Plan-Etapa-A.md` §1.2 afirma que están «fijadas» citando esa misma sección. Cuenta el alcance —**155 identificadores en 631 archivos**, con 396 documentos de valores de conjunto cerrado y 334 de códigos de condición— y verifica, antes de afirmarlo, que **ninguno de los 33 informes de auditoría** planteó jamás el idioma de los identificadores: la palabra «inglés» tiene **2 apariciones en todo el corpus vivo** y ninguna en una auditoría. Emite el instrumento faltante como documento aparte. Relaciona la anomalía con las dos observaciones previas por su raíz común —**tomar lo que el corpus hace como si fuera lo que el corpus decidió**— y agrega el criterio de que un hueco al lado de un control se confunde con lo que el control cubre. | Product Owner (observación) · Orquestador SDD (verificación y redacción) |
