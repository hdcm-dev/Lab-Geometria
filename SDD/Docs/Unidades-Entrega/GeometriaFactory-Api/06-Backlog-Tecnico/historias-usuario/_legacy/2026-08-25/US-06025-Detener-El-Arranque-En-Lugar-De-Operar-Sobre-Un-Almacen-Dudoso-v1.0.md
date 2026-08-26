> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-06025-Detener-El-Arranque-En-Lugar-De-Operar-Sobre-Un-Almacen-Dudoso.md` en su versión **1.0**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-06025-Detener-El-Arranque-En-Lugar-De-Operar-Sobre-Un-Almacen-Dudoso.md`](../../US-06025-Detener-El-Arranque-En-Lugar-De-Operar-Sobre-Un-Almacen-Dudoso.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-06025 — Detener el arranque en lugar de operar sobre un almacén en el que no se puede confiar

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-06025-Detener-El-Arranque-En-Lugar-De-Operar-Sobre-Un-Almacen-Dudoso.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06001 Esqueleto ambulante y verificación de viabilidad
**Etapa del producto:** `a`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el servicio no atienda ninguna petición si el almacén no quedó en condiciones**, para **que nunca se sirvan datos en los que no se puede confiar y para que el problema sea visible en lugar de silencioso**.

## 2. Contexto

`05` §4 declara esta forma de terminación como **la única que ninguna otra parte del producto tiene**: si la preparación del almacén no se completa, **el servicio no atiende ninguna petición**. Y agrega el fundamento: **un servicio que atiende sobre un almacén equivocado es peor que un servicio que no arranca**. `05` §9 declara los dos atajos que esta historia cierra, los dos con impacto **muy alto**. El contrato de uso es [`CU-06010`](../../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06010-Preparar-El-Almacen-Al-Arrancar.md).

## 3. Criterios de aceptación

- Given un esquema que no corresponde al linaje esperado, When arranca el servicio, Then **el arranque se detiene** y se devuelve la condición correspondiente; **jamás se descarta el almacén para crearlo de nuevo**.
- Given una ruta de almacén no disponible, When arranca, Then **el arranque se detiene** y **no se cae hacia ninguna ruta alternativa dentro de la imagen**.
- Given cualquiera de las dos, When se busca un modo degradado, Then **no hay modo de sólo lectura ni arranque parcial**: exactamente **0** peticiones atendidas con la preparación incompleta.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00008 (parcial) |
| CU cubiertos | CU-06010 |
| RN que ejerce | — |
| Componente de `05` §3.1 | Mecanismo de acceso firmado y preparación del almacén |
| Reglas conceptuales de modelo | — |
| ¿Toma alguna decisión de negocio? | **No** |
| ¿Toca el almacén? | **Sí** |
| BT derivadas | BT-06006, BT-06021 |
| Tests previstos en 08 | Prueba de arranque fallido contra el punto de salud, desde `GeometriaFactory-Api` |

## 5. Prioridad y estimación

`Must` por los dos riesgos de impacto **muy alto** que cierra: descartar el almacén ante un esquema que no corresponde **deja el servicio impecable y sin los trabajos de nadie**, y caer hacia una ruta dentro de la imagen hace que el servicio **acepte trabajos de la comisión entera y los pierda en el siguiente reemplazo de versión**.

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

**Lo que esta historia aporta a `NB-00008` es que el producto quede en un estado que la pieza pública pueda declarar** (`02` §7.2), en lugar de servir datos en los que no se puede confiar. Es su cobertura parcial de esa necesidad, y la única que esta capa puede dar.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
