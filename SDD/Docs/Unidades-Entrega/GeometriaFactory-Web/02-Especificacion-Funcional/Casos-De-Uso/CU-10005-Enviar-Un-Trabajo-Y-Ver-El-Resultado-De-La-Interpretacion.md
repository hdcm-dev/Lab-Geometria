# CU-10005 — Enviar un trabajo y ver el resultado de la interpretación

**Unidad de entrega:** GeometriaFactory-Web
**Documento:** CU-10005-Enviar-Un-Trabajo-Y-Ver-El-Resultado-De-La-Interpretacion.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** Analista Funcional senior (AG-02)
**Trazabilidad upstream:** `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md` §1, §5 (tercero, cuarto, quinto y sexto criterio); `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md` §1, §5 (primero, segundo y quinto criterio); `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00005-Visibilidad-Del-Error-De-Calculo.md` §5 (segundo y tercer criterio); `../../../../00-Contexto/Vision-Producto.md` §9.1 (enviar, observación, advertencia, error de validación, estado del trabajo); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-06, F-09, F-10, F-22), §4.1 (RN-10003, RN-10005, RN-10008, RN-10009), §4.2, §6 (flujo 2, flujo 4), §7 (CL-3, CL-4), §17.6 P.3, P.4, P.11 punto 4 y punto 5, §20.E-1, §20.E-2
**Trazabilidad downstream:** `03-UX-UI-DX` de este proyecto de código; `05-Arquitectura-Tecnica`; `06-Backlog-Tecnico`; `08-Calidad-Y-Pruebas`

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

Permitir que el alumno cargue un trabajo con su nombre, su fecha, su descripción y el texto que produjo su Actividad 1, lo previsualice si quiere, y lo **envíe**: la única acción de guardado del producto, cuyo resultado decide el estado del trabajo según si el texto verifica.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Alumno | Primario | Completa los datos, pega el texto, previsualiza y envía |
| Pieza pública | Sistema | Presenta el formulario, invoca la previsualización sobre la fachada del visualizador, envía el texto sin tocarlo y presenta el resultado con sus observaciones |
| Pieza de datos | Secundario | Interpreta el texto, decide el estado del trabajo y devuelve las observaciones |
| Bundle del visualizador | Sistema | Dibuja la previsualización a pedido de la pieza pública, sin conocer al alumno ni al trabajo |

## 3. Precondiciones

- El alumno tiene sesión iniciada por CU-10002 y su papel es el de alumno.
- Para crear: no hay trabajo previo. Para volver sobre uno, el trabajo está en estado `Borrador` y le pertenece.
- El navegador provee la capacidad gráfica tridimensional. Sin ella la previsualización no está disponible y el envío sigue estándolo, según FA-04.

## 4. Flujo principal

1. El alumno abre la ruta de trabajo nuevo desde su panel.
2. La pieza pública presenta el formulario con nombre, fecha, descripción y el área donde se pega el texto del trabajo.
3. El alumno pega el texto **tal como lo emitió su programa**, con las comas finales y las claves que ese programa usa. No se le pide ninguna corrección.
4. El alumno pide previsualizar. La pieza pública invoca `inicializar` sobre la fachada del visualizador con el elemento de dibujo de la vista, y luego `cargarJson` con el texto pegado. La escena se dibuja en el navegador **sin ninguna llamada a la pieza de datos**.
5. El alumno confirma el envío.
6. **La pieza pública invoca desde su servidor el contrato de envío del trabajo** de `GeometriaFactory-Contracts` CU-10003, asignando al campo de texto original **la cadena exacta** que el alumno pegó, sin normalizarla, sin reordenarla y sin quitarle caracteres.
7. La pieza de datos interpreta el texto y devuelve el identificador del trabajo, el estado que la interpretación decidió, la fecha de registro y las observaciones.
8. La pieza pública presenta el resultado: el estado alcanzado y la lista de observaciones, cada una con su severidad, su índice de figura y su campo señalado, y con el par de valores declarado y derivado cuando la observación es una advertencia.
9. La pieza pública devuelve al alumno a su listado de trabajos, de CU-10006, donde el trabajo ya figura con su estado.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El texto verifica | El resultado trae estado `Pendiente`. El trabajo queda entregado y **deja de ser editable y eliminable por el alumno** | El flujo continúa en el paso 8, y sigue en CU-10009 cuando el administrador lo resuelva |
| FA-02 | El texto no verifica | El envío **no falla**: el resultado trae estado `Borrador`, el texto conservado íntegro y las observaciones de error de validación con índice de figura y campo señalado. La pieza pública las presenta y ofrece volver a enviar | El flujo vuelve al paso 3, con el texto y los datos tal como quedaron |
| FA-03 | El texto verifica pero trae discrepancias entre valor declarado y valor derivado | El trabajo pasa igual a estado `Pendiente`, y la pieza pública muestra las advertencias con los dos valores. **Ninguna advertencia bloquea el envío** | El flujo continúa en el paso 8 |
| FA-04 | El navegador no provee la capacidad gráfica tridimensional | La fachada informa `CAPACIDAD_GRAFICA_AUSENTE` y no crea instancia. La pieza pública informa que la previsualización no está disponible en ese navegador y **mantiene disponible el envío**, que no depende del dibujo | El flujo continúa en el paso 5 |
| FA-05 | El alumno vuelve sobre un trabajo suyo en estado `Borrador` | La pieza pública lo abre con sus datos y su texto tal como quedaron, y el envío usa el mismo contrato con el identificador ya asignado | El flujo continúa en el paso 3 |
| FA-06 | El alumno abandona la vista de trabajo, con o sin enviar | La pieza pública invoca `destruir` sobre la instancia del visualizador al descartar el componente. Nada de lo escrito sobrevive del lado de la pieza pública | El flujo termina |

