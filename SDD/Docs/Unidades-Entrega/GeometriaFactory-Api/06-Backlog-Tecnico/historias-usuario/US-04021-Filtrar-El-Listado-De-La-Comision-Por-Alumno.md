# US-04021 — Filtrar el listado de la comisión por alumno, con el recorte vigente

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-04021-Filtrar-El-Listado-De-La-Comision-Por-Alumno.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04004 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el listado de la comisión admita acotarse a un alumno sin que el recorte de borradores deje de aplicarse**, para **que el administrador mire la entrega de una persona sin que se le cuele nada que no le corresponde ver**.

## 2. Contexto

`F-12` del intake §4 declara `Must Have` el listado del administrador **con agrupación y filtro por alumno**. El contrato de uso es [`CU-04007`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-04007-Revisar-Los-Trabajos-De-La-Comision.md). El riesgo de esta historia es concreto: un filtro que se arme del lado del consumidor podría reemplazar el predicado de alcance en lugar de sumarse a él.

## 3. Criterios de aceptación

- Given un solicitante con papel `Administrador` y un alumno indicado, When se resuelve el listado filtrado, Then vienen sólo los trabajos de ese alumno **y ninguno en `Borrador`**: el filtro **se suma** al recorte y no lo reemplaza.
- Given un alumno cuyo único trabajo está en `Borrador`, When se filtra por él, Then el resultado es una colección vacía y **no** un fallo: vacío y fallo son cosas distintas.
- Given un solicitante sin el papel `Administrador`, When pide el listado filtrado, Then se devuelve el motivo de facultad requerida.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00007 |
| CU cubiertos | CU-04007 |
| RN e invariantes que ejerce | RN-04001, RN-04011 |
| Componente de `05` §3.1 | Orquestación de la consulta, Guarda de autorización |
| Puertos que consume | Repositorio de trabajos, repositorio de cuentas |
| Comprobación de `02` §4 que la alcanza | Facultad y alcance del administrador, y cambio de contraseña pendiente antes que las dos |
| BT derivadas | BT-04010, BT-04016 |
| Tests previstos en 08 | Prueba de filtro sobre un alumno con un borrador y un pendiente |

## 5. Prioridad y estimación

`Must` por derivar de `F-12`, `Must Have`, y porque el criterio de transición `e` → `f` exige que el administrador vea los trabajos **agrupados y filtrados por alumno**.

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

**El panel de resumen por alumno y por estado no es esta historia**: es `F-15`, `Could Have`, de la fase `i…`, y no está en este backlog.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
