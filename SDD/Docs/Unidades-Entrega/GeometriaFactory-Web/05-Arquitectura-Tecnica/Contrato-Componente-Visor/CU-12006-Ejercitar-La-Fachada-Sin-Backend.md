# CU-12006 — Ejercitar la fachada completa sin backend

**Unidad de entrega:** GeometriaFactory-Web
**Documento:** CU-12006-Ejercitar-La-Fachada-Sin-Backend.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `NB-00006-Visualizacion-Dentro-Del-Producto.md` §5 (segundo, tercero, cuarto y quinto criterios); `00-Contexto/Vision-Producto.md` §3 (diferenciadores D-3 y D-4) y §9 (fallo silencioso); `00-Contexto/Alcance-Producto.md` §4.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §14 (RA-01 y RA-02), §16.1 (materialización de `/samples`), §4 (capacidad F-25), §17.7 P.3, P.4, P.5, P.6, P.10 y P.11, §18 (sample S-1 y punto de extensión), §20 E-7
**Trazabilidad downstream:** 03-UX-UI-DX, 05-Arquitectura-Tecnica, 06-Backlog-Tecnico, 08-Calidad-Y-Pruebas, 10-Examples (materializa el sample S-1)

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

Permitir que un componente anfitrión mínimo —una página integradora sin ninguna pieza del backend— recorra las **seis** funciones de la fachada de punta a punta con un texto que alguien pegó a mano. Es el caso de uso transversal del contrato: verifica juntas las **seis** propiedades que ninguna función sostiene sola —cero red, cero persistencia, se ejercita sin backend, disposición determinista, liberación de recursos y ausencia de fallo silencioso, con su umbral y sus condiciones de medición en `Especificacion-Funcional.md` §6— y es lo que hace reemplazable al motor de dibujo.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Componente anfitrión mínimo | Primario | Página integradora que carga el archivo de guion, ofrece un campo de texto y un elemento de dibujo, e invoca las seis funciones. No tiene backend, ni sesión, ni configuración |
| Fachada del visor | Sistema | Atiende las seis funciones exactamente igual que con cualquier otro componente anfitrión |
| Texto del trabajo | Secundario | Texto pegado a mano en el campo de la página integradora |

## 3. Precondiciones

1. El archivo de guion generado está disponible como recurso estático junto a la página integradora.
2. El navegador provee la capacidad gráfica tridimensional.
3. **No hay ningún servicio del backend disponible ni alcanzable**, y eso no impide ejecutar el caso de uso: es precisamente lo que se verifica.
4. La pestaña de red del navegador está abierta y sin peticiones registradas al comenzar.

## 4. Flujo principal

| Paso | Actor | Acción |
| --- | --- | --- |
| 1 | Componente anfitrión mínimo | Abre la página integradora, que carga el archivo de guion desde el mismo lugar donde está la página |
| 2 | Componente anfitrión mínimo | Invoca `inicializar(elemento, opciones)` sobre su elemento de dibujo y conserva el identificador |
| 3 | Componente anfitrión mínimo | Pega a mano el texto de un trabajo en su campo de texto e invoca `cargarJson(id, texto)` |
| 4 | Fachada del visor | Dibuja las piezas dibujables, las ubica por índice y devuelve el resultado de dibujo con la estructura del texto |
| 5 | Componente anfitrión mínimo | Presenta la estructura como árbol e invoca `seleccionarPieza(id, indice)` con el índice de un elemento del árbol |
| 6 | Componente anfitrión mínimo | Cambia el tamaño del elemento de dibujo e invoca `redimensionar(id)` |
| 7 | Componente anfitrión mínimo | Invoca `establecerMovimiento(id, opciones)` prendiendo los dos movimientos automáticos, y verifica que la pieza resaltada en el paso 5 sigue resaltada y que la disposición no cambió |
| 8 | Componente anfitrión mínimo | Invoca `destruir(id)` y verifica que el identificador dejó de ser válido |
| 9 | Componente anfitrión mínimo | Revisa la pestaña de red y el almacenamiento del navegador: ninguna petición originada por la fachada **durante todo el recorrido, con los dos movimientos corriendo**, y ninguna clave escrita |

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 · Recorrido repetido para verificar determinismo | Quien ejecuta el recorrido quiere comparar dos procesados del mismo texto | Repite los pasos 2 a 4 con el mismo texto en una instancia nueva y compara las dos disposiciones, que resultan idénticas | Paso 5 del flujo principal |
| FA-02 · Recorrido repetido para verificar continuidad | Quien ejecuta el recorrido quiere verificar que recorrer trabajos no degrada | Repite los pasos 2 a 8 completos diez veces, alternando entre dos textos y **con los dos movimientos prendidos**, que es el peor caso de la propiedad de liberación de recursos | Paso 9 del flujo principal |
| FA-03 · Texto con piezas de tipo no dibujable | El texto pegado incluye una pieza cuyo tipo no está entre los seis dibujables | El resultado de dibujo la enumera con su índice y la página integradora la muestra como no dibujada: no hay fallo silencioso | Paso 5 del flujo principal |
| FA-04 · Sin acceso a servicios externos de terceros | La página se abre en un entorno que no alcanza ningún servicio externo | El recorrido completo funciona igual: el motor de dibujo viaja dentro del archivo de guion y no se pide desde afuera | Paso 2 del flujo principal |

