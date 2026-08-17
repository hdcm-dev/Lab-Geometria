# CU-10007 — Abrir un trabajo y explorarlo en escena y árbol

**Unidad de entrega:** GeometriaFactory-Web
**Documento:** CU-10007-Abrir-Un-Trabajo-Y-Explorarlo-En-Escena-Y-Arbol.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional senior (AG-02)
**Trazabilidad upstream:** `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00006-Visualizacion-Dentro-Del-Producto.md` §1, §4, §5 (los siete criterios); `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00005-Visibilidad-Del-Error-De-Calculo.md` §5 (tercer criterio); `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00007-Revision-De-La-Comision-En-Un-Solo-Lugar.md` §5 (quinto criterio); `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00009-Desenlace-Explicito-De-La-Entrega.md` §5 (sexto criterio); `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md` §5 (sexto criterio); `../../../../00-Contexto/Vision-Producto.md` §9.1 (observación, comentario, fallo silencioso); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.14**, §4 (F-11, F-13, **F-25 `Must Have`**), §4.1 (RN-10003, RN-10008, RN-10009, RN-10011), §6 (flujo 3), §14 (RA-02), §17.6 P.3, P.10, **P.11 punto 4 y punto 5**, y §17.7 P.3 y P.10
**Trazabilidad downstream:** `03-UX-UI-DX` de este proyecto de código —**es su insumo más directo**—; `05-Arquitectura-Tecnica`; `06-Backlog-Tecnico`; `08-Calidad-Y-Pruebas`

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

Presentar un trabajo cargado dentro del producto, con sus datos y su texto original de un lado, y la escena tridimensional y el árbol de la estructura del otro, sincronizados por índice de pieza, junto con sus observaciones y, si lo hay, el comentario del administrador. Es la vista que ven **exactamente igual** el alumno dueño y el administrador.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Alumno dueño del trabajo | Primario | Abre su trabajo, lo recorre en escena y árbol y lee sus observaciones y su comentario |
| Pieza pública | Sistema | Pide el detalle, reparte su contenido entre las cuatro partes de la vista, y opera el ciclo de vida de la instancia del visualizador |
| Pieza de datos | Secundario | Devuelve el detalle del trabajo, acotado a lo que el solicitante ve |
| Bundle del visualizador | Sistema | Dibuja la escena y resalta la pieza seleccionada, sin conocer al solicitante ni al trabajo |
| Administrador | Secundario | Abre el mismo trabajo con la misma vista, según FA-01 |

El actor primario es uno solo. El administrador aparece como actor secundario y su curso está en FA-01, porque la vista es idéntica y no hay una variante enriquecida: separar dos casos de uso habría duplicado la superficie sin declarar ninguna decisión distinta.

## 3. Precondiciones

- La persona tiene sesión iniciada por CU-10002 y llegó acá desde su listado: CU-10006 si es el alumno dueño, CU-10008 si es el administrador.
- El trabajo existe y es visible para el solicitante. Un trabajo en estado `Borrador` no es visible para el administrador.
- El navegador provee la capacidad gráfica tridimensional. Sin ella rige FA-05.

## 4. Flujo principal

