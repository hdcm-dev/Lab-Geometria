# ADR-04 — Dos traducciones en orden, con una tabla única y sin inventar códigos

**Proyecto de código:** GeometriaFactory-Api
**Documento:** ADR-04-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Comunicación

---

## 1. Contexto

Una petición que falla atraviesa **dos** traducciones antes de convertirse en una respuesta —motivo interno a código del contrato, y código del contrato a código de respuesta— y confundirlas es el defecto característico de esta capa. Las dos son de acá y **ninguna otra capa las puede reparar**: si acá se elige mal, la regla se rompe hacia afuera sin que nadie de adentro se entere.

El caso más caro tiene nombre. `RN-03` exige que el trabajo ajeno responda «no encontrado» y **nunca** «no autorizado», y la capa de aplicación declara que quien traduce su motivo es **el consumidor**, que es este proyecto de código. Elegir el número informativo en lugar del correcto confirma la existencia de un recurso ajeno y permite averiguar por tanteo qué identificadores existen.

Y hay una restricción que atraviesa todo: **el conjunto de códigos del contrato es cerrado y tiene quince códigos vivos**, unión de los que declaran los ocho contratos de uso de `GeometriaFactory-Contracts`. Sobre **dieciocho** identificadores que ese ensamblado emitió a lo largo de su historia, **tres quedaron retirados y ninguno se recicla**: el del texto no interpretable, que dejó de describir un fallo cuando el envío pasó a ser la acción única de guardado, y los dos que `RN-16` volvió imposibles al unificar el mecanismo de credencial inicial. Esta capa **no agrega ninguno**.

Motivación upstream: NB-04, NB-08, NB-09; RN-02, RN-03, RN-06, RN-09, RN-10, RN-15; INV-01, INV-02, INV-07; `PRODUCT-INTAKE` §14 (RA-03), §17.5.P.5, §17.5.P.10.

## 2. Decisión

**Un único traductor, transversal a los quince puntos, con una única tabla, y ningún camino de fallo que lo evite.** Seis reglas:

1. **La tabla de traducción es una sola y vive en [`../Contratos-REST.md`](../Contratos-REST.md) §5**, con **quince** filas para los quince códigos vivos: **catorce con destino** y **uno declarado sin destino**, porque describe la ausencia de respuesta de esta pieza y **una respuesta con ese código sería una contradicción en sus términos**.
2. **Ningún código se inventa, se renombra ni se traduce a texto.** Donde el conjunto cerrado no tiene un código para un camino, el que corresponde es el genérico y **el hueco se declara**. Hay **dos** huecos declarados, y son el motivo por el que el código genérico tiene **cuatro** destinos en lugar de dos.
3. **Dos respuestas no llevan ningún código del contrato, y la ausencia es deliberada**: el `401` de la guardia —acceso ausente, vencido o con firma que no corresponde— y el `400` de una petición que **no llega a ser el tipo del contrato**. Las dos ocurren **antes** de que haya un contrato con el que hablar.
4. **Tres familias dicen menos de lo que el servicio sabe, y en las tres es la decisión y no el defecto**: credenciales inválidas sin declarar qué campo falló, recurso que no se ve sin distinguir inexistente de ajeno de fuera de alcance, y correo ya registrado sin declarar la situación ni el papel de la cuenta que lo ocupa. **Una sola prueba las cubre a las tres**: comparar dos respuestas que deberían ser indistinguibles y verificar que lo son.
5. **Dos señales declaradas del ensamblado no son fallos y viajan en respuestas exitosas**: el envío cuyo texto no verifica —que devuelve el trabajo guardado, en estado `Borrador`, con el texto íntegro y las observaciones ubicadas— y el listado sin elementos. **Ninguna tiene código de respuesta de fallo.**
6. **Ninguna respuesta lleva la dirección de un servicio interno, la ruta del almacén, la clave de firma, una contraseña, la provisoria fuera del cuerpo del reseteo, ni trazas de la implementación**, y **todas quedan registradas del lado del servidor**, junto con todo intento de acceso rechazado. Es `RA-03`, y acá es donde se puede violar hacia afuera.

**El orden de las dos traducciones no se invierte y no se saltea.** Un punto de acceso que eligiera su código de respuesta directamente desde un motivo interno estaría reimplementando media tabla en un lugar donde nadie la va a comparar.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Traductor único con tabla única y huecos declarados (**adoptada**) | La unidad de verificación es el **conjunto cerrado**, no el punto de acceso: se prueba recorriendo quince códigos y no quince rutas; los huecos quedan visibles en lugar de naturalizados | Un código genérico con cuatro destinos es menos preciso de lo que el producto sabe ser en todos los demás lugares |
| Traducción declarada punto por punto | Cada punto se lee entero | **Descartada.** La unidad de verificación pasaría a ser la ruta, y un código del conjunto cerrado sin traducción en algún punto no se detectaría; además `RN-03` podría traducirse distinto en dos puntos |
| Inventar los dos códigos que faltan en el conjunto cerrado | Cerraría los dos huecos y daría precisión donde hoy no la hay | **Descartada.** Los códigos son del ensamblado de contratos, y agregarlos es **decisión de aquel proyecto de código y del Product Owner**. Inventarlos acá los propagaría a 06, 08 y 10 como si fueran de la fuente |
| Responder «no autorizado» ante un trabajo ajeno, que es lo que el servicio sabe | Más informativo para el consumidor | **Descartada por `RN-03`**, que lo prohíbe por escrito y declara el criterio de verificación: pedir el trabajo de otro alumno devuelve «no encontrado» |
| Responder con un código de fallo ante un envío cuyo texto no verifica | Se leería como que algo salió mal, que es lo que la persona percibe | **Descartada.** El trabajo **se guardó** y su estado **se decidió**: lo que no verifica es el texto, no la petición. Un código de fallo le diría a la persona que su petición estaba mal mientras su trabajo, en realidad, quedó guardado |

