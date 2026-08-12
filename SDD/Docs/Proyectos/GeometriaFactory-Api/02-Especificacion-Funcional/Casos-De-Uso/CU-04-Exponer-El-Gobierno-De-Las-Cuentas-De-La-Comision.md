# CU-04 — Exponer el gobierno de las cuentas de la comisión

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** CU-04-Exponer-El-Gobierno-De-Las-Cuentas-De-La-Comision.md
**Versión:** 1.3
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.26** §4 (F-03, **F-04** precisada), §4.1 (RN-01, RN-06, RN-07, RN-13, RN-14, **RN-16**), §17.1.P.2 (**INV-09**), §7 (CL-6), §14 (RA-03), §17.5.P.5; `Proyectos/GeometriaFactory-Contracts/.../CU-02-Contrato-De-Administracion-De-Cuentas.md`; `Proyectos/GeometriaFactory-Application/.../CU-02-Gobernar-Las-Cuentas-De-La-Comision.md`; `Proyectos/GeometriaFactory-Infrastructure/.../CU-04-Ejecutar-El-Borrado-Fisico-Y-El-Arrastre-De-La-Baja.md`
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

Exponer los **tres** puntos de acceso con los que el administrador gobierna las cuentas de la comisión: el listado (**A-06**), el cambio de situación (**A-07**) y la baja física (**A-08**). Los tres exigen papel `Administrador` y los tres están bajo la guardia de CU-02.

**Desde `PRODUCT-INTAKE` 1.13, A-07 devuelve además la contraseña provisoria cuando la situación pretendida es habilitada.** **RN-16** declara que habilitar una cuenta produce una provisoria con el mismo mecanismo y el mismo tratamiento que el reseteo de CU-05: el sistema la produce, la respuesta se la devuelve al administrador **una sola vez** para que se la comunique, y la cuenta queda con **cambio de contraseña pendiente** (INV-09). Es lo que suprime del producto el punto de establecimiento anónimo que exponía CU-03.

El tercero es **la única operación destructiva de toda esta superficie**: la baja elimina la cuenta **y todos sus trabajos**, y no se deshace. Por eso su punto de acceso transporta un campo que ningún otro tiene —**el correo escrito como confirmación**— y por eso este contrato declara qué pasa cuando ese campo no coincide antes que ninguna otra cosa.

Lo que este caso de uso **no** hace: no compara el correo de confirmación —eso lo hace la capa de aplicación—, no ejecuta el arrastre —eso lo hace el adaptador de almacenamiento, todo o nada— y **no decide qué transiciones de situación son admisibles** —eso es del dominio—.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| `GeometriaFactory-Web` | Primario | Arma las tres solicitudes desde el panel del administrador y las envía con el acceso firmado |
| Administrador | Sujeto de la regla | Es quien ejerce las tres operaciones, **a través de la pieza pública** |

## 3. Precondiciones

- La petición trae acceso firmado con papel `Administrador` y atravesó la guardia de CU-02.
- El servicio arrancó y dejó el almacén en condiciones (CU-11).

## 4. Flujo principal

1. Llega una petición a **A-06**.
2. Se pide a la capa de aplicación el listado de cuentas.
3. Se responde `200` con la colección de elementos de listado: correo, nombre, apellido, situación y fecha de registro de cada cuenta, **y su marca de cambio de contraseña pendiente**.
4. Llega una petición a **A-07** con el identificador de la cuenta y la situación pretendida.
5. Se ejerce el cambio contra la capa de aplicación, que verifica la facultad sobre el dato y valida la transición contra el dominio. **Si la situación pretendida es habilitada**, la capa de aplicación obtiene además la provisoria ya producida y ya derivada, la fija como credencial y deja la marca de cambio pendiente. La provisoria **no queda en ninguna traza del servidor**, con la misma exigencia que declara CU-05.
6. Se responde `200` con la situación resultante.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El administrador da de baja una cuenta por **A-08** | La solicitud trae el identificador **y el correo escrito como confirmación**. La capa de aplicación compara, y el retiro de la cuenta y de todos sus trabajos ocurre **en una sola unidad de trabajo**. Se responde `204`, sin cuerpo | Termina |
| FA-02 | El listado se pide sobre una comisión sin ninguna cuenta de alumno | Se responde `200` con una colección vacía. **Un listado vacío no es un fallo**: la pieza pública distingue vacío de fallo por el tipo recibido y no por el conteo | Termina |
| FA-03 | Se pide la baja de la cuenta con papel `Administrador` | La capa de aplicación la rechaza con su motivo propio: **esa cuenta no admite baja**, por el invariante que la sostiene siempre habilitada. Ver §10 sobre el código que la transporta | Termina |
| FA-04 | Se pide una transición de situación que el dominio no admite | La capa de aplicación la rechaza. Ver §10: el conjunto cerrado del contrato **no declara un código para este camino** | Termina |