## 6. Excepciones y errores

| Código | Causa | Respuesta del sistema |
| --- | --- | --- |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | Falta el nombre, la fecha o el texto | La pieza pública señala el campo que el contrato nombra. Recuperación por corrección y reintento. **El texto vacío es campo ausente, no texto que no verifica** |
| `CONTRATO_TRABAJO_NO_ENCONTRADO` | Se envía sobre un identificador que no corresponde a un trabajo del alumno, o que no existe | La pieza pública informa con texto neutro que **no distingue** el trabajo ajeno del inexistente, y devuelve al listado. Terminación controlada |
| `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` | Aparece sólo en el camino de eliminación de CU-10006, no en el de envío | No aplica acá; se declara para que la ausencia sea deliberada y no un olvido |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Handoff a CU-10010: estado degradado explícito, sin dirección de servicio interno. **La pieza pública conserva en la vista lo que el alumno escribió** para que pueda reintentar, y no lo guarda en ningún lado |
| `TEXTO_NO_LEGIBLE` | La fachada del visualizador no obtiene piezas del texto en la previsualización | La instancia queda viva y vacía. La pieza pública avisa que la previsualización no pudo dibujar nada y **no deduce de eso ningún estado del trabajo**: quien decide es la pieza de datos, en el paso 7 |
| `TIPO_NO_DIBUJABLE`, `DIMENSION_NO_LEGIBLE` | Una pieza no produjo dibujo en la previsualización | La pieza pública enumera esas piezas por su índice junto a la escena. **No las califica de error del trabajo**: no son observaciones |

### 6.1 Señal declarada que no es error

| Código | Causa | Respuesta del sistema |
| --- | --- | --- |
| `CONTRATO_TEXTO_NO_INTERPRETABLE` | El texto enviado no verifica | El envío procede y devuelve estado `Borrador` con las observaciones. Es el curso de FA-02 y **no** es un fallo del envío |

## 7. Postcondiciones

