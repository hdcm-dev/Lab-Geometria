# Mini-Plan — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Mini-Plan.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API PM (AG-07)
**Tipo de proyecto de código (D8):** `rest-api`
**Trazabilidad upstream:** [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) **1.1** (seis épicas, treinta historias), [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) 1.0 (veintiséis tareas técnicas) y [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) 1.0; [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) 1.5 §2.1, §2.2, §4 y §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §2, §10, §13, §15, §16.1, §17.5, §18 y §22; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.1 §4, §5, §8, §9 y §11
**Trazabilidad downstream:** `08-Calidad-Y-Pruebas`, `09-Devops`, `10-Examples` y `11-Documentacion` de GeometriaFactory-Api

---

## Tabla de contenido

- [1. Información general](#1-información-general)
  - [1.1 Por qué esta categoría emite un mini-plan y no planes de iteración](#11-por-qué-esta-categoría-emite-un-mini-plan-y-no-planes-de-iteración)
  - [1.2 Capacidad disponible](#12-capacidad-disponible)
- [2. Objetivo de cada tramo](#2-objetivo-de-cada-tramo)
- [3. Ítems comprometidos por tramo](#3-ítems-comprometidos-por-tramo)
- [4. Alcance técnico y orden de construcción](#4-alcance-técnico-y-orden-de-construcción)
- [5. Definition of Done aplicada](#5-definition-of-done-aplicada)
- [6. Riesgos y mitigaciones](#6-riesgos-y-mitigaciones)
- [7. Criterios de hecho de cada tramo](#7-criterios-de-hecho-de-cada-tramo)
- [8. Trazabilidad](#8-trazabilidad)
- [9. Bitácora de avance](#9-bitácora-de-avance)
- [10. Control de cambios](#10-control-de-cambios)

---

## 1. Información general

| Campo | Valor |
| --- | --- |
| Unidad de planificación | La **etapa** del producto, no el sprint (`Roadmap-Producto.md` §1.2) |
| Etapas comprometidas del producto | **Ocho**, `a` a `h` (`PRODUCT-INTAKE` §15) |
| Etapas que toca este proyecto de código | **Seis**: `a`, `c`, `d`, `e`, `f` y `h` |
| Duración de cada etapa | **Sin fecha.** El avance se mide por etapas cerradas (`Roadmap-Producto.md` §1.1) |
| Tamaño del equipo | `equipo_n = 1` (`PRODUCT-INTAKE` §2) |
| Unidad de estimación | **Sin fijar**, por [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §4.1 |
| Nivel topológico | **3**, el último: es el **único de los siete que ensambla a los demás** (`PRODUCT-INTAKE` §13) |
| Unidad de despliegue | **Una imagen de contenedor**, y es la unidad desplegable del backend. Es una de las **dos** del producto |
| Puertas técnicas propias | **`PT-04`**, medida en la etapa `a`: la imagen se construye, arranca, aplica las transformaciones sobre almacén vacío y responde salud |
| Paralelismo entre etapas | **Ninguno** (`Roadmap-Producto.md` §4) |

### 1.1 Por qué esta categoría emite un mini-plan y no planes de iteración

El intake declara **`equipo_n = 1`** en su §2, y de ese dato el framework deriva que la categoría 07 emita **únicamente** `Mini-Plan.md`; `Roadmap-Producto.md` lo declara en su §2.1, en su §3 y en su §6. **No se emiten** `Plan-Iteracion-Sprint-XX.md`, `Template-Sprint-Review.md`, `Template-Sprint-Retrospectiva.md` ni `Velocidad-Equipo.md`.

**Y hay un segundo motivo**: este producto **no planifica en sprints**. Su ciclo es etapa, informe de cierre, punto de control bloqueante y fusión.

### 1.2 Capacidad disponible

**No se declara capacidad numérica, y es deliberado.** Ninguna fuente da base: sin plazo calendario, sin iteraciones cerradas y con una sola persona.

Y hay un motivo propio, que en este proyecto de código es el más pesado del producto: de los **diecisiete** requerimientos no funcionales de `05` §8, **cinco vienen rotulados como asunción** y siguen pendientes de confirmación —latencia, caudal, arranque en frío, cobertura y **la forma misma de la pirámide de pruebas**—. Es la **mayor concentración de valores sin confirmar de los siete proyectos de código**. Este plan **los usa como vigentes porque no los inventó**, y agregarle una capacidad en puntos sería inventar el sexto.

Lo que **sí** limita la capacidad y está declarado es el **cuello de diseño**: el punto de control de cada etapa (`PRODUCT-INTAKE` §10).

## 2. Objetivo de cada tramo

| Etapa | Objetivo de este proyecto de código al cerrar la etapa |
| --- | --- |
| `a` | El servicio arranca en dos fases con los **cuatro** puertos conectados, deja el almacén en condiciones o **se detiene**, responde salud sin exigir acceso, y su imagen se construye y arranca: **`PT-04` medida**. Y queda verificado que **la sesión interactiva del front no llega hasta acá**. |
| `c` | El canje de credenciales funciona con sus dos respuestas —la genérica y la que declara el motivo—, la guardia admite los once puntos que exigen acceso, y las **dos** traducciones ocurren en una tabla única sin códigos inventados. |
| `d` | El administrador gobierna la comisión desde la superficie: listado, cambio de situación con la provisoria devuelta, baja con el correo escrito y reseteo, y **ningún punto queda fuera de la guardia del cambio pendiente salvo uno**. |
| `e` | Los cinco puntos sobre trabajos están en pie, con el texto que **no se normaliza en el borde**, la eliminación verificada **forzando la petición** y el listado sin parámetro para pedir borradores ajenos. |
| `f` | El envío y el reenvío **responden con éxito** transportando el estado que la interpretación decidió, y el texto viaja byte a byte. |
| `h` | El desenlace está expuesto con su terminalidad, y **la colección de peticiones se reproduce en cinco pasos o menos sin datos inventados**. |

**Las etapas `b` y `g` no producen trabajo en este proyecto de código**, y por eso no tienen fila. El motivo está en [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2: la `b` no agrega ningún punto de acceso, y **todo lo que la `g` necesita de esta superficie ya está expuesto en la `e`**.

## 3. Ítems comprometidos por tramo

Los identificadores son los del backlog de 06 y **ninguno se inventa acá**.

| Etapa | ID | Tipo | Descripción corta | Prioridad | Estimación | Asignado | Estado |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `a` | BT-00001 | Tarea técnica | Crear el proyecto de código y su proyecto de pruebas de integración | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-00005 | Tarea técnica | Anclar nombres, espacios de nombres y versiones de paquetes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-00002 | Tarea técnica | Composición de raíz con los cuatro puertos y sus adaptadores | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-00007 | Tarea técnica | Fijar rutas y verbos de los quince puntos en el punto de control | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-00008 | Tarea técnica | Fijar el formato de intercambio para los dos extremos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-00009 | Tarea técnica | Fijar el límite de cuerpo que rechaza y nunca trunca | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-00010 | Tarea técnica | Fijar la vigencia del acceso firmado | Media | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-00003 | Tarea técnica | Arranque en dos fases con el punto de salud sin acceso | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-00006 | Tarea técnica | Puerta de construcción con cero advertencias | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-00004 | Tarea técnica | Imagen multietapa y medición de `PT-04` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | US-00026 | Historia | Conectar cada puerto con su adaptador y tomar la configuración | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | US-00027 | Historia | Aplicar las transformaciones de esquema al arrancar | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | US-00028 | Historia | Detener el arranque en lugar de atender sobre un almacén dudoso | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | US-00029 | Historia | Responder por el estado del servicio sin exigir acceso | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-00011 | Tarea técnica | Guardia de admisión transversal | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-00013 | Tarea técnica | Traductor con la tabla única, sin códigos inventados | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-00016 | Tarea técnica | Superficie de acceso y credencial propia | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-00022 | Tarea técnica | Batería de integración con la pirámide invertida | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-00014 | Tarea técnica | Prueba de las tres familias deliberadamente empobrecidas | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-00012 | Tarea técnica | Inspección de los quince puntos contra la guardia | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-00015 | Tarea técnica | Elevar los dos huecos del conjunto cerrado de códigos | Media | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-00001 | Historia | Canjear correo y contraseña por un acceso firmado | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-00002 | Historia | Responder credenciales inválidas sin declarar qué campo falló | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-00003 | Historia | Responder con motivo a la cuenta `Pendiente` o `Bloqueado` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-00004 | Historia | Rechazar toda petición sin acceso, vencido o con firma ajena | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-00005 | Historia | Exigir el papel declarado por cada punto de acceso | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-00008 | Historia | Configurar la cuenta de administrador sólo mientras no exista ninguna | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-00010 | Historia | Cambiar la contraseña propia exigiendo la vigente | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-00024 | Historia | Traducir cada código del contrato al código de respuesta | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-00025 | Historia | Responder sin exponer direcciones internas y registrar en el servidor | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-00017 | Tarea técnica | Superficie de gobierno de la comisión | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-00025 | Tarea técnica | Confirmar los cinco valores rotulados como asunción | Media | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-00006 | Historia | Guardia del cambio pendiente en todos los puntos salvo uno | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-00007 | Historia | Registrar una cuenta de alumno sin campo de contraseña | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-00009 | Historia | Cambiar la contraseña propia con la provisoria como vigente | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-00011 | Historia | Listar las cuentas de la comisión con su situación y su marca | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-00012 | Historia | Cambiar la situación de una cuenta con verificación de papel | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-00013 | Historia | Dar de baja transportando el correo escrito como confirmación | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-00014 | Historia | Resetear y devolver la provisoria una sola vez | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-00015 | Historia | No exigir ni comprobar la situación de la cuenta al resetear | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-00016 | Historia | No registrar la provisoria en ninguna traza | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-00018 | Tarea técnica | Superficie de trabajos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-00024 | Tarea técnica | Prueba del texto byte a byte y del rechazo sin truncamiento | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-00023 | Tarea técnica | Prueba de eliminación forzada contra la superficie | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-00019 | Historia | Transportar el texto original sin normalizarlo en el borde | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-00020 | Historia | Eliminar con los dos alcances, verificado forzando la petición | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-00021 | Historia | Listar sin parámetro para pedir borradores ajenos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-00022 | Historia | Detalle con piezas, componentes, observaciones y comentario | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-00017 | Historia | Enviar un trabajo nuevo y recibir el estado que la interpretación decidió | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-00018 | Historia | Reenviar un trabajo en `Borrador` con el texto corregido | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | BT-00019 | Tarea técnica | Superficie de desenlace | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | BT-00020 | Tarea técnica | Colección de peticiones reproducible | Media | Sin fijar | Equipo (1) | Pendiente |
| `h` | BT-00021 | Tarea técnica | Elevar el alcance de la colección de peticiones | Media | Sin fijar | Equipo (1) | Pendiente |
| `h` | BT-00026 | Tarea técnica | Probar una vez la construcción de la imagen en destino | Media | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-00023 | Historia | Aprobar o rechazar un trabajo en estado `Pendiente` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-00030 | Historia | Ejercitar la superficie con una colección reproducible | Media | Sin fijar | Equipo (1) | Pendiente |

**Total comprometido: 30 historias y 26 tareas técnicas**, repartidas en seis etapas. La prioridad de la columna es de **ejecución dentro de la etapa** y no reemplaza a la MoSCoW del backlog.

**US-00030 figura con prioridad de ejecución `Media`**, y su MoSCoW en 06 es `Should`: es la única historia de este backlog donde las dos coinciden en señalar lo mismo.

## 4. Alcance técnico y orden de construcción

Esta sección **no redefine arquitectura**: referencia la de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md), ni redefine la superficie: los quince puntos están en la categoría 02 y su contrato en [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md).

**Orden**, derivado de las dependencias de [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) §3:

1. `a`: BT-00001 y BT-00005 primero; BT-00002 sobre ellos; **BT-00007, BT-00008, BT-00009 y BT-00010 en el mismo tramo**, porque las cuatro son decisiones que se validan en el punto de control y **dos de ellas obligan o afectan a otro proyecto de código**; BT-00003 sobre BT-00002; las cuatro historias; **BT-00006 y BT-00004 al cerrar**, porque son puertas y BT-00004 es `PT-04`.
2. `c`: BT-00011 y BT-00013 primero —la guardia y el traductor son transversales—; BT-00016 sobre ellos; BT-00022 se abre acá y **acompaña todas las etapas siguientes**; las nueve historias; **BT-00014, BT-00012 y BT-00015 al cerrar**, porque son inspecciones y elevaciones sobre algo ya construido.
3. `d`: BT-00017 sobre BT-00011 y BT-00013; las nueve historias; **BT-00012 se vuelve a correr** por los puntos que la etapa agrega; BT-00025 antes del punto de control.
4. `e`: BT-00018 sobre BT-00008, BT-00011 y BT-00013; las cuatro historias; **BT-00023 y BT-00024 al cerrar**, porque son las dos pruebas de criterio propio del producto.
5. `f`: las dos historias sobre BT-00018; **BT-00012 se vuelve a correr**.
6. `h`: BT-00019 sobre BT-00011 y BT-00013; US-00023 después; **BT-00020 al final**, porque la colección recorre la superficie entera e incluye la aprobación y el rechazo; BT-00021 y BT-00026 antes del punto de control.

**Reglas de dependencia interna que ninguna tarea puede cruzar** (`05` §3.2): **ninguna superficie depende de otra superficie** —un punto que invocara a otro sería una petición encadenada, y **una petición ejerce a lo sumo un caso de uso**—; **el traductor está después de las cinco superficies**, incluidas las que no exigen acceso, de modo que **ningún camino de fallo sale sin pasar por la tabla única**; y **la composición de raíz no atiende peticiones**: construye el grafo y desaparece.

**Consecuencia del nivel topológico 3**: dentro de cada etapa, el trabajo de este proyecto de código va **último**. Y hay una consecuencia que no es de compilación: **`GeometriaFactory-Web` lo alcanza por HTTP en tiempo de ejecución**, de modo que una etapa no se demuestra sin que los dos extremos estén en pie.

## 5. Definition of Done aplicada

**La DoD canónica vive en `08-Calidad-Y-Pruebas` y todavía no está emitida.** Este plan la referencia por destino y **no la redefine**; hasta que exista, lo que gobierna el cierre son los criterios de transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5.

Criterios específicos que este plan agrega:

1. **La actualización de la categoría 11 forma parte del cierre.** La categoría 11 de este proyecto de código todavía no está emitida; hasta su emisión la condición se cumple de forma vacía y **se registra así en el informe de cierre**.
2. **Las dos inspecciones se corren en cada etapa que agregue un punto o un código**, no sólo en la que las introdujo: los quince puntos contra la guardia y los diecisiete códigos contra la tabla de traducción, **las dos en las dos direcciones**.
3. **Las dos pruebas de criterio propio del producto se ejecutan y pasan**: la **eliminación forzada contra la superficie** —el único criterio que la fuente exige ejercer así— y la del **texto byte a byte con rechazo sin truncamiento**.
4. **Ningún guion de prueba que involucre el texto de figuras usa datos inventados**: los cuerpos son los escenarios `E-1` a `E-8` del intake §20.
5. **Los cinco valores rotulados [ASUNCIÓN] se usan como vigentes y la puerta de cobertura no se declara bloqueante en 09** hasta que BT-00025 cierre.
6. **La imagen se construye con el archivo multietapa, arranca, aplica las transformaciones sobre almacén vacío y responde salud** antes de considerar cerrada la etapa `a`. Es `PT-04`.

## 6. Riesgos y mitigaciones

| Riesgo | Probabilidad | Impacto | Mitigación |
| --- | --- | --- | --- |
| Que un punto de acceso nuevo quede fuera de la guardia del cambio de contraseña pendiente | **Alta**: es un defecto de omisión, y **los defectos de omisión no se ven leyendo el punto nuevo** | **Muy alto**: `RN-00013` e `INV-09` dejan de valer y **nada falla**; una cuenta con la marca puesta ejercería una capacidad y ninguna capa de adentro se enteraría | BT-00012, con el NFR de **exactamente 4** puntos fuera de la guardia y la inspección que recorre los quince **en las dos direcciones**, corrida en cada etapa que agregue un punto |
| Que el trabajo ajeno responda «no autorizado» en lugar de «no encontrado» | Media: es la traducción que **parece más informativa** y por eso es la tentadora | **Muy alto**: confirma la existencia de un recurso ajeno y **ninguna capa de adentro puede repararlo** | BT-00013 con su fila única en la tabla de traducción, y BT-00014, que compara las **dos** respuestas y verifica que son indistinguibles en cuerpo y en código |
| Que el límite de tamaño del cuerpo trunque el texto de un alumno en lugar de rechazarlo | Media: **truncar es el comportamiento por defecto de varias capas de transporte** | Alto: **rompe `RN-00008` en silencio**; el trabajo se guarda, el texto queda mutilado y el alumno lo descubre al ver el dibujo | BT-00009, con la forma de rechazo **no configurable**, y BT-00024, con **0** truncamientos y la comparación byte a byte |
| Que los dos extremos serialicen distinto y el contrato deje de ser el mismo | Media, **y es el trade-off que el ensamblado de contratos aceptó por escrito** al no imponer formato | Alto: el fallo aparece en tiempo de ejecución y **no lo detecta la compilación**, que es la única red que este producto tiene | BT-00008, con **una sola** configuración declarada para los dos extremos, y BT-00022, que la verifica **golpeando el servicio real** |
| Que un envío cuyo texto no verifica responda con un código de fallo | Media: **es la lectura intuitiva de «no verificó»** | Medio: le diría a la persona que su petición estaba mal cuando **el trabajo ya quedó guardado** | US-00017 y la declaración de la superficie: **es una respuesta exitosa**, con el estado y las observaciones en el cuerpo |
| Que se agregue un punto pensado para el navegador o se configure el intercambio de origen cruzado | Baja, **pero el costo de equivocarse es de rediseño** | **Muy alto**: reabre las tres propiedades de la topología y rompe `RA-01` | Las **tres ausencias declaradas** de la superficie de 02, que dejan escrito lo que las repone, y el hecho de que **el único cliente legítimo esté declarado en el manifiesto y en el grafo** |
| Que la composición de raíz deje un puerto sin adaptador y el fallo aparezca en la primera petición | Media | Medio: el servicio arranca y falla al primer uso, **en producción y sin nadie mirando** | BT-00002, con composición **única**, resolución verificada en el arranque, NFR de **4 de 4** y **fallo en construcción** |
| Que el listado de la comisión crezca por encima de lo que el requerimiento de tiempo sostiene | Baja en el alcance declarado —una comisión durante una clase— | Medio: la pantalla más pesada del producto deja de cumplir su percentil | La decisión de no paginar está tomada **con condición de reingreso escrita**: cuando la medición deje de cumplirse, entra paginación, y **es un cambio del ensamblado de contratos** |
| Que el mecanismo de construcción de la imagen en destino no funcione y el despliegue quede sin camino | Media, **y la fuente lo rotula [A VERIFICAR]** por su cuenta | Alto: **es el único canal de entrega declarado** | BT-00026, que lo prueba **una vez antes de depender de él**, tal como el intake exige; la salida documentada y **no adoptada** es el túnel saliente |

## 7. Criterios de hecho de cada tramo

Una etapa de este proyecto de código está hecha cuando:

- [ ] Todas sus historias y tareas comprometidas en §3 están en estado terminado.
- [ ] Los criterios comunes a toda transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5.1 se cumplen, incluida la no regresión sin correcciones.
- [ ] Los criterios propios de la transición correspondiente de su §5.2 que alcanzan a este proyecto de código se cumplen.
- [ ] **Las dos inspecciones en las dos direcciones pasan**: los quince puntos contra la guardia y los diecisiete códigos contra la tabla de traducción.
- [ ] La batería de integración corre entera contra el servicio real y el almacén real.
- [ ] Para la etapa `a`: **`PT-04` está medida** y está verificado que **la sesión interactiva del front no llega hasta acá**.
- [ ] Para la etapa `e`: la **eliminación forzada** y la prueba del **texto byte a byte** pasan.
- [ ] Para la etapa `h`: la **colección de peticiones se reproduce en cinco pasos o menos, sin datos inventados**.
- [ ] El informe de cierre de la etapa está escrito y es autocontenido, con su índice.
- [ ] Los documentos de la categoría 11 afectados están revisados, o se registra que la categoría todavía no está emitida.
- [ ] El Product Owner dio **OK explícito** en el punto de control, y la rama está incorporada antes de abrir la siguiente.

## 8. Trazabilidad

| Etapa | NB que avanzan | CU que avanzan | ADR que gobiernan las decisiones |
| --- | --- | --- | --- |
| `a` | NB-00003, **NB-00008**, que recibe acá su primer tramo propio y no parcial | CU-00010, CU-00011 | ADR-00001, ADR-00002, ADR-00006, ADR-00007, ADR-00008 |
| `c` | NB-00001, NB-00002, NB-00004, NB-00008 | CU-00001, CU-00002, CU-00003, CU-00009 | ADR-00001, ADR-00003, ADR-00004 |
| `d` | NB-00001, NB-00002 | CU-00002, CU-00003, CU-00004, CU-00005 | ADR-00003, ADR-00004 |
| `e` | NB-00003, NB-00005 (parcial), NB-00006 (parcial), NB-00007 (parcial), NB-00009 (parcial) | CU-00006, CU-00007 | ADR-00002, ADR-00004, ADR-00005 |
| `f` | NB-00003, NB-00004 | CU-00006 | ADR-00002, ADR-00004 |
| `h` | NB-00009 | CU-00008, CU-00012 | ADR-00004, ADR-00008 |

**Las nueve necesidades de negocio avanzan en alguna etapa de este proyecto de código**, y **`NB-00008` recibe acá su primer tramo propio y no parcial**: `GeometriaFactory-Application` declara que no la toca y `GeometriaFactory-Infrastructure` la declara parcial. Su dolor es de acceso y de despliegue, y **es acá donde el producto se vuelve alcanzable**.

**Puertas técnicas del producto y este proyecto de código.** **`PT-04` es de esta pieza** y se mide en la etapa `a`. `PT-01` es del front —y su parte `PT-01.d` consulta el punto de salud que esta pieza expone—, `PT-02` y `PT-03` son del bundle del visor y de su anfitrión, y `PT-05` es del despliegue real de la fase `i`. Lo que alcanza a este proyecto de código de las otras es la consecuencia: **una puerta que no pasa detiene la planificación de las etapas que dependen de ella**.

## 9. Bitácora de avance

**Sin entradas al 2026-08-10.** Ninguna etapa está abierta: el producto está en fase de especificación.

| Fecha | Etapa | Qué se cerró | Qué quedó abierto | Punto de control |
| --- | --- | --- | --- | --- |
| — | — | — | — | — |

La bitácora se completa **al cerrar cada etapa**, junto con el informe de cierre. Para la etapa `a` lo que se registra es el **resultado de `PT-04`** y **las rutas y los verbos que el punto de control validó**; para la `h`, el **resultado de la colección de peticiones**.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del mini-plan de `GeometriaFactory-Api`, proyecto de código principal del producto. Declara por qué la categoría emite un único artefacto —`equipo_n = 1`, con los cuatro omitidos— y por qué no se declara capacidad numérica, con el fundamento propio de que **cinco de los diecisiete requerimientos no funcionales ya vienen rotulados como asunción**, la mayor concentración del producto. Fija el objetivo de cada uno de los **seis** tramos que este proyecto de código toca, compromete las **30** historias y las **26** tareas técnicas del backlog de 06 sin inventar ningún identificador, declara el orden de construcción con las cuatro decisiones de frontera en la etapa `a` y con las tres reglas de dependencia interna que ninguna tarea puede cruzar, referencia la Definition of Done por destino con la constancia de que 08 todavía no está emitida, y declara **nueve** riesgos con mitigación, tres de ellos de impacto **muy alto**, incluidos los dos por los que esta capa puede romper una regla de negocio hacia afuera sin que ninguna capa de adentro se entere. |
| 1.1 | 2026-08-11 | **Actualiza la trazabilidad upstream** a la versión del `Product-Backlog.md` de la sección 06, que subieron a **1.1** el 2026-08-11. El `Product-Backlog.md` subió al absorber la promoción de `F-13` a `Must Have` (`PRODUCT-INTAKE` **1.19** §4) y al declarar la regularidad de la distribución MoSCoW (hallazgo `D-06-03`). **Ninguna historia ni tarea técnica de este proyecto de código cambia de prioridad, de etapa ni de tramo**, y ningún compromiso, riesgo ni orden de construcción de este plan se toca: la fila existe para que la versión citada sea la vigente y no una que ya no está. Sube minor. |
| 1.2 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **2**. Sube minor. |
