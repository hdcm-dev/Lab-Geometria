# 05 · Arquitectura técnica — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** README.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Tipo de proyecto de código (D8):** `rest-api`

---

## 1. Punto de entrada

`GeometriaFactory-Api` es el **proyecto de código principal** del producto y el único que ensambla a los demás: **quince** puntos de acceso, la composición de raíz que conecta los cuatro puertos con sus adaptadores, la guardia de admisión y las dos traducciones que convierten los **quince** códigos vivos del contrato en **diez** códigos de respuesta. Es nivel 3 del orden topológico, el último, y **nadie depende de él por compilación**: lo alcanza `GeometriaFactory-Web` por HTTP, en tiempo de ejecución.

Lo que hay que haber entendido antes de tocar esta sección, y que atraviesa los cinco documentos: **acá se decide cómo se dice y no qué se dice**, y **acá es donde dos reglas de negocio se rompen hacia afuera sin que ninguna capa de adentro se entere**. Son `RN-03` —si el trabajo ajeno deja de ser indistinguible del inexistente— y `RN-13` —si un punto de acceso queda fuera de la guardia—. El punto de entrada es [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md).

## 2. Documentos de esta sección

| Documento | Propósito |
| --- | --- |
| [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) | Documento maestro: estilo, las cuatro vistas mínimas, cross-cutting, diecisiete NFR, nueve riesgos, trazabilidad de las dieciséis reglas, de los nueve invariantes y de las tres reglas de arquitectura, y diez puntos abiertos |
| [`Decisiones-Arquitectura.md`](Decisiones-Arquitectura.md) | Índice de las ocho ADR, con la correspondencia contra los cinco temas que el tipo exige y la tabla de consulta rápida por preocupación |
| [`Contratos-REST.md`](Contratos-REST.md) | Contrato de la superficie: quince puntos, diez códigos de respuesta, **la tabla de traducción de los quince códigos del contrato**, el formato de intercambio y el versionado |
| [`Adrs/`](Adrs/) | Las ocho decisiones, una por archivo |

## 3. ADR vigentes

| ADR | Título | Categoría | Estado |
| --- | --- | --- | --- |
| [ADR-01](Adrs/ADR-01-Host-Delgado-Con-Composicion-De-Raiz-Unica.md) | Host delgado, con la composición de raíz como único lugar de ensamblado | Estilo | Propuesto |
| [ADR-02](Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md) | El formato de intercambio y su configuración, fijados para los dos extremos | Comunicación | Propuesto |
| [ADR-03](Adrs/ADR-03-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md) | Credencial firmada, papel exigido por punto y una guardia transversal sin excepciones sueltas | Seguridad | Propuesto |
| [ADR-04](Adrs/ADR-04-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md) | Dos traducciones en orden, con una tabla única y sin inventar códigos | Comunicación | Propuesto |
| [ADR-05](Adrs/ADR-05-Sin-Paginacion-Con-Condicion-De-Reingreso-Declarada.md) | Sin paginación, con su condición de reingreso declarada | Comunicación | Propuesto |
| [ADR-06](Adrs/ADR-06-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md) | Composición de raíz única: ciclos de vida y configuración en un solo lugar | Persistencia | Propuesto |
| [ADR-07](Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) | Arranque en dos fases, y un punto de salud que no exige acceso | Despliegue | Propuesto |
| [ADR-08](Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) | Sin versionado de rutas, con despliegue conjunto como regla operativa | Despliegue | Propuesto |

Ninguna superada, ninguna rechazada.

## 4. NFR vigentes

