# 02 · Especificación funcional — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts (`GeometriaFactory.Contracts`, tipo `library`)
**Documento:** README.md
**Versión:** 1.5
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `Especificacion-Funcional.md` (índice maestro de esta sección); `00-Contexto/Vision-Producto.md` §9; `01-Necesidades-Negocio/Necesidades-Negocio.md` 1.1 §2 y §5.3; `PRODUCT-INTAKE` **1.14** §4, §4.1, §4.2, §17.4, §13 y §14
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Qué hay en esta carpeta](#1-qué-hay-en-esta-carpeta)
- [2. Los ocho casos de uso](#2-los-ocho-casos-de-uso)
- [3. Orden de lectura sugerido](#3-orden-de-lectura-sugerido)
- [4. Artefactos omitidos y su motivo](#4-artefactos-omitidos-y-su-motivo)
- [5. Notas de uso de esta sección](#5-notas-de-uso-de-esta-sección)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Qué hay en esta carpeta

| Documento | Propósito | Estado |
| --- | --- | --- |
| [`Especificacion-Funcional.md`](Especificacion-Funcional.md) | Índice maestro: catálogo, criterio de recorte, matriz NB→CU→RN→US, cobertura inversa y restricciones transversales del contrato. **Es el punto de entrada** | Propuesto |
| [`Casos-De-Uso/`](Casos-De-Uso/) | Un contrato de uso por archivo, `CU-01` a `CU-07`, con las once secciones obligatorias más la §17 de compatibilidad de versión pública, que es la opcional propia de `library` | Propuesto |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario que esta categoría acuña, los términos con más de un referente y los que se referencian del glosario raíz sin redefinirlos | Propuesto |
| `README.md` | Este archivo: índice navegable, orden de lectura y declaración de las omisiones | Propuesto |

## 2. Los ocho casos de uso

| CU | Título | Qué decide | NB que sostiene | Enlace |
| --- | --- | --- | --- | --- |
| CU-01 | Contrato de canje de credenciales y de sesión | Qué viaja al canjear credenciales, y sobre todo qué no: contraseña almacenada, clave de firma y direcciones de servicio interno quedan fuera | NB-02, NB-01 | [CU-01](Casos-De-Uso/CU-01-Contrato-De-Canje-De-Credenciales-Y-Sesion.md) |
| CU-02 | Contrato de administración de cuentas de alumno | Registro sin contraseña, establecimiento y cambio de credencial, listado de cuentas y cambio de situación, con la confirmación escrita de la baja como campo del contrato | NB-01, NB-02 | [CU-02](Casos-De-Uso/CU-02-Contrato-De-Administracion-De-Cuentas.md) |
| CU-03 | Contrato de carga y edición del trabajo | Que el texto original viaje como una sola cadena, sin interpretarse en el contrato y sin alterar un carácter; que **enviar** sea la única acción de guardado y el estado lo decida la interpretación; y que la solicitud de eliminación sea única para los dos papeles | NB-03, NB-04 | [CU-03](Casos-De-Uso/CU-03-Contrato-De-Carga-Y-Edicion-Del-Trabajo.md) |
| CU-04 | Contrato de listado de trabajos | Que la proyección de listado no arrastre ni el texto original, ni los componentes de las piezas, ni el comentario; y que el administrador no reciba los trabajos en estado `Borrador` | NB-03, NB-07, NB-09 | [CU-04](Casos-De-Uso/CU-04-Contrato-De-Listado-De-Trabajos.md) |
| CU-05 | Contrato de detalle del trabajo interpretado | Cómo viajan las piezas, los componentes y las observaciones, con severidad, índice de figura, campo señalado y el par de valor declarado y valor derivado; y cómo el **comentario del administrador** queda imposible de confundir con una observación | NB-04, NB-05, NB-06, NB-07, NB-09 | [CU-05](Casos-De-Uso/CU-05-Contrato-De-Detalle-Del-Trabajo-Interpretado.md) |
| CU-06 | Contrato de respuesta de error | La forma única con la que un fallo cruza la frontera de servicio: texto neutro, conjunto cerrado de **quince** códigos, ubicación del defecto y ninguna dirección de servicio | NB-04, NB-08, NB-09, NB-02 | [CU-06](Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md) |
| CU-07 | Contrato de desenlace de la revisión | Que aprobar y rechazar sean un conjunto cerrado de dos valores sobre un trabajo en estado `Pendiente`, que el comentario sea opcional en los dos casos y que ningún tipo permita salir de un estado terminal | NB-09, NB-07 | [CU-07](Casos-De-Uso/CU-07-Contrato-De-Desenlace-De-La-Revision.md) |
| CU-08 | Contrato de reseteo y de cambio obligatorio de contraseña | Que la solicitud de reseteo **no tenga forma de alcanzar los trabajos de la cuenta** —resetear no es dar de baja—, que el cambio de contraseña pendiente viaje como respuesta de error con código propio y no como campo de la sesión, y que un solo código cubra todas las operaciones bloqueadas | NB-01, NB-02 | [CU-08](Casos-De-Uso/CU-08-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md) |

## 3. Orden de lectura sugerido

1. **`Especificacion-Funcional.md`** — primero siempre. Sin su §2 y su §5, los ocho casos de uso se leen como una lista de tipos sin la decisión que los ordena.
2. **CU-06** — conviene leerlo temprano, aunque no sea el último del catálogo: los otros siete lo referencian en todos sus caminos de error.
3. **CU-01 y CU-02** — la identidad y la admisión, que son la entrada del circuito.
4. **CU-03** — el trabajo, con el texto original como cadena. Es la decisión que más consecuencias tiene aguas abajo.
5. **CU-04 y CU-05** — las dos lecturas, que se leen de a pares: la proyección de listado existe precisamente para no ser el detalle. CU-05 es además donde vive el comentario del administrador.
6. **CU-07** — el desenlace, que cierra el circuito de la revisión.
7. **CU-08** — el reseteo y el cambio obligatorio, que es lo último que se agregó al contrato. Se lee después de CU-01 y CU-02, porque reutiliza la solicitud de cambio de contraseña de uno y desvía el canje del otro.
8. **`Glosario-Funcional.md`** — se puede leer suelto, y conviene abrirlo antes que nada si el lector viene de otra categoría, por los tres referentes de «contrato» y por la forma calificada obligatoria de `Pendiente`.

## 4. Artefactos omitidos y su motivo

`Rules-Especificacion-Funcional.md` §2.1 define **ocho** artefactos posibles para esta categoría. Se emiten cuatro y se omiten cuatro, agrupados en tres filas porque el modelo conceptual y sus reglas conceptuales se omiten por el mismo motivo. La decisión de omitir la tomó el orquestador; acá se declara con la celda de la regla que corresponde a cada caso.

| Artefacto omitido | Regla que lo admite | Motivo |
| --- | --- | --- |
| `Definicion-<Concepto-Central>.md` | Ninguna. La celda «library con superficie estrecha» está en la columna **Recomendado** de §2.1, no en la de omisión: la regla lo recomienda para este caso exacto y no autoriza omitirlo por esa vía. La celda de omisión es «Tipos sin concepto central» | Es **una recomendación no seguida, con motivo declarado**, y no una omisión que la regla autorice. El motivo material: el ensamblado no tiene un concepto técnico central separable de los seis contratos de uso —son tipos de transferencia planos, sin comportamiento (`PRODUCT-INTAKE` §17.4 P.2)—, que es el supuesto de la columna «Tipos sin concepto central»; y un documento aparte duplicaría lo que ya está en el §1 y el §17 de cada uno de los ocho casos de uso, con el costo de dos definiciones del mismo concepto en la misma cadena |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | §2.1 las omite para «proyectos de código triviales sin estado ni invariantes», y §2.2 no las hace obligatorias para `library` | El ensamblado no tiene estado y no puede sostener invariantes. Las reglas de dominio del producto viven en `GeometriaFactory-Domain`, que se documenta en paralelo; acá sólo se decide **qué se expone**, y eso baja a criterios de aceptación verificables por inspección, no a reglas. El fundamento completo está en `Especificacion-Funcional.md` §5 |
| `Modelo-Datos/Modelo-Conceptual.md` y `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX-<Nombre>.md` | §2.1 y §2.2 los omiten para `library` puro sin estado | Doble motivo: el tipo D8 es `library` y el flag `tiene_persistencia` es false en este proyecto de código (`PRODUCT-MANIFEST` §5, `PRODUCT-INTAKE` §17.4 P.4, «no aplica»). No hay entidades que modelar: hay tipos de transferencia, y su forma es la que declaran los casos de uso |

La omisión de las `RN-XX` es la que explica que la columna RN de la matriz del índice maestro esté vacía en sus nueve filas. Esa columna se mantiene en la matriz, con el motivo declarado, en lugar de suprimirse.

## 5. Notas de uso de esta sección

- **Autoridad.** Ningún caso de uso de esta sección origina una capacidad, una prioridad ni una exclusión: todo se deriva del `PRODUCT-INTAKE`, de `00-Contexto/` y de las nueve `NB-XX` de `01-Necesidades-Negocio/`, y traza a su sección de origen.
- **Actor.** El actor de los ocho casos de uso es el código que compila contra el ensamblado de contratos. No hay actor humano: las personas del producto aparecen en la especificación funcional de `GeometriaFactory-Web`.
- **Fronteras.** La forma de los puntos de acceso del servicio es de `GeometriaFactory-Api`; las invariantes son de `GeometriaFactory-Domain`; la arquitectura y los ADR son de 05; las pruebas, de 08. Esta sección refiere, no redacta.
- **Verificación.** Este proyecto de código no tiene pruebas propias. Se ejercita íntegramente desde las pruebas de integración que golpean el servicio real, y su gate equivalente es que el cien por ciento de los tipos de transferencia esté ejercitado por al menos una prueba de integración (`PRODUCT-INTAKE` §17.4 P.6). **El intake rotula ese valor `[ASUNCIÓN]` y lo lista en §22**: está completo y se usa como valor vigente hasta que el Product Owner lo confirme, con el mismo tratamiento que CU-04 §10 le da al requisito estructural del listado.
- **Numeración.** Los identificadores `CU-XX` y `US-XX` son locales a este proyecto de código; la decisión está declarada en `Especificacion-Funcional.md` §3.2 y la correspondencia con las veintisiete previsiones de `Necesidades-Negocio.md` §5.3, una por una, en su §4.2. Los ocho casos de uso numeran su única sección opcional como **§17**, que es el número que `Rules-Especificacion-Funcional.md` §4.3 le asigna a la compatibilidad de versión pública para `library`; el hueco entre §11 y §17 es deliberado y está declarado en el encabezado de esa sección.
- **Vocabulario.** Los términos del dominio no se redefinen acá: están en `Vision-Producto.md` §9. La palabra «proyecto» a secas no se usa; «pieza» en su referente de artefacto desplegable se escribe siempre calificada; **`Pendiente` se escribe siempre calificado**, «cuenta `Pendiente`» o «trabajo en estado `Pendiente`», porque nombra dos estados distintos que cruzan este mismo contrato (`Glosario-Funcional.md` §3.3); y «contrato» tiene tres referentes declarados en `Glosario-Funcional.md` §3.1.
- **Nombres de archivo.** Ningún archivo vivo de esta carpeta lleva sufijo de versión: cada uno declara su versión en el campo `Versión` de su cabecera.

## 6. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial del índice de la sección. Enumera los cuatro documentos emitidos y los seis casos de uso con su decisión de contrato y su NB, fija el orden de lectura, declara las tres omisiones con la regla que las admite y su motivo, y las notas de autoridad, actor, fronteras, verificación, numeración y vocabulario. | Analista Funcional + API Designer (AG-02) |
| 1.0 | 2026-08-08 | Correcciones absorbidas de la ronda 1 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r1.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-10**: la cabecera pasa de tabla al bloque de metadatos de `Rules-Especificacion-Funcional.md` §4.1, que es la forma de los otros trece artefactos de la fase. **H-08**: §4 declaraba siete artefactos posibles y la tabla maestra de §2.1 tiene ocho; pasa a «ocho posibles, cuatro emitidos y cuatro omitidos, agrupados en tres filas». **H-05**: la fila de `Definicion-<Concepto-Central>.md` deja de citar como permiso de omisión una celda de la columna «Recomendado» y se declara como recomendación no seguida con motivo declarado. **H-11**: la nota de verificación incorpora el rótulo `[ASUNCIÓN]` que el intake §17.4 P.6 le pone al gate del 100 %. **H-09** y **H-03**, por arrastre: la nota de numeración remite a la correspondencia nueva de `Especificacion-Funcional.md` §4.2 y declara la numeración §17 de la sección opcional. | Analista Funcional + API Designer (AG-02) |
| 1.1 | 2026-08-09 | Actualización por contenido nuevo aguas arriba, no por auditoría: `PRODUCT-INTAKE` 1.3 con el circuito de revisión del administrador, `00-Contexto` y `01-Necesidades-Negocio` en 1.1 con **NB-09** nueva. Cambios: §1 y §2 pasan a **siete** casos de uso con la incorporación de **CU-07**, contrato de desenlace de la revisión, y las filas de CU-03, CU-04, CU-05 y CU-06 actualizan su decisión de contrato; §3 suma CU-07 al orden de lectura; §5 actualiza la nota de actor a siete casos de uso, la de numeración a veintisiete previsiones y la de vocabulario con la forma calificada obligatoria de `Pendiente`. Las cuatro omisiones de §4 no cambian: siguen siendo las mismas, con el mismo fundamento.  **Corrección de la ronda 3 de auditoría, hallazgo H-06**, absorbida sin subir versión: cinco conteos de §3, §4 y §5 seguían describiendo el catálogo anterior —«los seis casos de uso», «los otros cinco», el §17 «de cada caso de uso», «sus ocho filas» de la matriz y «las ocho `NB-XX`»— y se propagan a siete, seis, siete y nueve respectivamente. | Analista Funcional + API Designer (AG-02) |
| 1.2 | 2026-08-09 | Actualización por contenido nuevo aguas arriba: `PRODUCT-INTAKE` **1.7** incorpora la capacidad **F-26** —reseteo de contraseña por el administrador—, las reglas **RN-12** y **RN-13** y el invariante **INV-09**, y retira la exclusión **X-2**. Cambios: §1 y §2 pasan a **ocho** casos de uso con la incorporación de **CU-08**, contrato de reseteo y de cambio obligatorio de contraseña, y la fila de CU-06 actualiza su conjunto cerrado de catorce a **dieciséis** códigos; §3 suma CU-08 al final del orden de lectura, con su motivo; §5 actualiza la nota de actor y la de numeración a ocho casos de uso. Las cuatro omisiones de §4 no cambian: siguen siendo las mismas, con el mismo fundamento. | Analista Funcional + API Designer (AG-02) |
| 1.3 | 2026-08-09 | Actualización por las dos decisiones del Product Owner sobre **F-26** que `CU-08` 1.2 y `CU-06` 1.3 absorben: resetear **no exige** que la cuenta esté habilitada, y la contraseña provisoria **la produce el sistema**. Cambio acá: la fila de CU-06 actualiza su conjunto cerrado de dieciséis a **diecisiete** códigos. Ningún artefacto se agrega ni se omite y el orden de lectura no cambia. | Analista Funcional + API Designer (AG-02) |
| 1.4 | 2026-08-10 | Actualización por `PRODUCT-INTAKE` **1.13** §4.1 (**RN-16**) y la precisión de **F-04**, que `CU-02` 1.4, `CU-01` 1.5, `CU-08` 1.4 y `CU-06` 1.6 absorben: habilitar una cuenta **produce su contraseña provisoria** y el producto queda sin ninguna escritura anónima. Cambio acá: la fila de CU-06 actualiza su conjunto cerrado de diecisiete a **quince** códigos. Ningún artefacto se agrega ni se omite y el orden de lectura no cambia. | Analista Funcional + API Designer (AG-02) |
| 1.5 | 2026-08-10 | **Cierra el hallazgo `C-08` (P2) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0.** La cabecera de trazabilidad declaraba derivarse del `PRODUCT-INTAKE` **1.3**, versión archivada, y pasa a declarar la **1.14**, vigente. Entre la **1.3** y la **1.14** el intake atravesó once emisiones, entre ellas las que incorporaron **F-25**, **F-26** y las reglas **RN-12** a **RN-16**: una cabecera que declaraba 1.3 declaraba derivarse de un intake que no conocía ni el reseteo ni la habilitación con contraseña provisoria. Se revisó el cuerpo antes de mover la cabecera y **no arrastra ninguna decisión de las versiones intermedias**: no queda en él ningún recuento de «quince reglas» ni de «diecisiete códigos», ninguna cita a la exclusión **X-2** como vigente y ninguna afirmación de que la marca de cambio de contraseña pendiente la ponga únicamente el reseteo. **Ningún contenido normativo de este documento cambia: la corrección es de trazabilidad.** Sube minor. | Analista Funcional + API Designer (AG-02) |
