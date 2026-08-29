# CU-06003 — Guardar y recuperar los trabajos

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** CU-06003-Guardar-Y-Recuperar-Los-Trabajos.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00003`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md); [`NB-00007`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00007-Revision-De-La-Comision-En-Un-Solo-Lugar.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §4.1 (RN-06003, RN-06008, RN-06011), §7 (CL-1), §17.1.P.2 · GeometriaFactory-Infrastructure, §17.1.P.4 · GeometriaFactory-Infrastructure, §17.1.P.11 · GeometriaFactory-Infrastructure punto 2, §17.1.P.12 · GeometriaFactory-Infrastructure; implementa el puerto de repositorio de trabajos de `Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Especificacion-Funcional.md` §3; el modelo que materializa está en [`Modelo-Datos/Modelo-Conceptual.md`](../../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md)
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Infrastructure

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Actores](#2-actores)
- [3. Precondiciones](#3-precondiciones)
- [4. Flujo principal](#4-flujo-principal)
- [5. Flujos alternativos](#5-flujos-alternativos)
- [6. Excepciones y errores](#6-excepciones-y-errores)
- [7. Postcondiciones](#7-postcondiciones)
- [8. Criterios de aceptación](#8-criterios-de-aceptación)
- [9. Trazabilidad](#9-trazabilidad)
- [10. Notas y supuestos](#10-notas-y-supuestos)
- [11. Control de cambios](#11-control-de-cambios)
- [17. Compatibilidad de la superficie pública](#17-compatibilidad-de-la-superficie-pública)

---

## 1. Propósito

Materializar y recuperar el trabajo del alumno con sus piezas, sus componentes y sus observaciones, **conservando el texto original íntegro** (RN-06008), y resolver las consultas que el consumidor pide **ya acotadas** por dueño o por alcance.

Es la mitad de este proyecto de código que el intake declara «la responsabilidad central» (§17.1.P.4 · GeometriaFactory-Infrastructure): un archivo único de SQLite, en un volumen persistente, con un escritor único y una unidad de trabajo por operación.

Lo que este caso de uso **no** hace: no decide quién puede ver qué —eso lo resolvió el consumidor antes de pedir— y **no arma consultas por su cuenta**. Recibe un pedido con su recorte declarado y lo resuelve.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor del puerto de repositorio de trabajos (`GeometriaFactory-Application`) | Primario | Pide recuperar, consultar con alcance declarado o materializar |
| Almacén de datos | Sistema | El archivo único de SQLite en su ubicación configurada |

## 3. Precondiciones

- El almacén existe y su esquema está al día. Quien lo garantiza es `CU-06010`, que aplica las migraciones al arrancar.
- Toda consulta llega **con su recorte declarado**: por dueño, o por el alcance que excluye los trabajos en `Borrador`. Una consulta sin recorte no se resuelve, y ese es el motivo de una de las condiciones de §6.

## 4. Flujo principal

1. El consumidor pide una operación sobre trabajos, dentro de **una única unidad de trabajo**.
2. Si es una **recuperación**, se devuelve el trabajo con su texto original, su estado, su dueño, sus tres sellos de tiempo, su comentario si lo tiene, sus piezas con su posición y sus componentes, y sus observaciones.
3. Si es una **consulta de listado**, el recorte declarado se traslada al pedido que se resuelve contra el almacén —no se trae todo para filtrar después— y el resultado **no incluye los componentes de las piezas ni el texto original**. Es una decisión de modelado con efecto directo en el tiempo de respuesta del listado del administrador.
4. Si es una **materialización**, se escribe el trabajo con sus piezas, sus componentes y sus observaciones. El **texto original se escribe una sola vez, al crearse el trabajo, y ninguna escritura posterior lo reemplaza** (RN-06008).
5. La unidad de trabajo se cierra entera o no se cierra: no queda escritura parcial.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La recuperación no encuentra ningún trabajo con ese identificador | Se devuelve **nada encontrado**. No es una condición de error de este contrato: quién traduce eso a una negativa —y con qué motivo, que nunca revela la existencia de un recurso ajeno— es el consumidor | Termina la operación |
| FA-02 | La consulta con alcance devuelve el conjunto vacío | Se devuelve el conjunto vacío. Es un resultado legítimo: una comisión sin entregas todavía | Termina la operación |
| FA-03 | Se reedita un trabajo en `Borrador` y llega un texto original **igual** al ya conservado | Se materializan los datos del trabajo y **el texto no se reescribe**, porque es el mismo. La reedición cambia nombre, fecha y descripción, y descarta la interpretación anterior | Paso 5 |
| FA-04 | Se materializa un trabajo que reemplaza su conjunto de piezas y de observaciones | Se retiran las piezas, los componentes y las observaciones anteriores y se escriben los nuevos, **en la misma unidad de trabajo**. El texto original y los sellos de creación no se tocan | Paso 5 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `QUERY_WITHOUT_DECLARED_SCOPE` | Llegó una consulta de listado sin dueño y sin predicado de alcance | Termina sin consultar. Un listado sin recorte sería el listado de todos los trabajos de la comisión, que es exactamente el resultado que RN-06003 y RN-06011 vienen a impedir. **El recorte se traslada al pedido, no se aplica después** |
| `WRITE_REWRITES_ORIGINAL_JSON` | Una materialización aportó, para un trabajo existente, un texto original distinto del conservado | Termina sin escribir nada. **Es la condición que hace exigible RN-06008 en el único lugar donde el texto puede perderse.** El producto no edita el dato del alumno, y conservarlo es lo que permite reprocesar el mismo trabajo cuando el validador mejora |
| `CONCURRENT_WRITE_REJECTED` | Otra unidad de trabajo tenía el almacén tomado para escribir | Termina de forma **degradada**, sin escritura parcial. **SQLite no admite escrituras concurrentes** y el backend opera como escritor único; la concurrencia real es baja porque el alcance es de aula. **Este contrato no reintenta**: quien decida reintentar es el consumidor |
| `STORE_UNAVAILABLE` | El archivo del almacén no está alcanzable: la ruta configurada no responde, o el volumen no está montado | Termina de forma **degradada**. No hay réplica ni caché: el intake declara que el front muestra estado degradado y que los datos no están disponibles hasta que el servidor vuelva. Esta condición vuelve a declararse en `CU-06004` y en `CU-06005` |

## 7. Postcondiciones

- **Éxito en recuperación o consulta:** el consumidor recibe el resultado. Nada cambió en el almacén.
- **Éxito en materialización:** el trabajo y todo lo que cuelga de él quedan escritos, con el texto original conservado y con el sello de modificación que el consumidor aportó.
- **Fallo:** el almacén queda **exactamente como estaba**. Ninguna de las cuatro condiciones deja escritura parcial.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un trabajo con el texto del escenario **E-2**, con sus dos comas finales | Se materializa y se recupera | El texto recuperado es **idéntico carácter por carácter** al materializado, comas finales incluidas. El almacén guarda el texto **tal cual**, sin normalizarlo ni reindentarlo |
| CA-02 | Un trabajo ya materializado con el texto de E-2 | Se materializa de nuevo con un texto original distinto | Devuelve `WRITE_REWRITES_ORIGINAL_JSON` y el texto conservado **no cambia** |
| CA-03 | Un alumno con un trabajo en `Borrador` y otro en `Pendiente` | Se consulta el listado con el alcance del administrador | El resultado trae **sólo el trabajo en estado `Pendiente`**, y el recorte se resolvió en el pedido: el borrador **no viajó** |
| CA-04 | Un trabajo con tres piezas y sus componentes | Se consulta el listado que lo incluye | El resultado **no trae los componentes de las piezas ni el texto original** |
| CA-05 | El mismo trabajo | Se recupera su detalle | El resultado **sí** trae las piezas con su posición, sus componentes y sus observaciones |
| CA-06 | Una consulta de listado sin dueño y sin predicado de alcance | Se resuelve | Devuelve `QUERY_WITHOUT_DECLARED_SCOPE` y **0 filas leídas del almacén** |
| CA-07 | Una materialización de un trabajo con sus piezas y sus observaciones, interrumpida antes de terminar | Se recupera el trabajo | El almacén está **como antes de la operación**: ni piezas huérfanas ni observaciones sin trabajo |
| CA-08 | Un trabajo con una figura no reconstruida, cuya posición quedó reservada | Se materializa y se recupera | La observación de esa figura conserva **la misma posición**, que sigue perteneciendo al rango de figuras del conjunto raíz aunque no haya pieza en ella |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-00003, y NB-00007 en cuanto resuelve la consulta con el alcance ya aplicado |
| Reglas de negocio aplicables | [RN-02008](../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md), [RN-02003](../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) y [RN-02011](../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md), las dos últimas **por el traslado del recorte al pedido** |
| Reglas conceptuales de modelo | [`RC-06001`](../../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/RC-06001-Texto-Original-Escrito-Una-Sola-Vez.md), [`RC-06002`](../../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/RC-06002-Identidad-Posicional-De-La-Pieza.md), [`RC-06003`](../../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/RC-06003-Valor-Declarado-Y-Derivado-Por-Separado.md), [`RC-06006`](../../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/RC-06006-Tres-Sellos-De-Tiempo-Distintos.md) |
| Puerto que implementa | Repositorio de trabajos |
| Consumidor | `GeometriaFactory-Application`, sus CU-06002, CU-06004, CU-06005, CU-06006, CU-06007, CU-06008 y CU-06009 |
| Historias de usuario a generar en 06 | US-06008, US-06009, US-06010, US-06011 |
| Componentes esperados en 05 | Adaptador del repositorio de trabajos y su mapeo; alcance de la unidad de trabajo |
| Tests previstos en 08 | Pruebas de integración contra **SQLite real**, que es donde el intake ubica la verificación de la persistencia: conservación literal del texto, listado sin componentes, recorte resuelto en el pedido y ausencia de escritura parcial |

## 10. Notas y supuestos

- **Un `DbContext` por operación**, que del lado del consumidor se lee como «un caso de uso, una unidad de trabajo». No se reparte una operación entre varias.
- **El modo de diario es WAL** y el respaldo es la copia del archivo con WAL activo, consistente. **La frecuencia del respaldo queda a definir por el docente**, y así lo declara el intake: no se fija acá.
- **Los componentes se persisten pese a su redundancia** —un `Cubo(3)` serializa seis caras idénticas para expresar un solo número— porque **son parte del ejercicio**. Se compensa no cargándolos nunca en las consultas de listado, que es lo que verifica CA-04.
- **El texto se guarda como texto en la fila del trabajo.** No se usan consultas sobre su contenido: el almacén no lo interpreta, y quien lo interpreta es `CU-06001`.
- **El escritor único es una restricción aceptada, no un defecto a mitigar.** Se acepta a cambio de un despliegue sin servicio de base de datos aparte.
- **Este contrato no autoriza.** No comprueba pertenencia ni facultad: cuando resuelve una consulta acotada, el recorte ya venía decidido. Confundir el traslado del recorte con una comprobación de autorización llevaría a duplicar en el almacén una decisión que se toma en la capa de aplicación.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | Actualización de la cita del `PRODUCT-INTAKE` de **1.11** a **1.12** en la trazabilidad upstream: 1.11 quedó archivada al resolver el Product Owner el desenlace del envío del escenario `E-8`. Corrige el hallazgo **H-02** del informe de auditoría `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` (ronda 1). El delta entre 1.11 y 1.12 se revisó y sólo alcanza a `E-8`, que no toca lo que este documento declara: sin cambios de contenido. |
| 1.2 | 2026-08-29 | **Tramo `R-3c` del renombre `F-03`**, reactivado por el Product Owner el 2026-08-29 y registrado en [`../../../../Producto/Norma-De-Nomenclatura.md`](../../../../Producto/Norma-De-Nomenclatura.md) §8. **6 línea(s)** pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios, ni lo que está entre «…», ni los informes de `Audit/`. **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |

## 17. Compatibilidad de la superficie pública

Agregar un campo al resultado de una recuperación es compatible. **Incluir los componentes o el texto original en el resultado de un listado, resolver una consulta sin recorte declarado o permitir que una escritura reemplace el texto conservado son cambios incompatibles** y suben versión mayor: el primero rompe el requerimiento no funcional del listado, y los otros dos contradicen RN-06003, RN-06011 y RN-06008.
