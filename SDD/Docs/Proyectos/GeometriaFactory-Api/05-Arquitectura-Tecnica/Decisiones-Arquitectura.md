# Índice de decisiones de arquitectura — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Decisiones-Arquitectura.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)

---

## 1. Índice de ADR

Cada decisión vive en un archivo individual bajo [`Adrs/`](Adrs/). Este documento **no contiene el cuerpo de ninguna**: es el índice navegable.

| ADR | Título | Categoría | Estado | Fecha |
| --- | --- | --- | --- | --- |
| [ADR-01](Adrs/ADR-01-Host-Delgado-Con-Composicion-De-Raiz-Unica.md) | Host delgado, con la composición de raíz como único lugar de ensamblado | Estilo | Propuesto | 2026-08-10 |
| [ADR-02](Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md) | El formato de intercambio y su configuración, fijados para los dos extremos | Comunicación | Propuesto | 2026-08-10 |
| [ADR-03](Adrs/ADR-03-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md) | Credencial firmada, papel exigido por punto y una guardia transversal sin excepciones sueltas | Seguridad | Propuesto | 2026-08-10 |
| [ADR-04](Adrs/ADR-04-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md) | Dos traducciones en orden, con una tabla única y sin inventar códigos | Comunicación | Propuesto | 2026-08-10 |
| [ADR-05](Adrs/ADR-05-Sin-Paginacion-Con-Condicion-De-Reingreso-Declarada.md) | Sin paginación, con su condición de reingreso declarada | Comunicación | Propuesto | 2026-08-10 |
| [ADR-06](Adrs/ADR-06-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md) | Composición de raíz única: ciclos de vida y configuración en un solo lugar | Persistencia | Propuesto | 2026-08-10 |
| [ADR-07](Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) | Arranque en dos fases, y un punto de salud que no exige acceso | Despliegue | Propuesto | 2026-08-10 |
| [ADR-08](Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) | Sin versionado de rutas, con despliegue conjunto como regla operativa | Despliegue | Propuesto | 2026-08-10 |

**Ocho ADR, ninguna superada y ninguna rechazada.**

## 2. Las cinco decisiones que el tipo `rest-api` exige, y dónde está cada una

La guía de la categoría fija un mínimo de **cinco** ADR para este tipo, con sus temas nombrados. Las cinco están, y esta tabla declara cuál las cubre para que la correspondencia no haya que deducirla.

| Tema exigido por el tipo | ADR que lo cubre |
| --- | --- |
| **Estilo** | [ADR-01](Adrs/ADR-01-Host-Delgado-Con-Composicion-De-Raiz-Unica.md) |
| **Persistencia** | [ADR-06](Adrs/ADR-06-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md), que es donde la persistencia de este proyecto de código realmente vive: **toma de configuración la ubicación del almacén y fija los ciclos de vida**, porque **el modelo del dato lo tiene `GeometriaFactory-Infrastructure`** |
| **Autenticación** | [ADR-03](Adrs/ADR-03-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md) |
| **Paginación** | [ADR-05](Adrs/ADR-05-Sin-Paginacion-Con-Condicion-De-Reingreso-Declarada.md), que la registra **como decisión de no paginar, con su condición de reingreso medible**. Una ausencia sin sustituto sería un pendiente; con sustituto es una decisión |
| **Manejo de errores** | [ADR-04](Adrs/ADR-04-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md) |

Las **tres** restantes las agrega esta categoría porque el producto las necesitaba y ninguna fuente las tenía resueltas: el **formato de intercambio** ([ADR-02](Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md)), que dos proyectos de código reasignaron acá; el **arranque en dos fases** ([ADR-07](Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md)); y la **política de versionado de la frontera** ([ADR-08](Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md)).

## 3. La categoría de decisión que queda vacía, y por qué

| Categoría | Por qué no hay ninguna ADR |
| --- | --- |
| **Extensibilidad** | `tiene_extensibilidad` es **false** en este proyecto de código (`PRODUCT-MANIFEST` §5). El punto de extensión declarado del producto es el contrato de la fachada del visor, que es de otro proyecto de código. Y hacia esta superficie **no hay extensión posible por diseño**: su único cliente legítimo está declarado, y agregar otro rompe `RA-01` |

**Observabilidad no tiene ADR propia, y no es un hueco.** Es el único proyecto de código del producto con `tiene_observabilidad_critica` en true, pero lo que el intake declara —registro estructurado de cada error y de cada intento de acceso rechazado, y ningún mensaje con direcciones de servicios internos— **no es una decisión abierta sino la contracara obligatoria de `RA-03`**, y vive donde corresponde: centralizada en [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §7 y exigida por [`ADR-04`](Adrs/ADR-04-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md) §2 punto 6, con su métrica en §8. Las métricas numéricas que el flag habilita están en la tabla de NFR.

## 4. Qué decisión gobierna cada preocupación

Tabla de consulta rápida, para no tener que abrir las ocho.

| Si la pregunta es… | La respuesta está en |
| --- | --- |
| Qué puede y qué no puede hacer un punto de acceso | [ADR-01](Adrs/ADR-01-Host-Delgado-Con-Composicion-De-Raiz-Unica.md) |
| Cómo se nombran los campos al serializar, y qué pasa con los nulos y con los conjuntos cerrados | [ADR-02](Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md) |
| Qué pasa con un cuerpo que excede el límite de tamaño | [ADR-02](Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md) §2 punto 6: **se rechaza, nunca se trunca** |
| Qué puntos quedan fuera de la guardia, y cuántos | [ADR-03](Adrs/ADR-03-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md): **exactamente cuatro** |
| Cuánto dura una credencial firmada | [ADR-03](Adrs/ADR-03-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md) §2 punto 5, con el número abierto |
| Qué código de respuesta le toca a cada código del contrato | [ADR-04](Adrs/ADR-04-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md) y la tabla de [`Contratos-REST.md`](Contratos-REST.md) §5 |
| Qué se hace cuando el conjunto cerrado no tiene código para un camino | [ADR-04](Adrs/ADR-04-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md): el genérico, **y se declara el hueco** |
| Por qué el listado no se pagina, y cuándo habría que paginarlo | [ADR-05](Adrs/ADR-05-Sin-Paginacion-Con-Condicion-De-Reingreso-Declarada.md) |
| Dónde se conectan los cuatro puertos con sus adaptadores | [ADR-06](Adrs/ADR-06-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md) |
| Qué pasa si falta la clave de firma o el volumen del almacén | [ADR-06](Adrs/ADR-06-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md) y [ADR-07](Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md): **el servicio no arranca** |
| Qué responde el punto de salud, y qué no responde | [ADR-07](Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) |
| Qué reemplaza al versionado de rutas | [ADR-08](Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md): el **despliegue conjunto** |

## 5. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Indexa las ocho ADR con su categoría, su estado y su fecha; declara la correspondencia con los cinco temas que el tipo `rest-api` exige y las tres que esta categoría agrega; declara la categoría de decisión que queda vacía y por qué observabilidad no tiene ADR propia sin que sea un hueco; y agrega la tabla de consulta rápida por preocupación. |
