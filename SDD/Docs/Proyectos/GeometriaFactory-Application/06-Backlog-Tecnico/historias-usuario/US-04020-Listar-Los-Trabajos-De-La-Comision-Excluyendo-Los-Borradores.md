# US-04020 — Listar los trabajos de la comisión excluyendo los borradores

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-04020-Listar-Los-Trabajos-De-La-Comision-Excluyendo-Los-Borradores.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04004 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **resolver el listado de la comisión con los trabajos en `Borrador` ya excluidos por la consulta**, para **que el administrador revise la entrega de una sola vez sin ver lo que los alumnos todavía están armando**.

## 2. Contexto

`NB-00007` pide revisión de la comisión desde un solo lugar, `RN-04011` declara que el administrador no ve los borradores y `F-12` del intake §4 lo declara `Must Have`. El contrato de uso es [`CU-04007`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-04007-Revisar-Los-Trabajos-De-La-Comision.md). `05` §6 fija que el predicado de alcance **se traslada a la consulta** y no se aplica después.

## 3. Criterios de aceptación

- Given un solicitante con papel `Administrador` y un almacén con trabajos en los cuatro estados, When se resuelve el listado de la comisión, Then vienen los tres estados que el administrador ve y **ninguno en `Borrador`**.
- Given ese listado, When se inspecciona cómo se excluyó el borrador, Then la consulta **salió ya acotada** y no se filtró en memoria.
- Given un solicitante sin el papel `Administrador`, When pide el listado de la comisión, Then se devuelve el motivo de **facultad requerida** y no se resuelve ninguna consulta.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00007, NB-00009 |
| CU cubiertos | CU-04007 |
| RN e invariantes que ejerce | RN-04001, RN-04011 |
| Componente de `05` §3.1 | Orquestación de la consulta, Guarda de autorización |
| Puertos que consume | Repositorio de trabajos, repositorio de cuentas |
| Comprobación de `02` §4 que la alcanza | **Facultad** y **alcance del administrador**, y cambio de contraseña pendiente antes que las dos |
| BT derivadas | BT-04007, BT-04010, BT-04016 |
| Tests previstos en 08 | Prueba sobre un alumno con un borrador y un pendiente, comprobando que sólo viene el pendiente |

## 5. Prioridad y estimación

`Must` por derivar de `F-12`, `Must Have`, y porque el criterio de transición `e` → `f` exige que el listado del administrador **no incluya los que están en estado `Borrador`**.

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

**La agrupación, el orden y el filtro tal como la persona los ejerce son decisiones de presentación de `GeometriaFactory-Web`** (`02` §7.2). Lo que esta capa entrega es la colección con el recorte ya aplicado y el dato de dueño con el que agrupar.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
