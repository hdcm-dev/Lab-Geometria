# US-02011 — Reconstruir el conjunto de piezas con identidad posicional

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** US-02011-Reconstruir-El-Conjunto-De-Piezas-Con-Identidad-Posicional.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02005 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **incorporar al trabajo el conjunto de piezas y sus componentes, con la posición de cada pieza como su identidad**, para **que después se pueda seleccionar y resaltar una pieza concreta, y que dos procesados del mismo texto den la misma disposición**.

## 2. Contexto

`PRODUCT-INTAKE` §17.1.P.11 punto 2 declara que la identidad de la pieza es su posición en el conjunto raíz, porque el texto no trae identificador. Es lo que este proyecto de código aporta a `NB-00006`, de forma parcial: el dibujo, el árbol y la sincronización son de `GeometriaFactory-Visor` y de `GeometriaFactory-Web` ([`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.2).

## 3. Criterios de aceptación

- Given un conjunto de piezas producido afuera, When se lo adopta en el trabajo, Then cada pieza queda con la posición que tenía en el texto del alumno como identidad.
- Given un conjunto en el que alguna pieza no se pudo constituir, When se lo adopta, Then el conjunto **admite huecos y no se renumera** (`Definicion-Modelo-De-Dominio.md` §6, citado por `05` §6).
- Given un conjunto mal formado, When se intenta adoptarlo, Then se rechaza entero y el trabajo queda como estaba, por la terminación controlada de `05` §4.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00004, NB-00006 |
| CU cubiertos | CU-02006 |
| RN e invariantes que ejerce | RN-02008, RN-02009 |
| BT derivadas | BT-02013 |
| Etapa del producto | `f`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria sobre los escenarios de datos del intake §20; en particular `E-1` y `E-7`, que son el material declarado de piezas. |

## 5. Prioridad y estimación

`Must` por derivar de `F-09`, `Must Have` en `PRODUCT-INTAKE` §4, y porque la identidad posicional es la precondición de la visualización de la etapa `g`.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

**La interpretación del texto no es de este proyecto de código**: el conjunto de piezas llega producido afuera (`05` §3.1, entradas del componente de adopción). Lo que se construye acá es la adopción y su verificación de buena formación.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