Los **diecisiete**, con su objetivo numérico y su mecanismo, están en [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §8. En una línea: percentil 99 del listado por debajo de **500 ms** medido en el servidor, **20** peticiones por minuto sostenidas, arranque en frío por debajo de **30 segundos**, cobertura **75 %** de líneas y **70 %** de ramas, pirámide **60/40** invertida a propósito —los cinco rotulados `[ASUNCIÓN]` por el intake—, **4** puntos fuera de la guardia y ni uno más, **0** puntos que fijen contraseña sin credencial, **14 de 15** códigos con traducción declarada y **1** sin destino, **3 de 3** comparaciones de respuestas indistinguibles, **0** respuestas con dirección, ruta, secreto o traza, **1** sola configuración de intercambio en todo el producto, **0** textos alterados en el borde y **0** truncamientos, **4 de 4** puertos conectados, **0** peticiones atendidas con la preparación incompleta, **0** eliminaciones fuera de alcance al forzar la petición, **0** advertencias de construcción y la colección reproducible en **5 pasos o menos**.

## 5. Orden de lectura sugerido

1. [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §2 y §3 — el estilo, los ocho componentes y, sobre todo, **§2.2**, las siete decisiones de los cuatro proyectos de código que este ensambla y no reabre. Sin eso, tres de las ocho ADR se leen como decisiones sueltas en lugar de como el cierre de puntos que otras categorías reasignaron acá.
2. [`Adrs/ADR-03`](Adrs/ADR-03-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md) junto con [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §3.4 — la guardia y la tabla de los quince puntos, que se leen mejor juntas. **Es la propiedad que se rompe agregando un punto y olvidándose.**
3. [`Contratos-REST.md`](Contratos-REST.md) §5 junto con [`Adrs/ADR-04`](Adrs/ADR-04-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md) — la tabla de traducción y la decisión que la gobierna. Acá está el error más caro que esta capa puede cometer.
4. [`Adrs/ADR-02`](Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md) — el formato de intercambio, que **obliga a los dos extremos** y que `GeometriaFactory-Web` adopta.
5. [`Adrs/ADR-06`](Adrs/ADR-06-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md) y [`ADR-07`](Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) — cómo se arma y cómo arranca, que es lo que hace que un despliegue mal configurado no acepte ni un trabajo.
6. [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §10 — la trazabilidad, para consultar por regla, por invariante o por regla de arquitectura. **§10.4 es la única del producto donde las tres reglas de arquitectura tienen tratamiento**, porque acá está la frontera.

## 6. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Modelo-Datos-Logico.md` | **Omitido**, y es una omisión declarada y no un incumplimiento | La guía lo exige para el tipo `rest-api`, y **el modelo lógico del producto ya está emitido**, en [`../../GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Modelo-Datos-Logico.md`](../../GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Modelo-Datos-Logico.md), con sus cinco tablas, sus seis índices y sus quince restricciones. El flag `tiene_persistencia` vale true acá porque **toma de configuración la ruta del almacén y dispara las transformaciones al arrancar**, no porque modele el dato: el intake lo dice en una línea, «delega en `GeometriaFactory.Infrastructure`». Redactarlo de nuevo crearía dos descripciones del mismo dato guardado. Es el mismo criterio con el que la categoría 02 omitió su modelo conceptual. Lo que sí se documenta acá es lo que esta capa hace con él, en [`ADR-06`](Adrs/ADR-06-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md) y [`ADR-07`](Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) |
| Descripción formal de servicio | **Omitida**, y es un **apartamiento declarado** | La guía la exige para el tipo `rest-api`; **la fuente decide lo contrario por escrito y con fundamento** (`PRODUCT-INTAKE` §17.4.P.2 y §17.4.P.12): con dos consumidores compilados juntos, el costo de la cadena de herramientas no se paga. Emitirla crearía **una segunda fuente de verdad sobre la misma superficie**, que envejecería sin que nada la compare. El contrato formal del producto es el ensamblado de tipos de transferencia, y [`Contratos-REST.md`](Contratos-REST.md) §2.1 declara el apartamiento en su lugar |
| `Flujo-Ejecucion.md` | **Omitido** | La guía lo **recomienda** para `rest-api` con orquestación de varios pasos, y este proyecto de código no la tiene: **una petición ejerce a lo sumo un caso de uso** y no hay pipeline que describir. Los dos recorridos que sí tienen pasos están documentados donde corresponde: el de una petición, en [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §4 y en la tabla de traducción; el del arranque, en [`ADR-07`](Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md). El único motor de procesamiento del producto es el validador de figuras, y su flujo vive en [`../../GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Flujo-Ejecucion.md`](../../GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Flujo-Ejecucion.md) |
| `Extensibilidad.md` | **Omitido** | `tiene_extensibilidad` es **false** (`PRODUCT-MANIFEST` §5), y hacia esta superficie **no hay extensión posible por diseño**: su único cliente legítimo está declarado, y agregar otro rompe `RA-01` |
| `_legacy/` | **No existe** | Es la primera emisión de esta categoría en este proyecto de código: no hay ninguna versión superada que archivar |

## 7. Lo que esta sección resolvió de lo que aguas arriba quedó abierto

Cinco puntos abiertos llegaron a esta categoría desde otras Fases C, y conviene decir qué pasó con cada uno.

| Punto que llegó | Qué hizo esta sección |
| --- | --- |
| El **formato de intercambio y su configuración**, derivado por la Fase C de `GeometriaFactory-Contracts` a las categorías 05 de este proyecto de código y de `GeometriaFactory-Web`, y devuelto por aquélla a ésta por ser la del **productor** | **Resuelto, y para los dos extremos.** [`ADR-02`](Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md) y [`Contratos-REST.md`](Contratos-REST.md) §2.2: **ocho filas**, de las cuales las **seis reglas de formato** están elegidas para que **ninguna dependa de que dos configuraciones coincidan**, y las otras dos —la notación y la prohibición de normalizar el texto original— rigen la misma frontera sin ser reglas de formato. El cuadre **6 + 1 + 1 = 8** está en `ADR-02` §2 y en `Contratos-REST.md` §2.2. `GeometriaFactory-Web` ya declaró que la adopta |
| El **límite de tamaño del cuerpo**, reasignado por la Fase C de `GeometriaFactory-Infrastructure` con la exigencia de que el borde **rechace y nunca trunque** | **Resuelto en su forma, abierto en su número.** [`ADR-02`](Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md) §2 punto 6: **un solo límite en todo el producto**, tomado de configuración, que rechaza y nunca trunca. El valor se ancla en la etapa `a`. Sigue como `PA-05` |
| La **vigencia exacta de la credencial firmada**, declarada abierta por `GeometriaFactory-Infrastructure` | **Resuelto en su criterio, abierto en su número.** [`ADR-03`](Adrs/ADR-03-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md) §2 punto 5: que caduque dentro de la sesión de trabajo de una clase, con renovación por reingreso. Sigue como `PA-04` |
| Los **dos huecos del conjunto cerrado de códigos** —la facultad fuera del desenlace y el estado que no permite reenviar—, levantados por la categoría 02 | **No resueltos, y con fundamento.** [`ADR-04`](Adrs/ADR-04-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md) **no inventa códigos**: los códigos son del ensamblado de contratos, y agregarlos es del Product Owner y de aquel proyecto de código. Se usa el genérico y **el hueco se declara** como el síntoma medible de los cuatro destinos de ese código. Siguen como `PA-02` y `PA-03` |
| Las **rutas y los verbos definitivos**, propuesta derivada de la categoría 02 rotulada fila por fila | **No fijados acá, y con fundamento.** Esta categoría **los adopta sin cambiarlos** y no los decide por su cuenta: el intake ató todos los nombres al punto de control de la etapa `a`. Sigue como `PA-01` |

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Índice navegable de la sección: los cuatro documentos vigentes, las ocho ADR con su estado, los diecisiete NFR en una línea, el orden de lectura de seis pasos, los cuatro artefactos omitidos —dos de ellos con **apartamiento declarado** frente a la guía del tipo, y con el fundamento de la fuente en el caso de la descripción formal de servicio— y el destino de los cinco puntos abiertos que otras Fases C reasignaron a ésta, uno resuelto por completo, dos resueltos en su criterio y abiertos en su número, y dos declarados no resolubles acá. |
| 1.1 | 2026-08-10 | **Arrastre del cierre del hallazgo `C-05-03` (P2)** del informe de auditoría [`../../../Audit/C-05-Arquitectura-Siete-Proyectos-r1.md`](../../../Audit/C-05-Arquitectura-Siete-Proyectos-r1.md) 1.0. La fila del formato de intercambio de §7 importaba el número **ocho** de `Contratos-REST.md` §2.2 mientras citaba el fundamento de `ADR-02`, que allí se predica de **seis**. Pasa a declarar el reparto —**ocho filas**, de las cuales seis son reglas de formato y dos no lo son— y a remitir al cuadre **6 + 1 + 1 = 8** que las dos fuentes publican desde sus versiones 1.1. **Ningún documento de la sección, ninguna ADR y ningún NFR cambia.** Sube minor. |