1. La persona abre el trabajo desde su listado.
2. **La pieza pública invoca desde su servidor el contrato de detalle del trabajo** de `GeometriaFactory-Contracts` CU-10005, con el identificador del trabajo.
3. La pieza de datos devuelve el detalle con sus seis bloques: datos del trabajo con su estado, texto original íntegro, colección de piezas con sus componentes, colección de observaciones, datos del alumno dueño y comentario del administrador.
4. La pieza pública arma la vista de trabajo con **cuatro partes y una disposición ya decidida y probada en el aula**: a la izquierda, los datos del trabajo y su texto original; a la derecha, el elemento de dibujo arriba y el árbol de la estructura abajo.
5. La pieza pública invoca `inicializar` sobre la fachada del visualizador con el elemento de dibujo de la vista, y conserva el identificador de instancia que recibe.
6. La pieza pública invoca `cargarJson` con el texto original del trabajo. **El texto viaja del servidor al navegador una sola vez por trabajo.**
7. La fachada devuelve el resultado de dibujo: las piezas dibujadas con su índice y su tipo, las no dibujadas con su índice y su motivo, y la estructura del texto lista para presentarse como árbol.
8. La pieza pública presenta el árbol colapsable a partir de esa estructura, y enumera junto a la escena las piezas que no se dibujaron, con su índice. **Ninguna pieza desaparece sin dejar registro.**
9. La persona selecciona un elemento del árbol. La pieza pública invoca `seleccionarPieza` con el índice de esa pieza, y la escena resalta esa pieza y sólo esa. La selección desde la escena hacia el árbol usa el mismo índice, sin traducir identidades.
10. La pieza pública muestra la colección de observaciones junto al trabajo, **sin filtrarlas por severidad**, cada una con su índice de figura, su campo señalado y, cuando es una advertencia, su valor declarado y su valor derivado.
11. La pieza pública muestra el comentario del administrador, si viene poblado, **como bloque aparte de las observaciones**.
12. Cuando cambia el tamaño disponible para el elemento de dibujo, la pieza pública invoca `redimensionar`.
13. La pieza pública **consulta el entorno del navegador —incluida la preferencia de movimiento reducido— y decide el estado de los dos movimientos automáticos** (F-25): la órbita de la cámara y el giro de las piezas. Invoca `establecerMovimiento` con **dos valores de verdad**, uno por cada uno. **El bundle no consulta nada**: no lee esa preferencia, no guarda la elección y no decide por su cuenta.
14. Al descartar el componente que aloja el visualizador, la pieza pública invoca `destruir` con el identificador de instancia.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Quien abre el trabajo es el administrador | El contrato y la vista son los mismos, con los mismos cuatro elementos: datos, texto, escena y árbol. No hay variante enriquecida ni recortada | El flujo continúa en el paso 4 |
| FA-02 | El trabajo está en estado `Borrador` y su texto tiene errores de validación | El detalle llega igual, con la colección de piezas parcial y las observaciones de error de validación pobladas. Se dibuja lo que se pudo reconstruir y se enumeran las piezas faltantes | El flujo continúa en el paso 5, con lo que haya |
| FA-03 | El trabajo no produjo ninguna observación | La colección llega con cero elementos y no ausente. La pieza pública lo presenta como «sin observaciones», no como un hueco | El flujo continúa en el paso 11 |
| FA-04 | El trabajo ya recibió su desenlace y el administrador no escribió comentario | El bloque de comentario llega sin poblar. La pieza pública no muestra un bloque vacío: **el estado del trabajo expresa el desenlace por sí solo** | El flujo termina |
| FA-05 | El navegador no provee la capacidad gráfica tridimensional | La fachada informa `CAPACIDAD_GRAFICA_AUSENTE` y no crea instancia. La pieza pública informa que la escena no está disponible en ese navegador y **mantiene las otras tres partes de la vista**: datos, texto y árbol | El flujo continúa en el paso 8, sin escena |
| FA-06 | La persona pasa de un trabajo al siguiente sin salir de la ruta | La pieza pública invoca `destruir` sobre la instancia vigente antes de volver a `inicializar` para el trabajo nuevo, o invoca `cargarJson` sobre la instancia viva, que reemplaza por completo lo dibujado y libera lo anterior | El flujo vuelve al paso 2 |
| FA-07 | La persona prende o apaga uno de los dos movimientos automáticos | La pieza pública invoca `establecerMovimiento` **sobre la instancia viva y sin reconstruirla**: no recarga el texto, no altera la disposición y **conserva la selección de pieza vigente**. Lo no nombrado conserva su estado | El flujo vuelve al paso 9 |
| FA-08 | El navegador declara **preferencia de movimiento reducido** | **La pieza pública la consulta y la traduce**: manda los dos valores en falso y lo declara en la superficie. El bundle no participa de esa decisión y no habría podido tomarla: no consulta configuración del navegador (RA-02, RT-13) | El flujo continúa en el paso 13 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del sistema |
| --- | --- | --- |
| `CONTRATO_TRABAJO_NO_ENCONTRADO` | El identificador no corresponde a un trabajo visible para el solicitante, o no existe. Incluye el trabajo en estado `Borrador` pedido por el administrador | La pieza pública muestra un mensaje neutro que **no distingue** los casos y devuelve al listado del solicitante. Terminación controlada |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Handoff a CU-10010: estado degradado explícito, sin dirección de servicio interno. **No se arma ni la escena ni el árbol** |
| `ELEMENTO_DE_DIBUJO_INVALIDO`, curso de creación | El elemento de dibujo de la vista no sirve como superficie o tiene tamaño nulo al invocar `inicializar` | No hay instancia. La pieza pública informa que la escena no está disponible y conserva las otras tres partes de la vista. Recuperación: al recuperar tamaño la vista, se vuelve a inicializar |
| `ELEMENTO_DE_DIBUJO_INVALIDO`, curso de ajuste | El elemento de dibujo pasó a tamaño cero al invocar `redimensionar` | **La instancia sigue viva**, con su escena y su selección intactas. Una invocación posterior ajusta cuando el elemento vuelva a tener tamaño |
| `INSTANCIA_DESCONOCIDA` | Se invoca una función de la fachada con un identificador de una instancia ya liberada | Ninguna instancia cambia. Es un defecto del ciclo de vida del componente anfitrión y no una condición esperable: se corrige respetando el paso 14 |
| `TEXTO_NO_LEGIBLE` | La fachada no obtiene piezas del texto original | La instancia queda viva y vacía. La pieza pública lo informa junto a la escena y **el árbol y las observaciones se muestran igual** |
| `TIPO_NO_DIBUJABLE`, `DIMENSION_NO_LEGIBLE` | Una pieza no produjo dibujo | Esas piezas se enumeran por su índice junto a la escena. **No se presentan como observaciones del trabajo**: son condiciones del dibujo y no las emite la verificación |
| `INDICE_FUERA_DE_RANGO` | Se pide resaltar un índice que no corresponde a ninguna pieza dibujada del resultado vigente | La selección vigente se conserva. La pieza pública indica que esa pieza no tiene representación en la escena, y el elemento del árbol sigue siendo navegable |

