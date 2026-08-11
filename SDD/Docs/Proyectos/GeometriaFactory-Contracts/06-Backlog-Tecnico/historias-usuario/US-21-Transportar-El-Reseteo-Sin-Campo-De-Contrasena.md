# US-21 — Transportar el reseteo sin campo de contraseña y con la provisoria producida

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-21-Transportar-El-Reseteo-Sin-Campo-De-Contrasena.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-03 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **una solicitud de reseteo con el identificador de la cuenta **y nada más**, y un resultado que declare la situación conservada, el cambio pendiente y la provisoria producida**, para **que el administrador no pueda escribir la contraseña de un alumno y que el reseteo no se confunda nunca con una baja**.

## 2. Contexto

La capacidad `F-26` del intake §4 declara que **el panel no lleva campo de contraseña** y que el sistema produce la provisoria. `RN-12` declara qué conserva el reseteo y `RN-14` que la produce el sistema. `02` §3.1 declara que el reseteo se emitió como contrato de uso propio porque absorberlo en la administración de cuentas habría puesto en el mismo lugar la solicitud que **elimina** la cuenta y todos sus trabajos y la que los **conserva**.

## 3. Criterios de aceptación

- Given un reseteo pretendido, When se arma la solicitud, Then transporta el identificador de la cuenta y **ningún campo de contraseña**.
- Given un reseteo que procede, When se arma el resultado, Then declara la situación **conservada** de la cuenta, el cambio de contraseña pendiente y la provisoria producida.
- Given ese mismo resultado, When se lo inspecciona, Then **no declara ningún campo por el que los trabajos se pierdan**, por `RN-12`.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01, NB-02 |
| CU cubiertos | CU-08 |
| Familia de tipos de `05` §3.1 | Familia de reseteo |
| Restricciones transversales de `02` §6 | RT-01 |
| RN que refiere por identificador | RN-12, RN-13, RN-14, RN-15, RN-01, RN-07 |
| BT derivadas | BT-08, BT-11 |
| Etapa del producto | `d`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de integración del reseteo sobre una cuenta con trabajos en tres estados, verificando que los conserva. |

## 5. Prioridad y estimación

`Must` porque `PRODUCT-INTAKE` §4 declara `F-26` como `Must Have`, y porque el roadmap §5.2 incorpora sus criterios a la transición `d` → `e`, que no cierra sin ellos.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

**La ausencia de un código por cuenta no habilitada es parte del contrato** (`RN-15`, `02` §4.1): resetear procede sobre `Pendiente`, `Habilitado` y `Bloqueado`, de modo que esa causa no existe y no recibe código. Es un caso donde lo que el contrato **no** declara es tan contrato como lo que declara.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
