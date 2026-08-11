# US-14 — Enviar un trabajo con errores de validación y que quede en `Borrador` con su ubicación

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-14-Enviar-Un-Trabajo-Con-Errores-Y-Que-Quede-En-Borrador.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-05 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que un envío cuyas observaciones incluyen al menos un error deje el trabajo en `Borrador` con la ubicación de cada defecto**, para **que el alumno sepa en qué figura y en qué campo está el problema en lugar de adivinar**.

## 2. Contexto

`RN-05` fija que un trabajo no pasa a estado `Pendiente` con errores de interpretación, y `RN-09` exige que toda observación de error indique la posición de la pieza y el campo. El contrato de uso es [`CU-05`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-05-Enviar-Un-Trabajo-E-Interpretar-Su-Texto.md).

## 3. Criterios de aceptación

- Given un envío cuyo texto produce al menos una observación de especie error, When se resuelve el caso de uso, Then el trabajo **queda en `Borrador`** y las observaciones quedan incorporadas con su ubicación.
- Given un texto con un tipo desconocido, When se lo envía, Then la observación trae el **índice de la figura y el campo**, y el trabajo no pasa a estado `Pendiente`.
- Given una posición de pieza fuera del rango que la cantidad de figuras del conjunto raíz declara, When se procesa el conjunto de observaciones, Then el conjunto **se rechaza entero** y ese rechazo **no llega al alumno**: es un defecto de la interpretación y no de su trabajo.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-04, NB-05, NB-03 |
| CU cubiertos | CU-05 |
| RN e invariantes que ejerce | RN-05, RN-08, RN-09; INV-04 |
| Componente de `05` §3.1 | Orquestación del trabajo, Declaración de puertos |
| Puertos que consume | Validación de figuras, repositorio de trabajos, reloj del sistema |
| Comprobación de `02` §4 que la alcanza | Pertenencia, y cambio de contraseña pendiente antes que ella |
| BT derivadas | BT-08, BT-15, BT-19 |
| Tests previstos en 08 | Escenarios `E-5` y `E-8` del intake §20 como entrada del doble del puerto de validación |

## 5. Prioridad y estimación

`Must` por derivar de `F-09` y `F-22`, `Must Have`, y porque el criterio de transición `f` → `g` exige que un tipo desconocido produzca error con índice de figura y campo y que el trabajo **no** pase a estado `Pendiente`.

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

**La cantidad de figuras del conjunto raíz la produce el validador y esta capa la hace viajar** (`02` §3): no es derivable de las piezas adoptadas, que admiten huecos, y es el rango contra el que la posición se valida. El **escenario `E-8`** —dimensión no legible— produce **error y no advertencia**, y el intake 1.12 lo fijó con su fundamento.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
