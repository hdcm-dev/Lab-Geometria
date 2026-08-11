# CU-04 — Redimensionar la escena al elemento de dibujo

**Proyecto de código:** GeometriaFactory-Visor
**Documento:** CU-04-Redimensionar-La-Escena.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `NB-06-Visualizacion-Dentro-Del-Producto.md` §1 (descripción de la necesidad) y §5 (criterios de éxito); `00-Contexto/Vision-Producto.md` §3 (diferenciador D-4); `00-Contexto/Compatibilidad-Plataformas.md` §2.2 (plataforma del navegador) y §4 (alternativas para plataformas no soportadas); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §17.7 P.3, §17.7 P.10 (interacción fluida sin tráfico durante el gesto), §14 (RA-02)
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

Permitir que el componente anfitrión le avise a una instancia viva que el elemento de dibujo cambió de tamaño, para que la escena recalcule su relación de aspecto y las piezas no queden deformadas ni fuera de encuadre. La fachada no vigila el tamaño por su cuenta: quien sabe cuándo cambió el espacio disponible es el anfitrión.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Componente anfitrión | Primario | Detecta que el elemento de dibujo cambió de tamaño e invoca `redimensionar` |
| Fachada del visor | Sistema | Recalcula la relación de aspecto contra el tamaño vigente del elemento de dibujo y confirma el ajuste |

## 3. Precondiciones

1. Existe una instancia viva y el componente anfitrión tiene su identificador (`CU-01`).
2. El elemento de dibujo sigue presente en la página y tiene tamaño distinto de cero.

## 4. Flujo principal

| Paso | Actor | Acción |
| --- | --- | --- |
| 1 | Componente anfitrión | Invoca `redimensionar(id)` con el identificador de una instancia viva |
| 2 | Fachada del visor | Lee el tamaño vigente del elemento de dibujo de esa instancia |
| 3 | Fachada del visor | Recalcula la relación de aspecto de la escena contra ese tamaño |
| 4 | Fachada del visor | Conserva la disposición de las piezas, la selección vigente y la orientación de la cámara |
| 5 | Fachada del visor | Confirma que la escena quedó ajustada |

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 · Instancia sin trabajo cargado | Se invoca sobre una instancia viva que todavía no cargó ningún texto | La escena vacía se ajusta igual: la operación no depende de que haya piezas dibujadas | Paso 5 del flujo principal |
| FA-02 · Invocación sin cambio de tamaño | El componente anfitrión invoca aunque el tamaño no cambió | La operación es idempotente: el ajuste vuelve a dar el mismo encuadre y nada se mueve | Paso 5 del flujo principal |
| FA-03 · Invocaciones sucesivas durante un cambio de tamaño continuo | El componente anfitrión invoca varias veces seguidas mientras el elemento de dibujo cambia de tamaño | Cada invocación ajusta contra el tamaño vigente en ese momento; la última invocación deja el encuadre definitivo | Paso 2 del flujo principal |

## 6. Excepciones y errores

| Código | Causa | Respuesta de la fachada |
| --- | --- | --- |
| `INSTANCIA_DESCONOCIDA` | El identificador no corresponde a ninguna instancia viva, o corresponde a una ya liberada | Ninguna instancia cambia y se informa el código. Es la condición esperable cuando el anfitrión avisa un cambio de tamaño después de haber destruido la instancia |
| `ELEMENTO_DE_DIBUJO_INVALIDO` | El elemento de dibujo de la instancia pasó a tener tamaño cero, por ejemplo porque quedó oculto | No se recalcula nada, la instancia sigue viva con su escena y su selección, y se informa el código. Cuando el elemento vuelva a tener tamaño, una invocación nueva ajusta |

## 7. Postcondiciones

