# CU-00026 — Enviar un trabajo y ver sus observaciones

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-16
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00004`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md); [`NB-00005`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00005-Visibilidad-Del-Error-De-Calculo.md); [`NB-00003`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 y §4.2 (estados del trabajo), §4.1 (RN-02005, RN-02008, RN-02009), §6 (flujo 2), §17.1.P.11 · GeometriaFactory-Domain, §20 (escenarios E-1 a E-8)
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de la unidad de entrega
**Consolida a:** `CU-00006` §A-10 y §A-11, [`CU-04004`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-04004-Cargar-Y-Reeditar-Un-Trabajo-Propio.md), [`CU-04005`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-04005-Enviar-Un-Trabajo-E-Interpretar-Su-Texto.md), [`CU-02005`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-02005-Crear-Y-Reeditar-Un-Trabajo.md), [`CU-02006`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-02006-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md), [`CU-02007`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-02007-Registrar-Las-Observaciones-Del-Trabajo.md) y [`CU-02008`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-02008-Gobernar-El-Estado-Del-Trabajo.md), por `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2 §2.1 y §2.1.2

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

Que el alumno pegue el texto que emitió su programa de la Actividad 1, lo envíe, y vea **dónde** está
cada problema: en qué figura y en qué campo. Es lo que convierte ese texto en una unidad con
existencia propia y con dueño, y es **el mayor valor didáctico del producto**.

**El alumno tiene una sola acción de guardado, y es enviar.** No hay un «guardar» separado de un
«enviar»: cargar el trabajo, interpretar su texto y resolver su estado son **tres tramos de un solo
acto**. Por eso este caso de uso los declara juntos.

| Punto de acceso | Qué ejerce |
| --- | --- |
| **A-10** | El envío de un trabajo nuevo |
| **A-11** | El reenvío de uno que quedó en `Borrador`. **Es el mismo acto, repetido** |

**Un envío cuyo texto no verifica es una respuesta exitosa, y ésta es la confusión más cara de este
caso de uso.** El resultado trae estado `Borrador` y las observaciones localizadas; **la petición se
cumplió y el trabajo se guardó**. Convertirlo en un fallo destruiría exactamente la capacidad que el
producto viene a dar: que el alumno vea qué corregir, en lugar de recibir un mensaje genérico.

**El texto original cruza la frontera del proceso, y el borde es el primer lugar donde puede
alterarse.** El texto que el alumno pega **no es JSON estrictamente válido**: trae comas finales y
claves que un lector ingenuo rechaza. **Cualquier normalización en el borde** —de codificación, de
espacios, de saltos de línea— **rompe la conservación íntegra sin producir ningún error**. El producto
**no edita el dato del alumno** (RN-02008).

**La discrepancia se señala, no se corrige ni se rechaza.** Un área declarada 36.00 contra una derivada
54.00 es una **advertencia**: viaja con los dos números, no bloquea nada y no se corrige sola.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Alumno | Primario | Pega el texto de su programa, envía, lee las observaciones, corrige y vuelve a enviar **cuantas veces haga falta** |
| `GeometriaFactory-Web` | Intermediario | Arma la solicitud con el texto **exacto** que la persona pegó, **sin normalizarlo**, y la envía con la sesión firmada (RA-01) |
| Motor de interpretación de figuras | Sistema | Interpreta el texto y devuelve **la cantidad de figuras del conjunto raíz**, las piezas reconstruidas con su posición y sus valores declarado y derivado, y las observaciones con su especie y su ubicación |
| Almacén de trabajos | Sistema | Recupera el trabajo y materializa el resultado |
| Reloj del sistema | Sistema | Provee el sello de alta y el de modificación del trabajo |

## 3. Precondiciones

- La petición trae sesión firmada con papel `Alumno` y **atravesó la guardia** de `CU-00022`.
- La solicitud aporta **nombre, fecha, descripción y el texto original**; el reenvío aporta además el
  identificador. La descripción admite vacío.
- Para el reenvío, el trabajo existe, **es del solicitante** y está en `Borrador`, que es **el único
  estado que el alumno edita**.
