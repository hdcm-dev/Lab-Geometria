# CU-09 — Proveer el sello del reloj del sistema

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** CU-09-Proveer-El-Sello-Del-Reloj-Del-Sistema.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §17.2.P.11 punto 3 (el reloj es un puerto, para que las fechas de alta y de modificación sean verificables en prueba) y §17.3.P.4 (ampliación del 2026-08-09, sellos de tiempo del trabajo); implementa el puerto de reloj del sistema de `Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/Especificacion-Funcional.md` §3
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Infrastructure

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

Devolver el momento actual, que es el **sello** con el que la capa de aplicación registra el alta, la modificación y el desenlace.

Es el contrato más corto de este proyecto de código y el que mejor explica por qué existe la capa. Que el reloj sea un puerto es una decisión pre-tomada del producto, con un motivo declarado: **para que esos sellos sean verificables en prueba**. Sin él, un criterio de aceptación que exige un sello concreto no se puede escribir sin trucos; con él se escribe en una línea, porque la prueba fija el valor.

Lo que este caso de uso **no** hace: no interpreta el sello, no lo guarda y **no lo mezcla con la `Fecha` que el alumno declara en su trabajo**, que es un dato que él escribe y que este contrato nunca produce.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor del puerto de reloj (`GeometriaFactory-Application`) | Primario | Pide el sello para registrarlo |
| Reloj del anfitrión | Sistema | La única fuente del valor |

## 3. Precondiciones

Ninguna. Es el único contrato de este proyecto de código sin precondiciones: no necesita almacén, ni configuración, ni secreto, ni entrada.

## 4. Flujo principal

1. El consumidor pide el momento actual.
2. Se devuelve, tomado del reloj del anfitrión.

## 5. Flujos alternativos

Ninguno. **No tiene ramas y es deliberado**: cualquier decisión sobre el sello —cuál de los tres es, si se registra, con qué se compara— es del consumidor.

## 6. Excepciones y errores

**Este contrato no declara ninguna condición de error, y se declara así en vez de dejar la sección vacía.** No recibe entrada que pueda ser inválida, no toca el almacén, no consume secretos y no depende de nada que pueda no responder. La única forma de que falle es que falle el proceso entero, y eso no es una condición de este contrato.

Es también el motivo por el que **no aparece en el catálogo de condiciones de la categoría 03**: su ausencia allá está declarada y no es un olvido.

## 7. Postcondiciones

- **Éxito:** el consumidor recibe el momento actual. **Nada cambió en ninguna parte**: este contrato no tiene efecto observable más allá de su valor de retorno.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | El adaptador real | Se pide el momento actual dos veces seguidas | Los dos valores son **no decrecientes**: el segundo no es anterior al primero |
| CA-02 | Un doble de este contrato fijado en un momento concreto | Se ejerce un caso de uso de la capa de aplicación que registra el sello de alta | El sello registrado es **exactamente el fijado**. Es la propiedad entera por la que el reloj es un puerto |
| CA-03 | Cualquier caso de uso de la capa de aplicación que registre un sello | Se inspecciona su implementación | **El sello llega por este puerto y no se toma del ambiente**, aunque parezca ceremonia para una línea |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | Ninguna propia. Es un mecanismo transversal que sostiene la verificabilidad de los sellos de NB-01 y NB-03; su ausencia de la matriz está declarada en `Especificacion-Funcional.md` §7.2 |
| Reglas de negocio aplicables | Ninguna. Ninguna de las quince enuncia el momento actual |
| Puerto que implementa | Reloj del sistema |
| Consumidor | `GeometriaFactory-Application`, sus CU-01, CU-03, CU-04, CU-05, CU-08, CU-10 y CU-11 |
| Historias de usuario a generar en 06 | US-23 |
| Componentes esperados en 05 | Adaptador del reloj del sistema |
| Tests previstos en 08 | Una unitaria de monotonía sobre el adaptador real. **Las pruebas que importan no son de este contrato sino de sus consumidores**, que lo reemplazan por un doble fijado |

## 10. Notas y supuestos

- **Los tres sellos —alta, modificación y desenlace— son metadatos de orquestación de la capa de aplicación**, no atributos que el modelo del dominio declare. La discrepancia está elevada al Product Owner por `GeometriaFactory-Domain` y esta categoría **no la reabre**. Lo que sí quedó resuelto aguas arriba son los **dos sellos del trabajo** —creación y última modificación—, que el intake incorpora al modelo de datos en su §17.3.P.4 con rótulo de decisión del Product Owner, y que `RC-06` recoge.
- **La zona horaria y la precisión del sello no están declaradas por ninguna fuente** y son de `05-Arquitectura-Tecnica`. Esta categoría no las fija y las anota como punto abierto.
- **El `JSON` del alumno no lleva fechas y no se le agrega ninguna.** Los sellos viven en la fila del trabajo, nunca dentro del texto conservado.
- **Que este contrato sea trivial es la prueba de que la inversión está bien hecha.** Si alguna vez tuviera lógica —redondeos, zonas, cachés—, sería señal de que una decisión del consumidor se filtró acá.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | Actualización de la cita del `PRODUCT-INTAKE` de **1.11** a **1.12** en la trazabilidad upstream: 1.11 quedó archivada al resolver el Product Owner el desenlace del envío del escenario `E-8`. Corrige el hallazgo **H-02** del informe de auditoría `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` (ronda 1). El delta entre 1.11 y 1.12 se revisó y sólo alcanza a `E-8`, que no toca lo que este documento declara: sin cambios de contenido. |

## 17. Compatibilidad de la superficie pública

Este contrato no tiene margen de crecimiento y no lo necesita. **Devolver un valor que no sea el momento actual, agregarle lógica de redondeo o de zona, o cachearlo son cambios incompatibles** y suben versión mayor: los tres convierten en decisión de esta capa algo que pertenece al consumidor, y el último rompe además la monotonía de CA-01.
