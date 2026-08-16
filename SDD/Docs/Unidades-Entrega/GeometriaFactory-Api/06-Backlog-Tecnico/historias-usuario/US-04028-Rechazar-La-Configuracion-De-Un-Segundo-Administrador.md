# US-04028 — Rechazar la configuración de un segundo administrador

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-04028-Rechazar-La-Configuracion-De-Un-Segundo-Administrador.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que la configuración de administrador se rechace en cuanto ya exista una cuenta con ese papel**, para **que la ventana de alta se cierre para siempre y nadie pueda tomar el gobierno del laboratorio**.

## 2. Contexto

`RN-04001` declara que existe **exactamente un** administrador y que su alta sólo es posible mientras no exista ninguno; `INV-05` lo sostiene como condición permanente. El contrato de uso es [`CU-00025`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00025-Configurar-La-Cuenta-De-Administrador-En-El-Primer-Arranque.md). La respuesta a la pregunta sobre el conjunto la aporta el puerto de repositorio de cuentas, porque el dominio no consulta conjuntos.

## 3. Criterios de aceptación

- Given que el puerto de repositorio de cuentas responde que **ya existe** una cuenta con papel `Administrador`, When se pide la configuración, Then se rechaza con su motivo y **no se constituye nada**.
- Given ese rechazo, When se inspecciona el motivo emitido, Then **no revela nada** de la cuenta de administrador existente.
- Given una solicitud de auto-registro que pretende el papel `Administrador`, When se la procesa por CU-04001, Then también se rechaza: la ventana de alta del administrador es exclusiva de CU-04010.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001 |
| CU cubiertos | CU-04010 |
| RN e invariantes que ejerce | RN-04001; INV-05, INV-08 |
| Componente de `05` §3.1 | Orquestación del alta de cuentas |
| Puertos que consume | Repositorio de cuentas |
| Comprobación de `02` §4 que la alcanza | Ninguna: la ventana existe antes de que haya identidad que autenticar |
| BT derivadas | BT-04008, BT-04012 |
| Tests previstos en 08 | Prueba con doble que responde que ya existe administrador |

## 5. Prioridad y estimación

`Must` por `RN-04001` e `INV-05`, y porque el criterio de transición `c` → `d` exige que el administrador se configure **sólo** mientras no exista ninguno.

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

**Múltiples administradores están fuera del alcance de la primera versión**: `F-19` del intake §4 es `Won't Have v1`. Esta historia no es una restricción temporal sino la forma en que `INV-05` se sostiene desde el primer arranque.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
