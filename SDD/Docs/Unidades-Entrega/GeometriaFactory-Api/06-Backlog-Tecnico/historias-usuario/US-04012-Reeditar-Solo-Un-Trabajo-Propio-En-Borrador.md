# US-04012 — Reeditar sólo un trabajo propio en `Borrador`, descartando la interpretación anterior

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-04012-Reeditar-Solo-Un-Trabajo-Propio-En-Borrador.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04004 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que la reedición proceda sólo sobre un trabajo propio y sólo en estado `Borrador`, descartando la interpretación anterior**, para **que el alumno corrija lo que todavía no entregó sin arrastrar piezas ni observaciones viejas**.

## 2. Contexto

`RN-04003` y `RN-04004` acotan lo que el alumno reedita y elimina. El contrato de uso es [`CU-00026`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md). La verificación de **pertenencia** es la segunda comprobación de `02` §4, y su negativa oculta la existencia del recurso.

## 3. Criterios de aceptación

- Given un trabajo propio en estado `Borrador`, When se lo reedita, Then el texto se reemplaza y la interpretación anterior —piezas y observaciones— **queda descartada entera**.
- Given un trabajo de otro alumno, When el solicitante pide reeditarlo, Then se devuelve el motivo de **inexistencia para el solicitante**, el mismo que produce un identificador que no existe, y **nunca** un motivo de falta de autorización.
- Given un trabajo propio en un estado distinto de `Borrador`, When se pide reeditarlo, Then se rechaza con su motivo y nada cambia.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003 |
| CU cubiertos | CU-04004 |
| RN e invariantes que ejerce | RN-04003, RN-04004, RN-04008; INV-02, INV-03 |
| Componente de `05` §3.1 | Orquestación del trabajo, Guarda de autorización |
| Puertos que consume | Repositorio de trabajos, reloj del sistema |
| Comprobación de `02` §4 que la alcanza | **Pertenencia**, y cambio de contraseña pendiente antes que ella |
| BT derivadas | BT-04009, BT-04010, BT-04015 |
| Tests previstos en 08 | Prueba que pide el trabajo de otro y compara el motivo emitido |

## 5. Prioridad y estimación

`Must` por derivar de `F-07`, `Must Have`, y porque el criterio de transición `e` → `f` exige que un alumno que pide el trabajo de otro reciba «no encontrado».

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

**Que el trabajo ajeno y el identificador inexistente compartan motivo es deliberado**: distinguirlos permitiría averiguar por tanteo qué identificadores existen (`02` §4, precisión 4). `05` §9 declara este error de lectura como riesgo de impacto alto, y la categoría 03 lo llama «el error más caro que un consumidor puede cometer contra esta capa».

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
