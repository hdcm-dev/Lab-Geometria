# ADR-00003 — Credencial firmada, papel exigido por punto y una guardia transversal sin excepciones sueltas

**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Seguridad

---

## 1. Contexto

El intake fija el flujo por decisión explícita del docente: el front recibe correo y contraseña del formulario y los canjea por un acceso firmado con clave simétrica; el acceso vive en el circuito del front y **nunca llega al navegador**; la autorización es **«por papel en cada punto más verificación de pertenencia»**, y el intake agrega la frase que ordena todo lo demás: **«el papel no alcanza»** (`PRODUCT-INTAKE` §17.1.P.5 · GeometriaFactory-Api).

Sobre eso hay una decisión de arquitectura que ninguna fuente toma y que es la más importante de este proyecto de código. `RN-00013` e `INV-09` exigen que una cuenta con la marca de cambio de contraseña pendiente **no llegue a ninguna otra parte del sistema**. La comprobación la ejerce la capa de aplicación, en la primera de sus cuatro comprobaciones. Pero esa comprobación sólo se ejerce **si el punto de acceso invoca un caso de uso que la tenga**, y la categoría 02 lo dijo sin ambigüedad: **un punto nuevo que quede fuera de la guardia rompe la regla sin que nada falle**.

Es un defecto de omisión, y los defectos de omisión no se ven leyendo el código nuevo: se ven comparando contra una lista.

Motivación upstream: NB-00001, NB-00002; RN-00001, RN-00004, RN-00006, RN-00010, RN-00012, RN-00013, RN-00016; INV-02, INV-03, INV-06, INV-08, INV-09; `PRODUCT-INTAKE` §17.1.P.5 · GeometriaFactory-Api, §17.1.P.6 · GeometriaFactory-Api.

## 2. Decisión

**La admisión es un componente transversal por el que pasan los once puntos que exigen acceso, y la lista de los que no pasan es cerrada, contable y verificada por prueba.** Cinco reglas:

1. **La guardia hace tres cosas, en este orden**: verifica la firma y la expiración del acceso; exige el papel que el punto declara; y aplica la guardia del cambio de contraseña pendiente. Un fallo en la primera es `401`; en las otras dos, `403` con motivo.
2. **Cuatro puntos no exigen acceso firmado, y son exactamente cuatro**: el canje de credenciales, el registro de cuenta, la configuración del administrador y la salud. **Ni uno más.** La lista está en [`../Arquitectura-Unidad-Entrega.md`](../Arquitectura-Unidad-Entrega.md) §3.4 y **una prueba de inspección la recorre en las dos direcciones**.
3. **La guardia del cambio pendiente tiene una sola excepción declarada**: el cambio de la propia contraseña, que es lo único que la levanta. Ninguna otra excepción es admisible, y agregar una es un cambio de esta ADR y no de un punto de acceso.
4. **Exigir el papel no es autorizar.** El papel viaja en el acceso y esta capa lo exige por punto; **la verificación de pertenencia y la de facultad se hacen sobre el dato recuperado y son de la capa de aplicación**. Que un punto exija `Administrador` no exime a la capa de adentro de comprobar.
5. **La vigencia del acceso se toma de configuración**, con el criterio de que **caduque dentro de la sesión de trabajo de una clase** y con **renovación por reingreso**, sin acceso de refresco. El número se ancla en la etapa `a`.

**Y una ausencia que esta ADR sostiene explícitamente: ningún punto de acceso fija una contraseña sobre una cuenta existente sin credencial.** Es `RN-00016` vista desde la superficie, y se comprueba sobre los cuatro puntos que no exigen acceso: uno canja credenciales, uno registra una cuenta **sin** contraseña, uno sólo procede mientras no exista administrador y uno es de sólo lectura. El identificador `A-04`, que era la excepción, **quedó retirado y no se recicla**.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Guardia transversal con lista cerrada de excepciones y prueba de inspección (**adoptada**) | Convierte un defecto de omisión en un defecto de comparación, que sí se detecta; agregar un punto obliga a tocar la lista | La lista es un artefacto que hay que mantener, y una prueba que la recorre es una prueba más que escribir |
| Declarar la guardia punto por punto | Cada punto se lee entero, sin saber de una lista | **Descartada.** Es exactamente el defecto que la categoría 02 declara: un punto nuevo que se olvide de declararla **no falla**, y la regla se rompe hacia afuera sin que ninguna capa de adentro se entere |
| Declarar sólo las excepciones, con la guardia aplicada por omisión y sin prueba de inspección | Un punto nuevo queda protegido sin hacer nada | **Descartada a medias**: la aplicación por omisión se adopta, pero **sin la prueba de inspección no alcanza**, porque nada impediría que alguien agregue una excepción y nadie lo note. La prueba es la parte que no se puede omitir |
| Comprobar acá también la pertenencia sobre el dato recuperado | Un pedido mal formado fallaría antes | **Descartada.** Dos lugares que comprueban lo mismo terminan diciendo cosas distintas, y el intake es explícito: la autorización es por papel **más** verificación de pertenencia, y la segunda es de adentro |
| Emitir un acceso de refresco de vigencia larga | La persona no vuelve a escribir su contraseña cuando el acceso vence | **Descartada por el intake §17.1.P.5 · GeometriaFactory-Api**: vigencia corta y renovación por reingreso, sin acceso de refresco en este alcance |

