# US-04023 — Aprobar un trabajo en estado `Pendiente`, con comentario opcional

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04023-Aprobar-Un-Trabajo-En-Estado-Pendiente-Con-Comentario-Opcional.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04006 Desenlace de la entrega
**Etapa del producto:** `h`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **orquestar la aprobación de un trabajo en estado `Pendiente`, con un comentario opcional**, para **que la entrega tenga un desenlace explícito y no quede sólo depositada**.

## 2. Contexto

`NB-00009` pide desenlace explícito, `RN-04010` lo declara exclusivo del administrador y terminal, y `F-23` del intake §4 lo declara `Must Have`. El contrato de uso es [`CU-00029`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00029-Dar-Desenlace-A-La-Revision.md).

## 3. Criterios de aceptación

- Given un trabajo en estado `Pendiente` y un solicitante con papel `Administrador`, When se aprueba, Then el trabajo queda en `Finalizado` y el estado es **terminal**.
- Given esa misma aprobación **sin** comentario, When se resuelve, Then procede igual: el comentario es opcional en los dos desenlaces.
- Given un trabajo ya en `Finalizado`, When se pide aprobarlo de nuevo, Then se devuelve el motivo del dominio por estado terminal y **el contenido no cambia**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00009 |
| CU cubiertos | CU-04008 |
| RN e invariantes que ejerce | RN-04010, RN-04011; INV-07 |
| Componente de `05` §3.1 | Orquestación del desenlace, Guarda de autorización |
| Puertos que consume | Repositorio de trabajos, reloj del sistema |
| Comprobación de `02` §4 que la alcanza | **Facultad** y **alcance del administrador**, y cambio de contraseña pendiente antes que las dos |
| BT derivadas | BT-04009, BT-04010, BT-04017 |
| Tests previstos en 08 | Prueba de aprobación con y sin comentario, y de segunda aprobación rechazada |

## 5. Prioridad y estimación

`Must` por derivar de `F-23` y `F-21`, `Must Have`, y porque el criterio de transición `h` → `i…` exige que el administrador apruebe un trabajo en estado `Pendiente` y quede en `Finalizado`.

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

**La facultad se verifica antes de pedir la transición al dominio** (`05` §10.3 `INV-07`), para que el rechazo por facultad no se confunda con el rechazo por terminalidad. Es también lo que hace que esta capa emita **una sola** negativa de facultad donde el dominio declara dos motivos (`02` §4).

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
