# Estrategia de calidad — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Estrategia-Calidad.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `rest-api` · **Proyecto de código principal del producto**
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3, §4, §5 y §6; [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §5 y §6; [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §6.1; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1, §3.4, §8, §9 y §11; [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §5; [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §5; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §15, §17.5.P.6, §17.5.P.8, §17.5.P.10, §21 y §22
**Trazabilidad downstream:** [`Estrategia-Testing.md`](Estrategia-Testing.md), [`Plan-Pruebas.md`](Plan-Pruebas.md), [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Definition-Of-Done.md`](Definition-Of-Done.md); `09-Devops`, que materializa como etapas del pipeline los quality gates de §3; `11-Documentacion`, que cita esta estrategia sin redefinirla

---

## Tabla de contenido

- [1. Definición de calidad para este proyecto de código](#1-definición-de-calidad-para-este-proyecto-de-código)
- [2. Atributos de calidad priorizados](#2-atributos-de-calidad-priorizados)
- [3. Quality gates](#3-quality-gates)
  - [3.1 Qué significa que un gate esté condicionado](#31-qué-significa-que-un-gate-esté-condicionado)
  - [3.2 La batería del validador que corre desde acá](#32-la-batería-del-validador-que-corre-desde-acá)
  - [3.3 Las puertas técnicas y la frontera del despliegue](#33-las-puertas-técnicas-y-la-frontera-del-despliegue)
- [4. Roles de calidad dentro del equipo](#4-roles-de-calidad-dentro-del-equipo)
- [5. Cadencia de revisión](#5-cadencia-de-revisión)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Definición de calidad para este proyecto de código

`GeometriaFactory-Api` tiene calidad cuando **ningún punto de acceso queda fuera de la guardia que le corresponde**, cuando **ninguna traducción a protocolo deshace una decisión ya tomada adentro** y cuando **el servicio no atiende una sola petición sobre un almacén que no está en condiciones**.

Las tres partes describen el mismo peligro desde tres ángulos: **acá es donde una decisión correcta de una capa de adentro se puede perder sin que nada falle**. `05` §9 lo declara con precisión en su primer riesgo —un punto nuevo fuera de la guardia hace que `RN-13` e `INV-09` dejen de valer **y nada falla**— y en el segundo —un trabajo ajeno que responde «no autorizado» confirma la existencia de un recurso ajeno, y **ninguna capa de adentro puede repararlo**—.

**Este es además el proyecto de código donde vive la batería de integración del producto.** El intake §17.5.P.6 declara que `GeometriaFactory.Integration.Tests` golpea **la superficie real por su protocolo contra el almacén real**, y §17.3.P.6 le asigna a esa batería la persistencia real de `GeometriaFactory-Infrastructure`. La consecuencia para esta categoría es doble: su pirámide está **invertida a propósito**, y **lo que acá se rompe no lo cubre ninguna otra batería del producto**.

## 2. Atributos de calidad priorizados

Clasificación ISO/IEC 25010, con la métrica de origen cuando existe. Los valores rotulados **[ASUNCIÓN]** vienen así desde el intake y **no son compromisos**: se usan como vigentes hasta que el Product Owner los confirme (§22, asunciones `A-3` y `A-5`).

| Atributo ISO 25010 | Prioridad | Métrica y origen |
| --- | --- | --- |
| Seguridad | **Crítica** | Exactamente **4** puntos de acceso fuera de la guardia, **ni uno más**, verificado sobre los **quince** en las dos direcciones; **3 de 3** familias empobrecidas con respuestas indistinguibles en cuerpo y en código; **0** respuestas que expongan dirección, ruta, secreto o traza; **0** eliminaciones fuera de alcance aceptadas **al forzar la petición** (`05` §8) |
| Adecuación funcional | **Crítica** | **12 de 12** casos de uso con caso de verificación; **15 de 15** puntos de acceso ejercidos; **14 de 15** códigos del contrato con traducción declarada y **1** declarado **sin destino con su motivo**, con **0** inventados y **0** renombrados |
| Fiabilidad | **Crítica** | **0** peticiones atendidas con la preparación del almacén incompleta; **4 de 4** puertos conectados a su adaptador, con fallo en construcción si falta alguno; **0** caracteres de diferencia entre el texto enviado y el guardado, y **0** truncamientos silenciosos |
| Eficiencia de desempeño | **Alta** | Percentil 99 del listado por debajo de **500 ms**, medido **en el servidor** [ASUNCIÓN del intake §17.5.P.10]; caudal sostenido de **20 peticiones por minuto** [ASUNCIÓN]; arranque en frío en menos de **30 segundos** [ASUNCIÓN] |
| Mantenibilidad | **Alta** | **75 %** de líneas y **70 %** de ramas [ASUNCIÓN del intake §17.5.P.6]; pirámide de **60 %** integración y **40 %** unitarias [ASUNCIÓN], **invertida a propósito**; **1** sola configuración de intercambio declarada en el producto; **0** advertencias de construcción |
| Compatibilidad | **Media** | Los tipos que cruzan la frontera son los del ensamblado de contratos y **esta capa no agrega ni recorta campos**; sin versionado de rutas, porque no hay clientes de terceros |
| Usabilidad | **No aplica como atributo de interfaz** | `tiene_ui_final` es false. Su equivalente es la experiencia del desarrollador que consume la superficie, y la **colección de peticiones reproducible** de `CU-12` es su instrumento |
| Portabilidad | **Baja** | Plataforma única sobre el sistema operativo del contenedor, con la imagen final llevando **sólo el entorno de ejecución** y sin linaje con la imagen de desarrollo (intake §17.5.P.9) |

**Este es el único proyecto de código del producto con `tiene_observabilidad_critica` == true** (`PRODUCT-MANIFEST` §5), y el motivo está declarado: es el único que declara un percentil con métrica numérica. **No hay atributo de disponibilidad**, y es correcto: el intake declara «sin SLO», el servidor es domiciliario y la caída se responde con **estado degradado en el front**, no con redundancia.

## 3. Quality gates

Cada gate declara condición, cómo se verifica y qué pasa cuando no se cumple. Los cinco primeros salen del intake §17.5.P.8; los demás los deriva [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8, con una fila por NFR.

| Id | Condición | Cómo se verifica | Consecuencia si no se cumple |
| --- | --- | --- | --- |
| QG-01 | El guion de construcción termina en **0 y sin advertencias** | Etapa `build` del pipeline | **Bloquea la fusión** (intake §17.5.P.8) |
| QG-02 | El guion de pruebas pasa **entero**, **incluida la batería del validador** | Etapa `test` del pipeline | Bloquea la fusión. Ver §3.2 sobre el recuento de esa batería |
| QG-03 | La cobertura alcanza **75 %** de líneas y **70 %** de ramas [ASUNCIÓN del intake §17.5.P.6] | Informe de cobertura de la etapa `test`, **por componente** | **Condicionado**, ver §3.1 |
| QG-04 | La pirámide del proyecto de código es **60 %** de integración y **40 %** unitarias [ASUNCIÓN del intake §17.5.P.6] | Recuento de pruebas por clase en el informe de la etapa `test` (`TC-37`) | **Condicionado**, ver §3.1 |
| QG-05 | Exactamente **4** puntos de acceso quedan fuera de la guardia de admisión, **ni uno más**, sobre los **quince** | `TC-07`, inspección en las dos direcciones | **Bloquea la fusión.** Es el primer riesgo de `05` §9: un punto nuevo fuera de la guardia hace que `RN-13` deje de valer **y nada falla** |
| QG-06 | **14 de 15** códigos del contrato tienen traducción declarada, **1** está declarado **sin destino con su motivo**, y hay **0** inventados y **0** renombrados | `TC-24` y `TC-27`, comparación en las dos direcciones contra [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §5 | Bloquea la fusión |
| QG-07 | **3 de 3** familias empobrecidas dan respuestas **indistinguibles en cuerpo y en código** | `TC-25` | Bloquea la fusión. Es el segundo riesgo de `05` §9, y **ninguna capa de adentro puede repararlo** |
| QG-08 | **0** respuestas que expongan dirección de servicio, ruta de datos, secreto o traza, sobre los **quince** puntos **y** sobre el registro del servidor | `TC-26` | Bloquea la fusión. Es `RA-03` |
| QG-09 | **0** caracteres de diferencia entre el texto enviado y el guardado, y **0** truncamientos silenciosos | `TC-19` | Bloquea la fusión. **Rechazar, nunca truncar** |
| QG-10 | **4 de 4** puertos conectados a su adaptador, con **0** sin adaptador o con más de uno; y **1** sola configuración de intercambio declarada en el producto | `TC-28` y `TC-29` | Bloquea la fusión, **con fallo en construcción** cuando falta un puerto |
| QG-11 | **0** peticiones atendidas con la preparación del almacén incompleta | `TC-31` | Bloquea la fusión |
| QG-12 | **0** eliminaciones fuera de alcance aceptadas **al forzar la petición** contra esta superficie | `TC-20` | Bloquea la fusión. Es **el único criterio de verificación del producto que la fuente exige ejercer forzando la petición**, y el intake §17.5.P.6 lo declara bloqueante |
| QG-13 | El arranque en frío aplica las transformaciones y responde salud en menos de **30 segundos** [ASUNCIÓN del intake §17.5.P.10] | `TC-33` | **Condicionado**, ver §3.1 |
| QG-14 | Percentil 99 del listado por debajo de **500 ms** medido en el servidor, y caudal sostenido de **20 peticiones por minuto** [ASUNCIÓN del intake §17.5.P.10] | `TC-34`, en la batería de integración | **Condicionado**, ver §3.1 |
| QG-15 | La colección de peticiones reproducible tiene **5 pasos o menos** y **0 datos de prueba inventados** | `TC-35` | Bloquea el cierre de la etapa que la incorpora |

**Quince gates, y ninguno inventado.** Los que no salen del intake salen de una fila de `05` §8, que declara los **diecisiete** NFR de este proyecto de código.

### 3.1 Qué significa que un gate esté condicionado

`QG-03`, `QG-04`, `QG-13` y `QG-14` son los cuatro gates cuyo umbral es un valor rotulado **[ASUNCIÓN]** en el intake §22 —`A-3` para la cobertura, `A-5` para el percentil, el caudal y el arranque en frío, y `A-3` para la forma de la pirámide en cuanto viene de §17.5.P.6—. `05` §11 los registra y esta estrategia adopta el tratamiento sin cambiarlo: **los valores se usan como vigentes y la puerta no se declara bloqueante en `09-Devops` hasta que el Product Owner los confirme sobre su propio documento**.

Condicionado no quiere decir opcional. La medición se hace igual y el resultado se registra; lo que queda en suspenso es la consecuencia automática.

**Una precisión sobre `QG-04`.** Lo rotulado es **el reparto numérico**, no la decisión de invertir la pirámide: el intake §17.5.P.6 declara la inversión **a propósito**, «porque lo que este proyecto de código aporta es cableado, y el cableado se verifica ejerciéndolo». Esa decisión no es asunción y no queda en suspenso.

### 3.2 La batería del validador que corre desde acá

El intake **1.20** declara en §17.5.P.8 que el guion de pruebas de este proyecto de código pasa **«incluidas las diez pruebas del validador»**. Esa batería es de `GeometriaFactory-Infrastructure` y **tiene diez casos**: el intake §21 los cruza en una tabla de **diez** filas, la décima incorporada con `E-8` bajo el rótulo **[DECISIÓN 2026-08-09]**, y la Fase C de ese proyecto de código ya había resuelto la lectura en diez.

**Esta categoría aplicó diez y no bajó la batería a nueve para que coincidiera con la redacción de la puerta.** **Hasta 1.19** esa redacción decía nueve en **dos** gates —§17.3.P.8 y §17.5.P.8—, por ser anterior a la incorporación del décimo caso; **el intake la corrigió en 1.20**, junto con §17.3.P.6, §17.2.P.11 y el encabezado de §21, sobre el hallazgo que levantaron esta categoría y la de `GeometriaFactory-Infrastructure`. El desenlace queda registrado en [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8, y la categoría 08 de `GeometriaFactory-Infrastructure` lo registra del mismo modo desde su lado.

### 3.3 Las puertas técnicas y la frontera del despliegue

Se declaran aparte de los gates porque su consecuencia es distinta: el intake §15 declara que **una puerta que no pasa detiene la planificación de las etapas que dependen de ella y no se arrastra como deuda**.

| Puerta | Qué mide | Dónde se mide | Qué condiciona |
| --- | --- | --- | --- |
| `PT-04` | Que la imagen se construya con su archivo de construcción **multietapa** y arranque desde el contenedor de desarrollo, aplique las transformaciones sobre un almacén vacío y **responda salud** | Etapa `a` | Que el artefacto del servidor propio se pueda construir y arrancar |
| `PT-05` | La premisa completa de la topología, en el **despliegue real** | Etapa `i`, fuera del tramo comprometido | El despliegue real. La fuente **recomienda no relegarla** |

**Y una frontera que esta categoría no cruza.** El intake §17.5.P.8 declara el despliegue **manual, por el docente**, y que **el agente entrega el archivo de construcción y el de composición y no ejecuta el despliegue**. En consecuencia, **ningún criterio de esta categoría se cumple ejecutando un despliegue**: lo que se verifica es que el artefacto se construya, arranque y responda, y el resto es una acción del Product Owner.

## 4. Roles de calidad dentro del equipo

`equipo_n` es **1** (intake §2): la misma persona diseña las pruebas, las ejecuta y aprueba el cierre.

| Papel | Quién | Qué le corresponde |
| --- | --- | --- |
| AG-08, calidad y pruebas | La única persona del equipo, en este papel | Diseñar los casos de verificación, mantener la matriz de cobertura y la Definition of Done, y **mantener la batería de integración del producto**, que vive en este proyecto de código |
| Product Owner | El docente de la cátedra, que es también quien ejecuta | Aprobar el cierre de cada etapa en su punto de control, confirmar los valores rotulados [ASUNCIÓN] y **ejecutar el despliegue**, que no es del agente |
| Revisión mecánica | El pipeline | Los quince gates de §3, en sus etapas: `build`, `test`, cobertura e **imagen** |

**Lo que reemplaza al revisor humano independiente es el punto de control bloqueante de cada etapa** (intake §15, regla de delivery 2). Esta categoría no inventa un segundo revisor que no existe.

## 5. Cadencia de revisión

| Momento | Qué se revisa | Qué produce |
| --- | --- | --- |
| Al abrir la rama de cada etapa | Qué casos de verificación entran en alcance, y **qué puntos de acceso nuevos entran a la guardia** | El alcance de testing de la etapa, en [`Plan-Pruebas.md`](Plan-Pruebas.md) §5 |
| **Ante todo punto de acceso nuevo** | Que quede dentro de la guardia, o que su exención esté entre las **cuatro** declaradas | `TC-07` reejecutado, con el recuento de los quince en las dos direcciones. **Es el control que más veces hay que ejercer** |
| Al cerrar cada etapa | La matriz de cobertura entera; el estado de cada `TC-XX`; y **la batería de integración completa** | Matriz actualizada y la constancia de los gates medidos |
| Al cerrar la etapa `c` | Los valores rotulados [ASUNCIÓN] | La confirmación del Product Owner, o su continuidad como asunción |
| Ante todo defecto cerrado | Que exista al menos un `TC-XX` nuevo o extendido que lo prevenga | La entrada correspondiente en el catálogo de casos de verificación |

**La cadencia es por etapa y no por sprint**, porque este producto no tiene sprints. **No se declara ninguna frecuencia calendaria**: el intake declara «sin plazo calendario; el avance se mide por etapas cerradas».

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-01`.** §3.2 afirmaba en presente que el intake escribe «las nueve pruebas del validador» en §17.5.P.8. **El intake 1.20 dice diez**, y lo corrigió en el mismo commit que emitió este documento. §3.2 pasa a describir el estado vivo de la fuente —«incluidas las **diez** pruebas del validador»— y ubica el nueve **hasta 1.19**, con el desenlace de la corrección en lugar de una remediación pendiente del Product Owner. Ninguna decisión de prueba, umbral ni caso cambia: la batería era y sigue siendo de **diez**. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la definición de calidad en sus tres partes —ningún punto fuera de su guardia, ninguna traducción que deshaga una decisión de adentro y ninguna petición atendida sobre un almacén no preparado—, con la constancia de que **acá es donde una decisión correcta de una capa de adentro se puede perder sin que nada falle** y de que este proyecto de código aloja **la batería de integración del producto**. Declara los ocho atributos ISO 25010 y los **quince** quality gates con condición, verificación y consecuencia —cuatro condicionados por depender de un valor rotulado [ASUNCIÓN], con la precisión de que la **inversión** de la pirámide no es asunción aunque su reparto sí lo sea—. Su §3.2 declara que la batería del validador que corre desde acá tiene **diez** casos y que el intake escribe «nueve» en dos lugares por residuo. Su §3.3 declara las **dos** puertas técnicas y **la frontera del despliegue**, que es manual y del Product Owner. |
