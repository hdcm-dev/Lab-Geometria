> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-06021-Emitir-El-Acceso-Firmado-Con-Sus-Cuatro-Reclamos.md` en su versión **1.0**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-06021-Emitir-El-Acceso-Firmado-Con-Sus-Cuatro-Reclamos.md`](../../US-06021-Emitir-El-Acceso-Firmado-Con-Sus-Cuatro-Reclamos.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-06021 — Emitir el acceso firmado con sus cuatro reclamos

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-06021-Emitir-El-Acceso-Firmado-Con-Sus-Cuatro-Reclamos.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **emitir y verificar el acceso firmado con sus cuatro reclamos**, para **que cada petición del producto pueda declarar quién la hace y con qué papel**.

## 2. Contexto

`02` §3 declara el acceso firmado como uno de los **dos mecanismos** que esta capa provee y que **no son puertos de la capa de aplicación**: los consume la composición de raíz de `GeometriaFactory-Api`. El contrato de uso es [`CU-06008`](../../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06008-Emitir-El-Acceso-Firmado.md), y `02` §8 declara por qué la emisión y la verificación quedaron juntas: **las dos dependen de la misma clave**.

## 3. Criterios de aceptación

- Given una identidad ya resuelta y admitida, When se emite el acceso, Then lleva sus **cuatro** reclamos y viene firmado.
- Given un acceso emitido, When se lo verifica, Then el veredicto distingue firma que no corresponde de acceso vencido.
- Given el papel que viaja en el acceso, When se lo inspecciona, Then **esta capa lo transporta y no decide qué habilita**: qué puede hacer cada papel lo deciden el dominio y la capa de aplicación.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-06008 |
| RN que ejerce | RN-06001 en su parte de transporte del papel |
| Componente de `05` §3.1 | Mecanismo de acceso firmado y preparación del almacén |
| Reglas conceptuales de modelo | — |
| ¿Toma alguna decisión de negocio? | **No.** Una cuenta que no admite acceso **no llega** a la emisión |
| ¿Toca el almacén? | **No** para la emisión |
| BT derivadas | BT-06015 |
| Tests previstos en 08 | Pruebas de emisión y verificación, con acceso vencido y con firma que no corresponde |

## 5. Prioridad y estimación

`Must` porque sin el acceso firmado ninguna petición del producto puede declarar identidad, y `RN-06006` sólo puede sostenerse si una cuenta no admitida **no llega** hasta acá.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y, si toca el almacén, las reglas conceptuales de modelo que materializa
- [x] Declara que no toma ninguna decisión de negocio
- [x] Toda condición que produce existe en el catálogo de las 17 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../../../03-UX-UI-DX/DX-Error-Messages.md), declarada como resultado o como fallo
- [x] Declara el camino en que el mecanismo se detiene en lugar de cumplir a medias, cuando puede fallar
- [x] Declara si toca el almacén y, en consecuencia, dónde vive su prueba

## 7. Notas y supuestos

**La vigencia exacta del acceso sigue abierta**: el intake declara «corta» y **sin acceso de refresco**, y no fija un número. La categoría 05 de `GeometriaFactory-Api` fijó el **criterio** —que caduque dentro de la sesión de trabajo de una clase y que la renovación sea reingreso— y toma el número de configuración.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
