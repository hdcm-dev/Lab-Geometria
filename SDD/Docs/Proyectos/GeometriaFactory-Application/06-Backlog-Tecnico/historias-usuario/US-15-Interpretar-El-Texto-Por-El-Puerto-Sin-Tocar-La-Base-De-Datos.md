# US-15 — Interpretar el texto por el puerto de validación, sin tocar la base de datos

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-15-Interpretar-El-Texto-Por-El-Puerto-Sin-Tocar-La-Base-De-Datos.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-05 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que la interpretación del texto ocurra detrás del puerto de validación y que el caso de uso entero se pueda ejercer con un doble**, para **poder probar el envío sin base de datos y sin frontera de proceso**.

## 2. Contexto

`PRODUCT-INTAKE` §17.2.P.11 punto 1 declara que **el validador de figuras es un puerto y no una dependencia concreta**, y §17.2.P.8 fija la puerta propia: ninguna prueba de esta capa toca la base de datos real. El contrato de uso es [`CU-05`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-05-Enviar-Un-Trabajo-E-Interpretar-Su-Texto.md).

## 3. Criterios de aceptación

- Given un doble del puerto de validación que devuelve piezas y observaciones, When se ejerce el caso de uso de envío entero, Then se resuelve **sin abrir la base de datos**.
- Given ese mismo ejercicio, When se cuenta cuántas dependencias concretas de interpretación aparecen en el archivo de proyecto, Then son exactamente **0**: la interpretación llega por el puerto.
- Given el texto de **3** piezas del escenario `E-1` del intake §20, When se mide el caso de uso más pesado sin acceso a base, Then el tiempo se compara contra el valor vigente de `05` §8, **rotulado como asunción** hasta que el Product Owner lo confirme.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-04, NB-03 |
| CU cubiertos | CU-05 |
| RN e invariantes que ejerce | RN-05, RN-09 |
| Componente de `05` §3.1 | Declaración de puertos, Orquestación del trabajo |
| Puertos que consume | Validación de figuras |
| Comprobación de `02` §4 que la alcanza | Pertenencia, y cambio de contraseña pendiente antes que ella |
| BT derivadas | BT-06, BT-07, BT-15, BT-19 |
| Tests previstos en 08 | Batería unitaria con doble del puerto y medición del tiempo sobre `E-1` |

## 5. Prioridad y estimación

`Must` porque es la propiedad que justifica el estilo entero de la capa: `05` §9 declara como riesgo de impacto **alto** que un caso de uso consulte por su cuenta y deje de ser probable con dobles.

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

**El valor de los 500 ms se usa como vigente y viene rotulado [ASUNCIÓN]** desde el intake (`05` §8 y §11 `PA-05`). BT-18 lo eleva al Product Owner; hasta entonces la puerta **no se declara bloqueante en 09**, y esta historia **no inventa otro número**.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
