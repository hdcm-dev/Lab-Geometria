# Índice de decisiones de arquitectura — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
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
| [ADR-06001](Adrs/ADR-06001-Adaptadores-Por-Puerto-Sin-Repositorio-Generico.md) | Un adaptador por puerto, sin repositorio genérico y sin adaptador único | Estilo | Propuesto | 2026-08-10 |
| [ADR-06002](Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md) | Archivo único, escritor único y una unidad de trabajo por operación | Persistencia | Propuesto | 2026-08-10 |
| [ADR-06003](Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md) | Criterio de comparación de dos correos, y el índice que lo sostiene | Persistencia | Propuesto | 2026-08-10 |
| [ADR-06004](Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md) | Derivación de clave anclada, con sus parámetros versionados junto al valor guardado | Seguridad | Propuesto | 2026-08-10 |
| [ADR-06005](Adrs/ADR-06005-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md) | La contraseña provisoria: no adivinable, sin repetirse y transcribible de viva voz | Seguridad | Propuesto | 2026-08-10 |
| [ADR-06006](Adrs/ADR-06006-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md) | Lectura tolerante en un solo lugar, y una tabla de derivación por tipo | Estilo | Propuesto | 2026-08-10 |
| [ADR-06007](Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md) | Transformaciones de esquema al arrancar, con linaje inmutable y arranque detenido | Despliegue | Propuesto | 2026-08-10 |

**Siete ADR, ninguna superada y ninguna rechazada.** El mínimo para el tipo `library` es de **tres**; el número real lo determina el conjunto de decisiones que hubo que registrar, y acá pesan dos cosas que ninguna otra biblioteca del producto tiene: la persistencia y los dos mecanismos de seguridad.

## 2. Las dos categorías de decisión que quedan vacías, y por qué

| Categoría | Por qué no hay ninguna ADR |
| --- | --- |
| **Comunicación** | Este proyecto de código **no se comunica con nadie por un protocolo**. No expone puntos de acceso, no abre conexiones y no cruza la frontera del proceso: su único consumidor es la composición de raíz de `GeometriaFactory-Api`, por referencia de proyecto de código. La decisión de comunicación que le concierne —qué frontera hay y qué pasa por ella— ya la tomó `GeometriaFactory-Application` en su ADR-06002, y acá se implementa |
| **Observabilidad** | El flag `tiene_observabilidad_critica` es **false** en este proyecto de código (`PRODUCT-MANIFEST` §5), y el intake no declara métricas ni trazas para él. Lo único que sí declara —**registro del lado del servidor de todo error que se muestre al usuario**— no es una decisión de arquitectura de esta capa sino la contracara obligatoria de `RA-03`, y vive centralizada en [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §7 |

**Extensibilidad tampoco tiene ADR, y tampoco es un hueco**: `tiene_extensibilidad` es false acá. El punto de extensión declarado del producto es el contrato de la fachada del visor, que es de otro proyecto de código.

## 3. Qué decisión gobierna cada preocupación

Tabla de consulta rápida, para no tener que abrir las siete.

| Si la pregunta es… | La respuesta está en |
| --- | --- |
| Cuántos adaptadores hay y quién los conecta | [ADR-06001](Adrs/ADR-06001-Adaptadores-Por-Puerto-Sin-Repositorio-Generico.md) |
| Qué pasa si el almacén está tomado, o si una baja se interrumpe | [ADR-06002](Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md) |
| Con qué zona horaria y con qué precisión se guardan los sellos | [ADR-06002](Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md) §2 punto 4 |
| Cuándo dos correos son el mismo correo | [ADR-06003](Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md) |
| Cómo se nombra el adaptador del puerto que todavía no tiene nombre | [ADR-06003](Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md) §6 |
| Qué se guarda junto al valor derivado de una credencial, y qué pasa si falta la clave de firma | [ADR-06004](Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md) |
| Cuánto dura un acceso firmado | [ADR-06004](Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md) §2 punto 5, con el número abierto |
| Cuántos caracteres tiene una provisoria y de dónde salen | [ADR-06005](Adrs/ADR-06005-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md) |
| De dónde sale el área derivada de una pieza volumétrica | [ADR-06006](Adrs/ADR-06006-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md) y [`Flujo-Ejecucion.md`](Flujo-Ejecucion.md) §5 |
| Quién impone el límite de tamaño del texto | [ADR-06006](Adrs/ADR-06006-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md) §2 punto 3: el borde del proceso, y rechaza, nunca trunca |
| Qué pasa si el esquema del almacén no corresponde | [ADR-06007](Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md) |

## 4. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Indexa las siete ADR con su categoría, su estado y su fecha; declara las dos categorías de decisión que quedan vacías —comunicación y observabilidad— con su motivo, más la ausencia de extensibilidad por flag; y agrega la tabla de consulta rápida por preocupación. |