## 6. Excepciones y errores

| Código del contrato | Respuesta | Punto | Causa |
| --- | --- | --- | --- |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | `400` | A-07, A-08 | Falta el identificador, la situación pretendida o el correo de confirmación. La respuesta **nombra el campo ausente** |
| `CONTRATO_CONFIRMACION_NO_COINCIDE` | `400` | A-08 | El correo escrito no coincide con el de la cuenta. **La baja no procede y la respuesta no devuelve el correo esperado** |
| `CONTRATO_ALUMNO_NO_ENCONTRADO` | `404` | A-07, A-08 | La cuenta referenciada no existe. **Adopción declarada: ver §10** |
| `CONTRATO_ERROR_NO_CLASIFICADO` | `403`, `500` o `503` | Los tres | `403` cuando el papel no alcanza y el contrato no tiene código propio para ese camino (§10); `503` cuando el almacén no está disponible; `500` ante un defecto no previsto |

**La baja no deja retiro parcial.** El adaptador de almacenamiento lo garantiza —todo o nada, en una sola unidad de trabajo— y esta superficie lo hace observable: **una baja interrumpida responde con fallo y la cuenta y sus trabajos quedan enteros**. No hay ninguna respuesta de esta superficie que signifique «se borró una parte».

## 7. Postcondiciones

- **A-06 con éxito:** la pieza pública tiene el listado, **sin ninguna forma de la credencial de ninguna cuenta**.
- **A-07 con éxito:** la cuenta quedó en la situación resultante, que la respuesta declara. **Sus trabajos no se tocaron.** Si la situación pretendida fue habilitada, la respuesta trae además **la contraseña provisoria una sola vez** y la declaración del cambio pendiente; si fue bloqueada, no trae ninguna.
- **A-08 con éxito:** la cuenta **y todos sus trabajos** dejaron de existir. Se verifica comprobando que **no queda ningún trabajo** del alumno dado de baja.
- **Fallo:** el almacén queda como estaba y el intento queda registrado del lado del servidor.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un acceso con papel `Administrador` y tres cuentas de alumno | Se invoca A-06 | Responde `200` con **3** elementos, cada uno con situación y marca, y **0 campos** que transporten una credencial en cualquiera de sus formas |
| CA-02 | Una cuenta en situación `Pendiente` | Se invoca A-07 pidiendo habilitarla | Responde `200` con la situación resultante, **1 contraseña provisoria** y el cambio pendiente declarado, y sus trabajos quedan **sin ningún cambio** |
| CA-08 | Dos cuentas `Pendiente` distintas | Se las habilita a las dos por A-07, y después se bloquea y se rehabilita la primera | Las **3** provisorias devueltas son distintas entre sí y la del bloqueo son **0**; y en el registro del servidor la provisoria aparece **0 veces** |
| CA-03 | Un alumno con tres trabajos y un acceso de administrador | Se invoca A-08 con el correo correcto | Responde `204` y quedan **0** trabajos de ese alumno |
| CA-04 | El mismo alumno | Se invoca A-08 con un correo de confirmación distinto | Responde `400`, la cuenta **sigue existiendo** y sus **3** trabajos siguen ahí |
| CA-05 | Un acceso con papel `Alumno` | Se invocan los **3** puntos | Las 3 respuestas son de fallo y **0 de ellas leen o modifican** ninguna cuenta |
| CA-06 | Un identificador de cuenta que no existe | Se invocan A-07 y A-08 | Las **2** responden `404` |
| CA-07 | Cualquier respuesta de §6, con el cuerpo y el registro del servidor observados | Se produce | **0 apariciones** de la ruta del almacén, de la clave de firma y de la dirección de cualquier servicio interno |
| CA-08 | El almacén interrumpido a mitad de una baja | Se invoca A-08 | Responde `503`, y la cuenta y **todos** sus trabajos siguen enteros: **0 retiros parciales** |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01 |
| Reglas de negocio aplicables | [RN-07](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md), con su tramo acá: **el punto transporta el correo escrito y no procede sin él**. [RN-01](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md), por el papel que los tres puntos exigen y por la cuenta que no admite baja. [RN-06](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), porque la situación que este punto cambia es la que decide el acceso. [RN-12](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-12-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) **por contraste**: el reseteo es otro punto, otro verbo y otro contrato, y **no pasa por acá** |
| Regla de arquitectura del producto | **RA-03** en las condiciones de §6 |
| Puntos de acceso | A-06, A-07, A-08 |
| Contrato de uso que transporta | `GeometriaFactory-Contracts` `CU-02` |
| Historias de usuario a generar en 06 | US-11, US-12, US-13 |
| Componentes esperados en 05 | Tres puntos de acceso, con el papel exigido declarado en cada uno |
| Tests previstos en 08 | Integración por los ocho criterios, **incluida la de forzar los tres puntos con un acceso de alumno**; y la verificación de que la baja no deja trabajos huérfanos |