## 5. Consecuencias positivas

1. `RN-03` queda sostenida por una prueba directa y no por una convención: dos respuestas comparadas y verificadas indistinguibles.
2. La cobertura de traducción se verifica **en las dos direcciones** sobre un conjunto de quince, que es un objeto finito y estable.
3. Los **dos** huecos del conjunto cerrado quedan declarados y elevados, en lugar de tapados con un código inventado que después nadie podría quitar.
4. `RN-09` cruza la frontera sin recortarse: la posición y el campo viajan en el cuerpo del error del contrato.
5. `RA-03` queda exigible con una prueba de inspección sobre las respuestas de los quince puntos y sobre el registro del servidor.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que el código genérico tenga cuatro destinos.** No es una comodidad: es el **síntoma medible** de los dos huecos, y por eso se declara en vez de naturalizarse.
2. **Se acepta perder precisión en dos caminos** —la facultad fuera del desenlace y el estado que no permite reenviar— a cambio de no inventar identificadores de un contrato que no es de esta capa.
3. **Se acepta que dos respuestas no lleven código del contrato**, con la consecuencia de que la pieza pública decide qué hacer con ellas mirando el número.
4. **Se acepta que las tres familias empobrecidas den menos diagnóstico**, y se compensa con el registro del lado del servidor, que es donde el diagnóstico sí está.

## 7. Implementación

- El traductor de motivos y códigos de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1 es el único lugar donde se elige un código de respuesta.
- La tabla completa, con sus **quince** filas, está en [`../Contratos-REST.md`](../Contratos-REST.md) §5, y **este documento no la duplica**.
- **Convención impuesta:** ningún punto de acceso elige un código de respuesta por su cuenta, ni siquiera para un caso «obvio».
- **Convención impuesta:** un motivo interno nuevo que no tenga código del contrato usa el genérico **y agrega una fila a la lista de huecos**, que se eleva al Product Owner. No se inventa un código.
- La prueba de 08 recorre el conjunto cerrado, **no los puntos de acceso**: es **una prueba por código**, no una por ruta.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Códigos del contrato con traducción declarada | **14 de 15**, con **1** sin destino y su motivo | Prueba de inspección contra [`../Contratos-REST.md`](../Contratos-REST.md) §5, **en las dos direcciones** |
| Códigos inventados o renombrados por esta capa | Exactamente **0** | Inspección contra el conjunto cerrado de `GeometriaFactory-Contracts` |
| Respuestas de esta superficie con el código que describe la ausencia de respuesta | Exactamente **0** | Prueba de inspección: sería una contradicción en sus términos |
| Comparaciones de respuestas indistinguibles que dan idénticas | **3 de 3** | Trabajo ajeno contra inexistente; correo inválido contra contraseña inválida; correo ocupado por cuenta habilitada contra ocupado por cuenta bloqueada |
| Envíos cuyo texto no verifica que responden con código de fallo | Exactamente **0** | Prueba de integración con los textos de `E-5` y de `E-8` |
| Respuestas que exponen dirección, ruta, secreto o traza | Exactamente **0** | Prueba de inspección sobre las respuestas de fallo de los quince puntos |
| Errores y accesos rechazados sin registro del lado del servidor | Exactamente **0** | Inspección del registro tras la batería de integración |
| Códigos de respuesta distintos en toda la superficie | Exactamente **10** | Recuento sobre [`../Contratos-REST.md`](../Contratos-REST.md) §4 |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.17** §4.1 (RN-03, RN-06, RN-09, RN-10, RN-15), §14 (RA-03), §17.5.P.5 y §17.5.P.10.
- [`../../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §4, §5 y §6, de donde sale la tabla que [`../Contratos-REST.md`](../Contratos-REST.md) §5 adopta.
- [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md) §1.5, §2.2, §2.3 y §2.4.
- [`../../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-02-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md`](../../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-02-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md), que es el conjunto cerrado que esta ADR traduce y no amplía.
- ADR relacionadas: [`ADR-02`](ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md), [`ADR-03`](ADR-03-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Fija el traductor único con tabla única sobre los **quince** códigos vivos del conjunto cerrado —catorce con destino y uno declarado sin él—, las dos respuestas sin código, las tres familias deliberadamente empobrecidas con la prueba que las cubre a las tres, las dos señales que viajan en respuestas exitosas y la prohibición de `RA-03` con su contracara de registro. Declara los **dos** huecos del conjunto cerrado como el síntoma medible de los cuatro destinos del código genérico, en lugar de inventar identificadores. Evalúa cinco alternativas, declara cuatro trade-offs y fija ocho métricas de validación. |
