# CU-02 — Admitir la petición: acceso, papel y marca

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** CU-02-Admitir-La-Peticion-Acceso-Papel-Y-Marca.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.13** §4.1 (RN-01, RN-13, **RN-16**), §4 (**F-04** precisada), §17.1.P.2 (INV-09), §17.5.P.3, §17.5.P.5 (autorización por papel **más** verificación de pertenencia), §14 (RA-01, RA-03); `Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/Especificacion-Funcional.md` §4, y en particular su cuarta comprobación transversal y su precisión 5; `Proyectos/GeometriaFactory-Infrastructure/.../CU-08-Emitir-El-Acceso-Firmado.md`; `Proyectos/GeometriaFactory-Contracts/.../CU-06-Contrato-De-Respuesta-De-Error.md`
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

Declarar la **guardia** que toda petición atraviesa antes de llegar a un caso de uso: verificar el acceso firmado, exigir el papel que el punto declara y aplicar la comprobación del cambio de contraseña pendiente.

No es un punto de acceso: **es una condición de once de los dieciséis**. Su defecto característico no es hacer mal lo que hace, sino **no alcanzar a alguno**: se rompe agregando un punto de acceso nuevo y olvidándose, y cuando eso pasa **nada falla**. Por eso tiene contrato propio y por eso sus criterios de aceptación cuentan puntos en lugar de ejercer uno.

Lo que este caso de uso **no** hace, y hay que dejarlo imposible de confundir: **no autoriza**. Verificar que el acceso trae papel `Administrador` no es lo mismo que verificar que quien pide puede operar sobre ese trabajo. La comprobación sobre el dato recuperado —pertenencia, facultad y alcance— es de `GeometriaFactory-Application`, y el intake §17.5.P.5 lo dice en una línea: **el rol no alcanza**.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| `GeometriaFactory-Web` | Primario | Presenta el acceso firmado en la cabecera de autorización de cada petición |
| Mecanismo de acceso firmado de `GeometriaFactory-Infrastructure` | Sistema | Verifica la firma y la expiración y devuelve los reclamos, o el motivo por el que el acceso no es válido |
| Capa de aplicación | Sistema | Es la que **sí** comprueba pertenencia, facultad y alcance, sobre el dato recuperado |

## 3. Precondiciones

- El servicio está atendiendo peticiones (CU-11).
- La clave de firma está provista (CU-10). **Sin clave, ningún acceso se puede verificar y ninguna petición se admite.**
- Cada punto de acceso declara qué papel exige, según la tabla de [`Definicion-Superficie-HTTP.md`](../Definicion-Superficie-HTTP.md) §3.

## 4. Flujo principal

1. Llega una petición a uno de los once puntos que exigen acceso.
2. Se toma el acceso de la cabecera de autorización.
3. Se verifica su firma y su expiración por el mecanismo de acceso firmado, que devuelve los cuatro reclamos.
4. Se compara el reclamo de papel contra el papel que el punto declara exigir.
5. Se comprueba, contra la capa de aplicación, que la cuenta **no** tenga la marca de cambio de contraseña pendiente.
6. La petición se admite y sigue hacia el caso de uso del punto, llevando la identidad y el papel del acceso.