## 10. Notas y supuestos

- **Adopción declarada del código de cuenta no encontrada.** El conjunto cerrado de **diecisiete** códigos del ensamblado de contratos tiene `CONTRATO_ALUMNO_NO_ENCONTRADO`, cuya causa declarada es que **el filtro por alumno de un listado de trabajos referencie un identificador inexistente**. Esta categoría lo **adopta** para la cuenta que A-07 y A-08 referencian y no existe, porque describe exactamente la misma situación desde otro punto de acceso. **Es una ampliación de causa, no un código nuevo**, y se declara para que no se lea como si el contrato ya la hubiera previsto.
- **El papel insuficiente en estos tres puntos no tiene código propio en el contrato.** El único código de facultad del conjunto cerrado está acotado por su enunciado al desenlace de la revisión. Mientras eso no se resuelva, el camino disponible es el código genérico con respuesta `403`, que **dice el número correcto y no dice el motivo con la precisión que el producto ya sabe darle en el caso vecino**. Está elevado al Product Owner en el índice maestro §11.
- **La baja no es el remedio del olvido de contraseña, y esta superficie lo hace evidente por construcción.** El reseteo es un punto de acceso distinto, con otro verbo, que **conserva la cuenta y todos sus trabajos**. Que sean dos puntos y no dos variantes del mismo es lo que impide confundirlos desde afuera.
- **El listado transporta la marca de cambio de contraseña pendiente**, y no la contraseña ni su valor derivado. Es lo que le permite al administrador ver que una cuenta que él **habilitó o reseteó** todavía no cambió su provisoria, sin conocer ningún valor.
- **La provisoria de A-07 y la de A-09 son el mismo mecanismo con dos disparadores**, y las dos exigencias de CU-05 valen igual acá: **el administrador no la escribe** (RN-14), la respuesta la devuelve **una sola vez** y **no entra al registro del servidor** (RA-03). Que vivan en contratos de uso distintos es una consecuencia del recorte por punto de acceso, no una diferencia de tratamiento.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-16) y la precisión de F-04**: habilitar una cuenta **produce su contraseña provisoria**, con el mismo mecanismo y el mismo tratamiento que el reseteo de CU-05. **§1** declara el cambio y su consecuencia: es lo que suprime el punto de establecimiento anónimo de CU-03. **§4** paso 5 suma la producción, la fijación y la marca, con la exclusión de la provisoria del registro del servidor. **§7** parte la postcondición de A-07 según la situación pretendida. **§8** rehace CA-02 y suma **CA-08**, que verifica tres provisorias distintas, ninguna en el bloqueo y **0 apariciones** en el registro. **§10** amplía la nota del listado a los dos orígenes de la marca, suma la nota que declara que las dos provisorias son el mismo mecanismo, y actualiza el conjunto cerrado del ensamblado a **quince** códigos. La cabecera cita el intake **1.13**. **Los tres puntos de acceso, sus verbos y sus códigos de respuesta no cambian.** Sube minor. |
| 1.2 | 2026-08-11 | **Cierra el hallazgo `B-API-13` (P3)** del informe [`B-02-03-GeometriaFactory-Api-r1.md`](../../../../Audit/B-02-03-GeometriaFactory-Api-r1.md) 1.0, en la extensión que la búsqueda de propagación que el propio informe exige dejó al descubierto: la cabecera citaba `PRODUCT-INTAKE` **1.13** y pasa a citar **1.26**, vigentes hoy. El informe listaba **nueve** cabeceras envejecidas y sólo una de esta carpeta, `CU-12`; el `grep` sobre las categorías 02 y 03 devuelve **diecinueve** archivos con la cita vieja, **los doce casos de uso entre ellos**, y los diecinueve se corrigen en esta tanda. Se abrieron las secciones del intake que este caso de uso cita y **su contenido no cambió** entre 1.13 y 1.26 en nada que este documento afirme, de modo que **no había ninguna afirmación falsa**: lo que se repara es la trazabilidad. **Ningún paso, código, regla, criterio de aceptación ni recuento cambia.** Sube minor. |
| 1.3 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
