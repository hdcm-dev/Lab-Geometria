# US-02024 — Configurar la cuenta de administrador en el primer arranque, habilitada y con credencial

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** US-02024-Configurar-La-Cuenta-De-Administrador-En-El-Primer-Arranque.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **constituir la única cuenta de administrador, habilitada y con credencial, mientras no exista ninguna**, para **que el laboratorio tenga desde el primer arranque a alguien capaz de habilitar a los demás**.

## 2. Contexto

La capacidad `F-01` del intake §4 declara la configuración del administrador en el primer arranque y **sólo mientras no exista ninguna**. Este contrato de uso, [`CU-00025`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00025-Configurar-La-Cuenta-De-Administrador-En-El-Primer-Arranque.md), nació de la corrección del primer P0 del producto: el administrador sobrevivía como flujo alternativo del alta de alumno y nacía en estado `Pendiente`, con lo que la instancia quedaba sin nadie capaz de habilitar a nadie ([`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §10, versión 1.2).

## 3. Criterios de aceptación

- Given que no existe ninguna cuenta con papel `Administrador` y una credencial ya derivada, When se solicita configurarla, Then la cuenta queda constituida con papel `Administrador`, en estado `Habilitado` y con esa credencial.
- Given la cuenta de administrador ya constituida, When se consulta su estado, Then es `Habilitado`, y ninguna operación puede llevarla a `Pendiente` ni a `Bloqueado`, por `INV-08`.
- Given una solicitud de configuración sin credencial derivada, When se procesa, Then se rechaza: la cuenta que habilita a todas las demás no puede nacer sin poder entrar.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001 |
| CU cubiertos | CU-02012 |
| RN e invariantes que ejerce | RN-02001, RN-02002; INV-05, INV-08 |
| BT derivadas | BT-02006, BT-02010 |
| Etapa del producto | `c`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria de la constitución y del estado inicial, dentro de la matriz de ejercicio de `INV-05` e `INV-08`. |

## 5. Prioridad y estimación

`Must` por derivar de `F-01`, `Must Have` en `PRODUCT-INTAKE` §4, y porque es el primer criterio de la transición `c` → `d` del roadmap §5.2.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

Los **dos caminos de alta no se fusionan** y es decisión declarada: el auto-registro del alumno nace `Pendiente` y espera habilitación; esta cuenta nace `Habilitado` porque ninguna anterior podría habilitarla ([`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §3).

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