- En caso de éxito con texto que verifica: existe un trabajo en estado `Pendiente` con dueño, identificador, fecha y el texto original idéntico al que el alumno pegó.
- En caso de éxito con texto que no verifica: existe un trabajo en estado `Borrador` con su texto conservado y sus observaciones de error de validación localizadas.
- En caso de fallo del envío: no queda trabajo a medio crear, y lo que el alumno escribió sigue en la vista para el reintento.
- En ningún caso: la pieza pública modifica un carácter del texto del alumno, ni ofrece una acción de guardado distinta del envío, ni deduce por su cuenta el estado del trabajo.
- Al descartar la vista: la instancia del visualizador queda liberada por `destruir`.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno con sesión y el texto de `Ortoedro(7, 7, 21)` tal como lo emite su programa, con sus dos comas finales y su clave `Tapas` | Envía el trabajo | El trabajo pasa a estado `Pendiente` y aparece **una** advertencia de volumen, con declarado 343.00 y derivado 1029.00 |
| CA-02 | El mismo trabajo enviado | Se compara el texto guardado con el que el alumno pegó | Son idénticos carácter por carácter: cero caracteres modificados |
| CA-03 | El texto semilla de tres piezas —un cilindro, un cubo y un ortoedro— | El alumno lo envía | El trabajo pasa a estado `Pendiente` con **dos** advertencias: área del cubo, declarada 36.00 contra derivada 54.00, y volumen del ortoedro, declarado 343.00 contra derivado 1029.00. El área del ortoedro no produce observación |
| CA-04 | Un texto con una figura de tipo desconocido | El alumno lo envía | El trabajo queda en estado `Borrador`, el texto se conserva y el mensaje indica el **índice de figura** y el campo `Tipo`, nunca un texto genérico |
| CA-05 | La vista de trabajo nuevo | Se cuentan las acciones de guardado ofrecidas al alumno | Es exactamente una: enviar. No existe «guardar sin enviar» |
| CA-06 | Un texto pegado y la previsualización pedida | Se inspecciona el tráfico de red del navegador durante el dibujo y la interacción con la escena | Cero peticiones originadas por el navegador: el dibujo ocurre sin red y el envío sale del servidor de la pieza pública |
| CA-07 | La vista de trabajo con una instancia del visualizador viva | El alumno navega a otra ruta | Se invoca `destruir`; repetir el recorrido diez veces no degrada la visualización |
| CA-08 | El servicio de datos detenido y un texto ya pegado | El alumno confirma el envío | La página sigue en pie, muestra el estado degradado, **conserva lo escrito** y el mensaje no contiene ninguna dirección de servicio interno |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [`NB-00004`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md), [`NB-00003`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md), [`NB-00005`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00005-Visibilidad-Del-Error-De-Calculo.md) |
| Reglas de negocio aplicables | [`RN-02005`](../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md) —corta hoy en el envío y no en el cierre—, [`RN-02008`](../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md), [`RN-02009`](../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md), [`RN-02003`](../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) |
| Contratos de uso consumidos | [`GeometriaFactory-Contracts` CU-08003](../../../../Producto/Contratos-Inter-Unidad/CU-08003-Contrato-De-Carga-Y-Edicion-Del-Trabajo.md) flujo principal, FA-01 y FA-03, y su señal §6.1; [`CU-08006`](../../../../Producto/Contratos-Inter-Unidad/CU-08006-Contrato-De-Respuesta-De-Error.md) |
| Fachada del visualizador | [`inicializar`, `cargarJson`, `destruir`](../Definicion-Contrato-De-Fachada.md) §4.1, §4.2 y §4.5, para la previsualización previa al envío. El recorrido completo de las **seis** funciones es de CU-10007 |
| Historias de usuario a generar en 06 | US-10011, US-10012, US-10013, US-10014 |
| Componentes esperados en 05 | Vista de trabajo, componente anfitrión del visualizador y cliente tipado de la pieza de datos, todos del lado del servidor de la pieza pública salvo el dibujo |
| Tests previstos en 08 | Guion de demostración de la etapa `f` con los escenarios de datos verificados del intake; batería obligatoria de nueve casos, cuya titularidad es de la pieza de datos |

## 10. Notas y supuestos

- **El envío es la única acción de guardado**, y de ahí se sigue que `Borrador` significa exactamente «el texto no verificó». Un texto que verifica no puede quedarse en borrador, y la vista no ofrece ninguna forma de conseguirlo.
- La previsualización del paso 4 **no anticipa el estado del trabajo**. El bundle del visualizador lee dimensiones para dibujar; no valida, no emite observaciones y no recalcula valores. Que una pieza no se dibuje no dice nada sobre si el trabajo verifica, y que se dibujen todas tampoco.
- La verificación de valores es del lado de la pieza de datos y es no bloqueante por decisión declarada: es el mayor valor didáctico del producto. La pieza pública muestra los dos valores y no ordena corregir nada.
- La disposición concreta de la vista se declara en CU-10007 y su detalle visual pertenece a 03-UX-UI-DX.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, decisión **(b)**. §9 corrige el recuento de la fachada, que decía **cinco** funciones y son **seis** desde el intake 1.6, que incorporó `establecerMovimiento` para gobernar los dos movimientos automáticos de F-25. **Este caso de uso no la consume**: la previsualización previa al envío sigue usando `inicializar`, `cargarJson` y `destruir`, y el movimiento automático es de la vista de trabajo, CU-10007. Sube minor por alineación de una referencia al contrato de fachada, sin cambiar ningún flujo. |
