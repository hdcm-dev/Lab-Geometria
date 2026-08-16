# US-06012 — Retirar físicamente un trabajo con todo lo que cuelga de él

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** US-06012-Retirar-Fisicamente-Un-Trabajo-Con-Todo-Lo-Que-Cuelga-De-El.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06004 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que retirar un trabajo lo elimine físicamente junto con sus piezas, sus componentes y sus observaciones**, para **que no queden restos de un trabajo que ya no existe**.

## 2. Contexto

`RN-06004` declara **borrado físico**. `02` §5 llama a este contrato de uso **la única operación destructiva del producto**, y `02` §8 declara por qué se separó del guardado: lo que hay que poder verificar del retiro es que **no queda nada**, y eso no es un caso más de la materialización. El contrato de uso es [`CU-06004`](../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06004-Ejecutar-El-Borrado-Fisico-Y-El-Arrastre-De-La-Baja.md).

## 3. Criterios de aceptación

- Given un trabajo con piezas, componentes y observaciones, When se lo retira, Then **no queda nada** de él en el almacén.
- Given ese retiro, When se busca una marca de borrado lógico, Then **no hay ninguna**: el borrado es físico.
- Given un retiro interrumpido a mitad de operación, When se inspecciona el resultado, Then **no queda ningún retiro parcial**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00009 |
| CU cubiertos | CU-06004 |
| RN que ejerce | RN-06004, en su mitad de borrado físico |
| Componente de `05` §3.1 | Adaptador de repositorio de trabajos |
| Reglas conceptuales de modelo | `RC-06005`, retiro físico con arrastre |
| ¿Toma alguna decisión de negocio? | **No.** La acotación por estado y por papel es de la capa de aplicación |
| ¿Toca el almacén? | **Sí** |
| BT derivadas | BT-06011 |
| Tests previstos en 08 | Prueba de retiro con verificación de que no queda nada |

## 5. Prioridad y estimación

`Must` por `RN-06004` y porque el criterio de transición `h` → `i…` exige que el administrador elimine un trabajo en estado `Pendiente` y **el trabajo desaparezca**.

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

**Quién puede retirar y en qué estado no se decide acá.** El alumno sólo en `Borrador` y el administrador en todo lo que ve: las dos acotaciones son de `GeometriaFactory-Application`, y esta capa ejecuta el retiro sobre lo que ya llegó acotado.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
