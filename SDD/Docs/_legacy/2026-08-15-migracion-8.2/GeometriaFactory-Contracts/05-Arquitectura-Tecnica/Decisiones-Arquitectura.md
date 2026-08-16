# Índice de decisiones de arquitectura — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Decisiones-Arquitectura.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)

---

## 1. Qué es este documento

Índice navegable de las decisiones de arquitectura de `GeometriaFactory-Contracts`. **No contiene el cuerpo de ninguna decisión**: cada una vive en su archivo individual bajo [`Adrs/`](Adrs/), es inmutable una vez aceptada y, si evoluciona, se registra en una ADR nueva y la anterior pasa a `Superado por ADR-YY` sin reescribirse.

## 2. ADR vigentes

| ADR | Título | Categoría | Estado | Fecha |
| --- | --- | --- | --- | --- |
| [ADR-08001](Adrs/ADR-08001-Tipos-De-Transferencia-Planos-Sin-Dependencias.md) | Ensamblado de tipos de transferencia planos, sin comportamiento y sin dependencias | Estilo | Propuesto | 2026-08-10 |
| [ADR-08002](Adrs/ADR-08002-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md) | Un único tipo de error, con conjunto cerrado de diecisiete códigos | Comunicación | Propuesto | 2026-08-10 |
| [ADR-08003](Adrs/ADR-08003-Versionado-Por-Compilacion-Compartida.md) | Versionado por compilación compartida y despliegue conjunto, sin versionado de rutas | Despliegue | Propuesto | 2026-08-10 |
| [ADR-08004](Adrs/ADR-08004-Regla-De-Exposicion-De-La-Frontera.md) | Regla de exposición: lista cerrada de lo que nunca cruza la frontera | Seguridad | Propuesto | 2026-08-10 |
| [ADR-08005](Adrs/ADR-08005-Proyeccion-De-Listado-Separada-Del-Detalle.md) | Proyección de listado separada del detalle, y el comentario como bloque propio | Comunicación | Propuesto | 2026-08-10 |

**Cinco ADR**, sobre el mínimo de tres que la regla de la categoría fija para el tipo `library`. Ninguna superada, ninguna rechazada.

## 3. Por qué son cinco y no tres

El mínimo de tres cubre estilo, superficie pública y estrategia de versionado, y acá son ADR-08001, ADR-08002 y ADR-08003 —la superficie pública de este proyecto de código es, en buena medida, su tipo de error—. Las otras dos tienen origen declarado:

| ADR | Por qué existe |
| --- | --- |
| ADR-08004 | Es la decisión central del proyecto de código. `PRODUCT-INTAKE` §17.4.P.5 declara que acá «es donde se decide qué se expone», y las tres reglas de arquitectura de nivel producto caen sobre esta frontera. Dejarla como viñeta del documento maestro habría enterrado la decisión más importante que este proyecto de código toma |
| ADR-08005 | Es el único requerimiento no funcional propio que el intake le declara (§17.4.P.10), y la categoría 02 lo amplió y lo usó como criterio para separar dos contratos de uso. Merece registro con sus alternativas |

## 4. Cobertura de las categorías de decisión

| Categoría | ADR que la cubre | Observación |
| --- | --- | --- |
| Estilo | ADR-08001 | — |
| Persistencia | **Ninguna** | `tiene_persistencia` es false y el intake declara «no aplica» en §17.4.P.4 |
| Comunicación | ADR-08002, ADR-08005 | Es la categoría dominante: este proyecto de código **es** el contrato de comunicación del producto |
| Seguridad | ADR-08004 | — |
| Observabilidad | **Ninguna** | El intake no declara observabilidad propia en §17.4.P.10, y el ensamblado no ejecuta nada que instrumentar |
| Despliegue | ADR-08003 | No hay unidad de despliegue propia; lo que la ADR gobierna es la construcción, el versionado y la obligación de despliegue conjunto |
| Extensibilidad | **Ninguna** | `tiene_extensibilidad` es false. El punto de extensión del producto es el contrato de la fachada del visor |

Las cuatro categorías sin ADR se declaran vacías con su motivo, para que nadie las complete más adelante con decisiones inventadas.

## 5. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Indexa las cinco ADR de `GeometriaFactory-Contracts` con su categoría, su estado y su fecha, declara por qué son cinco y no tres, y declara vacías con su motivo las cuatro categorías de decisión que este proyecto de código no toca. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. Se cierran con su fila, su desenlace y su fecha los puntos abiertos que estas decisiones resolvían. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
