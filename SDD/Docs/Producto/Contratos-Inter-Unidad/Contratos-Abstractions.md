# Contrato de la superficie pública — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Producto:** Fábrica de Geometría
**Documento:** Contratos-Abstractions.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)

---

## Tabla de contenido

- [1. Alcance del contrato](#1-alcance-del-contrato)
- [2. Formato](#2-formato)
- [3. Operaciones](#3-operaciones)
- [4. Esquemas de datos](#4-esquemas-de-datos)
  - [4.1 Familias de tipos](#41-familias-de-tipos)
  - [4.2 Conjuntos cerrados](#42-conjuntos-cerrados)
  - [4.3 Lo que ningún tipo lleva](#43-lo-que-ningún-tipo-lleva)
- [5. Manejo de errores](#5-manejo-de-errores)
  - [5.1 Los diecisiete códigos vivos](#51-los-diecisiete-códigos-vivos)
  - [5.2 Los tres identificadores retirados](#52-los-tres-identificadores-retirados)
  - [5.3 Las tres señales declaradas, que no son error](#53-las-tres-señales-declaradas-que-no-son-error)
- [6. Versionado del contrato](#6-versionado-del-contrato)
- [7. Trazabilidad](#7-trazabilidad)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Alcance del contrato

Este documento declara la superficie pública de `GeometriaFactory-Contracts`: **el conjunto de tipos que atraviesan la frontera entre las dos unidades desplegables del producto**, y el único que la atraviesa (`PRODUCT-INTAKE` §14).

Los casos de uso que se materializan a través de este contrato son los **ocho** de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/02-Especificacion-Funcional/Especificacion-Funcional.md) §3, y sus consumidores son **dos**: `GeometriaFactory-Api`, que produce, y `GeometriaFactory-Web`, que consume; los dos lo referencian por proyecto de código (`PRODUCT-MANIFEST` §2).

**No hay integradores externos.** `redistribuible` es false y los dos consumidores son del mismo producto, compilados contra el mismo ensamblado.

## 2. Formato

**Contrato de tipos compilados, declarado en prosa estructurada.** No hay descripción formal del servicio ni clientes generados: `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Contracts descarta esa alternativa por costo de cadena de herramientas frente a dos consumidores compilados juntos, y [`ADR-08001`](../Adrs/ADR-08001-Tipos-De-Transferencia-Planos-Sin-Dependencias.md) la registra.

**El formato de intercambio no se fija acá.** Este proyecto de código exige que los tipos sean serializables sin comportamiento; qué formato se usa y cómo se configura pertenece a `GeometriaFactory-Api` y a `GeometriaFactory-Web`. Es el punto abierto PA-03 de [`Arquitectura-Proyecto-Codigo.md`](../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §11.

**Los nombres de tipos y de campos tampoco se fijan acá.** Se anclan en la etapa que implementa cada familia (punto abierto PA-01).

## 3. Operaciones

Un ensamblado de tipos no expone operaciones: expone **familias de tipos** que otro proyecto de código usa como carga útil de sus operaciones. La tabla declara qué transporta cada familia y en qué contrato de uso está especificada.

| Familia | Qué transporta | Contrato de uso | Códigos de error propios |
| --- | --- | --- | --- |
| Sesión | Solicitud de canje de credenciales y respuesta de sesión de cuatro campos | CU-08001 | `INVALID_CREDENTIALS`, `ACCOUNT_NOT_ENABLED` |
| Cuentas | Registro, credencial, listado de cuentas, cambio de situación, confirmación escrita de la baja y cambio de contraseña | CU-08002 | `EMAIL_ALREADY_REGISTERED`, `CONFIRMATION_MISMATCH`, `ADMINISTRATOR_ALREADY_CONFIGURED` |
| Trabajo | Envío, eliminación y estado del trabajo, con el texto original como cadena no interpretada | CU-08003 | `STATE_FORBIDS_DELETE`, `STATE_FORBIDS_UPDATE` |
| Listado | Proyección de trabajos, con alcance distinto según el papel | CU-08004 | `STUDENT_NOT_FOUND` |
| Detalle | Trabajo interpretado: piezas, componentes, observaciones y comentario del administrador | CU-08005 | Ninguno propio |
| Error | El único tipo con el que un fallo cruza la frontera, y el conjunto cerrado de diecisiete códigos | CU-08006 | `UNCLASSIFIED_ERROR` |
| Desenlace | Aprobación o rechazo de un trabajo en estado `Pendiente`, con comentario opcional | CU-08007 | `STATE_FORBIDS_OUTCOME`, `OUTCOME_ADMIN_ONLY` |
| Reseteo | Reseteo por el administrador y cambio obligatorio por la propia cuenta | CU-08008 | `RESET_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT`, `PASSWORD_CHANGE_REQUIRED` |

Los cuatro códigos que no figuran como propios de ninguna familia —`REQUIRED_FIELD_MISSING`, `SERVICE_UNAVAILABLE`, `WORK_NOT_FOUND` y `OPERATION_ADMIN_ONLY`— son **transversales a varias familias** y por eso no se atribuyen a una sola; §5.1 declara en qué contrato de uso aparece cada uno.

## 4. Esquemas de datos

### 4.1 Familias de tipos

Las ocho familias son los componentes de [`Arquitectura-Proyecto-Codigo.md`](../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1. Dos precisiones de forma que gobiernan al resto:

1. **La respuesta de sesión declara exactamente cuatro campos y ninguno más.** Las tres condiciones que impiden operar viajan como respuesta de error con código propio ([`ADR-08004`](../Adrs/ADR-08004-Regla-De-Exposicion-De-La-Frontera.md)).
2. **El tipo de error declara exactamente cuatro campos**: código, texto neutro, colección de detalles de ubicación y momento. Cada detalle lleva el nombre del campo señalado y, cuando el fallo proviene de la interpretación del texto del alumno, el índice de figura.

### 4.2 Conjuntos cerrados

| Conjunto | Valores | Cantidad |
| --- | --- | --- |
| Estado del trabajo | `Borrador`, `Pendiente`, `Finalizado`, `Rechazado`, con los dos últimos terminales | 4 |
| Desenlace de la revisión | Aprobar, rechazar | 2 |
| Situación de la cuenta | `Pendiente`, `Habilitado`, `Bloqueado` | 3 |
| Papel de la cuenta | `Alumno`, `Administrador` | 2 |
| Especie de la observación | Advertencia, error de validación | 2 |
| Códigos de error | Los diecisiete de §5.1 | 17 |

**Ningún tipo permite salir de `Finalizado` ni de `Rechazado`**: es la restricción `RT-08` de la categoría 02, y se materializa por ausencia —no hay solicitud que lo pida—.

### 4.3 Lo que ningún tipo lleva

Es la lista cerrada de [`ADR-08004`](../Adrs/ADR-08004-Regla-De-Exposicion-De-La-Frontera.md) §2, y se repite acá porque es parte del contrato y no de su fundamento: el hash de la contraseña, la clave de firma, cualquier dirección de servicio interno, las rutas de archivos de datos y las trazas de la implementación, y ninguna condición que impida operar como campo de la respuesta de sesión.

Y tres ausencias más, de las que cada una tiene su motivo:

| Ausencia | Motivo |
| --- | --- |
| La proyección de listado no lleva texto original, ni componentes de pieza, ni comentario del administrador | [`ADR-08005`](../Adrs/ADR-08005-Proyeccion-De-Listado-Separada-Del-Detalle.md) |
| La solicitud de reseteo no lleva campo de contraseña | La provisoria la produce el sistema (RN-08014) |
| No existe ningún tipo de establecimiento anónimo de contraseña | RN-08016 unificó los dos mecanismos de credencial inicial. El **registro de cuenta sigue siendo anónimo por diseño** y su solicitud es un tipo de este ensamblado |

## 5. Manejo de errores

Un único tipo de error para las ocho familias, con conjunto cerrado de códigos ([`ADR-08002`](../Adrs/ADR-08002-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md)). El texto es **neutro** y nunca contiene la dirección del servicio que falló.

### 5.1 Los diecisiete códigos vivos

La lista es la unión de las §6 de los ocho contratos de uso de la categoría 02, que es su fuente. Diecisiete filas.

| # | Código | Dónde se declara |
| --- | --- | --- |
| 1 | `REQUIRED_FIELD_MISSING` | CU-08001, CU-08002, CU-08003, CU-08006, CU-08007, CU-08008 |
| 2 | `SERVICE_UNAVAILABLE` | Los ocho contratos de uso |
| 3 | `WORK_NOT_FOUND` | CU-08003, CU-08005, CU-08006, CU-08007 |
| 4 | `INVALID_CREDENTIALS` | CU-08001, CU-08002, CU-08008 |
| 5 | `ACCOUNT_NOT_ENABLED` | CU-08001 |
| 6 | `PASSWORD_CHANGE_REQUIRED` | CU-08001, CU-08006, CU-08008 |
| 7 | `EMAIL_ALREADY_REGISTERED` | CU-08002 |
| 8 | `CONFIRMATION_MISMATCH` | CU-08002 |
| 9 | `ADMINISTRATOR_ALREADY_CONFIGURED` | CU-08002 |
| 10 | `STATE_FORBIDS_DELETE` | CU-08003 |
| 11 | `STUDENT_NOT_FOUND` | CU-08004 |
| 12 | `STATE_FORBIDS_OUTCOME` | CU-08006, CU-08007 |
| 13 | `OUTCOME_ADMIN_ONLY` | CU-08006, CU-08007 |
| 14 | `RESET_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT` | CU-08006, CU-08008 |
| 15 | `OPERATION_ADMIN_ONLY` | CU-08002, CU-08004, CU-08006, CU-08008 |
| 16 | `STATE_FORBIDS_UPDATE` | CU-08003, CU-08006 |
| 17 | `UNCLASSIFIED_ERROR` | CU-08006 |

**Diecisiete códigos**, dos de ellos incorporados por decisión del Product Owner (`PRODUCT-INTAKE` **1.29** §17.4 P.3), que este documento **emite formalmente**: el 15 y el 16. **Cinco** de ellos —el 1, el 2, el 3, el 6 y el 15— aparecen en más de un contrato de uso con la **misma** causa, y siguen siendo un código cada uno: la unidad del conjunto es la condición, no la operación. El catálogo de [`../03-UX-UI-DX/DX-Error-Messages.md`](../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/03-UX-UI-DX/DX-Error-Messages.md) los desarrolla con su texto neutro propuesto.

**Los dos códigos que entran, y por qué ninguno de los que había alcanzaba** (`PRODUCT-INTAKE` **1.29** §17.4 P.3).

| Código | Condición que representa | Código vecino que no la cubría |
| --- | --- | --- |
| `OPERATION_ADMIN_ONLY` | El papel no alcanza para la operación pedida **fuera del desenlace de un trabajo**: gobernar las cuentas de la comisión, resetear la contraseña de una cuenta de alumno y ver el listado de trabajos de la comisión | `OUTCOME_ADMIN_ONLY`, **acotado por su enunciado** a aprobar y a rechazar. Sin código propio, las tres caían en el genérico y el consumidor no podía distinguir «no tenés permiso» de «algo salió mal» |
| `STATE_FORBIDS_UPDATE` | Se pidió **enviar o reeditar** un trabajo que está en `Pendiente`, `Finalizado` o `Rechazado`, y por lo tanto es de **sólo lectura** | `STATE_FORBIDS_DELETE`, **acotado a la eliminación**, que no cubre las otras dos escrituras que el mismo estado prohíbe |

Los dos son códigos de **rechazo sin escritura**: la operación no ocurre y el estado no cambia. **Ninguno reemplaza a los quince que había ni recicla a los tres retirados**, y ninguno cambia los cuatro campos del tipo de error ni la regla de exposición de §4.3.

### 5.2 Los tres identificadores retirados

**Veinte identificadores emitidos: diecisiete vivos y tres retirados.** El único lugar del proyecto de código donde los veinte están enumerados juntos es la tabla de §3.2 del catálogo de 03; acá se declaran los tres retirados porque forman parte del contrato de compatibilidad.

| Identificador retirado | Cuándo salió | Por qué |
| --- | --- | --- |
| `TEXT_NOT_PARSEABLE` | Al unificarse guardar y enviar en una sola acción | Ninguna operación falla ya por ese motivo: el envío procede y el trabajo queda en `Borrador` con sus observaciones. Pasó a ser **señal declarada** |
| `PASSWORD_NOT_SET` | Con **RN-08016** | Su causa dejó de ser posible: habilitar produce y fija la contraseña provisoria, de modo que ninguna cuenta llega a estar habilitada sin contraseña |
| `RESET_NOT_APPLICABLE_TO_PASSWORDLESS_ACCOUNT` | Con **RN-08016** | Su causa dejó de ser posible: el reseteo sobre una cuenta sin contraseña la fija en lugar de rechazarla |

**Ninguno se recicla**, y **reponer cualquiera de los dos últimos se rechaza aunque compile**: contradice CA-09 de CU-08006 y describe situaciones que RN-08016 no admite.

Un cuarto identificador que conviene no confundir con éstos: **no existe ningún código para el reseteo sobre una cuenta no habilitada**, y no es que se haya retirado —nunca entró—. RN-08015 declara que resetear no exige cuenta habilitada, de modo que esa causa no existe y no recibe código.

### 5.3 Las tres señales declaradas, que no son error

Se catalogan para que no se traten como error, y **no se cuentan entre los diecisiete**.

| Señal | Dónde se declara | Qué significa |
| --- | --- | --- |
| `TEXT_NOT_PARSEABLE` | CU-08003 §6.1 | El envío **procede**: el resultado trae estado `Borrador`, el texto conservado íntegro y las observaciones con índice de figura y campo |
| `EMPTY_LIST` | CU-08004 §6.1 | La colección viene con cero elementos. La unidad pública distingue vacío de fallo por el tipo recibido, no por el conteo |
| `TEXT_NOT_PARSEABLE` | CU-08005 §6.1 | El detalle llega con la colección de piezas en cero elementos y las observaciones pobladas |

**Tres señales sobre dos identificadores**, porque la primera y la tercera son el mismo identificador visto desde dos contratos de uso. El cambio de contraseña pendiente **no es una señal**: impide la operación pedida, de modo que es un error transportado.

## 6. Versionado del contrato

Aplica el criterio de [`ADR-08003`](../Adrs/ADR-08003-Versionado-Por-Compilacion-Compartida.md) §7. Lo esencial, sobre los elementos de este contrato:

| Cambio | Clase | ¿Lo detecta la compilación? |
| --- | --- | --- |
| Quitar o renombrar un tipo o un campo, o cambiar el tipo de un campo | Mayor | Sí |
| Quitar un valor de cualquiera de los seis conjuntos cerrados de §4.2 | Mayor | No |
| Agregar un código al conjunto cerrado de error | Mayor | No |
| Agregar un campo que viole §4.3 | Mayor | No; se rechaza en revisión |
| Agregar un tipo o un campo opcional que no viole §4.3 | Menor | — |
| Agregar un valor a un conjunto cerrado que no sea el de códigos | Menor | — |
| Corregir el texto neutro de un código sin cambiar su causa | Parche | — |

**Compatibilidad hacia atrás y deprecación.** No hay ninguna de las dos, y es deliberado: no hay versionado de rutas ni convivencia de dos versiones del contrato porque no hay clientes de terceros. La política es **el despliegue conjunto** de las dos unidades desplegables ante cualquier cambio incompatible. **La versión vigente del contrato ya ejerció esa política**: el conjunto cerrado de estados y el de códigos de error cambiaron los dos (`RT-06` de la categoría 02).

## 7. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| CU que lo consumen | CU-08001 a CU-08008, los ocho de la categoría 02 de este proyecto de código |
| RN que cubre | Ninguna redactada acá. Transporta las dieciséis, con el reparto de [`Arquitectura-Proyecto-Codigo.md`](../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §10.3 |
| Restricciones transversales que materializa | `RT-01` a `RT-11`, con el reparto de [`Arquitectura-Proyecto-Codigo.md`](../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §10.2 |
| ADR que lo gobiernan | ADR-08001, ADR-08002, ADR-08003, ADR-08004, ADR-08005 |
| Consumidores | `GeometriaFactory-Api` y `GeometriaFactory-Web` |
| Tests previstos en 08 | Al menos una prueba de integración por tipo; prueba de inspección de superficie pública para §4.3; prueba de inspección del conjunto cerrado para §5.1 y §5.2 |

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Declara las ocho familias de tipos con sus códigos propios, los seis conjuntos cerrados, la lista de lo que ningún tipo lleva, el inventario completo de los **quince** códigos vivos con el contrato de uso donde se declara cada uno, los **tres** identificadores retirados con su motivo y la regla de no reciclado, las **tres** señales declaradas sobre dos identificadores, y el criterio de versionado con la columna que declara cuáles cambios no detecta la compilación. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **22**. Sube minor. |
| 1.2 | 2026-08-29 | **Tramo `R-3c` del renombre `F-03`**, reactivado por el Product Owner el 2026-08-29 y registrado en [`../Norma-De-Nomenclatura.md`](../Norma-De-Nomenclatura.md) §8. **33 línea(s)** pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios, ni lo que está entre «…», ni los informes de `Audit/`. **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
