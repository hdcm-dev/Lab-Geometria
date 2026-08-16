# ADR-08003 — Versionado por compilación compartida y despliegue conjunto, sin versionado de rutas

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** ADR-08003-Versionado-Por-Compilacion-Compartida.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Despliegue

---

## 1. Contexto

`PRODUCT-INTAKE` §17.1.P.3 · GeometriaFactory-Contracts declara la política de cambios incompatibles: como los dos extremos se compilan contra el mismo ensamblado, un cambio incompatible **rompe la compilación antes de romper el tiempo de ejecución**; la regla operativa es que las dos unidades desplegables se despliegan juntas ante un cambio de contrato, y **no hay versionado de rutas del servicio en este alcance porque no hay clientes de terceros**. §17.1.P.7 · GeometriaFactory-Contracts agrega que un cambio incompatible en un tipo es **breaking** y sube major del producto en el registro de cambios, aunque no se publique en ningún feed.

Este proyecto de código no tiene pruebas propias: se ejercita íntegramente desde las pruebas de integración que golpean el servicio real, y su gate equivalente es que **el 100 % de los tipos esté ejercitado por al menos una prueba de integración**, valor que el intake rotula `[ASUNCIÓN]` (§17.1.P.6 · GeometriaFactory-Contracts).

La restricción transversal `RT-06` de la categoría 02 registra además que **la versión vigente del contrato ya ejerció esa política**: el conjunto cerrado de estados y el de códigos de error cambiaron los dos.

Motivación upstream: `PRODUCT-INTAKE` §17.1.P.3 · GeometriaFactory-Contracts, §17.1.P.6 · GeometriaFactory-Contracts, §17.1.P.7 · GeometriaFactory-Contracts y §17.1.P.8 · GeometriaFactory-Contracts; `RT-06` y `RT-07` de la categoría 02.

## 2. Decisión

**La compatibilidad del contrato la gobierna la compilación compartida, y no un esquema de versiones de ruta ni una negociación en tiempo de ejecución.** En consecuencia:

1. **No hay versionado de rutas del servicio** ni convivencia de dos versiones del contrato. No hay clientes de terceros a quienes dar plazo.
2. **Un cambio incompatible obliga al despliegue conjunto** de las dos unidades desplegables, y esa obligación es una entrada para la categoría 09.
3. **La verificación del contrato ocurre en la batería de integración**, no en pruebas propias de este proyecto de código.
4. **Hay una clase de cambio incompatible que compila igual**, y por eso el criterio de §7 la nombra explícitamente: agregar o quitar un valor de un conjunto cerrado, y agregar un campo capaz de transportar lo que la regla de exposición prohíbe.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Compilación compartida con despliegue conjunto (**adoptada**) | El error aparece en la construcción, que es lo más barato posible; cero infraestructura de versionado | Las dos unidades quedan atadas en su calendario de despliegue; un cambio de contrato no se puede desplegar por partes |
| Versionado de rutas del servicio con convivencia de dos versiones | Permitiría desplegar las unidades por separado y dar plazo de migración | No hay a quién dárselo: los dos consumidores son del mismo producto. Descartada por `PRODUCT-INTAKE` §17.1.P.3 · GeometriaFactory-Contracts, que declara explícitamente que no hay versionado de rutas en este alcance |
| Negociación de versión en tiempo de ejecución | Tolerante a despliegues desfasados | Convierte un error de compilación en un error de ejecución, que es exactamente lo que la decisión adoptada evita; y agrega un mecanismo que nadie va a ejercer |
| Sólo compatibilidad aditiva, sin cambios incompatibles nunca | Las unidades nunca quedarían desfasadas | El contrato ya ejerció dos cambios incompatibles —el conjunto de estados y el de códigos—, y el segundo fue una **reducción** obligada por una decisión de negocio. Prohibirlos habría dejado dos códigos vivos describiendo situaciones imposibles |

## 5. Consecuencias positivas