## 7. Postcondiciones

- En caso de éxito: la persona tiene a la vista los datos, el texto original íntegro, la escena y el árbol, con sus observaciones y, si lo hay, el comentario en bloque aparte.
- Al abandonar la vista: la instancia del visualizador está liberada y no queda ningún contexto gráfico acumulado.
- En caso de fallo del detalle: no se arma la vista y la persona ve el estado degradado o el mensaje neutro, sin quedar en una pantalla rota.
- En ningún caso: la pieza pública reescribe el texto original, ni accede al interior del bundle, ni manipula el elemento de dibujo por su cuenta, ni el navegador emite una petición hacia la pieza de datos.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un trabajo con el texto semilla de tres piezas —cilindro, cubo y ortoedro— | El alumno lo abre | Se dibujan **3 de 3** piezas, **ortoedro incluido**, y las dos advertencias de valor figuran junto al trabajo con sus dos valores cada una |
| CA-02 | La vista de trabajo abierta | Se observan sus partes | Son cuatro: datos y texto a la izquierda; elemento de dibujo arriba y árbol abajo, a la derecha |
| CA-03 | Un trabajo dibujado y su árbol desplegado | La persona selecciona el elemento del árbol correspondiente al índice 2 | La escena resalta esa pieza y sólo esa; el índice usado es el mismo con el que la pieza figura en el resultado de dibujo |
| CA-04 | Un trabajo con una figura de tipo desconocido en el índice 1 | La persona lo abre | La pieza del índice 1 se enumera como no dibujada con su índice a la vista; las demás se dibujan. **Ninguna desaparece sin registro** |
| CA-05 | Dos trabajos cargados | La persona recorre de uno a otro y vuelve, diez veces | Las diez idas y vueltas no degradan la visualización: se invoca `destruir` en cada descarte del componente |
| CA-06 | El mismo trabajo | La persona lo abre dos veces | La disposición de las piezas en la escena es la misma en los dos procesados, comparable pieza por pieza |
| CA-07 | Un trabajo en estado `Pendiente` de un alumno de la comisión | El administrador lo abre desde su listado | Ve los **4 de 4** elementos que ve el alumno: datos, texto, escena y árbol, con las mismas observaciones |
| CA-08 | Un trabajo en estado `Borrador` de un alumno | El administrador lo pide por dirección directa | Recibe «no encontrado» y vuelve a su listado, sin que se confirme que ese trabajo existe |
| CA-09 | Un trabajo `Rechazado` con comentario escrito | El alumno dueño lo abre | El comentario se muestra en un bloque propio, separado de las observaciones, sin severidad, sin índice y sin campo señalado |
| CA-10 | Un trabajo abierto con su escena dibujada, **con los dos movimientos automáticos prendidos** | Se inspecciona el tráfico de red del navegador mientras se rota y se acerca la escena | Cero peticiones originadas por el navegador, y el texto del trabajo viajó una sola vez. **Los dos movimientos prendidos son la condición de medición**, por ser el peor caso |
| CA-11 | Un trabajo abierto con una pieza seleccionada | La persona apaga el giro de las piezas | La escena deja de girar las piezas, **la selección se conserva**, la disposición no cambia y no hubo reconstrucción de la instancia |
| CA-12 | Un navegador con preferencia de movimiento reducido declarada | La persona abre un trabajo | Los dos movimientos arrancan apagados porque **la pieza pública leyó la preferencia** y mandó dos valores en falso. Se verifica además que **el bundle no consultó nada**: ninguna lectura de configuración del navegador se origina en él |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [`NB-00006`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00006-Visualizacion-Dentro-Del-Producto.md), [`NB-00005`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00005-Visibilidad-Del-Error-De-Calculo.md), [`NB-00007`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00007-Revision-De-La-Comision-En-Un-Solo-Lugar.md), [`NB-00009`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00009-Desenlace-Explicito-De-La-Entrega.md), [`NB-00004`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) |
| Reglas de negocio aplicables | [`RN-02003`](../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [`RN-02008`](../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md), [`RN-02009`](../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md), [`RN-02011`](../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md) |
| Contratos de uso consumidos | [`GeometriaFactory-Contracts` CU-08005](../../../../Producto/Contratos-Inter-Unidad/CU-08005-Contrato-De-Detalle-Del-Trabajo-Interpretado.md) completo, con FA-01 a FA-04 y su señal §6.1; [`CU-08006`](../../../../Producto/Contratos-Inter-Unidad/CU-08006-Contrato-De-Respuesta-De-Error.md) |
| Fachada del visualizador | **Las seis funciones**: [`inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir`](../Definicion-Contrato-De-Fachada.md) §4, más `establecerMovimiento` de su §4.6, y sus siete códigos de condición de §6, que **no aumentan**: el gobierno del movimiento no abre ninguna condición nueva |
| Historias de usuario a generar en 06 | US-10018, US-10019, US-10020, US-10021 |
| Componentes esperados en 05 | Vista de trabajo, componente anfitrión del visualizador con su ciclo de vida, y componente del árbol de la estructura |
| Tests previstos en 08 | Guion de demostración de la etapa `g` completo, con los escenarios de datos verificados del intake, las diez idas y vueltas, la comparación de dos procesados, y la medición de cero peticiones **con los dos movimientos prendidos** |

## 10. Notas y supuestos

- **La disposición del paso 4 no se reinventa**: viene decidida aguas arriba y probada en el aula. Su detalle visual —proporciones, tipografía, comportamiento en pantallas angostas— pertenece a 03-UX-UI-DX, que toma este caso de uso como insumo.
- **El bundle se invoca exclusivamente a través de sus seis funciones.** Ningún componente de la pieza pública toca su interior ni manipula el elemento de dibujo por su cuenta. Es lo que permite reemplazar el motor de dibujo sin tocar estas páginas.
- **El gobierno del movimiento automático viaja en un solo sentido y no admite la lectura inversa** (RT-13). El anfitrión —esta pieza— manda **dos valores de verdad**, uno por movimiento, y el bundle **no consulta nada**: ni la preferencia de movimiento reducido del navegador, ni almacenamiento, ni configuración propia. **Quien lee esa preferencia es esta pieza**, que la traduce a los dos booleanos. Es lo que exige RA-02, un visualizador puro, sin red, sin configuración y sin identidad: un bundle que consultara la preferencia estaría tomando una decisión de producto por su cuenta.
- **La preferencia de cada movimiento es de esta pieza y no del bundle.** Si el producto decide conservarla entre trabajos, la conserva esta pieza. La fachada **no guarda nada**, y el umbral de cero escrituras en el almacenamiento del navegador atribuibles al bundle es exactamente 0.
- **Ninguno de los dos movimientos altera la disposición de las piezas.** El determinismo comprometido es el de la **posición**, derivada del índice, y no el de la orientación en un instante (`PRODUCT-INTAKE` §17.7 P.10). CA-06 sigue siendo verificable con los movimientos prendidos.
- **La invocación de `destruir` del paso 14 no es opcional.** Sin ella, recorrer trabajos acumula contextos gráficos en el navegador. CA-05 la verifica de forma observable.
- La palabra «vista» se usa acá en un solo sentido —la página que presenta un trabajo, con sus cuatro partes—. El glosario declara sus otros dos referentes para quien lea este caso de uso como sección suelta.
- Las condiciones del dibujo y las observaciones del trabajo son cosas distintas y la vista no las mezcla: una pieza que no se dibuja no es un error del trabajo, y quien decide si el trabajo verifica es la pieza de datos.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, decisión **(b)**: **F-25 sube a `Must Have`** y su frontera queda fijada. **§4**: paso 13 nuevo —la pieza pública consulta el entorno, decide los dos movimientos e invoca `establecerMovimiento` con **dos valores de verdad**—, y el `destruir` pasa a ser el paso 14. **§5**: **FA-07** y **FA-08** nuevas, el gobierno sobre la instancia viva sin perder la selección, y la preferencia de movimiento reducido **leída por esta pieza y traducida**, no por el bundle. **§8**: CA-10 pasa a medirse **con los dos movimientos prendidos**, que es el peor caso, y se agregan CA-11 y CA-12. **§9**: la fachada pasa de cinco a **seis funciones**, con la aclaración de que los siete códigos de condición **no aumentan**. **§10**: cuatro notas nuevas sobre la dirección única del gobierno, la preferencia que es de esta pieza, la ausencia de escrituras del bundle y el determinismo de la posición y no de la orientación. Sube minor: agrega un paso, dos flujos alternativos, dos criterios de aceptación y una función consumida, sin invalidar ninguna decisión previa. |
| 1.2 | 2026-08-10 | **Cierra el hallazgo `C-08` (P2) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0.** La cabecera de trazabilidad declaraba derivarse del `PRODUCT-INTAKE` **1.7**, versión archivada, y pasa a declarar la **1.14**, vigente. La **1.7** es la versión cuya letra sobre **RN-10013** e **INV-09** fue precisada en la 1.8 y corregida en la 1.14, que es exactamente el punto donde el corpus más se equivocó. Se revisó el cuerpo antes de mover la cabecera y **no arrastra ninguna decisión de las versiones intermedias**: no queda en él ningún recuento de «quince reglas» ni de «diecisiete códigos», ninguna cita a la exclusión **X-2** como vigente y ninguna afirmación de que la marca de cambio de contraseña pendiente la ponga únicamente el reseteo. **Ningún contenido normativo de este documento cambia: la corrección es de trazabilidad.** Sube minor. |
