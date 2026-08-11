# US-13 — Arrastrar todos los trabajos de una cuenta dada de baja, todo o nada

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** US-13-Arrastrar-Todos-Los-Trabajos-De-Una-Cuenta-Dada-De-Baja.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-03 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que la baja de una cuenta retire la cuenta y todos sus trabajos dentro de la misma unidad de trabajo**, para **que no queden trabajos huérfanos ni cuentas a medio borrar**.

## 2. Contexto

`RN-07` declara que la baja física elimina la cuenta y **todos sus trabajos**. `05` §4 llama a esta operación **el caso testigo** del alcance transaccional de la capa, y `RC-05` la declara como regla conceptual de modelo. El contrato de uso es [`CU-04`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-04-Ejecutar-El-Borrado-Fisico-Y-El-Arrastre-De-La-Baja.md).

## 3. Criterios de aceptación

- Given una cuenta con trabajos en varios estados, When se ejecuta la baja, Then la cuenta y **todos** sus trabajos quedan retirados en la **misma** unidad de trabajo.
- Given esa baja interrumpida a mitad de operación, When se inspecciona el almacén, Then **0** retiros parciales: o se retiró todo, o no se retiró nada.
- Given una cuenta sin trabajos, When se ejecuta la baja, Then procede igual y el arrastre no produce efecto adicional.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01 |
| CU cubiertos | CU-04 |
| RN que ejerce | RN-07, en su mitad de arrastre; RN-04 |
| Componente de `05` §3.1 | Adaptador de repositorio de trabajos, Adaptador de repositorio de cuentas |
| Reglas conceptuales de modelo | `RC-05` |
| ¿Toma alguna decisión de negocio? | **No.** La confirmación escrita es de la capa de aplicación |
| ¿Toca el almacén? | **Sí** |
| BT derivadas | BT-11 |
| Tests previstos en 08 | Prueba de baja con el almacén interrumpido a mitad de operación |

## 5. Prioridad y estimación

`Must` por `RN-07`, y porque `05` §8 fija el NFR de **0** retiros parciales tras una baja interrumpida.

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

**El reseteo no pasa por acá, y es la distinción que el producto vino a cerrar.** `RC-07` declara que la marca **no es un estado de cuenta** y `RN-12` que el reseteo conserva la cuenta y sus trabajos: US-16 es el contraste exacto de esta historia.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
