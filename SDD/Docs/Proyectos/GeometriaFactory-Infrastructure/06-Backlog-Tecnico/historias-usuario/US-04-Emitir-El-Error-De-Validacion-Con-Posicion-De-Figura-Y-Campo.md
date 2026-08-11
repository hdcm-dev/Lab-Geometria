# US-04 — Emitir el error de validación con posición de figura y campo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** US-04-Emitir-El-Error-De-Validacion-Con-Posicion-De-Figura-Y-Campo.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-05 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que cada error de validación traiga la posición de la figura y el campo donde está el problema**, para **que el alumno no tenga que adivinar dónde falla su salida**.

## 2. Contexto

`RN-09` exige que los mensajes de error indiquen **índice de figura y campo, nunca un texto genérico**, y `02` §6 declara que **el tramo principal de esa regla está acá**: es donde el mensaje ubicado se produce. El contrato de uso es [`CU-01`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-01-Interpretar-El-Texto-Original-Y-Reconstruir-Las-Piezas.md).

## 3. Criterios de aceptación

- Given el escenario `E-5`, con un tipo desconocido, When se lo interpreta, Then la observación es de especie **error** y trae el índice de la figura y el campo correspondiente.
- Given el escenario `E-8`, con una dimensión no legible, When se lo interpreta, Then el desenlace es **error y no advertencia**, con su mensaje localizado.
- Given un texto ilegible entero, When se lo interpreta, Then la condición emitida es un **resultado** del contrato y **no la de servicio no disponible**: el alumno no debe creer que hay que esperar a que algo se recupere.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-04, NB-05 |
| CU cubiertos | CU-01 |
| RN que ejerce | RN-09, con tramo principal acá; RN-05 en su parte de insumo |
| Componente de `05` §3.1 | Motor de interpretación de figuras |
| Reglas conceptuales de modelo | `RC-02` |
| ¿Toma alguna decisión de negocio? | **No.** El estado del trabajo lo resuelve el dominio |
| ¿Toca el almacén? | **No** |
| BT derivadas | BT-16, BT-18, BT-21 |
| Tests previstos en 08 | Casos 8 y 10 de la batería, con `E-5` y `E-8` |

## 5. Prioridad y estimación

`Must` por `RN-09`, y porque el criterio de transición `f` → `g` exige que un tipo desconocido produzca error **con índice de figura y campo**.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y, si toca el almacén, las reglas conceptuales de modelo que materializa
- [x] Declara que no toma ninguna decisión de negocio
- [x] Toda condición que produce existe en el catálogo de las 17 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md), declarada como resultado o como fallo
- [x] Declara el camino en que el mecanismo se detiene en lugar de cumplir a medias, cuando puede fallar
- [x] Declara si toca el almacén y, en consecuencia, dónde vive su prueba

## 7. Notas y supuestos

**El escenario `E-8` es el modo de falla más probable de los ocho**, porque lo produce la configuración regional de la máquina y no un error del alumno. El intake 1.12 fijó su desenlace: **error**, con el trabajo en `Borrador`. Y `05` §9 declara con probabilidad **alta** el riesgo de que un texto ilegible devuelva la condición de servicio no disponible, que es el tercer criterio de esta historia.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