- El motor de interpretación devuelve, junto con el resultado, **la cantidad de figuras del conjunto
  raíz**. **No es derivable de las piezas adoptadas**, que admiten huecos, y es el rango contra el que
  se valida la posición de cada observación.

## 4. Flujo principal

1. El alumno corre su programa, copia el texto que emitió y lo pega en el formulario junto con el
   nombre, la fecha y la descripción de su trabajo.
2. Llega una petición a **A-10** con esos cuatro datos. **El texto se transporta tal como llegó**: no
   se normaliza, no se reordena y **no se le quita ningún carácter**.
3. Se toma el sello de alta del reloj y se constituye el trabajo con el solicitante como dueño, el
   texto adoptado **tal cual**, estado `Borrador`, **0** piezas, **0** observaciones y sin comentario
   del administrador. Un trabajo **no puede constituirse sin dueño** (INV-02).
4. Se entrega el texto original al motor de interpretación, que devuelve **cuántas figuras trae el
   conjunto raíz** —incluidas las que no se pudieron reconstruir—, las piezas y las observaciones.
5. Se incorporan las piezas: a cada una **la posición que su figura ocupa en el conjunto raíz del
   texto**, empezando en 0. **Esa posición es su identidad** y no se recalcula: es la que el alumno ve.
   Se verifica que el tipo pertenezca al conjunto conocido, se **deriva** la familia plana o
   volumétrica desde el tipo —sin guardarla ni admitir que se la declare— y se adoptan los
   componentes y **los cuatro valores de área y volumen, declarado y derivado, como atributos
   distintos**.
6. Se incorpora la cantidad de figuras del conjunto raíz, y con ella las observaciones: cada una con
   su especie —`Advertencia` o `Error de validación`— y su ubicación. Se verifica que todo error de
   validación indique posición y campo (RN-02009), que esa posición **pertenezca al rango del conjunto
   raíz** —esté o no adoptada la pieza— y que toda advertencia de discrepancia traiga **los dos
   valores**.
7. Se toma el sello de modificación del reloj y se resuelve el estado: **no hay ninguna observación de
   especie error de validación**, de modo que el trabajo pasa a `Pendiente`, **conservando sus
   advertencias**, que no impiden el paso (RN-02005).
8. Se materializan el trabajo, sus piezas y sus observaciones **en una única unidad de trabajo**, y se
   responde `201` con el identificador, **el estado que la interpretación decidió**, la fecha de
   registro y las observaciones **con su índice de figura y su campo**.

**El estado llega decidido y el borde no lo interpreta.** Que el resultado traiga `Borrador` **no
cambia el código de respuesta**.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | **El texto no verifica** —por ejemplo el de **E-5**, con `"Tipo": "Piramide"` en la segunda figura, o el de **E-8**, con `"Largo": "3,50"` como cadena— | **El envío procede y responde con éxito.** El trabajo **queda en `Borrador`**, con el texto conservado íntegro y las observaciones de error **localizadas por índice de figura y campo**. No es un rechazo: **es el resultado declarado del envío**, y el alumno corrige y vuelve a enviar | Paso 8, con estado `Borrador` |
| FA-02 | El texto verifica y produce **advertencias** —por ejemplo el de **E-2**, con volumen declarado 343.00 contra derivado 1029.00— | El envío procede igual, el trabajo pasa a `Pendiente` **con** sus advertencias, y **ninguna bloquea nada**. El carácter no bloqueante es deliberado | Paso 8 |
| FA-03 | **Alguna figura no se pudo reconstruir** | **Su posición queda reservada**: no se reasigna a ninguna otra pieza, no se renumera nada, y sigue perteneciendo al rango del conjunto raíz, **de modo que una observación puede ubicarse en ella** (RN-02009). El conjunto de piezas queda con un hueco. **Las piezas válidas del mismo conjunto se adoptan igual**: un defecto en un elemento no descarta el resto | Paso 6 |
| FA-04 | **El reenvío** por **A-11** de un trabajo en `Borrador` | Se recupera, se comprueba pertenencia y estado, se reemplazan nombre, fecha, descripción y texto, se **descartan** las piezas y las observaciones de la interpretación anterior, y se vuelve a interpretar. Conserva **identificador y dueño**. Se responde `200` con el mismo tipo de resultado | Paso 4 |
| FA-05 | La verificación **no encuentra ninguna discrepancia** —el cubo de **E-4**, cuyo declarado sí corresponde a sus dimensiones— | Se adoptan **0** observaciones y el trabajo pasa a `Pendiente`. **El criterio negativo importa tanto como el positivo** | Paso 7 |
| FA-06 | Una pieza trae una dimensión en **0.00** | Se adopta igual y **no se descarta**: el criterio es de **existencia del dato y no de veracidad geométrica** | Paso 6 |
| FA-07 | Una figura plana aparece **en el conjunto raíz**, sin componentes | Se adopta con **0** componentes: es el caso de **E-7**, donde las planas son figuras del conjunto raíz y no partes de un volumen | Paso 6 |
| FA-08 | La observación **no es atribuible a ninguna figura**, por ejemplo un conjunto raíz vacío | Se adopta **sin posición**, con el campo que corresponda. Sigue siendo de especie error de validación | Paso 6 |
| FA-09 | El mismo trabajo se **reprocesa** después de una mejora del motor | Se reemplaza el conjunto de observaciones **entero**. **El texto original nunca cambió, y eso es lo que hace posible reprocesar** | Paso 6 |