## 5. Consecuencias positivas

1. `RN-00013` e `INV-09` quedan sostenidos por una propiedad **contable**: cuatro puntos fuera de la guardia, once dentro, y una prueba que lo recorre en las dos direcciones.
2. `RN-00016` queda sostenida como **ausencia comprobable**: ningún punto fija una contraseña sobre una cuenta existente sin credencial.
3. La autorización real queda donde la capa de aplicación la puso, y `RN-00004` conserva su criterio de verificación —forzar la petición contra esta superficie— probando la regla y no una copia del borde.
4. `INV-08` queda protegido por ausencia: **no hay punto de acceso** que cambie la situación de la cuenta de administrador ni que la dé de baja.
5. Un punto de acceso nuevo tiene un procedimiento de alta explícito: entra a la tabla, declara su papel, y si pretende quedar fuera de la guardia hay que modificar esta ADR.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta mantener una lista y una prueba que la recorre.** Es el costo de convertir una omisión en una comparación.
2. **Se acepta que el registro de cuenta sea anónimo**, y se declara que **debe seguir siéndolo**: es como el alumno entra al laboratorio. La escritura anónima que `RN-00016` eliminó es la **de contraseña**, no toda escritura anónima.
3. **Se acepta que una persona vuelva a escribir su contraseña cuando su acceso vence.**
4. **Se acepta que el tramo del front hacia esta superficie viaje sin cifrar** si ese salto es texto plano. Es un riesgo declarado y **aceptado por escrito** por la fuente, con el túnel saliente como salida documentada y no adoptada. Esta ADR **no lo reabre**: lo registra para que no se lea como un descuido.

## 7. Implementación

- La guardia de admisión de [`../Arquitectura-Unidad-Entrega.md`](../Arquitectura-Unidad-Entrega.md) §3.1 es el único lugar donde se verifica el acceso y se exige el papel.
- **Convención impuesta:** un punto de acceso nuevo entra a la tabla de §3.4 **en la misma intervención** en que se lo agrega. La prueba de inspección falla si no está.
- **Convención impuesta:** ningún punto de acceso declara su propia excepción a la guardia del cambio pendiente.
- El mecanismo de verificación del acceso es de `GeometriaFactory-Infrastructure`; lo que esta capa aporta es **exigirlo en cada punto**.
- La vigencia se toma de configuración y llega por la composición de raíz ([`ADR-00006`](ADR-00006-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md)).

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Puntos de acceso fuera de la guardia | Exactamente **4**, y son los declarados | Prueba de inspección que recorre los **15** puntos y compara contra la lista, **en las dos direcciones** |
| Excepciones a la guardia del cambio pendiente | Exactamente **1** | Prueba que ejerce los once puntos bajo la guardia con una cuenta con la marca puesta, y comprueba que **diez** son rechazados y **uno** procede |
| Puntos que fijan una contraseña sobre una cuenta existente sin credencial | Exactamente **0** | Inspección de los cuatro puntos que no exigen acceso |
| Puntos que cambian la situación o dan de baja la cuenta de administrador | Exactamente **0** | Inspección de la superficie |
| Comprobaciones de pertenencia duplicadas en esta capa | Exactamente **0** | Inspección en revisión |
| Eliminaciones fuera de alcance aceptadas al forzar la petición | Exactamente **0**, en los **2** alcances | Prueba de integración que fuerza la petición, tal como la fuente lo exige |
| Respuestas de credencial inválida que declaran cuál campo falló | Exactamente **0** | Prueba que compara la respuesta con correo inexistente contra la de contraseña equivocada |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.17** §4.1 (RN-00013, RN-00016), §17.1.P.5 · GeometriaFactory-Api y §17.1.P.6 · GeometriaFactory-Api.
- [`../../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3 y §7.
- [`CU-00022`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md).
- [`../../../GeometriaFactory-Application/05-Arquitectura-Tecnica/Adrs/ADR-04004-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md`](ADR-04004-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md), que es la comprobación que esta guardia garantiza que se ejerza siempre.
- ADR relacionadas: [`ADR-00001`](ADR-00001-Host-Delgado-Con-Composicion-De-Raiz-Unica.md), [`ADR-00004`](ADR-00004-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Convierte la garantía de `RN-00013` e `INV-09` en una propiedad contable —cuatro puntos fuera de la guardia, once dentro, una excepción declarada— verificada por una prueba de inspección en las dos direcciones, y sostiene como ausencia comprobable la de `RN-00016`. Evalúa cinco alternativas, declara cuatro trade-offs —incluido el registro del riesgo aceptado del tramo sin cifrar, que no se reabre— y fija siete métricas de validación. |
