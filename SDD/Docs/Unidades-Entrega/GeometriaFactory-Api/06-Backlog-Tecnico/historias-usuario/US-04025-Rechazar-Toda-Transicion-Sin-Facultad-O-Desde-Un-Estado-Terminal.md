# US-04025 — Rechazar toda transición pedida por quien no tiene la facultad o desde un estado terminal

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-04025-Rechazar-Toda-Transicion-Sin-Facultad-O-Desde-Un-Estado-Terminal.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04006 Desenlace de la entrega
**Etapa del producto:** `h`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que toda transición de desenlace pedida sin facultad o desde un estado terminal se rechace con motivos distinguibles**, para **que forzar la petición contra el servicio no logre nada y para saber cuál de las dos cosas falló**.

## 2. Contexto

`RN-04010` declara el desenlace exclusivo del administrador y los dos estados de cierre terminales. El contrato de uso es [`CU-00029`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00029-Dar-Desenlace-A-La-Revision.md). El criterio de transición `h` → `i…` exige que **un alumno que fuerce la transición contra el servicio de datos sea rechazado**.

## 3. Criterios de aceptación

- Given un solicitante sin el papel `Administrador`, When pide aprobar o rechazar, Then se devuelve el motivo de **facultad requerida** y **el estado no cambia**.
- Given un trabajo en `Finalizado` o en `Rechazado` y un solicitante con la facultad correcta, When pide un desenlace, Then se devuelve el motivo del dominio por **estado terminal**, distinguible del anterior.
- Given los dos rechazos, When se comparan los motivos emitidos, Then **no se colapsan en uno**: la facultad se verifica antes de pedir la transición, precisamente para que se puedan distinguir.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00009 |
| CU cubiertos | CU-04008 |
| RN e invariantes que ejerce | RN-04001, RN-04010; INV-07 |
| Componente de `05` §3.1 | Guarda de autorización, Orquestación del desenlace |
| Puertos que consume | Repositorio de trabajos |
| Comprobación de `02` §4 que la alcanza | **Facultad**, **alcance del administrador** y cambio de contraseña pendiente |
| BT derivadas | BT-04008, BT-04010, BT-04011, BT-04017 |
| Tests previstos en 08 | Matriz comprobación contra prueba de `05` §8, con las dos negativas verificadas sin base de datos |

## 5. Prioridad y estimación

`Must` porque es el criterio de la transición `h` → `i…` que verifica la exclusividad de la facultad, y porque `05` §8 exige **4 de 4** comprobaciones con al menos una prueba que verifique su negativa.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y los puertos que consume
- [x] Declara qué comprobación de `02` §4 la alcanza
- [x] Las condiciones de rechazo que produce existen en el catálogo de las 36 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Se puede verificar con dobles de los cuatro puertos, sin base de datos

## 7. Notas y supuestos

**Esta capa emite una sola negativa de facultad y el dominio declara dos motivos para la misma situación** (`02` §4): esta capa corta con su propia verificación **antes** de invocar al dominio, de modo que ninguno de los dos motivos del dominio llega a producirse. Quien lea las dos capas no debe leer tres negativas donde hay una.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