## 6. Excepciones y errores

| Motivo interno | Código del contrato | Respuesta | Punto | Causa |
| --- | --- | --- | --- | --- |
| `REQUIRED_FIELD_MISSING` | `REQUIRED_FIELD_MISSING` | `400` | A-10, A-11 | Falta el nombre, la fecha o el texto original. La respuesta **nombra el campo ausente** |
| `WORK_NOT_FOUND_FOR_REQUESTER` | `WORK_NOT_FOUND` | `404` | A-11 | El identificador no existe, **o no es del solicitante**. **Las dos respuestas son indistinguibles**, y es lo que impide averiguar por tanteo qué identificadores existen (INV-02). **No se invoca el motor de interpretación** |
| `SUBMISSION_OUTSIDE_DRAFT` | `STATE_FORBIDS_UPDATE` | `409` | A-11 | Se fuerza un reenvío sobre un trabajo en `Pendiente`: **ya salió de las manos del alumno**. La respuesta **declara el estado actual y no ofrece forma de volver a `Borrador`, porque no existe** |
| `TRANSITION_FROM_TERMINAL_STATUS` | `STATE_FORBIDS_UPDATE` | `409` | A-11 | Se fuerza un reenvío sobre un trabajo `Finalizado` o `Rechazado`. Los dos estados de cierre **son terminales y de ellos no sale ninguna transición** (INV-07), y el invariante **no los distingue entre sí** |
| `PARSE_RESULT_UNAVAILABLE` | `UNCLASSIFIED_ERROR` | `503` | A-10, A-11 | El motor no puede completar la interpretación. **Termina de forma controlada, deja el trabajo en `Borrador` con su texto intacto y no inventa observaciones ni lo pasa a `Pendiente`** |
| `ORIGINAL_JSON_ALTERED` | — | `500` | A-10, A-11 | Se aporta como texto original una versión **corregida** del que pegó el alumno. **El producto no edita el dato del alumno** (RN-02008) |
| `MALFORMED_PIECE_SET` | — | `500` | A-10, A-11 | Posición **repetida, negativa o fuera del rango** del conjunto raíz declarado; tipo desconocido; familia declarada que contradice al tipo; o reconstrucción sobre un trabajo terminal. **Un hueco no es un defecto**: es la posición reservada de FA-03 |
| `MALFORMED_OBSERVATION` | — | `500` | A-10, A-11 | Especie desconocida; error de validación **sin ubicación** siendo atribuible; advertencia **sin los dos valores**; u observación sobre una posición que **no pertenece al rango del conjunto raíz**. **Una posición reservada no es una posición inexistente** |
| `WORK_WITHOUT_OWNER` | — | — | A-10 | No se aporta la identidad del solicitante. **Inalcanzable desde la superficie por construcción**: la identidad viene de la sesión que la guardia ya verificó. **Un trabajo sin dueño no es un trabajo** |
| — | `UNCLASSIFIED_ERROR` | `403`, `503` | A-10, A-11 | `403` cuando el papel no alcanza; `503` cuando el almacén no está disponible o rechazó una escritura concurrente |

