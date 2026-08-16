# NB-00009 — Desenlace explícito de la entrega

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Documento | NB-00009-Desenlace-Explicito-De-La-Entrega.md |
| Versión | 1.0 |
| Estado | Aprobado |
| Fecha | 2026-08-08 |
| Autor | Analista de Negocio Senior (AG-01) |
| Trazabilidad upstream | PRODUCT-INTAKE §4 (capacidades F-21, F-23 y F-24), §4.1 (reglas RN-04, RN-10 y RN-11), §4.2 (modelo de estados del trabajo y sus tres consecuencias aceptadas), §5 (historia 7.1), §6 (flujo 2.1), §7 (casos límite CL-10 y CL-11), §8 (métrica de aprobación del administrador), §9 (retiro de la exclusión X-5), §12 (glosario: «aprobar / rechazar» y «comentario»), §15 (etapa `h`), §22 (asunción A-2); `Vision-Producto.md` §5 (OBJ-03), §6 y §9; `Alcance-Producto.md` §4.1 y §8; `Roadmap-Producto.md` §2.1 y §5.2 |
| Trazabilidad downstream | `CU-00006`, `CU-00008`, `CU-02010`, `CU-02011`, `CU-04006`, `CU-04007`, `CU-04008`, `CU-04009`, `CU-06004` en `GeometriaFactory-Api`; `CU-10006`, `CU-10008`, `CU-10009` en `GeometriaFactory-Web` (emitidos en 02-Especificacion-Funcional); 06-Backlog-Tecnico, 07-Plan-Sprint, 08-Calidad-Y-Pruebas |

---

## Tabla de contenido

