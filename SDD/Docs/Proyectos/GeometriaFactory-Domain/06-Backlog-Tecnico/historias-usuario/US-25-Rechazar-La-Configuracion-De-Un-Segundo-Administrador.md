# US-25 — Rechazar la configuración de un segundo administrador

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** US-25-Rechazar-La-Configuracion-De-Un-Segundo-Administrador.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **que la ventana de configuración del administrador se cierre en cuanto exista uno**, para **que no haya dos administradores y que la ruta de alta inicial deje de ser un camino de entrada**.

## 2. Contexto

`RN-01` declara que existe **exactamente un** administrador y que su alta sólo es posible mientras no exista ninguno; `INV-05` lo expresa como condición permanente. `PRODUCT-INTAKE` §4 declara además que los múltiples administradores y los permisos finos son `Won't Have` de la primera versión (`F-19`).

## 3. Criterios de aceptación

- Given una cuenta con papel `Administrador` ya constituida, When se solicita configurar otra, Then se rechaza con la condición que 03 declara para ese caso.
- Given la misma situación, When se intenta la configuración por segunda vez, Then el rechazo es el mismo: la ventana no se reabre.
- Given que no existe ninguna cuenta con papel `Administrador`, When se solicita configurarla, Then procede, que es US-24.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01 |
| CU cubiertos | CU-12 |
| RN e invariantes que ejerce | RN-01; INV-05 |
| BT derivadas | BT-10, BT-14 |
| Etapa del producto | `c`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria del rechazo con administrador existente, dentro de la matriz de ejercicio de `INV-05`. |

## 5. Prioridad y estimación

`Must` por `RN-01`, declarada cerrada en `PRODUCT-INTAKE` §4.1, y porque el roadmap §5.2 la verifica en la transición `c` → `d`.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

La **redirección** de la ruta de alta inicial hacia el inicio de sesión, que `PRODUCT-INTAKE` §4.1 declara como forma de verificar `RN-01`, es de `GeometriaFactory-Web`. Lo que este proyecto de código aporta es la guarda que la sostiene.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
