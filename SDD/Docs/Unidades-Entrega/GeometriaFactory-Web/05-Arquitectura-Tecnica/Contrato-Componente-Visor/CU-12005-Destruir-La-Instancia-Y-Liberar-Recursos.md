# CU-12005 — Destruir la instancia y liberar sus recursos

**Unidad de entrega:** GeometriaFactory-Web
**Documento:** CU-12005-Destruir-La-Instancia-Y-Liberar-Recursos.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `NB-00006-Visualizacion-Dentro-Del-Producto.md` §5 (tercer criterio, continuidad de uso: 10 recorridos de ida y vuelta); `00-Contexto/Vision-Producto.md` §3 (diferenciador D-4); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §17.7 P.3, §17.7 P.4 (persistencia: prohibición explícita), §17.7 P.8 (criterio de las 10 navegaciones), §17.7 P.10, §14 (RA-02)
**Trazabilidad downstream:** 03-UX-UI-DX, 05-Arquitectura-Tecnica, 06-Backlog-Tecnico, 08-Calidad-Y-Pruebas, 10-Examples

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

Permitir que el componente anfitrión libere una instancia del visor cuando deja de necesitarla, devolviendo las geometrías, los materiales y el contexto gráfico que había tomado. Sin esta función, recorrer trabajos de ida y vuelta acumula contextos gráficos y la visualización se degrada; con ella, diez recorridos completos no degradan.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Componente anfitrión | Primario | Decide que la instancia ya no se necesita —porque su vista se cierra o se reemplaza— e invoca `destruir` |
| Fachada del visor | Sistema | Libera los recursos de esa instancia, invalida su identificador y confirma la liberación |

## 3. Precondiciones

1. Existe una instancia viva y el componente anfitrión tiene su identificador (`CU-12001`).
2. No se requiere que haya un trabajo cargado: una instancia vacía también se destruye.

## 4. Flujo principal

| Paso | Actor | Acción |
| --- | --- | --- |
| 1 | Componente anfitrión | Invoca `destruir(id)` con el identificador de una instancia viva |
| 2 | Fachada del visor | Quita de la escena las mallas de todas las piezas dibujadas y libera sus geometrías y sus materiales |
| 3 | Fachada del visor | Libera el contexto gráfico tridimensional que la instancia había tomado sobre el elemento de dibujo |
| 4 | Fachada del visor | Deja de atender los gestos sobre esa escena y descarta la selección vigente y el resultado de dibujo |
| 5 | Fachada del visor | Invalida el identificador de instancia y confirma la liberación |
| 6 | Componente anfitrión | Descarta el identificador: cualquier invocación posterior con él produce `UNKNOWN_INSTANCE` |

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 · Destrucción de una instancia vacía | Se invoca sobre una instancia viva que nunca cargó un texto | Se libera el contexto gráfico y se invalida el identificador; los pasos 2 y 4 no tienen nada que liberar y la operación termina igual | Paso 5 del flujo principal |
| FA-02 · Destrucción con otras instancias vivas | Hay dos o más instancias vivas y se destruye una | Sólo se liberan los recursos de la instancia indicada; las demás siguen vivas, con su escena, su selección y su encuadre intactos | Paso 5 del flujo principal |
| FA-03 · Recorrido de ida y vuelta entre trabajos | El componente anfitrión destruye la instancia al salir de una vista y crea una nueva al volver | Cada destrucción libera lo que su inicialización había tomado. Repetido diez veces, la visualización no degrada | `CU-12001`, paso 1, para la instancia siguiente |

## 6. Excepciones y errores

| Código | Causa | Respuesta de la fachada |
| --- | --- | --- |
| `UNKNOWN_INSTANCE` | El identificador no corresponde a ninguna instancia viva, o corresponde a una ya liberada | No se libera nada, ninguna instancia viva se altera y se informa el código. Destruir dos veces el mismo identificador no rompe nada: la segunda invocación informa esta condición y termina |

Es la única condición de error del caso de uso: `destruir` no puede fallar a medias sobre una instancia viva. Si alguno de los recursos ya no estaba tomado, la liberación continúa con los demás y el identificador queda invalidado igual (garantía G-7 del contrato de fachada).

## 7. Postcondiciones