- [1. Descripción de la necesidad](#1-descripción-de-la-necesidad)
- [2. Ejemplo de uso desde la perspectiva del negocio](#2-ejemplo-de-uso-desde-la-perspectiva-del-negocio)
- [3. Impacto](#3-impacto)
- [4. Problema específico que resuelve](#4-problema-específico-que-resuelve)
- [5. Criterios de éxito](#5-criterios-de-éxito)
- [6. Stakeholders involucrados](#6-stakeholders-involucrados)
- [7. Trazabilidad a CU](#7-trazabilidad-a-cu)
- [8. Dependencias con otras NB](#8-dependencias-con-otras-nb)
- [9. Prioridad MoSCoW](#9-prioridad-moscow)
- [10. Control de cambios](#10-control-de-cambios)

---

## 1. Descripción de la necesidad

Un trabajo entregado y nunca respondido es una entrega a medias. Con el trabajo guardado, con dueño y a la vista del docente, queda todavía un hueco: el alumno entrega y después no pasa nada. No sabe si su trabajo fue aceptado, si el docente lo miró siquiera, ni si tiene que hacer algo más. Del otro lado, el docente no tiene forma de dejar registrado que ya revisó un trabajo, así que en la segunda pasada vuelve a mirar lo mismo. La entrega queda **depositada** en lugar de resuelta (PRODUCT-INTAKE §5, historia 7.1).

La cátedra necesita que cada entrega tenga un **desenlace explícito**: que el docente la acepte o la rechace, que esa decisión quede registrada en el trabajo y que el alumno la vea en su propio listado. La decisión es facultad exclusiva del docente —el alumno no la ejerce ni sobre sus propios trabajos— y los dos desenlaces son terminales: una vez tomada, el trabajo no vuelve atrás, y corregir un rechazo significa cargar un trabajo nuevo (PRODUCT-INTAKE §4.1, RN-10, y §7, CL-10). Esa terminalidad es la que hace que el desenlace signifique algo: si el estado pudiera deshacerse, no sería una respuesta sino una anotación provisoria.

Junto con el desenlace, el docente necesita poder dejarle al alumno un **comentario escrito**, opcional en los dos casos. No es una calificación: no lleva nota ni escala, y calificar sigue estando fuera del producto. Tampoco es una observación de las que el producto emite al interpretar el texto: el comentario lo escribe una persona y dice lo que esa persona quiere decir (PRODUCT-INTAKE §12). Su carácter opcional también fue decidido con su consecuencia a la vista: un alumno puede recibir un rechazo sin explicación escrita, y el estado le informa que no fue aceptado aunque no tenga el motivo (PRODUCT-INTAKE §7, CL-11). Por último, la terminalidad deja un residuo que alguien tiene que poder limpiar —un alumno que rebota varias veces acumula trabajos rechazados que sólo el docente puede quitar—, y por eso el retiro de cualquier trabajo que el docente ve forma parte de esta misma necesidad.

## 2. Ejemplo de uso desde la perspectiva del negocio

El docente abre su listado y encuentra catorce trabajos esperando respuesta. Abre el primero, lo mira en tres dimensiones y en su árbol, y lo acepta: le escribe dos líneas diciéndole al alumno que el modelo está bien pero que revise la fórmula del área del cubo, que el producto le señaló. Abre el segundo y no lo acepta: el alumno modeló otra cosa, así que lo rechaza y le explica por qué. El tercero lo rechaza sin escribir nada, porque el motivo es evidente y ya se lo dijo en clase. Al final de la sesión no queda ningún trabajo sin respuesta, y él sabe que no le queda ninguno por mirar.

La alumna del primer trabajo entra al día siguiente y ve en su lista que quedó aceptado, con el comentario del docente. El del segundo ve que el suyo fue rechazado, lee la explicación, corrige su programa y carga un trabajo nuevo: el rechazado queda ahí, como registro de lo que intentó. Semanas después el docente limpia de su listado los intentos viejos que ya no hacen falta.

## 3. Impacto

- Si se resuelve: la entrega deja de quedar depositada y pasa a estar resuelta, que es la diferencia entre un depósito de archivos y un circuito de laboratorio.
- Si se resuelve: el alumno sabe si su trabajo fue aceptado y, cuando el docente lo escribió, por qué.
- Si se resuelve: el docente puede trabajar en varias sesiones sin volver a mirar lo que ya resolvió, porque el estado se lo dice.
- Si se resuelve: aparece la fuente de dato con la que se mide que ninguna entrega quede sin revisar.
- Si se resuelve: el docente puede retirar los intentos acumulados que la terminalidad deja en la lista del alumno.
- Si queda sin resolver: el producto guarda, valida, dibuja y muestra, pero nadie contesta, y el alumno queda igual que hoy respecto de saber si entregó bien.
- Riesgo residual aceptado: un rechazo puede llegar sin explicación escrita, porque el comentario es opcional en los dos desenlaces (PRODUCT-INTAKE §4.2).

## 4. Problema específico que resuelve

- El alumno entrega y no recibe respuesta: no sabe si su trabajo fue aceptado ni si el docente lo miró.
- El docente no tiene dónde registrar que ya resolvió un trabajo, y vuelve a revisar lo mismo en cada sesión.
- No hay forma de que el docente le diga algo por escrito al alumno sobre su entrega.
- Una respuesta que se pudiera deshacer no sería una respuesta: sin estados terminales el desenlace no significa nada.
- Un alumno podría alterar el desenlace de su propio trabajo si la facultad no estuviera acotada al docente.
- Los intentos rechazados se acumulan en la lista del alumno y él no puede quitarlos.

## 5. Criterios de éxito

| Criterio | Métrica | Target | Plazo |
| --- | --- | --- | --- |
| Aprobación del administrador | Trabajos en estado `Pendiente` que reciben desenlace —`Finalizado` o `Rechazado`— sobre el total de trabajos que llegaron a estado `Pendiente` | 100 % | Al cierre de la primera cursada en que se use el producto |
| Cobertura de los desenlaces | Desenlaces disponibles sobre un trabajo en estado `Pendiente`, sobre los 2 declarados: aprobar y rechazar | 2 de 2 | Punto de control de la etapa `h` |
| Facultad exclusiva del administrador | Transiciones de desenlace que un alumno consigue ejecutar, forzando la petición al servicio de datos y no sólo desde la pantalla | 0 | Punto de control de la etapa `h` |
| Terminalidad del desenlace | Transiciones que salen de un trabajo en estado `Finalizado` o `Rechazado` | 0 | Punto de control de la etapa `h` |
| Carácter opcional del comentario | Desenlaces que el producto exige acompañar de un comentario escrito, sobre los 2 declarados | 0 de 2 | Punto de control de la etapa `h` |
| Devolución visible para el alumno | Trabajos con desenlace cuyo estado el alumno ve en su propio listado y cuyo comentario ve al abrir el trabajo, sobre el total de trabajos con desenlace | 100 % | Punto de control de la etapa `h` |
| Retiro de trabajos por el administrador | Estados en los que el administrador puede eliminar un trabajo de los que ve, sobre los 3 que ve: `Pendiente`, `Finalizado` y `Rechazado` | 3 de 3 | Punto de control de la etapa `h` |

Origen de cada criterio: el primero deriva de PRODUCT-INTAKE §8 y de `Vision-Producto.md` §5 (OBJ-03); el segundo, de PRODUCT-INTAKE §4 (F-23); el tercero y el cuarto, de PRODUCT-INTAKE §4.1 (RN-10) y de la transición `h` a `i…` de `Roadmap-Producto.md` §5.2; el quinto, de PRODUCT-INTAKE §4 (F-21) y §7 (CL-11); el sexto, de PRODUCT-INTAKE §6 (flujo 2.1) y de la misma transición del roadmap; el séptimo, de PRODUCT-INTAKE §4 (F-24), §4.1 (RN-04) y §7 (CL-10). **El target del primer criterio está rotulado como asunción A-2 en PRODUCT-INTAKE §22 y está pendiente de confirmación del Product Owner**; se usa como valor vigente hasta que la confirmación llegue. Los otros seis están declarados en las fuentes y no son asunciones.

El target del primer criterio es 100 % y no un porcentaje menor, y el motivo está declarado aguas arriba: un trabajo entregado y nunca revisado es exactamente el problema que esta necesidad viene a resolver, de modo que cualquier valor menor admitiría como éxito el estado de cosas que se quiere eliminar (PRODUCT-INTAKE §8).

## 6. Stakeholders involucrados

| Rol | Nivel | Qué pide o aporta |
| --- | --- | --- |
| Docente de Programación 2 (TUP), responsable de la cátedra y de la Actividad 1, en su papel de Product Owner | Propietario | Pidió el circuito de revisión el 2026-08-08, con lo que se retiró la exclusión que lo dejaba afuera; decidió la terminalidad de los dos desenlaces y el carácter opcional del comentario, con sus consecuencias aceptadas por escrito |
| Cátedra de Programación 2, como dueño del problema | Propietario | Necesita que la entrega del laboratorio se resuelva y no quede depositada |
| El mismo docente, en su papel de equipo de desarrollo (una persona, asistida por un agente de IA) | Implementador | Construye el circuito de desenlace y lo demuestra en el punto de control de la etapa `h`, que cierra el alcance comprometido |
| El mismo docente, en su papel de administrador del laboratorio, con la cuenta única de administrador | Beneficiario y operador | Ejerce la facultad exclusiva de aprobar y rechazar, deja el comentario cuando corresponde y retira los trabajos que ya no hacen falta |
| Alumno de la comisión | Beneficiario | Recibe una respuesta explícita sobre su entrega, y el comentario del docente cuando lo hay, en su propio listado |

## 7. Trazabilidad a CU

| NB | Casos de uso emitidos | Estado |
| --- | --- | --- |
| NB-00009 | `CU-00006`, `CU-00008`, `CU-02010`, `CU-02011`, `CU-04006`, `CU-04007`, `CU-04008`, `CU-04009`, `CU-06004` en `GeometriaFactory-Api`; `CU-10006`, `CU-10008`, `CU-10009` en `GeometriaFactory-Web` aprobar un trabajo en estado `Pendiente`, con comentario opcional | Emitidos |
| NB-00009 | `CU-00006`, `CU-00008`, `CU-02010`, `CU-02011`, `CU-04006`, `CU-04007`, `CU-04008`, `CU-04009`, `CU-06004` en `GeometriaFactory-Api`; `CU-10006`, `CU-10008`, `CU-10009` en `GeometriaFactory-Web` rechazar un trabajo en estado `Pendiente`, con comentario opcional | Emitidos |
| NB-00009 | `CU-00006`, `CU-00008`, `CU-02010`, `CU-02011`, `CU-04006`, `CU-04007`, `CU-04008`, `CU-04009`, `CU-06004` en `GeometriaFactory-Api`; `CU-10006`, `CU-10008`, `CU-10009` en `GeometriaFactory-Web` consultar el desenlace y el comentario del trabajo propio | Emitidos |
| NB-00009 | `CU-00006`, `CU-00008`, `CU-02010`, `CU-02011`, `CU-04006`, `CU-04007`, `CU-04008`, `CU-04009`, `CU-06004` en `GeometriaFactory-Api`; `CU-10006`, `CU-10008`, `CU-10009` en `GeometriaFactory-Web` eliminar un trabajo desde el panel del administrador | Emitidos |

## 8. Dependencias con otras NB

- Depende de: NB-00004, porque el desenlace se ejerce sobre trabajos en estado `Pendiente`, y a ese estado sólo se llega por un envío cuyo texto verifica; y NB-00007, porque el administrador decide sobre los trabajos que su listado le pone delante.
- Es prerequisito de: ninguna otra NB. Es una hoja de la cadena de dependencias, y su etapa cierra el alcance comprometido del producto.

## 9. Prioridad MoSCoW

**Must Have.** Se deriva de PRODUCT-INTAKE §4: las tres capacidades que esta NB agrupa —F-21, F-23 y F-24— están declaradas Must Have, las tres con la misma prioridad, de modo que no hay agregación de prioridades distintas. F-21 estaba en `Won't Have v1` y excluida en X-5 por «no fue pedido»; el Product Owner la pidió el 2026-08-08, con lo que se cumplió la condición de reingreso que la propia exclusión declaraba, y la exclusión se retiró aguas arriba. Esta categoría no origina el cambio de prioridad: lo consume.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial, derivada del circuito de revisión del administrador que el Product Owner incorporó en `PRODUCT-INTAKE` 1.3. Articula la necesidad de que la entrega tenga desenlace explícito a partir de las capacidades F-21, F-23 y F-24, con siete criterios de éxito trazados a su sección de origen —uno de ellos con target rotulado como asunción A-2— y cuatro casos de uso previstos. Se emite como necesidad propia y no como ampliación de NB-00007 por la regla de división de `Rules-Necesidades-Negocio.md` §2.2: el dolor, el beneficiario y la métrica son distintos, y el fundamento completo del recorte está en `Necesidades-Negocio.md` §3.2. **Corrección de la ronda 3 de auditoría, hallazgo H-03**, absorbida en esta misma emisión: el sexto criterio de §5 precisa dónde ve el alumno cada cosa —el desenlace en su listado y el comentario al abrir el trabajo—, para cerrar la lectura literal con la que la categoría 08 podría exigir el texto del comentario dentro del listado. La intención de §1 y el target de 100 % no cambian. |