**El paso 5 corta antes que cualquier otra cosa que el punto vaya a hacer.** Una cuenta marcada no lee ni escribe nada: es INV-09, y la capa de aplicación lo declara como su cuarta comprobación transversal.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El punto no exige ningún papel en particular y admite los dos | Los pasos 1 a 3 y el 5 se ejercen igual; el paso 4 se satisface con cualquiera de los dos valores. **Es el caso de los puntos de lectura de trabajos y del cambio de la propia contraseña**, y no es una excepción a la guardia | Paso 6 |
| FA-02 | La petición llega al punto de **cambio de la propia contraseña** con una cuenta marcada | Los pasos 1 a 4 se ejercen igual y **el paso 5 no rechaza**: es la **única excepción declarada**, porque cambiar la contraseña es lo único que esa cuenta puede hacer y es lo que levanta la marca | Paso 6 |
| FA-03 | La petición llega a uno de los cuatro puntos que no exigen acceso | **La guardia no se aplica**, y se declara para que su ausencia no se lea como un hueco: el canje de credenciales, el registro de una cuenta, la configuración del administrador y la salud se ejercen sin acceso por construcción | Termina fuera de este contrato |
| FA-04 | La petición llega a **A-05**, el cambio de la propia contraseña, con una cuenta que tiene la marca puesta | **Es la única excepción declarada de esta guardia**, y desde `PRODUCT-INTAKE` 1.13 cubre también el **primer ingreso**: la cuenta recién habilitada llega acá con la provisoria que la habilitación produjo, igual que la reseteada (**RN-16**). La guardia admite la petición porque cambiar la propia contraseña es lo único que INV-09 le deja hacer | Paso siguiente, con el cambio ejercido |

## 6. Excepciones y errores

| Código del contrato | Respuesta | Causa |
| --- | --- | --- |
| — | `401` | No hay acceso en la petición, el acceso está vencido, o su firma no corresponde. **Las tres responden igual**: el cuerpo no declara cuál de las tres ocurrió |
| `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` | `403` | El papel del acceso no es el que el punto exige, **en el punto del desenlace**. Para los demás puntos que exigen `Administrador` **el conjunto cerrado del contrato no declara ningún código**, y es punto abierto: ver §10 |
| `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` | `403` | La cuenta tiene una provisoria sin cambiar. **Un solo código para todas las operaciones bloqueadas**, porque lo que le queda por hacer al consumidor es siempre lo mismo |

**El `401` de la guardia no lleva código del contrato, y es deliberado.** El conjunto cerrado no tiene ninguno que describa un acceso ausente o inválido, y **esta capa no inventa códigos**: lo que el contrato no declara, no viaja como código. Lo que viaja es el código de respuesta, que es lo que la pieza pública necesita para volver a canjear credenciales.

**Ninguna condición de esta guardia llega al caso de uso del punto.** Si la guardia rechaza, **no se lee ni se escribe nada**, y ese es el criterio con el que se verifica: se comprueba el estado del almacén después del rechazo, no la respuesta.

## 7. Postcondiciones

- **Admitida:** el caso de uso del punto recibe la identidad y el papel del acceso, y **nada más**: la guardia no agrega ningún dato a la petición.
- **Rechazada:** la pieza pública recibe `401` o `403`, **el almacén queda exactamente como estaba** y el intento queda registrado del lado del servidor.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | La superficie completa de [`Definicion-Superficie-HTTP.md`](../Definicion-Superficie-HTTP.md) §3 | Se recorren sus **16** puntos | **11** exigen acceso y aplican esta guardia; **4** no exigen acceso por construcción; **1** tiene su identificación abierta. 11 + 4 + 1 = 16 |
| CA-02 | Los **11** puntos que exigen acceso | Se invoca cada uno **sin** cabecera de autorización | Los 11 responden `401`, y en los 11 el almacén queda **sin ningún cambio** |
| CA-03 | Los mismos 11 puntos, con un acceso vencido y con un acceso firmado con otra clave | Se invoca cada uno con cada uno de los dos | Las 22 respuestas son `401` y sus cuerpos son indistinguibles del de CA-02 |
| CA-04 | Un acceso válido con papel `Alumno` | Se invoca cada uno de los puntos que exigen `Administrador` | Todos responden `403` y **0 de ellos leen o escriben** el recurso pedido |
| CA-05 | Una cuenta con la marca de cambio de contraseña pendiente, con acceso válido | Se invocan **todos** los puntos que exigen acceso salvo el del cambio de la propia contraseña | **Todas** las respuestas traen `403` con el **mismo** código del contrato, con 0 detalles y sin nombrar la operación pedida |
| CA-06 | La misma cuenta marcada | Se invoca el punto de cambio de la propia contraseña con la provisoria correcta | La guardia **admite**, el cambio procede y la marca queda levantada. **Es la única excepción, y es una** |
| CA-07 | Un acceso válido con papel `Administrador` sobre un punto que admite los dos papeles | Se invoca | La guardia admite, y **la comprobación de alcance sobre el dato la hace la capa de aplicación**, no ésta |
| CA-08 | Cualquier rechazo de esta guardia, con el cuerpo y el registro del servidor observados | Se produce | **0 apariciones** de la clave de firma, del acceso presentado, de la ruta del almacén y de la dirección de cualquier servicio interno |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-02 |
| Reglas de negocio aplicables | [**RN-13**](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), **con el tramo que esta capa puede romper sola**: un punto de acceso fuera de la guardia la incumple sin que nada falle. [RN-01](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md), por el papel que cada punto exige. [RN-10](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-10-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md), en el único punto donde el papel insuficiente tiene código del contrato |
| Invariante del producto | **INV-09**: una cuenta marcada no ejerce ninguna capacidad salvo cambiar su propia contraseña. Esta guardia es su expresión en la frontera del proceso |
| Reglas de arquitectura del producto | **RA-01**: la guardia da por sentado que quien presenta el acceso es la pieza pública. **RA-03**: ningún rechazo expone secretos ni direcciones, y todo rechazo queda registrado |
| Puntos de acceso que gobierna | A-05 a A-15, **once** |
| Historias de usuario a generar en 06 | US-04, US-05, US-06 |
| Componentes esperados en 05 | Guardia de admisión previa a todo punto que exija acceso; conexión con el verificador del acceso firmado y con la comprobación de la marca |
| Tests previstos en 08 | **Una prueba por punto y por condición**, no una prueba por condición: es la única forma de detectar el punto que quedó afuera. Y una prueba estructural que compare la lista de puntos contra la lista de puntos guardados |