- **Éxito:** la relación de aspecto de la escena corresponde al tamaño vigente del elemento de dibujo; las piezas conservan su disposición y su proporción; la selección vigente no cambió; hubo 0 peticiones de red.
- **Fallo:** la instancia y su escena quedan exactamente como estaban.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una instancia viva sobre un elemento de dibujo de 800 × 600, con el texto del escenario E-1 cargado y sus tres piezas dibujadas | El elemento de dibujo pasa a 400 × 600 y el componente anfitrión invoca `redimensionar(id)` | La escena se ajusta a 400 × 600, las tres piezas conservan su proporción —ninguna queda achatada— y siguen dentro del encuadre |
| CA-02 | La misma instancia, con la pieza de índice 2 resaltada | El componente anfitrión invoca `redimensionar(id)` | La pieza de índice 2 sigue siendo la única resaltada y la disposición de las tres piezas no cambia |
| CA-03 | Una instancia viva cuyo elemento de dibujo mide 800 × 600 y no cambió de tamaño | El componente anfitrión invoca `redimensionar(id)` tres veces seguidas | El encuadre resultante es el mismo después de cada invocación: la operación es idempotente |
| CA-04 | Una instancia ya liberada con `destruir` | El componente anfitrión invoca `redimensionar(id)` con ese identificador | La fachada informa `INSTANCIA_DESCONOCIDA` y ninguna otra instancia viva se altera |
| CA-05 | Una instancia viva con el texto del escenario E-7 cargado y la pestaña de red vacía | El componente anfitrión invoca `redimensionar(id)` después de llevar el elemento de dibujo a 1200 × 400 | La escena se ajusta y la pestaña de red registra exactamente 0 peticiones originadas por la fachada |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-06 |
| Reglas de negocio aplicables | Ninguna. Este proyecto de código no declara RN (ver `README.md` de la sección) |
| Historias de usuario a generar | US de ajuste de la escena al espacio disponible, en 06-Backlog-Tecnico |
| Componentes esperados | Fachada plana y servicio de dibujo, con el manejo de cámara y encuadre; 05-Arquitectura-Tecnica fija la composición |
| Tests previstos | 08-Calidad-Y-Pruebas: ajuste con proporción conservada, conservación de la selección, idempotencia, identificador liberado y conteo de peticiones en 0 |
| Concepto central | `Definicion-Contrato-De-Fachada.md` §4.4 y §6 |

## 10. Notas y supuestos

- La fachada **no observa** el tamaño del elemento de dibujo ni decide cuándo hay que ajustar. Quién detecta el cambio y con qué mecanismo es decisión del componente anfitrión y de 03-UX-UI-DX.
- Rotar y acercar con el mouse son gestos que la instancia atiende por su cuenta sobre la escena ya creada, sin invocación de la fachada y **sin tráfico de circuito durante el gesto**; no constituyen un caso de uso del contrato porque no atraviesan ninguna de las seis funciones. Distinto es el **movimiento automático** de la escena, que sí se gobierna por la fachada y tiene caso de uso propio (`CU-07`).
- Este caso de uso no redibuja el trabajo ni vuelve a leer el texto: el contenido de la escena es el que dejó la última carga.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Contrato de uso de `redimensionar`, con tres flujos alternativos, dos condiciones de error y cinco criterios de aceptación con medidas concretas del elemento de dibujo. |
| 1.0 | 2026-08-08 | Corrección absorbida del audit `B-02-03-GeometriaFactory-Visor-r1.md`, sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-10**: la cabecera sustituye la referencia a `Compatibilidad-Plataformas.md` sin sección por §2.2 y §4, y completa con su sección concreta la referencia a `NB-06`. §6 no se modifica: el audit lo declara correcto, y el defecto de `ELEMENTO_DE_DIBUJO_INVALIDO` (H-01) se corrigió en `Definicion-Contrato-De-Fachada.md` §6, que ahora declara este curso como C-2. |
| 1.0 | 2026-08-09 | Absorción de la **Fase B2**, por la decisión del Product Owner de agregar una **sexta función** a la fachada. **Sin subir versión** por `Master-Prompt.md` §5 (documento en estado `Propuesto`). §10 pasa a decir «ninguna de las **seis** funciones» y distingue los gestos de la persona —que no atraviesan la fachada— del movimiento automático, que sí se gobierna por ella y tiene su contrato de uso en `CU-07`. El contrato de `redimensionar` no cambia: ajustar la escena es inocuo respecto del estado de los movimientos. |
