# 05 · Arquitectura técnica — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** README.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Tipo de proyecto de código (D8):** `library`

---

## 1. Punto de entrada

`GeometriaFactory-Contracts` es la biblioteca de tipos y códigos que viajan entre las dos unidades desplegables del producto. Es nivel 0 del orden topológico, no tiene dependencias y **no tiene comportamiento**: son tipos de transferencia planos.

Lo que hay que haber entendido antes de tocar esta sección: **la arquitectura de este proyecto de código no es de ejecución sino de exposición**. Lo único que se decide acá es la forma de la frontera y la lista cerrada de lo que la atraviesa —incluido un conjunto cerrado de **diecisiete** códigos de error vivos, sobre **veinte** identificadores emitidos—. El punto de entrada es [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md), y el atajo de cinco líneas es su §3.2.

## 2. Documentos de esta sección

| Documento | Propósito |
| --- | --- |
| [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) | Documento maestro: estilo, las ocho familias de tipos, la regla de exposición, las cuatro vistas mínimas, cross-cutting, siete NFR, seis riesgos, trazabilidad de las once restricciones transversales y de las dieciséis reglas, y cuatro puntos abiertos |
| [`Decisiones-Arquitectura.md`](Decisiones-Arquitectura.md) | Índice de las cinco ADR, con las cuatro categorías de decisión declaradas vacías y su motivo |
| [`Contratos-Abstractions.md`](Contratos-Abstractions.md) | Contrato de la superficie pública: ocho familias, seis conjuntos cerrados, el inventario de los diecisiete códigos vivos, los tres retirados, las tres señales y el versionado |
| [`Adrs/`](Adrs/) | Las cinco decisiones, una por archivo |

## 3. ADR vigentes

| ADR | Título | Categoría | Estado |
| --- | --- | --- | --- |
| [ADR-01](Adrs/ADR-01-Tipos-De-Transferencia-Planos-Sin-Dependencias.md) | Ensamblado de tipos de transferencia planos, sin comportamiento y sin dependencias | Estilo | Propuesto |
| [ADR-02](Adrs/ADR-02-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md) | Un único tipo de error, con conjunto cerrado de diecisiete códigos | Comunicación | Propuesto |
| [ADR-03](Adrs/ADR-03-Versionado-Por-Compilacion-Compartida.md) | Versionado por compilación compartida y despliegue conjunto, sin versionado de rutas | Despliegue | Propuesto |
| [ADR-04](Adrs/ADR-04-Regla-De-Exposicion-De-La-Frontera.md) | Regla de exposición: lista cerrada de lo que nunca cruza la frontera | Seguridad | Propuesto |
| [ADR-05](Adrs/ADR-05-Proyeccion-De-Listado-Separada-Del-Detalle.md) | Proyección de listado separada del detalle, y el comentario como bloque propio | Comunicación | Propuesto |

Ninguna superada, ninguna rechazada.

## 4. NFR vigentes

Los siete, con su objetivo numérico y su mecanismo, están en [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §8. En una línea: **100 %** de los tipos ejercitados por integración y **0** ocurrencias de texto original, componentes o comentario en la proyección de listado —los dos rotulados `[ASUNCIÓN]` por el intake—, **0** referencias hacia el dominio, **0** campos capaces de transportar dirección, ruta o secreto, exactamente **17** códigos vivos, exactamente **4** campos en la respuesta de sesión y **0** advertencias de construcción.

Ninguno de los siete es de latencia ni de throughput, y es correcto: el ensamblado no ejecuta nada. El único atributo de rendimiento que puede empeorar es el **tamaño de la carga útil**.

## 5. Orden de lectura sugerido

1. [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §3.2 — la regla de exposición. Son dos tablas y es lo primero que hay que haber entendido.
2. [`Adrs/ADR-04`](Adrs/ADR-04-Regla-De-Exposicion-De-La-Frontera.md) — su fundamento, con las alternativas que se evaluaron.
3. [`Contratos-Abstractions.md`](Contratos-Abstractions.md) §5 — el inventario de los diecisiete códigos, los tres retirados y las tres señales. Es donde más se equivoca quien llega de otra categoría, porque los tres conjuntos se parecen y no son lo mismo.
4. [`Adrs/ADR-02`](Adrs/ADR-02-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md) — el criterio con el que un código se justifica, que es lo que evita que el conjunto crezca por operación.
5. [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §10 — la trazabilidad, para consultar por restricción transversal o por regla.

## 6. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Modelo-Datos-Logico.md` | **Omitido** | La regla de la categoría lo omite para `library` puro sin estado. `tiene_persistencia` es false y el intake declara «no aplica» en §17.4.P.4 |
| `Flujo-Ejecucion.md` | **Omitido** | La regla lo recomienda para `library` **con motor de procesamiento**. Este proyecto de código no ejecuta nada: son tipos planos sin comportamiento, y no hay canalización que documentar |
| `Extensibilidad.md` | **Omitido** | `tiene_extensibilidad` es false en el `PRODUCT-MANIFEST` §5. El punto de extensión declarado del producto es el contrato de la fachada del visor |
| `_legacy/` | **No existe** | Es la primera emisión de esta categoría en este proyecto de código: no hay ninguna versión superada que archivar |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Índice navegable de la sección: los cuatro documentos vigentes, las cinco ADR con su estado, los NFR en una línea, el orden de lectura de cinco pasos y los cuatro artefactos omitidos con su motivo. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. Se cierran con su fila, su desenlace y su fecha los puntos abiertos que estas decisiones resolvían. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **5**. Sube minor. |
