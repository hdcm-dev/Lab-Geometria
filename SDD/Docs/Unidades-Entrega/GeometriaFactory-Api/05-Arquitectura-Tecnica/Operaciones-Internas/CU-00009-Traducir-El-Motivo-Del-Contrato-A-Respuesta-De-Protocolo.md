# CU-00009 — Traducir el motivo del contrato a respuesta de protocolo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** CU-00009-Traducir-El-Motivo-Del-Contrato-A-Respuesta-De-Protocolo.md
**Versión:** 1.3
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00004`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md), [`NB-00008`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00008-Alcance-Del-Laboratorio-Desde-El-Aula.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.26** §4.1 (RN-00003, RN-00009, **RN-00016**), §7 (CL-2, CL-5, CL-8), §14 (**RA-03**), §17.5.P.5, §17.5.P.10; `Proyectos/GeometriaFactory-Contracts/.../CU-00006-Contrato-De-Respuesta-De-Error.md` completo, y la §6 de sus otros siete contratos de uso; `Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/Especificacion-Funcional.md` §4; `Proyectos/GeometriaFactory-Infrastructure/03-UX-UI-DX/DX-Error-Messages.md` §1.3, §2.3 y §5, que declara que la traducción de sus condiciones hacia afuera del proceso pertenece a este proyecto de código
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Api

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

---

## 1. Propósito

Declarar **cómo un fallo se convierte en una respuesta**, que es lo que ningún punto de acceso puede declarar por su cuenta y lo que ninguna capa de adentro puede hacer.

No es un punto de acceso: es una responsabilidad de **todos** ellos, y su unidad de verificación no es una ruta sino **el conjunto cerrado de diecisiete códigos** del ensamblado de contratos. Se prueba recorriéndolo entero, no ejerciendo un camino.

Dos cosas se deciden acá y ninguna otra capa puede repararlas:

1. **Qué código de contrato recibe un motivo interno.** Los motivos de la capa de aplicación y las condiciones de los adaptadores **no cruzan la frontera**: llegan hasta acá y acá se convierten en uno de los diecisiete. Cuando ninguno corresponde, lo que corresponde es el genérico, y **el hueco se declara en lugar de inventarse un código**.
2. **Qué código de respuesta recibe cada código de contrato.** Es la traducción que RN-00003 vuelve crítica: **si el trabajo ajeno recibiera un código distinto del inexistente, la regla estaría rota hacia afuera** y ninguna capa de adentro lo notaría, porque las dos habrían hecho su parte bien.

Y una tercera que es una prohibición: **nada de lo que sale por acá puede exponer lo que RA-03 prohíbe**. Es la última vez que un dato del backend se toca antes de salir del servidor propio.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Los quince puntos de acceso que pueden fallar | Primario | Entregan un motivo o una condición y reciben una respuesta armada |
| `GeometriaFactory-Web` | Consumidor | Recibe el código de respuesta y el código del contrato, y decide qué mostrarle a la persona y cuándo pasar a estado degradado |
| Registro del lado del servidor | Sistema | Recibe **todo** error respondido y todo intento de acceso rechazado |

## 3. Precondiciones

- Existe un motivo de la capa de aplicación, una condición de un adaptador, o una falla no prevista.
- El conjunto cerrado de códigos del contrato es el que declara el ensamblado, **y esta capa no lo amplía**.

## 4. Flujo principal

1. Un punto de acceso termina con un motivo interno en lugar de un resultado.
2. Se busca el código del contrato que le corresponde, dentro del conjunto cerrado de **diecisiete**.
3. Se busca el código de respuesta que le corresponde a ese código, en la tabla de [`Definicion-Superficie-HTTP.md`](../Definicion-Superficie-HTTP.md) §6.
4. Se compone la respuesta de error del contrato: **código, texto neutro, detalles de ubicación cuando los hay y momento**, y nada más.
5. Se verifica que el texto neutro **no contenga direcciones de servicio, rutas de archivo de datos ni valores de secreto**.
6. Se responde, y **se registra del lado del servidor**.

**El paso 5 no es una recomendación de estilo: es RA-03**, que es regla de nivel producto, y el paso 6 es su contracara obligatoria. Sin el registro, la prohibición de exponer se convierte en imposibilidad de diagnosticar.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El motivo interno **no tiene** código correspondiente en el conjunto cerrado | Se usa el **código genérico**, con el código de respuesta que la causa determine. **No se inventa un código nuevo**: los códigos son del ensamblado de contratos y ampliarlos es una decisión de aquel proyecto de código. El hueco se declara en §10 | Paso 3 |
| FA-02 | Lo que falla es una **terminación degradada de un adaptador** —el almacén que no responde, una escritura concurrente rechazada, la fuente de material impredecible que no respondió— | Se usa el código genérico con respuesta `503`. **Esta capa no reintenta**, igual que la capa que la produjo: quien decida reintentar es la pieza pública | Paso 4 |
| FA-03 | La falla **no estaba prevista** por ninguna capa | Se usa el código genérico con respuesta `500`, y **el texto no lleva ninguna traza de la implementación**. Es la garantía de que nunca llega un fallo sin representación, que es la definición de fallo silencioso que el producto viene a eliminar | Paso 4 |
| FA-04 | El fallo ocurre **antes** de que la petición llegue a ser el tipo del contrato | Se responde sin código de contrato: es el `401` de la guardia y el `400` de un cuerpo o un valor que no se puede leer. **No se fuerza un código que no corresponde** | Paso 6 |
| FA-05 | La observación viene de la interpretación del texto del alumno | **No es un fallo y no pasa por este contrato**: viaja como parte de un resultado exitoso, con su índice de figura y su campo. Lo único que esta capa hace con ella es **no recortarla** | Termina fuera de este contrato |

## 6. Excepciones y errores

Este contrato no declara condiciones propias: **es el que las traduce**. Lo que sí declara son las **tres reglas de asignación** que ningún punto de acceso puede contradecir.

| Regla de asignación | Enunciado | Por qué |
| --- | --- | --- |
| **R-1 · Lo ajeno se responde como inexistente** | El trabajo que no es del solicitante, el que está fuera de lo que ve y el que no existe reciben **el mismo código de respuesta, el mismo código de contrato y cuerpos idénticos** | Es RN-00003. Distinguirlos permitiría averiguar por tanteo qué identificadores existen, y es la única regla del producto que **esta capa puede romper sola** |
| **R-2 · Lo genérico no explica** | La respuesta genérica de credenciales inválidas **no declara cuál de los dos campos falló**, y el correo ocupado **no declara la situación ni el papel** de la cuenta que lo ocupa | Es la misma familia: no confirmar la existencia de algo que el solicitante no debería saber que existe |
| **R-3 · Lo degradado se declara, no se disfraza** | Una terminación degradada responde con su código y **no devuelve un resultado vacío como si fuera un resultado**. Un listado vacío y un servicio caído **no se parecen en nada** desde el otro lado | Es lo que permite que la pieza pública presente estado degradado explícito en lugar de una comisión que parece no haber entregado nada |

## 7. Postcondiciones

- **Siempre:** la pieza pública recibe un código de respuesta y, cuando corresponde, un código del conjunto cerrado, un texto neutro y la ubicación del defecto.
- **Nunca:** la respuesta transporta direcciones de servicio interno, rutas de archivos de datos, valores de secreto o trazas de la implementación.
- **Siempre:** el error quedó registrado del lado del servidor, con la excepción declarada de la contraseña provisoria y del texto del alumno, que **no entran al registro**.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | El conjunto cerrado de **17** códigos del ensamblado de contratos | Se recorre entero contra la tabla de `Definicion-Superficie-HTTP.md` §6 | **16** tienen código de respuesta asignado y **1** no lo tiene: el que describe la ausencia de respuesta de esta pieza, que **esta superficie no puede producir**. 16 + 1 = 17 |
| CA-02 | La misma tabla | Se recorre en sentido inverso | **0** filas citan un código que no pertenezca al conjunto cerrado, y **0** códigos del conjunto quedan sin fila |
| CA-03 | Un trabajo del alumno A y un identificador que no existe | El alumno B los pide por **todos** los puntos que los aceptan | Todas las respuestas son iguales entre sí: mismo código de respuesta, mismo código de contrato y cuerpos **idénticos** |
| CA-04 | Un canje con contraseña equivocada y otro con un correo inexistente | Se comparan las dos respuestas | Son **idénticas**: 0 campos permiten distinguirlas |
| CA-05 | El almacén no disponible | Se invoca cualquier punto que lo necesite | Responde `503` con el código genérico, y **no** una colección vacía ni un resultado parcial |
| CA-06 | Una falla no prevista, provocada en prueba | Se invoca el punto afectado | Responde `500` con el código genérico y **0 trazas** de implementación en el cuerpo |
| CA-07 | Todas las respuestas de error de la superficie, con sus cuerpos y el registro del servidor | Se inspeccionan | **0 apariciones** de la ruta del almacén, de la clave de firma, de una contraseña, de la provisoria, del texto del alumno y de la dirección de cualquier servicio interno; y **cada** error respondido tiene su entrada en el registro |
| CA-08 | Un trabajo enviado con el texto de **E-5** | Se pide su detalle | La observación llega con **índice de figura 1** y campo `Tipo`: la ubicación **no se recortó al traducir** |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-00004, por la ubicación que no se pierde; NB-00008, porque la respuesta neutra es lo que permite presentar estado degradado sin revelar la dirección del servicio que falló |
| Reglas de negocio aplicables | [**RN-02003**](../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), en la regla de asignación R-1. [RN-02009](../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md), por la ubicación que cruza sin recortarse |
| Regla de arquitectura del producto | **RA-03**, y es el contrato donde se ejerce entera: ninguna respuesta expone direcciones de servicios internos, y todo error respondido queda registrado del lado del servidor |
| Puntos de acceso que gobierna | Los **15** que pueden fallar. El punto de salud responde por sí mismo y no pasa por este contrato |
| Contrato de uso que transporta | `GeometriaFactory-Contracts` `CU-00006`, el transversal del ensamblado |
| Historias de usuario a generar en 06 | US-00024, US-00025 |
| Componentes esperados en 05 | Un único lugar de traducción, común a todos los puntos; y la decisión sobre los dos huecos del conjunto cerrado, si el Product Owner los cierra |
| Tests previstos en 08 | **Una prueba por código del conjunto cerrado**, no una por punto de acceso; la comparación de respuestas de CA-03 y CA-04; y la inspección de secretos y rutas sobre todas las respuestas de error |

## 10. Notas y supuestos

- **Los dos huecos del conjunto cerrado, declarados y no resueltos.** Verificado recorriendo la §6 de los ocho contratos de uso del ensamblado:
  1. **No hay código para «el papel no alcanza» fuera del desenlace.** La capa de aplicación emite un motivo de facultad requerida también en el gobierno de cuentas, en el reseteo y en la revisión de la comisión; el único código de facultad del conjunto cerrado está acotado por su enunciado al desenlace.
  2. **No hay código para «el trabajo no está en `Borrador`» fuera de la eliminación.** El código análogo está acotado por su enunciado a la eliminación y al camino del alumno, y el envío y la reedición forzados no tienen dónde ir.
  En los dos casos lo que queda es el genérico, que responde el número correcto y **no dice el motivo con la precisión que el producto ya sabe darle en el caso vecino**. **Esta capa no inventa códigos**: ampliarlos es decisión del ensamblado de contratos y del Product Owner, y está elevado en el índice maestro §11.
- **El código que describe la ausencia de respuesta de esta pieza no se produce acá**, y no es un olvido: el ensamblado lo declara como el único que la propia pieza pública produce. Una respuesta de esta superficie con ese código sería una contradicción —**si hubo respuesta, el servicio respondió**—.
- **La distinción entre `500` y `503` bajo el mismo código de contrato es derivación de esta categoría.** El conjunto cerrado no permite distinguirlas por contrato, sólo por número. Si el Product Owner quisiera que la pieza pública las distinga sin mirar el número, haría falta un código nuevo, y eso es decisión del ensamblado.
- **Esta capa no reintenta nada.** La capa que produce las terminaciones degradadas ya lo declara, y acá se conserva: se informa el estado y quien decida reintentar es la pieza pública, que es la que sabe qué estaba haciendo la persona.
- **Lo que sale de acá no es un mensaje para una persona.** Es un código y un texto neutro; la pieza pública compone lo que la persona lee, y está sujeta a la misma prohibición.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-00016) y la precisión de F-04.** El conjunto cerrado de códigos del ensamblado —que es la **unidad de verificación** de este caso de uso— pasa de diecisiete a **quince**: salen `CONTRATO_CONTRASENA_NO_ESTABLECIDA` y `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA`, los dos por imposibilidad de su causa, y ninguno entra. §1 y §4 actualizan el recuento, y la prueba prevista de §9 —**una por código del conjunto cerrado**— pasa por lo tanto de diecisiete a **quince** casos. La cabecera cita el intake **1.13**. **Las dos traducciones, la tabla de destinos y las tres reglas de asignación de §6 no cambian de forma.** Sube minor. |
| 1.2 | 2026-08-11 | **Cierra los hallazgos `B-API-03` (P1), `B-API-15` (P3) y `B-API-13` (P3)** del informe [`B-02-03-GeometriaFactory-Api-r1.md`](../../../../Audit/B-02-03-GeometriaFactory-Api-r1.md) 1.0. **§1**, punto 1: los motivos internos se convierten en uno de los **quince** códigos y no de los diecisiete. Era la **segunda** mención del recuento en §1 —el párrafo inmediatamente anterior ya decía «el conjunto cerrado de quince códigos», y §4 paso 2 también—, y la fila 1.1 de esta tabla declaraba «§1 y §4 actualizan el recuento» cuando §1 quedó actualizado a medias. **La fila 1.1 se corrige en su auto-cita**: las condiciones de §6 no son siete, porque §6 **no declara condiciones propias** —es el contrato que las traduce— sino **tres reglas de asignación**, `R-1` a `R-3`, contadas sobre su tabla y coincidentes con lo que [`../README.md`](../README.md) §2 describe. **Cabecera**: pasa a citar `PRODUCT-INTAKE` **1.26**, vigente hoy. **Búsqueda de propagación hecha con `grep` sobre todo el corpus vivo**: «diecisiete» como tamaño del conjunto cerrado no queda vivo en ninguna otra afirmación de las categorías 02 y 03 —las ocurrencias restantes son filas de control de cambios, que registran el paso de diecisiete a quince y son correctas—; los otros dos lugares que lo conservaban, `Especificacion-Funcional.md` §5 y §8, se corrigen en la misma tanda. **Las dos traducciones, la tabla de destinos, las tres reglas de asignación y los dos huecos declarados no cambian.** Sube minor. |
| 1.3 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **3**. Sube minor. |