**Un texto que no verifica no aparece en esta tabla, y no es un olvido.** El ensamblado de contratos lo
declara **señal y no error**. Los cinco `500` son **defectos del motor o de la orquestación, y ninguno
es un resultado que el alumno deba ver**: por eso no tienen código del contrato.

**Ninguna condición modifica el texto original y ninguna deja escritura parcial.** En particular, **un
reenvío rechazado no reemplaza el texto guardado**.

## 7. Postcondiciones

- **Éxito sin errores de validación:** el trabajo está en **`Pendiente`**, con su conjunto de piezas,
  la cantidad de figuras de su conjunto raíz, sus advertencias si las hubo y el sello del reloj, **a
  la espera de la revisión del administrador**.
- **Éxito con errores de validación:** el trabajo sigue en **`Borrador`**, con sus observaciones
  localizadas y **su texto original íntegro**.
- **En los dos casos:** existe un trabajo con dueño, con identificador propio y con **el texto
  exactamente como llegó**. Las observaciones viajaron **con su ubicación**.
- **Éxito del reenvío:** el trabajo conserva **identificador y dueño**, tiene los datos nuevos y **su
  interpretación anterior quedó descartada**.
- **Fallo:** el almacén queda como estaba. **Ningún rechazo altera el texto original.**

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | El texto del escenario **E-2**, con sus **2** comas finales y su clave `"Tapas"`, tal como lo emite el programa del alumno | Se envía por A-10 | Responde con éxito, el estado resultante es `Pendiente`, y el texto guardado es **idéntico carácter por carácter** al enviado: **0 normalizaciones** |
| CA-02 | El texto del escenario **E-5**, con `"Tipo": "Piramide"` en la segunda figura de un conjunto raíz de 2 | Se envía por A-10 | **Responde con éxito, no con fallo.** El estado es `Borrador`, se adopta **1** pieza —la de posición 0—, **la posición 1 queda reservada**, y hay **1** observación de error de validación con **índice de figura 1** y campo `Tipo` |
| CA-03 | El texto del escenario **E-8**, con `"Largo": "3,50"` como cadena | Se envía por A-10 | **Responde con éxito.** El estado es `Borrador` y la observación está **localizada por índice de figura y campo** |
| CA-04 | El texto del escenario **E-1**, de 3 piezas —cilindro, cubo y ortoedro— | Se envía por A-10 | Responde con éxito con estado `Pendiente`, **3** piezas en las posiciones 0, 1 y 2, y **2** advertencias —área 36.00 contra 54.00 en el cubo, volumen 343.00 contra 1029.00 en el ortoedro—, y **0** de ellas impide el paso de estado |
| CA-05 | La pieza de posición 1 de **E-1**, un `Cubo` con área declarada 36.00 y derivada 54.00 | Se envía y se inspecciona el trabajo | Los **2** valores se conservan **por separado**, sin sustituir uno por el otro |
| CA-06 | El cubo del escenario **E-4**, cuyo declarado corresponde a sus dimensiones | Se envía por A-10 | Se adoptan **0** observaciones y el estado es `Pendiente` |
| CA-07 | El escenario **E-6**, con una pieza `Rectangulo` de `Largo` 0.00 | Se envía por A-10 | Se adopta **1** pieza y **no se descarta** |
| CA-08 | El trabajo de CA-02, en `Borrador` con 1 pieza y 1 error | El alumno corrige el texto y lo reenvía por **A-11**, y ahora no hay errores | Responde `200` con estado `Pendiente`, **0** observaciones de error de validación, y el **mismo identificador y dueño** |
| CA-09 | Un trabajo en `Borrador` del alumno A con 3 piezas y 2 observaciones | El alumno A lo reenvía con un texto nuevo | El trabajo queda con **0** piezas y **0** observaciones de la interpretación anterior, en `Borrador` o `Pendiente` según el texto nuevo, y con el texto nuevo íntegro |
| CA-10 | Un trabajo en `Borrador` del alumno A | El alumno **B** lo reenvía por A-11 | Responde `404` con el mismo cuerpo que ante un identificador inexistente —**0 campos** permiten distinguirlos— y el motor de interpretación registra **0** invocaciones |
| CA-11 | Un trabajo en `Pendiente` del alumno A | El alumno A lo reenvía por A-11 | Responde `409` y el estado no cambia |
| CA-12 | Un trabajo en `Finalizado` y otro en `Rechazado` del alumno A | El alumno A los reenvía por A-11 | Las **2** responden `409` **con el motivo de terminalidad, no con el de fuera de borrador**, y los estados no cambian |
| CA-13 | Un conjunto raíz de 2 figuras entregado con una pieza en la posición 5, y una pieza `Cubo` a la que se le declara la familia plana | Se envía cada caso | Las **2** terminan en fallo `500` y **0** trabajos quedan materializados |
| CA-14 | Un trabajo en `Borrador` con su texto guardado | Se reenvía por A-11 con un texto nuevo y la petición falla por almacén no disponible | Responde `503` y el texto guardado **sigue siendo el anterior**: **0** reemplazos parciales |
| CA-15 | Un trabajo en `Borrador` con el texto de 3 piezas y el motor sin latencia añadida | Se envía por A-10 | Se resuelve en **menos de 500 ms**, medido con el motor detrás de su puerto y **sin acceso a base de datos** |
| CA-16 | Cualquier respuesta de §6, con el cuerpo y el registro del servidor observados | Se produce la condición | **0 apariciones** de la ruta del almacén, de la dirección de cualquier servicio interno, y **0 apariciones del texto completo del alumno** en el registro |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [NB-00004](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md), que el dato del alumno se interprete **sin editarlo**; [NB-00005](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00005-Visibilidad-Del-Error-De-Calculo.md), que es la capacidad didáctica central —señalar **dónde** está el problema en lugar de rechazar con un mensaje genérico—; [NB-00003](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md), que el trabajo tenga dueño, estado y persistencia |
| Reglas de negocio aplicables | [RN-02005](../Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md), que decide el estado: **los errores bloquean, las advertencias no**. [RN-02008](../Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md), que es lo que el borde puede romper sin que nada falle. [RN-02009](../Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md), que exige la ubicación y **cuyo caso insignia es la figura que no se pudo reconstruir** |
| Invariantes del producto | **INV-02**, todo trabajo tiene dueño y el ajeno es indistinguible del inexistente. **INV-07**, `Finalizado` y `Rechazado` son terminales y su contenido no cambia |
| Reglas de arquitectura del producto | **RA-01**, el portal envía servidor a servidor con el texto exacto. **RA-03**, ninguna respuesta expone rutas ni direcciones, y **el texto completo del alumno no va al registro** |
| Puntos de acceso | **A-10** el envío, **A-11** el reenvío |
| Contrato de uso que transporta | `GeometriaFactory-Contracts` `CU-00005` |
| Puertos que consume | Motor de interpretación de figuras, almacén de trabajos, reloj del sistema |
| Historias de usuario a generar en 06 | US-00016, US-00017, US-00018 |
| Componentes esperados en 05 | Los dos puntos de acceso; la orquestación del envío con su verificación de pertenencia; la reconstrucción posicional de piezas; el registro de observaciones; la resolución de estado |
| Tests previstos en 08 | Integración por los dieciséis criterios, **con los escenarios E-1 a E-8 del intake como insumo**; una prueba de conservación carácter por carácter (CA-01); una prueba con motor doble para la posición reservada (CA-02) y para la medición sin base de datos (CA-15); y una inspección de que el texto completo del alumno no aparece en el registro (CA-16) |

