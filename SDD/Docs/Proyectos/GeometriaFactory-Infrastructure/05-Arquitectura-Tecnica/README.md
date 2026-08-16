# 05 · Arquitectura técnica — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** README.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Tipo de proyecto de código (D8):** `library`

---

## 1. Punto de entrada

`GeometriaFactory-Infrastructure` es la capa donde el producto **toca el mundo**: implementa los **cuatro** puertos que declara `GeometriaFactory-Application`, provee los **dos** mecanismos de seguridad que las capas de adentro delegaron y **modela y ejerce la persistencia del producto**. Es nivel 2 del orden topológico y sus dependencias de compilación son `GeometriaFactory-Application` y `GeometriaFactory-Domain`.

Lo que hay que haber entendido antes de tocar esta sección, y que atraviesa los seis documentos: **acá vive el mecanismo y no la decisión**, y **la mitad de esta capa no toca el almacén**. Esa segunda propiedad no es un detalle de implementación: es lo que hace que la batería obligatoria del validador —la mitigación del único riesgo alto de negocio del producto— corra sin base de datos. El punto de entrada es [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md).

## 2. Documentos de esta sección

| Documento | Propósito |
| --- | --- |
| [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) | Documento maestro: estilo, las cuatro vistas mínimas, cross-cutting, catorce NFR, ocho riesgos, trazabilidad de las dieciséis reglas, de los nueve invariantes, de las tres reglas de arquitectura y de los ocho escenarios, y once puntos abiertos —diez abiertos y `PA-08` resuelto— |
| [`Decisiones-Arquitectura.md`](Decisiones-Arquitectura.md) | Índice de las siete ADR, con las dos categorías de decisión que quedan vacías y la tabla de consulta rápida por preocupación |
| [`Contratos-Abstractions.md`](Contratos-Abstractions.md) | Contrato de la superficie: siete operaciones en tres clases, qué cruza cada frontera, manejo de errores y versionado |
| [`Modelo-Datos-Logico.md`](Modelo-Datos-Logico.md) | Esquema físico: cinco tablas, seis índices, quince restricciones y la transformación inicial. **Es el único documento del producto que describe el esquema del dato guardado** |
| [`Flujo-Ejecucion.md`](Flujo-Ejecucion.md) | Pipeline del validador en siete pasos, con la **tabla de derivación por tipo** y las tres terminaciones posibles |
| [`Adrs/`](Adrs/) | Las siete decisiones, una por archivo |

## 3. ADR vigentes

| ADR | Título | Categoría | Estado |
| --- | --- | --- | --- |
| [ADR-06001](Adrs/ADR-06001-Adaptadores-Por-Puerto-Sin-Repositorio-Generico.md) | Un adaptador por puerto, sin repositorio genérico y sin adaptador único | Estilo | Propuesto |
| [ADR-06002](Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md) | Archivo único, escritor único y una unidad de trabajo por operación | Persistencia | Propuesto |
| [ADR-06003](Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md) | Criterio de comparación de dos correos, y el índice que lo sostiene | Persistencia | Propuesto |
| [ADR-06004](Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md) | Derivación de clave anclada, con sus parámetros versionados junto al valor guardado | Seguridad | Propuesto |
| [ADR-06005](Adrs/ADR-06005-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md) | La contraseña provisoria: no adivinable, sin repetirse y transcribible de viva voz | Seguridad | Propuesto |
| [ADR-06006](Adrs/ADR-06006-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md) | Lectura tolerante en un solo lugar, y una tabla de derivación por tipo | Estilo | Propuesto |
| [ADR-06007](Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md) | Transformaciones de esquema al arrancar, con linaje inmutable y arranque detenido | Despliegue | Propuesto |

Ninguna superada, ninguna rechazada.

## 4. NFR vigentes

