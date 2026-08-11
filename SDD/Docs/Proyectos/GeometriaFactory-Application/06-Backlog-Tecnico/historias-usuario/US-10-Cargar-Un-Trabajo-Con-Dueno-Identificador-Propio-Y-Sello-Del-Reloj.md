# US-10 — Cargar un trabajo con dueño, identificador propio y sello tomado del reloj

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-10-Cargar-Un-Trabajo-Con-Dueno-Identificador-Propio-Y-Sello-Del-Reloj.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **constituir un trabajo con su dueño, su identificador propio y el sello que el puerto de reloj provee**, para **que el trabajo del alumno quede guardado, tenga dueño y no se pierda al cerrar la página**.

## 2. Contexto

`NB-03` pide trabajo con dueño, estado y persistencia, y `F-06` del intake §4 lo declara `Must Have`. El contrato de uso es [`CU-04`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-04-Cargar-Y-Reeditar-Un-Trabajo-Propio.md). El **sello** entra por el puerto de reloj precisamente para que sea verificable en prueba (`PRODUCT-INTAKE` §17.2.P.11 punto 3).

## 3. Criterios de aceptación

- Given los datos de un trabajo y un dueño identificado, When se lo constituye, Then el trabajo queda con dueño, identificador propio y estado inicial `Borrador`, con el sello tomado del puerto de reloj.
- Given una batería de pruebas con un doble del reloj, When se repite la constitución, Then el sello es **reproducible**: ninguna operación obtiene el momento por su cuenta.
- Given una solicitud sin dueño identificado, When se la procesa, Then se rechaza con su motivo y no se constituye nada.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-03, NB-04 |
| CU cubiertos | CU-04 |
| RN e invariantes que ejerce | RN-03, RN-08 |
| Componente de `05` §3.1 | Orquestación del trabajo |
| Puertos que consume | Repositorio de trabajos, reloj del sistema |
| Comprobación de `02` §4 que la alcanza | Cambio de contraseña pendiente; la **pertenencia** todavía no aplica, porque el trabajo se está constituyendo |
| BT derivadas | BT-07, BT-09, BT-15 |
| Tests previstos en 08 | Prueba con doble de reloj, comprobando reproducibilidad del sello |

## 5. Prioridad y estimación

`Must` por derivar de `F-06`, `Must Have`, y porque el criterio de transición `e` → `f` exige que un trabajo se cargue con nombre, fecha, descripción y texto y reciba identificador propio y estado.

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

**El sello del reloj no es la «Fecha» que el alumno declara en su trabajo**, que es un dato del alumno y la modela el dominio. `05` §6 declara los sellos como **metadatos de orquestación de esta capa**, y la discrepancia con el modelo del dominio está elevada al Product Owner: es `PA-06` de [`../Product-Backlog.md`](../Product-Backlog.md) §6, acompañado por BT-20.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