- **Éxito:** la instancia no existe; sus geometrías, sus materiales y su contexto gráfico quedaron liberados; su identificador es inválido; las demás instancias siguen vivas e intactas; el elemento de dibujo sigue en la página, sin contenido dibujado; no quedó ninguna clave en el almacenamiento del navegador; hubo 0 peticiones de red.
- **Fallo:** el identificador no correspondía a una instancia viva y nada cambió.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una instancia viva con el texto del escenario E-1 cargado y sus tres piezas dibujadas | El componente anfitrión invoca `destruir(id)` | La fachada confirma la liberación, el elemento de dibujo queda sin contenido dibujado y el identificador deja de ser válido |
| CA-02 | Una instancia recién destruida | El componente anfitrión invoca `cargarJson(id, texto)`, `seleccionarPieza(id, 0)`, `redimensionar(id)`, `establecerMovimiento(id, opciones)` o `destruir(id)` con ese identificador | Cada una de las cinco invocaciones informa `UNKNOWN_INSTANCE` y ninguna instancia viva se altera |
| CA-03 | Dos instancias vivas, A y B, cada una con el texto del escenario E-7 cargado y 6 piezas dibujadas | El componente anfitrión invoca `destruir` sobre A | B conserva sus 6 piezas, su selección y su encuadre, y sigue respondiendo a las seis funciones |
| CA-04 | Una página integradora que crea la instancia, carga el escenario E-1, **prende los dos movimientos automáticos**, la destruye y repite el recorrido completo | El componente anfitrión completa **10 recorridos de ida y vuelta** con los movimientos prendidos en cada uno | Los 10 recorridos terminan dibujando las 3 piezas, sin degradación de la visualización, sin acumulación de contextos gráficos y **sin ningún bucle de dibujo que sobreviva a `destruir`**. Es la condición de medición que declara `Especificacion-Funcional.md` §6 |
| CA-05 | Una instancia viva y la pestaña de red vacía, con el almacenamiento del navegador sin claves de la fachada | El componente anfitrión invoca `destruir(id)` | La pestaña de red registra exactamente 0 peticiones originadas por la fachada y el almacenamiento del navegador sigue sin ninguna clave de la fachada |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-00006, tercer criterio de éxito (continuidad de uso: 10 recorridos de 10) |
| Reglas de negocio aplicables | Ninguna. Este proyecto de código no declara RN (ver `README.md` de la sección) |
| Historias de usuario a generar | US de liberación de recursos de la instancia y de no degradación entre trabajos, en 06-Backlog-Tecnico |
| Componentes esperados | Fachada plana y servicio de dibujo, con la liberación de geometrías, materiales y contexto gráfico; 05-Arquitectura-Tecnica fija la composición |
| Tests previstos | 08-Calidad-Y-Pruebas: liberación e invalidación del identificador, aislamiento entre instancias, 10 recorridos sin degradación y conteo de peticiones en 0 |
| Concepto central | `Definicion-Contrato-De-Fachada.md` §3.1, §4.5, §5.1 y §6 |

## 10. Notas y supuestos

- La liberación es explícita: la fachada no adivina cuándo el componente anfitrión dejó de necesitar la instancia. Quién invoca `destruir` y en qué momento del ciclo de vida de su vista es decisión del anfitrión.
- «No degradar» se verifica con el criterio del intake y de NB-00006: diez recorridos de ida y vuelta entre trabajos, con la visualización funcionando igual al final que al principio. La forma de medirlo la fija 08-Calidad-Y-Pruebas.
- `destruir` no borra ni oculta el elemento de dibujo de la página: quién lo creó es el componente anfitrión, y a él le corresponde decidir qué hacer con él.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.1 | 2026-08-29 | **Tramo `R-3c` del renombre `F-03`**, reactivado por el Product Owner el 2026-08-29 y registrado en [`../../../../Producto/Norma-De-Nomenclatura.md`](../../../../Producto/Norma-De-Nomenclatura.md) §8. **3 línea(s)** pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios, ni lo que está entre «…», ni los informes de `Audit/`. **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
| 1.0 | 2026-08-08 | Emisión inicial. Contrato de uso de `destruir`, con tres flujos alternativos, una condición de error y cinco criterios de aceptación, incluido el de los 10 recorridos de ida y vuelta. |
| 1.0 | 2026-08-09 | Absorción de las **dos decisiones del Product Owner** de la **Fase B2**. **Sin subir versión** por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **(a) Sexta función**: **CA-02** suma `establecerMovimiento(id, opciones)` a las invocaciones que sobre un identificador liberado informan `INSTANCIA_DESCONOCIDA` —la sexta función no emite condición nueva— y **CA-03** pasa a decir «las **seis** funciones». **(b) Condiciones de medición**: **CA-04** completa los 10 recorridos **con los dos movimientos automáticos prendidos**, que es el peor caso de la propiedad de liberación de recursos: un bucle de dibujo que sobreviviera a `destruir` es exactamente la degradación que esta función tiene que descartar, y con los movimientos apagados no se ejercitaría (`Especificacion-Funcional.md` §6). |