Los **catorce**, con su objetivo numérico y su mecanismo, están en [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §8. En una línea: interpretación del texto semilla de **3** piezas por debajo de **200 ms** sin almacén, cobertura **85 %** de líneas y **80 %** de ramas con **95 %** de líneas en el validador —los cuatro rotulados `[ASUNCIÓN]` por el intake—, tolerancia **0.01** con operador **estricto** —que **no** es asunción—, **10 de 10** casos de la batería del validador, **0** peticiones de red de los dos motores, **1 de 1** aplicación de transformaciones sobre almacén inexistente, **0** provisorias repetidas, **0** componentes cargados en listado, **0** reescrituras del texto original aceptadas, **0** retiros parciales, **0** mensajes con secretos o rutas, **100 %** de las 17 condiciones ejercitadas en las dos direcciones y **0** advertencias de construcción.

## 5. Orden de lectura sugerido

1. [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §2 y §3 — el estilo, los ocho componentes y, sobre todo, **§2.2**, las cinco decisiones de los niveles 0 y 1 que esta capa hereda y no reabre. Sin eso, la ausencia de autorización acá se lee como un olvido en lugar de como la frontera que es.
2. [`Adrs/ADR-06001`](Adrs/ADR-06001-Adaptadores-Por-Puerto-Sin-Repositorio-Generico.md) — la decisión de la que dependen todas las demás, y la que explica por qué la mitad de la capa se prueba sin almacén.
3. [`Flujo-Ejecucion.md`](Flujo-Ejecucion.md) junto con [`Adrs/ADR-06006`](Adrs/ADR-06006-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md) — el pipeline y la decisión que lo gobierna, que se leen mejor juntos. Acá está la pieza de más riesgo del producto.
4. [`Modelo-Datos-Logico.md`](Modelo-Datos-Logico.md) §3 y §4 — los índices y las restricciones. Es donde se ve que la unicidad del correo y el arrastre de la baja **no dependen de que nadie se acuerde**.
5. [`Adrs/ADR-06004`](Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md) y [`ADR-06005`](Adrs/ADR-06005-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md) — las dos piezas sensibles, con los tres atajos que fallan hacia el lado seguro.
6. [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §10 — la trazabilidad, para consultar por regla, por invariante, por regla de arquitectura o por escenario.

## 6. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Modelo-Datos-Logico.md` | **Emitido, y no omitido** | Es la diferencia con las cuatro bibliotecas hermanas y se declara con su fundamento. La guía de la categoría lo omite para «`library` **puro sin estado**», y éste no lo es: `tiene_persistencia` vale **true** (`PRODUCT-MANIFEST` §5) y el intake declara la persistencia «la responsabilidad central del proyecto de código». Omitirlo dejaría al producto sin ningún documento que describa el esquema del dato guardado. Es el mismo **apartamiento declarado** que la categoría 02 hizo con su modelo conceptual |
| `Flujo-Ejecucion.md` | **Emitido, y no omitido** | La guía lo pide para `library` **con motor de procesamiento**, y éste lo tiene: el validador de figuras. Es además donde vive la tabla de derivación por tipo que la categoría 02 derivó a ésta |
| `Contratos-<area>` adicionales | **Omitidos** | La guía exige `Contratos-Abstractions.md` para `library`, y eso es lo que se emitió. No hay contrato de protocolo que declarar: el intake declara «no aplica» en comunicación e integración porque este proyecto de código **no expone puntos de acceso** |
| `Extensibilidad.md` | **Omitido** | `tiene_extensibilidad` es **false** en el `PRODUCT-MANIFEST` §5. El punto de extensión declarado del producto es el contrato de la fachada del visor, que es de otro proyecto de código |
| `_legacy/` | **No existe** | Es la primera emisión de esta categoría en este proyecto de código: no hay ninguna versión superada que archivar |

## 7. Lo que esta sección resolvió de lo que aguas arriba quedó abierto

Siete puntos abiertos llegaron a esta categoría, y conviene decir qué pasó con cada uno en lugar de dejarlo repartido.

| Punto que llegó | Qué hizo esta sección |
| --- | --- |
| El **criterio de comparación de dos correos, con su índice**, reasignado explícitamente por la Fase C de `GeometriaFactory-Application` | **Resuelto.** [`ADR-06003`](Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md): dos correos son el mismo ignorando mayúsculas y minúsculas y nada más; se conserva la forma escrita y se indexa la normalizada, con índice único como **segunda línea**. Es `IX-01` de [`Modelo-Datos-Logico.md`](Modelo-Datos-Logico.md) §3 |
| El **valor derivado del área de una pieza volumétrica** | **Resuelto.** [`ADR-06006`](Adrs/ADR-06006-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md) y [`Flujo-Ejecucion.md`](Flujo-Ejecucion.md) §5: suma de las áreas declaradas de los componentes, con fórmula sólo donde no hay componentes que sumar. Es **la única vía que reproduce el número medido del intake** en el cilindro de `E-1` |
| La **longitud y el alfabeto de la contraseña provisoria** | **Resuelto.** [`ADR-06005`](Adrs/ADR-06005-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md): doce caracteres alfanuméricos sin caracteres ambiguos, producidos íntegramente desde la fuente de material impredecible. Resuelve la tensión que la categoría 02 dejó escrita y no resolvió |
| La **zona horaria y la precisión de los sellos** | **Resuelto.** [`ADR-06002`](Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md) §2 punto 4: tiempo universal coordinado, sin truncar la precisión que el reloj entrega, con la conversión a cargo de quien lo muestra |
| El **límite de tamaño del texto** | **Reasignado con fundamento.** [`ADR-06006`](Adrs/ADR-06006-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md) §2 punto 3 decide que el motor **no impone límite propio**, porque dos límites distintos —uno en el borde y otro acá— permitirían que un texto entrara y no se interpretara. Pasa a la categoría 05 de `GeometriaFactory-Api`, con la exigencia escrita de que **rechace y nunca trunque**. Sigue como `PA-05` |
| **Cuál función de derivación de clave se ancla** | **Resuelto a medias, y declarado así.** [`ADR-06004`](Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md) fija la **forma** —parámetros versionados junto al valor derivado— y el **criterio de elección**, y deja la elección concreta en la regla de anclaje de la etapa `a`, que es donde el intake puso todas las versiones. Sigue como `PA-03` |
| El **identificador del puerto de repositorio de cuentas** | **No resuelto, y con fundamento.** El puerto lo declara `GeometriaFactory-Application`, y su ADR-06002 —ya emitida— ató el nombre al punto de control de la etapa `a`. Esta categoría **no puede nombrar un tipo que no declara** sin contradecir una decisión emitida. Lo que sí fija es el criterio de nombrado del **adaptador**, en [`ADR-06003`](Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md) §6. Sigue como `PA-01` |

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Índice navegable de la sección: los seis documentos vigentes, las siete ADR con su estado, los catorce NFR en una línea, el orden de lectura de seis pasos, los artefactos emitidos como apartamiento declarado y los omitidos con su motivo, y el destino de los siete puntos abiertos que llegaron a esta categoría, cuatro resueltos, uno resuelto a medias, uno reasignado con fundamento y uno declarado no resoluble acá. |
| 1.1 | 2026-08-10 | **Arrastre del cierre del hallazgo `C-05-01` (P1)** del informe de auditoría [`../../../Audit/C-05-Arquitectura-Siete-Proyectos-r1.md`](../../../Audit/C-05-Arquitectura-Siete-Proyectos-r1.md) 1.0. La fila de `Arquitectura-Proyecto-Codigo.md` de §2 declaraba «once puntos abiertos» sin distinguir estado; pasa a **«once puntos abiertos —diez abiertos y `PA-08` resuelto—»**, que es el reparto que el documento maestro declara desde su versión 1.1 tras cerrarse el recuento de escenarios contra `PRODUCT-INTAKE` **1.18**. **Ningún documento de la sección, ninguna ADR y ningún NFR cambia.** Sube minor. |
