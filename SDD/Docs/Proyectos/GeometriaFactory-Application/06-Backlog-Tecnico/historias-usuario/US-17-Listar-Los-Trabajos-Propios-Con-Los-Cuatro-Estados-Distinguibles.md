# US-17 — Listar los trabajos propios con los cuatro estados distinguibles

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-17-Listar-Los-Trabajos-Propios-Con-Los-Cuatro-Estados-Distinguibles.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **resolver el listado de los trabajos de un alumno acotado a los propios y con su estado**, para **que la persona vea en un solo lugar qué entregó, qué le falta y qué le respondieron**.

## 2. Contexto

`NB-03` pide trabajo con dueño, estado y persistencia, y `F-08` del intake §4 lo declara `Must Have`. El contrato de uso es [`CU-06`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-06-Consultar-Los-Trabajos-Propios-Del-Alumno.md). El **predicado de alcance se traslada a la consulta** y no se aplica después de traerla (`05` §6).

## 3. Criterios de aceptación

- Given un alumno con trabajos en los cuatro estados, When se resuelve su listado, Then vienen los cuatro y **cada estado es distinguible**.
- Given un almacén con trabajos de varios alumnos, When se resuelve el listado de uno, Then la consulta **sale ya acotada por dueño** y no se filtra en memoria.
- Given el listado resuelto, When se inspeccionan sus piezas, Then **no vienen los componentes**: el listado usa la proyección y no el detalle.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-03, NB-09 |
| CU cubiertos | CU-06 |
| RN e invariantes que ejerce | RN-03; INV-02 |
| Componente de `05` §3.1 | Orquestación de la consulta, Guarda de autorización |
| Puertos que consume | Repositorio de trabajos |
| Comprobación de `02` §4 que la alcanza | **Pertenencia**, y cambio de contraseña pendiente antes que ella |
| BT derivadas | BT-07, BT-16 |
| Tests previstos en 08 | Prueba de que la consulta sale acotada y de que la colección de componentes no viene materializada |

## 5. Prioridad y estimación

`Must` por derivar de `F-08`, `Must Have`, y porque sin el listado propio el alumno no tiene dónde ver el desenlace de US-18.

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

**Que el listado no arrastre componentes es una decisión que aguas abajo no se puede invertir sin romper un NFR** (`05` §6 y §8). Coincide con la proyección de listado que `GeometriaFactory-Contracts` separó del detalle en su ADR-05, y `GeometriaFactory-Web` la consume sin invertirla.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