## 10. Notas y supuestos

- **El punto abierto del reenvío fuera de `Borrador` está cerrado, y las vistas de origen no lo
  habían absorbido.** Hasta el `PRODUCT-INTAKE` 1.29 el conjunto cerrado sólo tenía
  `STATE_FORBIDS_DELETE`, **acotado a la eliminación**, y este camino respondía `409`
  con el genérico. La decisión del Product Owner del **2026-08-12** incorporó
  `STATE_FORBIDS_UPDATE`, con destino `409`, para el envío y la reedición forzados.
  `CU-00006` 1.2 seguía declarándolo abierto; el alcance de esa propagación incompleta está en
  `CU-00023` §10.
- **La cantidad de figuras del conjunto raíz no es derivable de las piezas adoptadas**, porque el
  conjunto admite huecos. Por eso el motor la devuelve por separado, y por eso el rango contra el que
  se valida cada observación es ése y no el de las piezas.
- **Una posición reservada no es una posición inexistente.** Es la distinción que hace posible
  RN-02009 en su caso más importante: decirle al alumno «la figura 1 tiene un tipo que no conozco»
  **exige poder nombrar una figura que no se pudo reconstruir**.
- **La familia se deriva del tipo y no se guarda.** Admitir que se la declare abriría la posibilidad
  de que contradiga al tipo, y por eso hay una condición para eso.