## 6. Excepciones y errores

| Código | Causa | Respuesta del sistema |
| --- | --- | --- |
| `GRAPHICS_CAPABILITY_MISSING` | El navegador donde se abre la página integradora no provee la capacidad gráfica tridimensional | El recorrido se detiene en el paso 2 sin instancia creada. La combinación está declarada no soportada; la página integradora informa la condición y termina de forma controlada |
| `UNREADABLE_TEXT` | Se pega un texto del que no se puede obtener un conjunto de piezas | La instancia queda viva y vacía; el recorrido continúa pegando otro texto. La fachada no emite advertencias ni errores de validación: no le corresponde |
| `INDEX_OUT_OF_RANGE` | Se pide resaltar un índice que no está en el resultado de dibujo vigente | La selección vigente se conserva y el recorrido continúa |
| Petición de red observada | La pestaña de red registra una petición originada por la fachada | **El caso de uso falla.** El umbral es exactamente 0 y no admite excepción: una sola petición viola RA-02 y reabre lo que RA-01 sostiene. La medición vale **con los dos movimientos prendidos** (`Especificacion-Funcional.md` §6) |

## 7. Postcondiciones

- **Éxito:** las seis funciones se ejercieron sobre un texto pegado a mano, sin ninguna pieza del backend; la pestaña de red registra 0 peticiones originadas por la fachada; el almacenamiento del navegador no tiene claves de la fachada; la instancia quedó liberada y su identificador inválido.
- **Fallo:** el recorrido queda detenido en el paso donde se produjo la condición, con la instancia viva o inexistente según el caso, y sin ningún estado escrito fuera de la página.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una página integradora con el archivo de guion, sin ningún servicio del backend disponible, y el texto del escenario E-7 pegado a mano | Se recorren `inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `establecerMovimiento` y `destruir` en ese orden | Las **seis** funciones responden, se dibujan **6 piezas** con los índices 0 a 5 y el recorrido termina con la instancia liberada |
| CA-02 | El mismo recorrido de CA-01, con la pestaña de red abierta y vacía y **los dos movimientos automáticos prendidos** con `establecerMovimiento` | Se completa el recorrido entero con los dos movimientos corriendo durante al menos 60 segundos, incluidos los gestos de rotar y acercar sobre la escena | La pestaña de red registra exactamente **0 peticiones** originadas por la fachada, durante el recorrido, durante el movimiento automático y durante los gestos. **Es la condición de medición declarada** en `Especificacion-Funcional.md` §6: con los movimientos apagados la prueba no ejercitaría el bucle de dibujo |
| CA-03 | El mismo recorrido de CA-01, con el almacenamiento del navegador sin claves de la fachada | Se completa el recorrido entero prendiendo y apagando los dos movimientos, y se recarga la página | El almacenamiento del navegador sigue sin claves de la fachada y la página vuelve a arrancar sin ninguna pieza dibujada y **con los dos movimientos apagados**: la preferencia no se conservó, porque no es de la fachada conservarla |
| CA-04 | Una página integradora y el texto del escenario E-1, con tres piezas | Se carga el texto en dos instancias sucesivas y se comparan las disposiciones | Las dos disposiciones son **idénticas** pieza por pieza, y las dos dibujan 3 de 3 piezas, ortoedro incluido |
| CA-05 | Una página integradora que alterna entre el texto de E-1 y el de E-7, **con los dos movimientos automáticos prendidos en cada recorrido** | Se completan **10 recorridos** de crear, cargar, seleccionar, redimensionar, gobernar el movimiento y destruir | Los 10 recorridos terminan con todas sus piezas dibujadas y la visualización no degrada: ningún bucle de dibujo sobrevive a `destruir` |
| CA-06 | Una página integradora abierta en un entorno sin acceso a ningún servicio externo de terceros, con el texto del escenario E-7 | Se recorren las seis funciones | El recorrido funciona completo: el motor de dibujo viaja dentro del archivo de guion y no se pide desde afuera |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-00006, criterios segundo (6 tipos de 6), tercero (10 recorridos de 10), cuarto (disposición estable) y quinto (sincronización por índice) |
| Reglas de negocio aplicables | Ninguna. Este proyecto de código no declara RN (ver `README.md` de la sección) |
| Historias de usuario a generar | US de la página integradora sin backend y de la verificación de cero red y cero persistencia, en 06-Backlog-Tecnico |
| Componentes esperados | El archivo de guion generado y la página integradora del sample; 05-Arquitectura-Tecnica fija la composición y 09-Devops la generación del artefacto |
| Tests previstos | 08-Calidad-Y-Pruebas: recorrido completo sin backend con E-7, conteo de peticiones en 0, ausencia de claves de almacenamiento, dos procesados con disposición idéntica y 10 recorridos sin degradación |
| Ejemplos | 10-Examples: este caso de uso es el que materializa el sample **S-1** del intake |
| Concepto central | `Definicion-Contrato-De-Fachada.md` §3.2, §3.3, §4 completo —incluida §4.6—, §5.5 y §6 |

## 10. Notas y supuestos

- Este caso de uso **no agrega ninguna función** a la fachada: recorre las seis que el contrato declara. La sexta, `establecerMovimiento`, la agregó el Product Owner el 2026-08-09 y tiene su propio contrato de uso en `CU-12007`. Este caso existe como caso de uso propio porque las propiedades que verifica son transversales y, si se repartieran como excepciones de los otros seis, ninguno las verificaría juntas.
- El actor primario sigue siendo un componente que embebe el archivo de guion. Que sea una página mínima en vez del componente del producto es exactamente el punto: la fachada no distingue quién la invoca.
- La propiedad de ejercitarse sin backend no es un agregado de conveniencia. Es la propiedad que hoy tiene la página de visualización previa y que el intake exige no perder, y es la que demuestra el punto de extensión del producto.
- El texto pegado a mano no sale de ningún servicio: lo pega una persona en el campo de la página. La fachada no lo pide, no lo guarda y no lo reescribe.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.1 | 2026-08-29 | **Tramo `R-3c` del renombre `F-03`**, reactivado por el Product Owner el 2026-08-29 y registrado en [`../../../../Producto/Norma-De-Nomenclatura.md`](../../../../Producto/Norma-De-Nomenclatura.md) §8. **3 línea(s)** pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios, ni lo que está entre «…», ni los informes de `Audit/`. **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
| 1.0 | 2026-08-08 | Emisión inicial. Caso de uso transversal que recorre las cinco funciones desde una página integradora sin backend, con cuatro flujos alternativos, cuatro condiciones de error —incluida la petición de red observada, que hace fallar el caso— y seis criterios de aceptación. |
| 1.0 | 2026-08-08 | Corrección absorbida del audit `B-02-03-GeometriaFactory-Visor-r1.md`, sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-02**: §1 pasa a nombrar las **seis** propiedades transversales, con la misma membresía que `Especificacion-Funcional.md` §6 y `Definicion-Contrato-De-Fachada.md` §4.6, y remite a §6 como lugar único de los umbrales. |
| 1.0 | 2026-08-09 | Absorción de las **dos decisiones del Product Owner** de la **Fase B2**. **Sin subir versión** por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **(a) Sexta función**: el recorrido pasa de cinco a **seis** funciones —§1, §2, §7 y CA-01 y CA-06—, y el flujo principal suma el **paso 7**, que invoca `establecerMovimiento` prendiendo los dos movimientos y verifica que la selección y la disposición no se pierden; los pasos siguientes se renumeran a 8 y 9. §10 deja de decir que este caso «no agrega una sexta función» y remite a `CU-12007`. **(b) Condiciones de medición**: **CA-02** pasa a medir las 0 peticiones **con los dos movimientos prendidos** durante al menos 60 segundos, que es el peor caso declarado en `Especificacion-Funcional.md` §6; **CA-05** y **FA-02** completan los 10 recorridos con los movimientos prendidos, para que ningún bucle de dibujo que sobreviva a `destruir` pase inadvertido; **CA-03** verifica además que la preferencia de movimiento **no se conserva** al recargar. Ningún código de condición nuevo. |
