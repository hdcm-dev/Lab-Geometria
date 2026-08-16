# US-10005 — Cerrar sesión y acotar las rutas por papel

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-10005-Cerrar-Sesion-Y-Acotar-Las-Rutas-Por-Papel.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-10003 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Superficie de 03:** Los dos shells, sobre las once superficies
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **persona con cuenta**, quiero **cerrar mi sesión y que la aplicación sólo me ofrezca lo que mi papel admite**, para **no dejar mi sesión abierta y no encontrarme con destinos que no me corresponden**.

## 2. Contexto

`F-05` del intake §4 declara `Must Have` el inicio y el cierre de sesión. El caso de uso es [`CU-10002`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-10002-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md). `RT-09` de `02` §6 declara la parte más importante de esta historia: **esto acota lo que se ofrece; la verificación de pertenencia y de papel la hace el servicio de datos en cada solicitud**.

## 3. Criterios de aceptación

- Given una sesión abierta, When la persona cierra sesión, Then la sesión termina y ninguna ruta del panel queda alcanzable.
- Given una sesión de alumno, When se recorre la navegación, Then **ninguna ruta de administrador es alcanzable** y su destino **no se dibuja en la barra lateral, ni siquiera deshabilitado**.
- Given una ruta de administrador pedida por dirección directa con sesión de alumno, When se la solicita, Then la aplicación desvía y **el servicio de datos rechaza igual**: la acotación de la pantalla no es la defensa.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-10002 |
| Restricciones transversales que la alcanzan | RT-02, RT-09 |
| Componente de `05` §3.1 | Armazón y encaminamiento, Sesión y estado del circuito |
| Quién hace cumplir lo que esta historia sólo ofrece | El servicio de datos, que verifica papel y pertenencia en cada solicitud |
| BT derivadas | BT-10007, BT-10014 |
| Tests previstos en 08 | Paso del guion de la etapa `c`, y prueba de ruta forzada contra la superficie de `GeometriaFactory-Api` |

## 5. Prioridad y estimación

`Must` por derivar de `F-05`, `Must Have`, y porque la protección de rutas es criterio de aceptación declarado en `PRODUCT-INTAKE` §17.2.P.5 · GeometriaFactory-Web.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara la superficie de 03 que la aloja y el componente de `05` §3.1 que la sostiene
- [x] Declara qué restricciones transversales de `02` §6 la alcanzan
- [x] Toda condición que presenta es uno de los diecisiete códigos vivos del contrato, o el camino de ausencia de respuesta
- [x] Ninguna afirmación depende de que la pieza pública haga cumplir una regla
- [x] Se puede maquetar y validar sin servicio de datos

## 7. Notas y supuestos

**No dibujar el destino del otro papel es una decisión de presentación legítima y necesaria, y no hace cumplir nada** (`02` §5). Es también lo que `05` §10.3 declara que esta pieza hace por `RN-10001`: el aprovisionamiento se ofrece una sola vez y los dos shells no muestran el destino del otro papel.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
