# Contratos de integración entre unidades de entrega

| Campo | Valor |
| --- | --- |
| Documento | README.md |
| Nivel | Producto |
| Versión | 1.0 |
| Fecha | 2026-08-15 |
| Estado | Vigente |
| Regla | `Rules-Arquitectura-Tecnica.md` §2.1, artefactos de nivel producto |

---

## 1. Qué hay acá

Los contratos que las dos unidades de entrega del producto se prometen **en runtime**:
`GeometriaFactory-Api` los expone y `GeometriaFactory-Web` los consume, por HTTP.

Son contratos de **integración**, no de compilación. El intake §14 los distingue: un contrato de
integración une piezas desplegadas, y uno de compilación une proyectos de código al construir. Los
dos grafos no coinciden, y la arista `Web → Api` existe solo en el primero.

## 2. Por qué viven en el nivel producto

Los materializa `GeometriaFactory-Contracts`, un proyecto de código que la matriz de composición de
[`../Vista-Producto.md`](../Vista-Producto.md) muestra **compartido entre las dos unidades de
entrega**. Un proyecto compartido no tiene unidad dueña: asignarlo a una dejaría en la otra una
referencia colgada, y documentarlo en las dos produciría dos fuentes del mismo contrato.

Por eso su contenido se inventaría una sola vez, acá, y las unidades de entrega lo referencian.

## 3. Los ocho contratos

| Contrato | Qué declara |
| --- | --- |
| [`CU-08001`](CU-08001-Contrato-De-Canje-De-Credenciales-Y-Sesion.md) | Canje de credenciales y sesión |
| [`CU-08002`](CU-08002-Contrato-De-Administracion-De-Cuentas.md) | Administración de cuentas de alumno |
| [`CU-08003`](CU-08003-Contrato-De-Carga-Y-Edicion-Del-Trabajo.md) | Carga y edición del trabajo |
| [`CU-08004`](CU-08004-Contrato-De-Listado-De-Trabajos.md) | Listado de trabajos |
| [`CU-08005`](CU-08005-Contrato-De-Detalle-Del-Trabajo-Interpretado.md) | Detalle del trabajo interpretado |
| [`CU-08006`](CU-08006-Contrato-De-Respuesta-De-Error.md) | Respuesta de error |
| [`CU-08007`](CU-08007-Contrato-De-Desenlace-De-La-Revision.md) | Desenlace de la revisión |
| [`CU-08008`](CU-08008-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md) | Reseteo y cambio obligatorio de contraseña |

La superficie pública completa —el conjunto de tipos que atraviesan la frontera— está en
[`Contratos-Abstractions.md`](Contratos-Abstractions.md).

**Sobre el prefijo `CU-`.** Los ocho conservan el identificador con el que se emitieron. No son casos
de uso de una unidad de entrega: son los contratos con los que los casos de uso de las dos unidades
cruzan la frontera. Renombrarlos a una familia nueva sería acuñar un identificador que ninguna regla
del framework declara, que es exactamente lo que la migración retiró al eliminar `P·CU`. La
reasignación de familia, si se decide, es una intervención sobre el framework y no sobre este
destino.

## 4. Decisiones de arquitectura

Las cinco decisiones sobre este proyecto compartido son de **nivel producto** y viven en
[`../Adrs/`](../Adrs/): un cambio sobre un proyecto que la matriz de composición muestra compartido
alcanza a todas las unidades marcadas, de modo que su decisión no puede vivir en la carpeta de una
de ellas.

## 5. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-15 | Emisión inicial, en la migración normativa de SDD 6.0 a 8.2. Reúne a nivel producto los ocho contratos de integración y la superficie pública que `GeometriaFactory-Contracts` materializa, por ser proyecto de código compartido entre las dos unidades de entrega. |
