# US-02007 — Reemplazar la credencial derivada exigiendo la vigente

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-02007-Reemplazar-La-Credencial-Derivada-Exigiendo-La-Vigente.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **reemplazar la credencial derivada de una cuenta exigiendo que la vigente esté verificada**, para **que nadie cambie la contraseña de una cuenta sin conocer la actual, y que el mismo contrato sirva para el cambio voluntario y para el obligatorio**.

## 2. Contexto

La capacidad `F-05` del intake §4 declara el cambio de contraseña exigiendo la actual. El contrato de uso es [`CU-00022`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md), y [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §3 declara que el **cambio forzado no tiene caso de uso propio**: es un flujo alternativo de este mismo reemplazo.

## 3. Criterios de aceptación

- Given una cuenta con credencial vigente verificada por el consumidor, When se solicita el reemplazo con la credencial nueva ya derivada, Then la credencial queda reemplazada.
- Given una cuenta cuya credencial vigente **no** llega verificada, When se solicita el reemplazo, Then se rechaza y la credencial anterior no se toca.
- Given una cuenta con la marca de cambio de contraseña pendiente puesta, When el reemplazo se completa, Then además **se levanta la marca**, que es lo único propio del flujo forzado.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-02003 |
| RN e invariantes que ejerce | RN-02013, RN-02014; INV-09 |
| BT derivadas | BT-02007, BT-02010 |
| Etapa del producto | `c`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria del reemplazo con la vigente verificada y sin ella, y del levantamiento de la marca en el flujo forzado. |

## 5. Prioridad y estimación

`Must` por derivar de `F-05`, `Must Have` en `PRODUCT-INTAKE` §4. Está en la etapa `c` porque el cambio de contraseña del administrador es criterio de la transición `c` → `d` del roadmap §5.2.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

La marca **la levanta únicamente el cambio efectivo hecho por la propia cuenta**, según el enunciado de `INV-09` que `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Domain consolidó en su versión 1.14. Ningún acto del administrador la levanta.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
