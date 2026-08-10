# Contrato de la superficie pública — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Contratos-Abstractions.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
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
  - [5.1 Los quince códigos vivos](#51-los-quince-códigos-vivos)
  - [5.2 Los tres identificadores retirados](#52-los-tres-identificadores-retirados)
  - [5.3 Las tres señales declaradas, que no son error](#53-las-tres-señales-declaradas-que-no-son-error)
- [6. Versionado del contrato](#6-versionado-del-contrato)
- [7. Trazabilidad](#7-trazabilidad)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Alcance del contrato

Este documento declara la superficie pública de `GeometriaFactory-Contracts`: **el conjunto de tipos que atraviesan la frontera entre las dos unidades desplegables del producto**, y el único que la atraviesa (`PRODUCT-INTAKE` §14).

Los casos de uso que se materializan a través de este contrato son los **ocho** de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3, y sus consumidores son **dos**: `GeometriaFactory-Api`, que produce, y `GeometriaFactory-Web`, que consume; los dos lo referencian por proyecto de código (`PRODUCT-MANIFEST` §2).

**No hay integradores externos.** `redistribuible` es false y los dos consumidores son del mismo producto, compilados contra el mismo ensamblado.

## 2. Formato

**Contrato de tipos compilados, declarado en prosa estructurada.** No hay descripción formal del servicio ni clientes generados: `PRODUCT-INTAKE` §17.4.P.2 descarta esa alternativa por costo de cadena de herramientas frente a dos consumidores compilados juntos, y [`ADR-01`](Adrs/ADR-01-Tipos-De-Transferencia-Planos-Sin-Dependencias.md) la registra.

**El formato de intercambio no se fija acá.** Este proyecto de código exige que los tipos sean serializables sin comportamiento; qué formato se usa y cómo se configura pertenece a `GeometriaFactory-Api` y a `GeometriaFactory-Web`. Es el punto abierto PA-03 de [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §11.

**Los nombres de tipos y de campos tampoco se fijan acá.** Se anclan en la etapa que implementa cada familia (punto abierto PA-01).

## 3. Operaciones

Un ensamblado de tipos no expone operaciones: expone **familias de tipos** que otro proyecto de código usa como carga útil de sus operaciones. La tabla declara qué transporta cada familia y en qué contrato de uso está especificada.

| Familia | Qué transporta | Contrato de uso | Códigos de error propios |
| --- | --- | --- | --- |
| Sesión | Solicitud de canje de credenciales y respuesta de sesión de cuatro campos | CU-01 | `CONTRATO_CREDENCIAL_INVALIDA`, `CONTRATO_CUENTA_NO_HABILITADA` |
| Cuentas | Registro, credencial, listado de cuentas, cambio de situación, confirmación escrita de la baja y cambio de contraseña | CU-02 | `CONTRATO_CORREO_YA_REGISTRADO`, `CONTRATO_CONFIRMACION_NO_COINCIDE`, `CONTRATO_ADMINISTRADOR_YA_CONFIGURADO` |
| Trabajo | Envío, eliminación y estado del trabajo, con el texto original como cadena no interpretada | CU-03 | `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` |
| Listado | Proyección de trabajos, con alcance distinto según el papel | CU-04 | `CONTRATO_ALUMNO_NO_ENCONTRADO` |
| Detalle | Trabajo interpretado: piezas, componentes, observaciones y comentario del administrador | CU-05 | Ninguno propio |
| Error | El único tipo con el que un fallo cruza la frontera, y el conjunto cerrado de quince códigos | CU-06 | `CONTRATO_ERROR_NO_CLASIFICADO` |
| Desenlace | Aprobación o rechazo de un trabajo en estado `Pendiente`, con comentario opcional | CU-07 | `CONTRATO_ESTADO_NO_PERMITE_DESENLACE`, `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` |
| Reseteo | Reseteo por el administrador y cambio obligatorio por la propia cuenta | CU-08 | `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR`, `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` |

Los tres códigos que no figuran como propios de ninguna familia —`CONTRATO_CAMPO_REQUERIDO_AUSENTE`, `CONTRATO_SERVICIO_NO_DISPONIBLE` y `CONTRATO_TRABAJO_NO_ENCONTRADO`— son **transversales a varias familias** y por eso no se atribuyen a una sola; §5.1 declara en qué contrato de uso aparece cada uno.

## 4. Esquemas de datos

### 4.1 Familias de tipos

Las ocho familias son los componentes de [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §3.1. Dos precisiones de forma que gobiernan al resto:

1. **La respuesta de sesión declara exactamente cuatro campos y ninguno más.** Las tres condiciones que impiden operar viajan como respuesta de error con código propio ([`ADR-04`](Adrs/ADR-04-Regla-De-Exposicion-De-La-Frontera.md)).
2. **El tipo de error declara exactamente cuatro campos**: código, texto neutro, colección de detalles de ubicación y momento. Cada detalle lleva el nombre del campo señalado y, cuando el fallo proviene de la interpretación del texto del alumno, el índice de figura.

### 4.2 Conjuntos cerrados

| Conjunto | Valores | Cantidad |
| --- | --- | --- |
| Estado del trabajo | `Borrador`, `Pendiente`, `Finalizado`, `Rechazado`, con los dos últimos terminales | 4 |
| Desenlace de la revisión | Aprobar, rechazar | 2 |
| Situación de la cuenta | `Pendiente`, `Habilitado`, `Bloqueado` | 3 |
| Papel de la cuenta | `Alumno`, `Administrador` | 2 |
| Especie de la observación | Advertencia, error de validación | 2 |
| Códigos de error | Los quince de §5.1 | 15 |

**Ningún tipo permite salir de `Finalizado` ni de `Rechazado`**: es la restricción `RT-08` de la categoría 02, y se materializa por ausencia —no hay solicitud que lo pida—.

### 4.3 Lo que ningún tipo lleva

Es la lista cerrada de [`ADR-04`](Adrs/ADR-04-Regla-De-Exposicion-De-La-Frontera.md) §2, y se repite acá porque es parte del contrato y no de su fundamento: el hash de la contraseña, la clave de firma, cualquier dirección de servicio interno, las rutas de archivos de datos y las trazas de la implementación, y ninguna condición que impida operar como campo de la respuesta de sesión.

Y tres ausencias más, de las que cada una tiene su motivo:

| Ausencia | Motivo |
| --- | --- |
| La proyección de listado no lleva texto original, ni componentes de pieza, ni comentario del administrador | [`ADR-05`](Adrs/ADR-05-Proyeccion-De-Listado-Separada-Del-Detalle.md) |
| La solicitud de reseteo no lleva campo de contraseña | La provisoria la produce el sistema (RN-14) |
| No existe ningún tipo de establecimiento anónimo de contraseña | RN-16 unificó los dos mecanismos de credencial inicial. El **registro de cuenta sigue siendo anónimo por diseño** y su solicitud es un tipo de este ensamblado |

## 5. Manejo de errores

Un único tipo de error para las ocho familias, con conjunto cerrado de códigos ([`ADR-02`](Adrs/ADR-02-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md)). El texto es **neutro** y nunca contiene la dirección del servicio que falló.

### 5.1 Los quince códigos vivos

La lista es la unión de las §6 de los ocho contratos de uso de la categoría 02, que es su fuente. Quince filas.

| # | Código | Dónde se declara |
| --- | --- | --- |
| 1 | `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | CU-01, CU-02, CU-03, CU-06, CU-07, CU-08 |
| 2 | `CONTRATO_SERVICIO_NO_DISPONIBLE` | Los ocho contratos de uso |
| 3 | `CONTRATO_TRABAJO_NO_ENCONTRADO` | CU-03, CU-05, CU-06, CU-07 |
| 4 | `CONTRATO_CREDENCIAL_INVALIDA` | CU-01, CU-02, CU-08 |
| 5 | `CONTRATO_CUENTA_NO_HABILITADA` | CU-01 |
| 6 | `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` | CU-01, CU-06, CU-08 |
| 7 | `CONTRATO_CORREO_YA_REGISTRADO` | CU-02 |
| 8 | `CONTRATO_CONFIRMACION_NO_COINCIDE` | CU-02 |
| 9 | `CONTRATO_ADMINISTRADOR_YA_CONFIGURADO` | CU-02 |
| 10 | `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` | CU-03 |
| 11 | `CONTRATO_ALUMNO_NO_ENCONTRADO` | CU-04 |
| 12 | `CONTRATO_ESTADO_NO_PERMITE_DESENLACE` | CU-06, CU-07 |
| 13 | `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` | CU-06, CU-07 |
| 14 | `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` | CU-06, CU-08 |
| 15 | `CONTRATO_ERROR_NO_CLASIFICADO` | CU-06 |

**Quince códigos.** Cuatro de ellos —el 1, el 2, el 3 y el 6— aparecen en más de un contrato de uso con la **misma** causa, y siguen siendo un código cada uno: la unidad del conjunto es la condición, no la operación. El catálogo de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) los desarrolla con su texto neutro propuesto.

### 5.2 Los tres identificadores retirados

**Dieciocho identificadores emitidos: quince vivos y tres retirados.** El único lugar del proyecto de código donde los dieciocho están enumerados juntos es la tabla de §3.2 del catálogo de 03; acá se declaran los tres retirados porque forman parte del contrato de compatibilidad.

| Identificador retirado | Cuándo salió | Por qué |
| --- | --- | --- |
| `CONTRATO_TEXTO_NO_INTERPRETABLE` | Al unificarse guardar y enviar en una sola acción | Ninguna operación falla ya por ese motivo: el envío procede y el trabajo queda en `Borrador` con sus observaciones. Pasó a ser **señal declarada** |
| `CONTRATO_CONTRASENA_NO_ESTABLECIDA` | Con **RN-16** | Su causa dejó de ser posible: habilitar produce y fija la contraseña provisoria, de modo que ninguna cuenta llega a estar habilitada sin contraseña |
| `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA` | Con **RN-16** | Su causa dejó de ser posible: el reseteo sobre una cuenta sin contraseña la fija en lugar de rechazarla |

**Ninguno se recicla**, y **reponer cualquiera de los dos últimos se rechaza aunque compile**: contradice CA-09 de CU-06 y describe situaciones que RN-16 no admite.

Un cuarto identificador que conviene no confundir con éstos: **no existe ningún código para el reseteo sobre una cuenta no habilitada**, y no es que se haya retirado —nunca entró—. RN-15 declara que resetear no exige cuenta habilitada, de modo que esa causa no existe y no recibe código.

### 5.3 Las tres señales declaradas, que no son error

Se catalogan para que no se traten como error, y **no se cuentan entre los quince**.

| Señal | Dónde se declara | Qué significa |
| --- | --- | --- |
| `CONTRATO_TEXTO_NO_INTERPRETABLE` | CU-03 §6.1 | El envío **procede**: el resultado trae estado `Borrador`, el texto conservado íntegro y las observaciones con índice de figura y campo |
| `CONTRATO_LISTADO_VACIO` | CU-04 §6.1 | La colección viene con cero elementos. La unidad pública distingue vacío de fallo por el tipo recibido, no por el conteo |
| `CONTRATO_TEXTO_NO_INTERPRETABLE` | CU-05 §6.1 | El detalle llega con la colección de piezas en cero elementos y las observaciones pobladas |

**Tres señales sobre dos identificadores**, porque la primera y la tercera son el mismo identificador visto desde dos contratos de uso. El cambio de contraseña pendiente **no es una señal**: impide la operación pedida, de modo que es un error transportado.

## 6. Versionado del contrato

Aplica el criterio de [`ADR-03`](Adrs/ADR-03-Versionado-Por-Compilacion-Compartida.md) §7. Lo esencial, sobre los elementos de este contrato:

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
| CU que lo consumen | CU-01 a CU-08, los ocho de la categoría 02 de este proyecto de código |
| RN que cubre | Ninguna redactada acá. Transporta las dieciséis, con el reparto de [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §10.3 |
| Restricciones transversales que materializa | `RT-01` a `RT-11`, con el reparto de [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §10.2 |
| ADR que lo gobiernan | ADR-01, ADR-02, ADR-03, ADR-04, ADR-05 |
| Consumidores | `GeometriaFactory-Api` y `GeometriaFactory-Web` |
| Tests previstos en 08 | Al menos una prueba de integración por tipo; prueba de inspección de superficie pública para §4.3; prueba de inspección del conjunto cerrado para §5.1 y §5.2 |

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Declara las ocho familias de tipos con sus códigos propios, los seis conjuntos cerrados, la lista de lo que ningún tipo lleva, el inventario completo de los **quince** códigos vivos con el contrato de uso donde se declara cada uno, los **tres** identificadores retirados con su motivo y la regla de no reciclado, las **tres** señales declaradas sobre dos identificadores, y el criterio de versionado con la columna que declara cuáles cambios no detecta la compilación. |