## 10. Notas y supuestos

- **El hueco del código de facultad es real y está declarado.** El conjunto cerrado de **quince** códigos del ensamblado de contratos tiene un único código de facultad, **acotado por su enunciado al desenlace**. La capa de aplicación emite un motivo de facultad requerida también en el gobierno de cuentas, en la revisión de la comisión y en el reseteo, y para esos tres caminos **el contrato no declara ningún código**. Verificado recorriendo la §6 de los ocho contratos de uso. Esta capa **no inventa uno**: los códigos son del ensamblado. Está en el índice maestro §11 y elevado al Product Owner.
- **Exigir el papel no reemplaza a comprobar la pertenencia**, y duplicar la comprobación acá sería peor que no hacerla: crearía un segundo lugar donde la regla puede decir otra cosa. Lo que esta guardia aporta es cortar temprano lo que **ningún dato podría autorizar**.
- **La guardia no distingue las tres causas del `401`.** Un acceso ausente, uno vencido y uno con firma que no corresponde reciben la misma respuesta, porque para la pieza pública el trabajo que queda es el mismo: volver a canjear credenciales.
- **El punto de establecimiento de la contraseña inicial no está entre los once**, y no porque se lo haya eximido: **no puede exigir un acceso que la persona todavía no puede obtener**. Su forma es un punto abierto elevado al Product Owner y descrito en `CU-03` §10.
- **La verificación del acceso es un mecanismo de `GeometriaFactory-Infrastructure`.** Lo propio de esta capa es **exigirlo en cada punto**, que es la parte que nadie más puede hacer.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-16) y la precisión de F-04.** El punto de acceso **A-04 se retiró** de la superficie —era el que tenía su forma de identificación abierta— y su capacidad se ejerce por A-05, bajo esta misma guardia. **§5**: **FA-04** se rehace: deja de describir un punto sin guardia definida y pasa a describir la **excepción declarada** de A-05, que desde el intake 1.13 cubre el primer ingreso además del cambio posterior a un reseteo. **§10**: el conjunto cerrado del ensamblado pasa de diecisiete a **quince** códigos. La cabecera cita el intake **1.13**. **La guardia, sus tres causas de `401` y su recuento de once puntos protegidos no cambian**, y ahora esos once son todos los que exigen acceso: no queda ninguno aparte. Sube minor. |