- **La comparación de valores se resuelve con tolerancia absoluta de 0.01, nunca por igualdad
  exacta.** Es lo que evita que una diferencia de redondeo se lea como una discrepancia.
- **`Borrador` significa dos cosas a la vez, y es correcto:** que el trabajo recién se creó, o que su
  texto todavía no verifica. El intake §4.2 lo declara así.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.1 | 2026-08-29 | **Tramo `R-3b` del renombre `F-03`**, reactivado por el Product Owner el 2026-08-29 y registrado en [`../../../../Producto/Norma-De-Nomenclatura.md`](../../../../Producto/Norma-De-Nomenclatura.md) §8. **12 línea(s)** de este documento pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios ni lo que está entre «…». **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
| 1.0 | 2026-08-16 | Emisión inicial, como **caso de uso consolidado** de la unidad de entrega por `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2 §2.1. Absorbe los puntos **A-10 y A-11** de `CU-00006` 1.2, `CU-04004` 1.1, `CU-04005` 1.2, `CU-02005` 1.1, `CU-02006` 1.2, `CU-02007` 1.2 y `CU-02008` 1.1 — **siete vistas de un solo acto**. La versión 1.0 de la tabla de consolidación cortaba esto en dos capacidades, «cargar y reeditar» y «enviar e interpretar», que es **el corte por capa disfrazado de corte por capacidad**: las propias fuentes declaran que el alumno tiene **una sola acción de guardado**. El motivo del recorte está en §2.1.2 del documento de consolidación. La unión no es la suma: el actor primario pasa a ser el alumno; el flujo va del texto pegado a la respuesta con las observaciones localizadas, **sin cortes por capa**; §6 queda en una sola tabla con el motivo interno y su traducción, con los cinco `500` declarados como **defectos que el alumno no debe ver** y `TRABAJO_SIN_DUENO` marcado **inalcanzable por construcción**; y los criterios se rehacen sobre la capacidad y quedan **dieciséis**, cubriendo los escenarios **E-1, E-2, E-4, E-5, E-6 y E-8** del intake, que las siete vistas cubrían por separado y con solapamiento. Los seis documentos absorbidos enteros quedan archivados en `_legacy/2026-08-16-consolidacion-8.5/` y citados desde la cabecera. |

## 17. Compatibilidad de la superficie pública

**Normalizar el texto en el borde** —codificación, espacios, saltos de línea, reordenar claves— rompe
RN-02008 y CA-01 **sin producir ningún error**, y es el cambio incompatible más probable de esta
superficie. **Convertir un texto que no verifica en un código de respuesta de fallo** destruye la
capacidad central del producto y contradice CA-02 y CA-03. Renumerar las piezas para tapar los huecos
rompe la identidad posicional y deja a RN-02009 sin poder nombrar la figura que falló. Hacer que una
advertencia bloquee el paso a `Pendiente` contradice RN-02005 y CA-04. Distinguir en la respuesta el
trabajo ajeno del inexistente contradice INV-02 y CA-10. Admitir la reedición fuera de `Borrador`, o
cualquier transición desde un estado terminal, contradice INV-07.
