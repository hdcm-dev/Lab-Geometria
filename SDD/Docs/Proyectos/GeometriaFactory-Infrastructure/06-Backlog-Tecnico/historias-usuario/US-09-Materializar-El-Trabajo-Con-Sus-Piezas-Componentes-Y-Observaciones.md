# US-09 — Materializar el trabajo con sus piezas, componentes y observaciones en una unidad de trabajo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** US-09-Materializar-El-Trabajo-Con-Sus-Piezas-Componentes-Y-Observaciones.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que un trabajo se guarde con sus piezas, sus componentes y sus observaciones dentro de una sola unidad de trabajo**, para **que no queden trabajos guardados a medias**.

## 2. Contexto

`NB-03` pide persistencia del trabajo. El alcance transaccional **llega decidido** desde `GeometriaFactory-Application` —un caso de uso, una unidad de trabajo— y acá se materializa como **una por operación** (`05` §2 propiedad 3). El contrato de uso es [`CU-03`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-03-Guardar-Y-Recuperar-Los-Trabajos.md).

## 3. Criterios de aceptación

- Given un trabajo con sus piezas, componentes y observaciones, When se lo materializa, Then todo se escribe **dentro de la misma unidad de trabajo**, que se cierra entera o no se cierra.
- Given una escritura que llega mientras otra tiene el almacén tomado, When se la intenta, Then termina en su condición de **escritura concurrente rechazada**, que es **terminación degradada y no espera activa**: esta capa **no reintenta**.
- Given los **tres** sellos de tiempo del trabajo, When se los guarda, Then se distinguen entre sí y **la fecha que el alumno declara no se confunde** con las dos que registra el sistema.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-03 |
| CU cubiertos | CU-03 |
| RN que ejerce | RN-08 |
| Componente de `05` §3.1 | Adaptador de repositorio de trabajos, Contexto de persistencia y mapeo |
| Reglas conceptuales de modelo | `RC-01`, `RC-03`, `RC-06` —tres sellos de tiempo distintos— |
| ¿Toma alguna decisión de negocio? | **No** |
| ¿Toca el almacén? | **Sí** |
| BT derivadas | BT-05, BT-08, BT-10 |
| Tests previstos en 08 | Pruebas de integración contra el almacén real, desde `GeometriaFactory-Api` |

## 5. Prioridad y estimación

`Must` por derivar de `F-06`, `Must Have`, y porque sin materialización no hay nada que listar ni que revisar.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y, si toca el almacén, las reglas conceptuales de modelo que materializa
- [x] Declara que no toma ninguna decisión de negocio
- [x] Toda condición que produce existe en el catálogo de las 17 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md), declarada como resultado o como fallo
- [x] Declara el camino en que el mecanismo se detiene en lugar de cumplir a medias, cuando puede fallar
- [x] Declara si toca el almacén y, en consecuencia, dónde vive su prueba

## 7. Notas y supuestos

**El escritor único es una restricción del motor y no una elección**, y el intake la acepta por escrito a cambio de un despliegue sin servicio de base de datos aparte. **Reintentar es del consumidor**, que es el que sabe si la operación es repetible; un reintento acá escondería la única señal que el producto tiene de que el almacén no está.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