1. Un cambio incompatible se detecta en la construcción de la etapa, antes del punto de control.
2. No hay infraestructura de versionado que mantener en un producto que las fuentes declaran básico.
3. La regla de despliegue conjunto es simple de verificar: una sola pregunta en el pull request de la etapa.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta el acoplamiento de calendario entre las dos unidades desplegables.** Es el precio de no tener versionado de rutas, y el riesgo correspondiente está registrado en [`../Arquitectura-Proyecto-Codigo.md`](../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §9.
2. **Se acepta que este proyecto de código no tenga batería propia.** Su verificación depende de que la batería de integración cubra todos los tipos, y ese porcentaje está rotulado `[ASUNCIÓN]`.
3. **Se acepta que haya cambios incompatibles que la compilación no detecta.** §7 los enumera, y la mitigación es la revisión más las dos pruebas de inspección de CA-01 y CA-09 de CU-08006.

## 7. Implementación

Qué constituye cada clase de cambio sobre este contrato:

| Clase | Qué la produce | ¿Lo detecta la compilación? |
| --- | --- | --- |
| **Mayor** | Quitar o renombrar un tipo o un campo; cambiar el tipo de un campo | Sí |
| **Mayor** | Quitar un valor de un conjunto cerrado —los cuatro estados del trabajo, los dos valores del desenlace, los diecisiete códigos de error— | **No**: compila igual, y el consumidor deja de cubrir todos los casos |
| **Mayor** | Agregar un código al conjunto cerrado de error | **No**: compila igual, y el consumidor deja de cubrir todos los casos. Es la cláusula de §17 de CU-08006 |
| **Mayor** | Agregar un campo capaz de transportar una dirección de servicio, una ruta de datos o un secreto | **No**: compila igual, y viola RA-03. Se rechaza en revisión |
| **Menor** | Agregar un tipo o un campo opcional que no viole la regla de exposición | — |
| **Menor** | Agregar un valor a un conjunto cerrado que no sea el de códigos de error | — |
| **Parche** | Corregir el texto neutro de un código sin cambiar su causa | — |

- La regla operativa: **Api y Web se despliegan juntos** ante un cambio de contrato.
- El registro de cambios del producto recibe la fila de todo cambio mayor.
- **Reponer un identificador de código retirado se rechaza aunque compile**: contradice CA-09 de CU-08006 y describe situaciones que RN-08016 no admite.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Tipos ejercitados por prueba de integración | **100 %** [ASUNCIÓN del intake] | Matriz tipo contra prueba en 08 |
| Advertencias de construcción | Exactamente **0** | Etapa de `build`, bloqueante para fusionar |
| Referencias hacia `GeometriaFactory-Domain` | Exactamente **0** | Puerta bloqueante de construcción, la misma de ADR-08001 |
| Cambios mayores desplegados sin la contraparte | Exactamente **0** | Revisión del pull request de la etapa, que **es** el punto de control |
| Cambios mayores sin fila en el registro de cambios del producto | Exactamente **0** | Revisión del pull request de la etapa |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.15 §17.1.P.3 · GeometriaFactory-Contracts, §17.1.P.6 · GeometriaFactory-Contracts, §17.1.P.7 · GeometriaFactory-Contracts y §17.1.P.8 · GeometriaFactory-Contracts.
- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/02-Especificacion-Funcional/Especificacion-Funcional.md) §6, `RT-06` y `RT-07`.
- [`../../02-Especificacion-Funcional/Casos-De-Uso/CU-08006-Contrato-De-Respuesta-De-Error.md`](../Contratos-Inter-Unidad/CU-08006-Contrato-De-Respuesta-De-Error.md) §17.
- ADR relacionadas: [`ADR-08001`](ADR-08001-Tipos-De-Transferencia-Planos-Sin-Dependencias.md), [`ADR-08002`](ADR-08002-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra la compilación compartida como mecanismo de compatibilidad, la ausencia deliberada de versionado de rutas, la obligación de despliegue conjunto como entrada para 09, el criterio de cambio mayor con la columna que declara **cuáles no detecta la compilación**, cuatro alternativas evaluadas y cinco métricas. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
