# US-04022 — Abrir el detalle de un trabajo de la comisión con los mismos elementos que ve el alumno

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04022-Abrir-El-Detalle-De-Un-Trabajo-De-La-Comision.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04004 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el detalle que recibe el administrador sea equivalente al que recibe el alumno dueño**, para **que el docente revise exactamente lo que el alumno entregó y no una versión distinta**.

## 2. Contexto

`NB-00007` pide revisión de la comisión desde un solo lugar y el intake §5 historia 8 pide abrir cualquier trabajo con el mismo visor y el mismo árbol que ve el alumno. El contrato de uso es [`CU-00028`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md), que `02` §8 separó de CU-04006 porque las comprobaciones que los acotan son **opuestas**.

## 3. Criterios de aceptación

- Given un trabajo en estado `Pendiente` y un solicitante con papel `Administrador`, When se pide su detalle, Then trae **los mismos elementos** que el detalle que recibe su dueño.
- Given un trabajo en `Borrador`, When el administrador pide su detalle, Then se devuelve el motivo de **fuera del alcance del administrador** y no el contenido.
- Given un identificador que no existe, When el administrador pide su detalle, Then el motivo emitido **no permite distinguirlo** de un trabajo fuera de su alcance.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00007, NB-00009 |
| CU cubiertos | CU-04007 |
| RN e invariantes que ejerce | RN-04001, RN-04011 |
| Componente de `05` §3.1 | Orquestación de la consulta, Guarda de autorización |
| Puertos que consume | Repositorio de trabajos |
| Comprobación de `02` §4 que la alcanza | **Facultad** y **alcance del administrador**, y cambio de contraseña pendiente antes que las dos |
| BT derivadas | BT-04010, BT-04016 |
| Tests previstos en 08 | Comparación del detalle del administrador contra el del dueño |

## 5. Prioridad y estimación

`Must` porque el criterio de transición `g` → `h` exige que el administrador abra cualquier trabajo que ve y encuentre **exactamente lo mismo** que vio el alumno.

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

**Que la vista sea idéntica para los dos papeles es un criterio de éxito de negocio y no una comodidad de implementación**: `GeometriaFactory-Web` emitió un único caso de uso para las dos personas justamente para que no puedan divergir.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
