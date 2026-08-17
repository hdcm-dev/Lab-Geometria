# US-04016 — Terminar de forma controlada cuando la interpretación no está disponible

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04016-Terminar-De-Forma-Controlada-Cuando-La-Interpretacion-No-Esta-Disponible.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04005 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** **Should**
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el caso de uso de envío termine con una condición legible cuando la interpretación no está disponible, dejando el texto original intacto**, para **poder decirle a la persona que el servicio no pudo interpretar en lugar de mostrarle un fallo sin manejar**.

## 2. Contexto

`05` §4, última viñeta, declara que **la indisponibilidad de un puerto es una condición y no una excepción que escapa**, y que si la interpretación no está disponible el caso de uso de envío termina de forma controlada y el texto original queda intacto. **Ninguna capacidad del intake §4 la pide**: su origen es esta decisión de arquitectura, y eso es lo que la hace `Should`.

## 3. Criterios de aceptación

- Given un doble del puerto de validación que no puede resolver, When se pide el envío, Then el caso de uso devuelve la condición correspondiente del catálogo cerrado y **no propaga ninguna excepción**.
- Given esa terminación, When se inspecciona el trabajo, Then el **texto original queda intacto** y el estado no cambió.
- Given la misma terminación, When se compara la condición emitida contra el catálogo de las 36, Then figura en él: esta historia **no acuña ninguna condición nueva**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00004, NB-00003 |
| CU cubiertos | CU-04005 |
| RN e invariantes que ejerce | RN-04008 |
| Componente de `05` §3.1 | Orquestación del trabajo |
| Puertos que consume | Validación de figuras, repositorio de trabajos |
| Comprobación de `02` §4 que la alcanza | Pertenencia, y cambio de contraseña pendiente antes que ella |
| BT derivadas | BT-04008, BT-04015 |
| Tests previstos en 08 | Prueba con doble del puerto que no resuelve, comprobando texto intacto y condición emitida |

## 5. Prioridad y estimación

**`Should`, y es la única de las treinta y dos.** Su origen **no es una capacidad** de `PRODUCT-INTAKE` §4 sino la decisión de `05` §4 sobre la indisponibilidad de un puerto. El producto funciona sin ella —el caso de uso terminaría con una excepción que el consumidor tendría que atrapar— y lo que se pierde es que el motivo sea legible y que el texto quede garantizadamente intacto. **Diferible, y no gratis.** El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.2.

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

**La prioridad declarada no es la prioridad de ejecución.** Esta historia vive en la etapa `f` y se construye con las otras tres de su épica; ser `Should` significa que, si la etapa aprieta, es la primera candidata a diferirse, no que se construya después de la etapa.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
