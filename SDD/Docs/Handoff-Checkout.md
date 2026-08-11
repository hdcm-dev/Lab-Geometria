# Resumen ejecutivo de check-out — Fábrica de Geometría

**Producto:** Fábrica de Geometría
**Documento:** Handoff-Checkout.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Orquestador SDD
**Instrumento normativo:** `Master-Prompt.md` 5.2 §12 (repositorio del framework, sólo lectura)
**Trazabilidad upstream:** [`../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.26**; [`../Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md`](../Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md) **1.3**; el árbol completo de [`.`](.) y de [`Audit/`](Audit/); [`../Maquetas/GeometriaFactory-Web/`](../Maquetas/GeometriaFactory-Web/); [`../../samples/`](../../samples/)
**Trazabilidad downstream:** el ciclo de codificación. Este documento es el instrumento que el equipo se lleva al Sprint 1.

---

## Tabla de contenido

- [0. Qué es este documento y cómo se produjo](#0-qué-es-este-documento-y-cómo-se-produjo)
- [1. Proyectos de código del producto](#1-proyectos-de-código-del-producto)
- [2. Documentos generados por proyecto de código y categoría](#2-documentos-generados-por-proyecto-de-código-y-categoría)
- [3. Cobertura de la cadena de trazabilidad](#3-cobertura-de-la-cadena-de-trazabilidad)
- [4. Ítems del Sprint 1 listos para codear](#4-ítems-del-sprint-1-listos-para-codear)
- [5. Audits aprobados](#5-audits-aprobados)
- [6. Decisiones pendientes](#6-decisiones-pendientes)
- [7. Flags activos](#7-flags-activos)
- [8. Línea de base y sensado de deriva](#8-línea-de-base-y-sensado-de-deriva)
- [9. Plan documental de la categoría 11](#9-plan-documental-de-la-categoría-11)
- [10. Contratos de verificación pendientes](#10-contratos-de-verificación-pendientes)
- [11. Divergencias entre lo contado y lo declarado](#11-divergencias-entre-lo-contado-y-lo-declarado)
- [12. Control de cambios](#12-control-de-cambios)

---

## 0. Qué es este documento y cómo se produjo

Es el resumen ejecutivo que `Master-Prompt.md` §12 exige **antes** del traspaso a codificación. Tiene los diez bloques que esa sección enumera, en su orden.

**Este documento no decide nada.** Es un inventario. Todo lo que no está decidido va al bloque 6, que es el que le importa a quien empiece a construir.

**Todas las cifras se contaron con herramienta sobre el árbol vivo**, no se heredaron de otro documento. Los instrumentos fueron `find`, `grep -c`, `grep -o | sort -u | wc -l`, `awk` sobre rangos de sección y `wc -c`. En todos los casos se excluyeron las carpetas `_legacy/`, que conservan versiones superadas. Donde una cifra contada **no coincide** con la que otro documento del corpus declara, el desajuste está escrito en el bloque 11 y referenciado desde el bloque donde aparece; no se copió la cifra declarada.

**Ningún secreto, credencial, clave ni dirección concreta aparece acá**, en línea con `RA-01` y con lo que los siete proyectos de código declaran en su categoría 05.

---

## 1. Proyectos de código del producto

Fuente de la composición: [`../Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md`](../Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md) 1.3 §2 y §3. La columna «estado» la determiné yo, cruzando la existencia de los directorios de documentación contra la existencia del código en el repositorio.

| Proyecto de código | `tipo_proyecto_codigo` | Rol en el producto | Dependencias de compilación | Estado |
| --- | --- | --- | --- | --- |
| `GeometriaFactory-Domain` | `library` | Entidades e invariantes; centro de la regla de dependencias | — (nivel topológico 0) | Documentación completa (categorías 02, 03, 05, 06, 07, 08, 09, 10, 11). **Sin código**: `src/GeometriaFactory.Domain/` no existe |
| `GeometriaFactory-Contracts` | `library` | DTOs de la API; contrato compartido por los dos procesos desplegables | — (nivel topológico 0) | Documentación completa. **Sin código**: `src/GeometriaFactory.Contracts/` no existe |
| `GeometriaFactory-Visor` | `library` | Bundle JavaScript del visor 3D; visualizador puro (`RA-02`) | — (nivel topológico 0) | Documentación completa; Fase B2 ejecutada. **Sin código**: `visor/` no existe |
| `GeometriaFactory-Application` | `library` | Casos de uso y puertos | `GeometriaFactory-Domain` | Documentación completa. **Sin código**: `src/GeometriaFactory.Application/` no existe |
| `GeometriaFactory-Web` | `web-monolith` | Front Blazor Interactive Server; único punto de contacto del navegador | `GeometriaFactory-Contracts`, `GeometriaFactory-Visor` | Documentación completa; Fase B2 ejecutada con maqueta aprobada. **Sin código**: `src/GeometriaFactory.Web/` no existe |
| `GeometriaFactory-Infrastructure` | `library` | EF Core sobre SQLite, seguridad y validador de figuras | `GeometriaFactory-Application`, `GeometriaFactory-Domain` | Documentación completa. **Sin código**: `src/GeometriaFactory.Infrastructure/` no existe |
| `GeometriaFactory-Api` | `rest-api` | Host REST; endpoints, autenticación y migraciones al arrancar (**principal**) | `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure`, `GeometriaFactory-Contracts` | Documentación completa. **Sin informe de auditoría de Fase B** (ver bloques 5 y 6). **Sin código**: `src/GeometriaFactory.Api/` no existe |

**Verificado con `ls` sobre la raíz del repositorio:** los únicos directorios presentes son `SDD/`, `samples/` y `README.md`. **No existen `src/`, `visor/`, `tests/` ni `scripts/`.** Todo lo que este corpus afirma sobre comportamiento es una afirmación sobre el papel.

**Aristas de compilación contadas por mí sobre la columna «Dependencias» del manifiesto §2: ocho.** El grafo en prosa del manifiesto §3 dibuja **siete** y su §4 valida **siete**. Ver bloque 11, desajuste `D-1`.

Orden topológico de construcción, del manifiesto §3, verificado consistente con las dependencias de §2:

```text
nivel 0: GeometriaFactory-Domain, GeometriaFactory-Contracts, GeometriaFactory-Visor
nivel 1: GeometriaFactory-Application, GeometriaFactory-Web
nivel 2: GeometriaFactory-Infrastructure
nivel 3: GeometriaFactory-Api
```

---

## 2. Documentos generados por proyecto de código y categoría

Contado con `find ... -name '*.md' -not -path '*_legacy*' | wc -l` para la cantidad y con `cat | wc -c` para el tamaño. El tamaño es la suma de bytes de los archivos vivos de la categoría, redondeada a KB.

**Total del corpus vivo de `SDD/Docs/`: 639 archivos `.md`, 9.837.307 bytes (≈ 9,4 MB).** Además hay **171** archivos `.md` archivados en carpetas `_legacy/`, que no se cuentan en ninguna cifra de este documento.

**Estado de los documentos:** ningún documento vivo del corpus declara `Estado: Aprobado`. El barrido con `grep -rh '^\*\*Estado:\*\*'` devuelve **589** ocurrencias de `Propuesto` y **166** de `Propuesta`, y **cero** de `Aprobado`. La columna «estado» de las tablas siguientes reproduce eso.

### 2.1 Categorías de nivel producto

| Categoría | Cantidad de archivos | Tamaño aprox | Estado |
| --- | --- | --- | --- |
| `00-Contexto` | 5 | 152 KB | Propuesto |
| `01-Necesidades-Negocio` | 11 | 184 KB | Propuesto |
| `Producto/` (vista de producto y pipeline) | 2 | ver nota | Propuesto |
| `Producto/11-Documentacion` | 1 | ver nota | Propuesto |
| `Audit/` (informes de auditoría) | 30 | 1400 KB | N/A — son informes, no entregables |
| `README.md` de `SDD/Docs/` | 1 | 21 KB | Propuesto |

Nota: `Producto/` completo —los tres archivos, incluido el de `11-Documentacion`— suma 74 KB.

### 2.2 `GeometriaFactory-Domain`

| Categoría | Cantidad de archivos | Tamaño aprox | Estado |
| --- | --- | --- | --- |
| `02-Especificacion-Funcional` | 33 | 428 KB | Propuesto |
| `03-UX-UI-DX` | 5 | 151 KB | Propuesto |
| `05-Arquitectura-Tecnica` | 10 | 96 KB | Propuesto |
| `06-Backlog-Tecnico` | 31 | 165 KB | Propuesto |
| `07-Plan-Sprint` | 2 | 24 KB | Propuesto |
| `08-Calidad-Y-Pruebas` | 9 | 130 KB | Propuesto |
| `09-Devops` | 5 | 60 KB | Propuesto |
| `10-Examples` | 4 | 49 KB | Propuesto |
| `11-Documentacion` | 1 | 7 KB | Propuesto (plan, Momento 1) |

### 2.3 `GeometriaFactory-Contracts`

| Categoría | Cantidad de archivos | Tamaño aprox | Estado |
| --- | --- | --- | --- |
| `02-Especificacion-Funcional` | 11 | 254 KB | Propuesto |
| `03-UX-UI-DX` | 5 | 143 KB | Propuesto |
| `05-Arquitectura-Tecnica` | 9 | 92 KB | Propuesto |
| `06-Backlog-Tecnico` | 26 | 148 KB | Propuesto |
| `07-Plan-Sprint` | 2 | 22 KB | Propuesto |
| `08-Calidad-Y-Pruebas` | 9 | 125 KB | Propuesto |
| `09-Devops` | 5 | 64 KB | Propuesto |
| `10-Examples` | 4 | 47 KB | Propuesto |
| `11-Documentacion` | 1 | 7 KB | Propuesto (plan, Momento 1) |

### 2.4 `GeometriaFactory-Visor`

| Categoría | Cantidad de archivos | Tamaño aprox | Estado |
| --- | --- | --- | --- |
| `02-Especificacion-Funcional` | 11 | 173 KB | Propuesto |
| `03-UX-UI-DX` | 5 | 162 KB | Propuesto |
| `05-Arquitectura-Tecnica` | 12 | 121 KB | Propuesto |
| `06-Backlog-Tecnico` | 4 | 87 KB | Propuesto (modo inline, sin `historias-usuario/`) |
| `07-Plan-Sprint` | 2 | 25 KB | Propuesto |
| `08-Calidad-Y-Pruebas` | 10 | 164 KB | Propuesto |
| `09-Devops` | 6 | 90 KB | Propuesto |
| `10-Examples` | 4 | 53 KB | Propuesto |
| `11-Documentacion` | 1 | 7 KB | Propuesto (plan, Momento 1) |

### 2.5 `GeometriaFactory-Application`

| Categoría | Cantidad de archivos | Tamaño aprox | Estado |
| --- | --- | --- | --- |
| `02-Especificacion-Funcional` | 14 | 249 KB | Propuesto |
| `03-UX-UI-DX` | 5 | 204 KB | Propuesto |
| `05-Arquitectura-Tecnica` | 10 | 125 KB | Propuesto |
| `06-Backlog-Tecnico` | 36 | 197 KB | Propuesto |
| `07-Plan-Sprint` | 2 | 29 KB | Propuesto |
| `08-Calidad-Y-Pruebas` | 9 | 154 KB | Propuesto |
| `09-Devops` | 5 | 65 KB | Propuesto |
| `10-Examples` | 4 | 56 KB | Propuesto |
| `11-Documentacion` | 1 | 7 KB | Propuesto (plan, Momento 1) |

### 2.6 `GeometriaFactory-Web`

| Categoría | Cantidad de archivos | Tamaño aprox | Estado |
| --- | --- | --- | --- |
| `02-Especificacion-Funcional` | 13 | 242 KB | Propuesto |
| `03-UX-UI-DX` | 20 | 475 KB | Propuesto (incluye los tres artefactos de línea de base de la Fase B2) |
| `05-Arquitectura-Tecnica` | 10 | 134 KB | Propuesto |
| `06-Backlog-Tecnico` | 34 | 200 KB | Propuesto |
| `07-Plan-Sprint` | 2 | 33 KB | Propuesto |
| `08-Calidad-Y-Pruebas` | 9 | 220 KB | Propuesto |
| `09-Devops` | 6 | 114 KB | Propuesto |
| `10-Examples` | 2 | 34 KB | Propuesto |
| `11-Documentacion` | 1 | 6 KB | Propuesto (plan, Momento 1) |

### 2.7 `GeometriaFactory-Infrastructure`

| Categoría | Cantidad de archivos | Tamaño aprox | Estado |
| --- | --- | --- | --- |
| `02-Especificacion-Funcional` | 22 | 265 KB | Propuesto |
| `03-UX-UI-DX` | 5 | 135 KB | Propuesto |
| `05-Arquitectura-Tecnica` | 13 | 201 KB | Propuesto |
| `06-Backlog-Tecnico` | 29 | 180 KB | Propuesto |
| `07-Plan-Sprint` | 2 | 29 KB | Propuesto |
| `08-Calidad-Y-Pruebas` | 9 | 169 KB | Propuesto |
| `09-Devops` | 5 | 82 KB | Propuesto |
| `10-Examples` | 4 | 58 KB | Propuesto |
| `11-Documentacion` | 1 | 7 KB | Propuesto (plan, Momento 1) |

### 2.8 `GeometriaFactory-Api`

| Categoría | Cantidad de archivos | Tamaño aprox | Estado |
| --- | --- | --- | --- |
| `02-Especificacion-Funcional` | 16 | 278 KB | Propuesto |
| `03-UX-UI-DX` | 5 | 112 KB | Propuesto |
| `05-Arquitectura-Tecnica` | 12 | 195 KB | Propuesto |
| `06-Backlog-Tecnico` | 34 | 198 KB | Propuesto |
| `07-Plan-Sprint` | 2 | 32 KB | Propuesto |
| `08-Calidad-Y-Pruebas` | 9 | 173 KB | Propuesto |
| `09-Devops` | 6 | 121 KB | Propuesto |
| `10-Examples` | 4 | 61 KB | Propuesto |
| `11-Documentacion` | 1 | 7 KB | Propuesto (plan, Momento 1) |

**La categoría 04 no existe en ningún proyecto de código**, por gating: `usa_llm` es false en los siete (manifiesto §5). Verificado con `find`: no hay ningún directorio `04-*`.

---

## 3. Cobertura de la cadena de trazabilidad

Eslabones de `Master-Prompt.md` §12: Visión, NB, CU, RN, ADR, US, BT, Sprint, Test, Pipeline.

**Cómo se contó cada eslabón.** Visión y NB son de nivel producto y se cuentan una vez. CU: archivos `CU-*.md` en `02-Especificacion-Funcional/Casos-De-Uso/`. RN: archivos `RN-*.md`. ADR: archivos `ADR-*.md` en `05-Arquitectura-Tecnica/Adrs/`. US: archivos `US-*.md` en `06-Backlog-Tecnico/historias-usuario/`, salvo en `GeometriaFactory-Visor`, que emite las suyas **inline** por decisión declarada y ahí se contaron los identificadores únicos en filas de tabla de `Product-Backlog.md`. BT: identificadores únicos en filas de tabla de `Backlog-Tecnico.md`. Sprint: filas de la tabla de ítems comprometidos de `Mini-Plan.md` §3. Test: identificadores `TC-XX` únicos en `08-Calidad-Y-Pruebas/`. Pipeline: filas de la tabla de stages y gates de `09-Devops/Pipeline-CI-CD.md` §2.1.

**Cómo se contaron los huérfanos.** Un eslabón es huérfano si el archivo o la fila no referencia ningún identificador aguas arriba. Se buscó, por archivo, la presencia de `NB-XX` en los CU; de `CU-XX` en las US; de `CU-XX`, `RN-XX`, `US-XX`, `INV-XX`, `NB-XX`, `F-XX` o `RA-XX` en los ADR; y de una celda de justificación upstream no vacía en las filas BT.

### 3.1 Eslabones de nivel producto

| Eslabón | Artefacto canónico | Cantidad de ítems | Huérfanos |
| --- | --- | --- | --- |
| Visión | [`00-Contexto/Vision-Producto.md`](00-Contexto/Vision-Producto.md) | 1 documento | 0 |
| NB | [`01-Necesidades-Negocio/Necesidades-De-Negocio/`](01-Necesidades-Negocio/Necesidades-De-Negocio/) | **9** (`NB-01` a `NB-09`, un archivo cada una) | 0 |
| Pipeline de producto | [`Producto/Pipeline-Producto.md`](Producto/Pipeline-Producto.md) | 1 documento | 0 |

### 3.2 `GeometriaFactory-Domain`

| Eslabón | Artefacto canónico | Cantidad de ítems | Huérfanos |
| --- | --- | --- | --- |
| Visión | nivel producto | — | — |
| NB | nivel producto | — | — |
| CU | `02-Especificacion-Funcional/Casos-De-Uso/` | 13 | 0 |
| RN | `02-Especificacion-Funcional/Reglas-De-Negocio/` | **16** (`RN-01` a `RN-16`) | 0 |
| ADR | `05-Arquitectura-Tecnica/Adrs/` | 6 | 1 sin traza a CU/NB/RN: `ADR-03` (versionado), que traza al intake §17.1.P.7 |
| US | `06-Backlog-Tecnico/historias-usuario/` | 27 | 0 |
| BT | `06-Backlog-Tecnico/Backlog-Tecnico.md` | 16 | 0 |
| Sprint | `07-Plan-Sprint/Mini-Plan.md` §3 | 43 filas comprometidas, seis etapas (`a`, `c`, `d`, `e`, `f`, `h`) | 0: los 27 US y los 16 BT están comprometidos |
| Test | `08-Calidad-Y-Pruebas/` | 27 `TC-XX` | 0 |
| Pipeline | `09-Devops/Pipeline-CI-CD.md` §2.1 | 8 filas de stage/gate | 0 |

Este es además el proyecto de código donde viven los **9** invariantes del producto (`INV-01` a `INV-09`), contados con `grep -o` sobre todo el corpus vivo.

### 3.3 `GeometriaFactory-Contracts`

| Eslabón | Artefacto canónico | Cantidad de ítems | Huérfanos |
| --- | --- | --- | --- |
| Visión | nivel producto | — | — |
| NB | nivel producto | — | — |
| CU | `02-Especificacion-Funcional/Casos-De-Uso/` | 8 | 0 |
| RN | no emite | 0 | — (las reglas viven en `GeometriaFactory-Domain`) |
| ADR | `05-Arquitectura-Tecnica/Adrs/` | 5 | 0 |
| US | `06-Backlog-Tecnico/historias-usuario/` | 22 | 0 |
| BT | `06-Backlog-Tecnico/Backlog-Tecnico.md` | 18 | 0 |
| Sprint | `07-Plan-Sprint/Mini-Plan.md` §3 | 39 filas, siete etapas (todas salvo `b`) | 0: los 22 US y los 18 BT están comprometidos |
| Test | `08-Calidad-Y-Pruebas/` | 22 `TC-XX` | 0 |
| Pipeline | `09-Devops/Pipeline-CI-CD.md` §2.1 | 3 filas de stage/gate | 0 |

Acá vive el conjunto cerrado de códigos del contrato: **15 vivos sobre 18 emitidos**, con 3 retirados que no se reciclan.

### 3.4 `GeometriaFactory-Visor`

| Eslabón | Artefacto canónico | Cantidad de ítems | Huérfanos |
| --- | --- | --- | --- |
| Visión | nivel producto | — | — |
| NB | nivel producto | — | — |
| CU | `02-Especificacion-Funcional/Casos-De-Uso/` | 7 | 0 |
| RN | no emite | 0 | — |
| ADR | `05-Arquitectura-Tecnica/Adrs/` | 6 | 1 sin traza a CU/NB/RN: `ADR-06` (versionado del bundle), que traza al intake §17.7.P.7 |
| US | `06-Backlog-Tecnico/Product-Backlog.md`, **inline** | 14 | 0 |
| BT | `06-Backlog-Tecnico/Backlog-Tecnico.md` | 18 | 0 |
| Sprint | `07-Plan-Sprint/Mini-Plan.md` §3 | 17 filas, dos etapas (`a` y `g`) | 0: los 14 US y los 18 BT están comprometidos, algunas filas agrupan más de un identificador |
| Test | `08-Calidad-Y-Pruebas/` | 21 `TC-XX` | 0 |
| Pipeline | `09-Devops/Pipeline-CI-CD.md` §2.1 | 8 filas de stage/gate | 0 |

Acá vive el punto de extensión del producto: la fachada de **6** funciones (`inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir`, `establecerMovimiento`), contadas sobre `02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`.

### 3.5 `GeometriaFactory-Application`

| Eslabón | Artefacto canónico | Cantidad de ítems | Huérfanos |
| --- | --- | --- | --- |
| Visión | nivel producto | — | — |
| NB | nivel producto | — | — |
| CU | `02-Especificacion-Funcional/Casos-De-Uso/` | 11 | 0 |
| RN | no emite | 0 | — |
| ADR | `05-Arquitectura-Tecnica/Adrs/` | 6 | 1 sin traza a CU/NB/RN: `ADR-03` (versionado), que traza al intake §17.2.P.7 |
| US | `06-Backlog-Tecnico/historias-usuario/` | 32 | 0 |
| BT | `06-Backlog-Tecnico/Backlog-Tecnico.md` | 21 | 0 |
| Sprint | `07-Plan-Sprint/Mini-Plan.md` §3 | 53 filas, seis etapas (`a`, `c`, `d`, `e`, `f`, `h`) | 0: los 32 US y los 21 BT están comprometidos |
| Test | `08-Calidad-Y-Pruebas/` | 31 `TC-XX` | 0 |
| Pipeline | `09-Devops/Pipeline-CI-CD.md` §2.1 | 10 filas de stage/gate | 0 |

### 3.6 `GeometriaFactory-Web`

| Eslabón | Artefacto canónico | Cantidad de ítems | Huérfanos |
| --- | --- | --- | --- |
| Visión | nivel producto | — | — |
| NB | nivel producto | — | — |
| CU | `02-Especificacion-Funcional/Casos-De-Uso/` | 10 | 0 |
| RN | no emite | 0 | — |
| ADR | `05-Arquitectura-Tecnica/Adrs/` | 7 | 0 |
| US | `06-Backlog-Tecnico/historias-usuario/` | 30 | 0 |
| BT | `06-Backlog-Tecnico/Backlog-Tecnico.md` | 23 | 0 |
| Sprint | `07-Plan-Sprint/Mini-Plan.md` §3 | 52 filas, **las ocho etapas** | 0: los 30 US y los 23 BT están comprometidos |
| Test | `08-Calidad-Y-Pruebas/` | 35 `TC-XX` | 0 |
| Pipeline | `09-Devops/Pipeline-CI-CD.md` §2.1 | 8 pasos del flujo de publicación | 0 |

### 3.7 `GeometriaFactory-Infrastructure`

| Eslabón | Artefacto canónico | Cantidad de ítems | Huérfanos |
| --- | --- | --- | --- |
| Visión | nivel producto | — | — |
| NB | nivel producto | — | — |
| CU | `02-Especificacion-Funcional/Casos-De-Uso/` | 10 | 0 |
| RN | no emite reglas; emite **reglas conceptuales de modelo** en `02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/` | — | — |
| ADR | `05-Arquitectura-Tecnica/Adrs/` | 7 | 0 |
| US | `06-Backlog-Tecnico/historias-usuario/` | 25 | 0 |
| BT | `06-Backlog-Tecnico/Backlog-Tecnico.md` | 26 | 0 |
| Sprint | `07-Plan-Sprint/Mini-Plan.md` §3 | 51 filas, cinco etapas (`a`, `c`, `d`, `e`, `f`) | 0: los 25 US y los 26 BT están comprometidos |
| Test | `08-Calidad-Y-Pruebas/` | 35 `TC-XX` | 0 |
| Pipeline | `09-Devops/Pipeline-CI-CD.md` §2.1 | 14 filas de stage/gate | 0 |

Acá corre la batería del validador: **10** casos, cruzados en el intake §21 contra los **8** escenarios de §20.

### 3.8 `GeometriaFactory-Api`

| Eslabón | Artefacto canónico | Cantidad de ítems | Huérfanos |
| --- | --- | --- | --- |
| Visión | nivel producto | — | — |
| NB | nivel producto | — | — |
| CU | `02-Especificacion-Funcional/Casos-De-Uso/` | 12 | **2** sin referencia a ninguna `NB-XX`: `CU-10` (composición de raíz y conexión de puertos) y `CU-12` (colección de peticiones reproducible). Son casos de uso técnicos: el primero traza a `05` §3.1 y el segundo a la estrategia de demostración del intake §16.1 y §18 |
| RN | no emite | 0 | — |
| ADR | `05-Arquitectura-Tecnica/Adrs/` | 8 | 0 |
| US | `06-Backlog-Tecnico/historias-usuario/` | 30 | 0 |
| BT | `06-Backlog-Tecnico/Backlog-Tecnico.md` | 26 | 0 |
| Sprint | `07-Plan-Sprint/Mini-Plan.md` §3 | 56 filas, seis etapas (`a`, `c`, `d`, `e`, `f`, `h`) | 0: los 30 US y los 26 BT están comprometidos |
| Test | `08-Calidad-Y-Pruebas/` | 37 `TC-XX` | 0 |
| Pipeline | `09-Devops/Pipeline-CI-CD.md` §2.1 | 15 filas de stage/gate | 0 |

Acá viven los **15** puntos de acceso: `A-01` a `A-03` y `A-05` a `A-16`. **`A-04` está retirado y no se recicla.** Contado sobre `05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md` §3.4.

### 3.9 Totales del producto, contados

| Magnitud | Contado por mí | Instrumento |
| --- | --- | --- |
| Casos de uso | **71** (Domain 13, Api 12, Application 11, Infrastructure 10, Web 10, Contracts 8, Visor 7) | `find -name 'CU-*.md'` por proyecto |
| Reglas de negocio | **16** | `find -name 'RN-*.md'` |
| Invariantes | **9** | `grep -o 'INV-[0-9][0-9]' \| sort -u` |
| Escenarios del intake §20 | **8** | `grep -c '^### §20.E-'` |
| Casos de la batería del validador | **10** | filas de la tabla del intake §21 |
| Códigos del contrato | **15 vivos sobre 18 emitidos**, 3 retirados | `05` §5.1 y `08` de Contracts |
| Puntos de acceso | **15** | filas `A-XX` de `Api/05` §3.4 |
| Funciones de la fachada | **6** | `Visor/02/Definicion-Contrato-De-Fachada.md` |
| Sondas `VER-XX` | **19** (6 proyectos × 3, más 1 de Web) | `grep -o 'VER-[0-9][0-9]'` en cada `10-Examples/` |
| Quality gates `QG-XX` | **77** (Api 15, Infrastructure 14, Application 11, Web 11, Contracts 9, Visor 9, Domain 8) | `grep -o 'QG-[0-9][0-9]' \| sort -u` en `08` y `09` |
| ADR | **45** (Api 8, Infrastructure 7, Web 7, Application 6, Domain 6, Visor 6, Contracts 5) | `find -name 'ADR-*.md'` |
| Historias de usuario | **178** (Application 32, Api 30, Web 30, Domain 27, Infrastructure 25, Contracts 22, Visor 14) | archivos, salvo Visor inline |
| Tareas técnicas | **148** (Api 26, Infrastructure 26, Web 23, Application 21, Contracts 18, Visor 18, Domain 16) | identificadores en filas de `Backlog-Tecnico.md` |
| Casos de prueba `TC-XX` | **208** (Api 37, Infrastructure 35, Web 35, Application 31, Domain 27, Contracts 22, Visor 21) | `grep -o 'TC-[0-9][0-9]' \| sort -u` |
| Criterios BDD `Given/When/Then` en historias | **498** | `grep -h '^- Given' \| wc -l` sobre `historias-usuario/` |
| Artefactos documentales planificados en la categoría 11 | **72** | §2 de cada plan y §3 del plan de producto |

Los primeros nueve coinciden con las cifras de control cruzado. Los que **no** coinciden con lo que otro documento declara están en el bloque 11.

---

## 4. Ítems del Sprint 1 listos para codear

**Este producto no planifica en sprints, y hay que decirlo antes de la tabla.** `equipo_n = 1` (intake §2), y de ahí el framework deriva que la categoría 07 emita únicamente `Mini-Plan.md`. La unidad de planificación es **la etapa del producto** (`a` a `h`), no el sprint: cada `Mini-Plan.md` §1.1 lo declara y `00-Contexto/Roadmap-Producto.md` lo repite. **No existe ningún archivo `Plan-Iteracion-Sprint-XX.md` en el corpus**, verificado con `find`.

**Equivalencia adoptada acá, y es de lectura, no de decisión:** el «Sprint 1» de `Master-Prompt.md` §12 se lee como **el primer tramo comprometido de cada proyecto de código**, que en los siete es la **etapa `a`**. Es el único tramo que los siete tienen en común y el único que puede arrancar sin que ninguna etapa anterior haya cerrado. Si el Product Owner prefiere otra equivalencia, la decisión es suya y va al bloque 6.

**Total de la etapa `a` en los siete proyectos: 48 ítems** —42 tareas técnicas y 6 historias—, contados sobre las filas `| \`a\` |` de cada `Mini-Plan.md` §3.

### 4.1 `GeometriaFactory-Domain` — nivel 0, etapa `a`: 5 ítems

| ID | Tipo | Descripción corta | CU asociado | Criterios | Componentes de 05 |
| --- | --- | --- | --- | --- | --- |
| `BT-01` | Tarea técnica | Crear el proyecto de código y su proyecto de pruebas, sin dependencias salientes | — (andamiaje) | Criterio en `Backlog-Tecnico.md` | Intake §16 y §17.1.P.1; `ADR-01` |
| `BT-02` | Tarea técnica | Fijar los nombres de tipos y de espacios de nombres, y validarlos en el punto de control | — | Criterio en `Backlog-Tecnico.md` | `05` §11 `PA-01`; intake §17.1.P.11 |
| `BT-03` | Tarea técnica | Elegir y anclar la herramienta que calcula la versión | — | Criterio en `Backlog-Tecnico.md` | `05` §11 `PA-04`; intake §17.1.P.7 |
| `BT-04` | Tarea técnica | Puerta bloqueante de cero dependencias salientes | — | Criterio en `Backlog-Tecnico.md` | `05` §8, fila de dependencias salientes; `05` §9, primer riesgo |
| `BT-05` | Tarea técnica | Puerta de construcción con cero advertencias | — | Criterio en `Backlog-Tecnico.md` | `05` §8, fila de advertencias; intake §17.1.P.8 |

### 4.2 `GeometriaFactory-Contracts` — nivel 0, etapa `a`: 3 ítems

| ID | Tipo | Descripción corta | CU asociado | Criterios | Componentes de 05 |
| --- | --- | --- | --- | --- | --- |
| `BT-01` | Tarea técnica | Crear el ensamblado de tipos, sin dependencias | — (andamiaje) | Criterio en `Backlog-Tecnico.md` | Intake §16 y §17.4.P.1; `ADR-01` |
| `BT-02` | Tarea técnica | Puerta de cero referencias hacia `GeometriaFactory-Domain` | — | Criterio en `Backlog-Tecnico.md` | `05` §8; `05` §9, primer riesgo; intake §17.4.P.8 |
| `BT-03` | Tarea técnica | Puerta de construcción con cero advertencias | — | Criterio en `Backlog-Tecnico.md` | `05` §8, fila de advertencias; intake §17.4.P.8 |

### 4.3 `GeometriaFactory-Visor` — nivel 0, etapa `a`: 3 ítems

| ID | Tipo | Descripción corta | CU asociado | Criterios | Componentes de 05 |
| --- | --- | --- | --- | --- | --- |
| `BT-01` | Tarea técnica | Crear el proyecto del bundle con su cadena de construcción reproducible | — (andamiaje) | Criterio en `Backlog-Tecnico.md` | Intake §16 y §17.7.P.8; `05` §5 |
| `BT-02` | Tarea técnica | Guion de construcción propio del bundle, para el ciclo corto | — | Criterio en `Backlog-Tecnico.md` | `05` §5, fila de ciclo corto de trabajo |
| `BT-03` | Tarea técnica | Decidir si el bundle generado se versiona o se ignora | — | Criterio en `Backlog-Tecnico.md` | `05` §11 `PA-05`; intake §17.7.P.7. **Es una decisión abierta**, ver bloque 6 |

### 4.4 `GeometriaFactory-Application` — nivel 1, etapa `a`: 6 ítems

| ID | Tipo | Descripción corta | CU asociado | Criterios | Componentes de 05 |
| --- | --- | --- | --- | --- | --- |
| `BT-01` | Tarea técnica | Crear el proyecto de código y su proyecto de pruebas, con una sola dependencia saliente | — (andamiaje) | Criterio en `Backlog-Tecnico.md` | Intake §16 y §17.2.P.1; `ADR-01` |
| `BT-02` | Tarea técnica | Fijar los nombres de tipos, de espacios de nombres y el del cuarto puerto | — | Criterio en `Backlog-Tecnico.md` | `05` §11 `PA-01` y `PA-02`; `05` §3.4; `05` §9, sexto riesgo |
| `BT-03` | Tarea técnica | Elegir y anclar la herramienta que calcula la versión | — | Criterio en `Backlog-Tecnico.md` | `05` §11 `PA-06`; intake §17.2.P.7 |
| `BT-04` | Tarea técnica | Puerta bloqueante de dependencias salientes | — | Criterio en `Backlog-Tecnico.md` | `05` §8; `05` §9, primer riesgo |
| `BT-05` | Tarea técnica | Puerta de construcción con cero advertencias | — | Criterio en `Backlog-Tecnico.md` | `05` §8; intake §17.2.P.8 |
| `BT-06` | Tarea técnica | Puerta propia de cero pruebas que tocan la base de datos real | — | Criterio en `Backlog-Tecnico.md` | Intake §17.2.P.8; `05` §5 y §8; `05` §9, primer riesgo |

### 4.5 `GeometriaFactory-Web` — nivel 1, etapa `a`: 7 ítems

| ID | Tipo | Descripción corta | CU asociado | Criterios | Componentes de 05 |
| --- | --- | --- | --- | --- | --- |
| `BT-01` | Tarea técnica | Crear el proyecto del front con su flujo de publicación | — (andamiaje) | Criterio en `Backlog-Tecnico.md` | Intake §16 y §17.6.P.8; `05` §5 |
| `BT-02` | Tarea técnica | Anclar la versión de la biblioteca de componentes de interfaz | — | Criterio en `Backlog-Tecnico.md` | `05` §11 `PA-01`; intake §17.6.P.1, rotulado **[A VERIFICAR]** |
| `BT-03` | Tarea técnica | Página de salud que consume el punto de salud del servicio de datos | — | Criterio en `Backlog-Tecnico.md` | Intake §15 y §17.5.P.3; `Roadmap-Producto.md` §5.2, transición `a` → `b` |
| `BT-04` | Tarea técnica | Medir `PT-01` en sus cuatro partes | — | Criterio en `Backlog-Tecnico.md` | Intake §15 y §17.6.P.10; `05` §8; `05` §11 `PA-02` |
| `BT-05` | Tarea técnica | Dirección del servicio de datos desde configuración, con secretos | — | Criterio en `Backlog-Tecnico.md` | `ADR-07`; `05` §7, fila de configuración y secretos; intake §17.6.P.5 |
| `BT-06` | Tarea técnica | Puerta de publicación que comprueba que la dirección pública responde | — | Criterio en `Backlog-Tecnico.md` | `05` §5, puertas bloqueantes; `05` §9, sexto riesgo |
| `BT-12` | Tarea técnica | Adoptar el formato de intercambio que fija la categoría 05 de la Api | — | Criterio en `Backlog-Tecnico.md` | `05` §11 `PA-03`; `Api` `ADR-02`. **Los dos extremos tienen que coincidir**, ver bloque 6 |

### 4.6 `GeometriaFactory-Infrastructure` — nivel 2, etapa `a`: 10 ítems

| ID | Tipo | Descripción corta | CU asociado | Criterios | Componentes de 05 |
| --- | --- | --- | --- | --- | --- |
| `BT-01` | Tarea técnica | Crear el proyecto de código y su proyecto de pruebas | — (andamiaje) | Criterio en `Backlog-Tecnico.md` | Intake §16 y §17.3.P.1; `ADR-01` |
| `BT-02` | Tarea técnica | Fijar nombres y el criterio de nombrado del adaptador de cuentas | — | Criterio en `Backlog-Tecnico.md` | `05` §11 `PA-01` y `PA-02`; `ADR-03` §6 |
| `BT-03` | Tarea técnica | Anclar la función de derivación de clave y sus parámetros versionados | — | Criterio en `Backlog-Tecnico.md` | `05` §11 `PA-03`; `ADR-04` §7. **La fuente declara dos candidatas y no elige**, ver bloque 6 |
| `BT-04` | Tarea técnica | Puerta de construcción con cero advertencias | — | Criterio en `Backlog-Tecnico.md` | `05` §5 y §8; intake §17.3.P.8 |
| `BT-05` | Tarea técnica | Contexto de persistencia y mapeo de las cinco entidades | — | Criterio en `Backlog-Tecnico.md` | `05` §3.1, componente transversal; `05` §6; `Modelo-Datos-Logico.md` |
| `BT-06` | Tarea técnica | Preparación del almacén con linaje inmutable y arranque detenido | `CU-10` | Criterio en `Backlog-Tecnico.md` | `05` §3.1 y §4; `ADR-07`; `05` §9, riesgos quinto y sexto |
| `BT-07` | Tarea técnica | Puerta de transformaciones sobre un almacén inexistente | `CU-10` | Criterio en `Backlog-Tecnico.md` | Intake §17.3.P.8; `05` §5 y §8; `Roadmap-Producto.md` §5.2 (`PT-04`) |
| `BT-08` | Tarea técnica | Fijar la zona horaria y la precisión de los sellos | — | Criterio en `Backlog-Tecnico.md` | `05` §7, fila de zona horaria; `ADR-02` §2; `RC-06` |
| `US-24` | Historia | Aplicar las transformaciones de esquema al arrancar | `CU-10` | **3** criterios `Given/When/Then` en su §3 | `05` §3.1, mecanismo de arranque; `ADR-07` |
| `US-25` | Historia | Detener el arranque en lugar de operar sobre un almacén dudoso | `CU-10` | **3** criterios `Given/When/Then` en su §3 | `05` §3.1 y §4, última viñeta; `ADR-07` |

### 4.7 `GeometriaFactory-Api` — nivel 3, etapa `a`: 14 ítems

| ID | Tipo | Descripción corta | CU asociado | Criterios | Componentes de 05 |
| --- | --- | --- | --- | --- | --- |
| `BT-01` | Tarea técnica | Crear el proyecto de código y su proyecto de pruebas de integración | — (andamiaje) | Criterio en `Backlog-Tecnico.md` | Intake §16 y §17.5.P.1; `05` §5 |
| `BT-02` | Tarea técnica | Composición de raíz con los cuatro puertos y sus adaptadores | `CU-10` | Criterio en `Backlog-Tecnico.md` | `05` §3.1, componente «Composición de raíz»; `ADR-06`; `05` §9, séptimo riesgo |
| `BT-03` | Tarea técnica | Arranque en dos fases con el punto de salud sin acceso | `CU-11` | Criterio en `Backlog-Tecnico.md` | `05` §4, quinta viñeta; `ADR-07`; intake §17.5.P.4 |
| `BT-04` | Tarea técnica | Imagen multietapa y medición de `PT-04` | — | Criterio en `Backlog-Tecnico.md` | Intake §15 y §17.5.P.8; `05` §5 |
| `BT-05` | Tarea técnica | Anclar nombres, espacios de nombres y versiones de paquetes | — | Criterio en `Backlog-Tecnico.md` | `05` §11 `PA-07`; intake §17.5.P.11 |
| `BT-06` | Tarea técnica | Puerta de construcción con cero advertencias | — | Criterio en `Backlog-Tecnico.md` | `05` §8, última fila; intake §17.5.P.8 |
| `BT-07` | Tarea técnica | Fijar rutas y verbos de los quince puntos en el punto de control | — | Criterio en `Backlog-Tecnico.md` | `05` §3.4 y §11 `PA-01`; `Definicion-Superficie-HTTP.md` §3. **Las rutas son propuesta derivada**, ver bloque 6 |
| `BT-08` | Tarea técnica | Fijar el formato de intercambio para los dos extremos | — | Criterio en `Backlog-Tecnico.md` | `ADR-02`; `05` §2.2; `05` §9, cuarto riesgo |
| `BT-09` | Tarea técnica | Fijar el límite de cuerpo que rechaza y nunca trunca | — | Criterio en `Backlog-Tecnico.md` | `ADR-02` §2 punto 6; `05` §11 `PA-05`; `Infrastructure` `ADR-06` §2 punto 3 |
| `BT-10` | Tarea técnica | Fijar la vigencia del acceso firmado | — | Criterio en `Backlog-Tecnico.md` | `05` §11 `PA-04`; `ADR-03`. **La fuente dice «corta» y no fija número**, ver bloque 6 |
| `US-26` | Historia | Conectar cada puerto con su adaptador y tomar la configuración | `CU-10` | **3** criterios `Given/When/Then` en su §3 | `05` §3.1, «Composición de raíz»; `ADR-06` |
| `US-27` | Historia | Aplicar las transformaciones de esquema al arrancar | `CU-11` | **3** criterios `Given/When/Then` en su §3 | `05` §4; `ADR-07` |
| `US-28` | Historia | Detener el arranque en lugar de atender sobre un almacén dudoso | `CU-11` | **3** criterios `Given/When/Then` en su §3 | `05` §4; `ADR-07` |
| `US-29` | Historia | Responder por el estado del servicio sin exigir acceso | `CU-11` | **3** criterios `Given/When/Then` en su §3 | `05` §3.4, punto `A-16`; `ADR-07` |

**Orden de despacho recomendado**, del orden topológico del manifiesto §3: los tres de nivel 0 en paralelo, después `Application` y `Web`, después `Infrastructure`, y `Api` al final. La etapa `a` de `Api` depende de que existan los ensamblados que su composición de raíz conecta.

---

## 5. Audits aprobados

Contados con `ls SDD/Docs/Audit/`: **32** informes. Cada veredicto se leyó abriendo la sección de dictamen del propio informe.

| Fase | Proyecto de código | Ronda | Veredicto | Informe |
| --- | --- | --- | --- | --- |
| A (00, 01) | Nivel producto | r1 | APROBADO CON OBSERVACIONES | [`Audit/A-00-01-r1.md`](Audit/A-00-01-r1.md) |
| A (00, 01) | Nivel producto | r2 | APROBADO CON OBSERVACIONES | [`Audit/A-00-01-r2.md`](Audit/A-00-01-r2.md) |
| A (00, 01) | Nivel producto | r3 | APROBADO CON OBSERVACIONES | [`Audit/A-00-01-r3.md`](Audit/A-00-01-r3.md) |
| B (02, 03) | `GeometriaFactory-Domain` | r1 | APROBADO CON OBSERVACIONES | [`Audit/B-02-03-GeometriaFactory-Domain-r1.md`](Audit/B-02-03-GeometriaFactory-Domain-r1.md) |
| B (02, 03) | `GeometriaFactory-Domain` | r2 | APROBADO CON OBSERVACIONES | [`Audit/B-02-03-GeometriaFactory-Domain-r2.md`](Audit/B-02-03-GeometriaFactory-Domain-r2.md) |
| B (02, 03) | `GeometriaFactory-Domain` | r3 | APROBADO CON OBSERVACIONES | [`Audit/B-02-03-GeometriaFactory-Domain-r3.md`](Audit/B-02-03-GeometriaFactory-Domain-r3.md) |
| B (02, 03) | `GeometriaFactory-Contracts` | r1 | APROBADO CON OBSERVACIONES | [`Audit/B-02-03-GeometriaFactory-Contracts-r1.md`](Audit/B-02-03-GeometriaFactory-Contracts-r1.md) |
| B (02, 03) | `GeometriaFactory-Contracts` | r2 | APROBADO CON OBSERVACIONES | [`Audit/B-02-03-GeometriaFactory-Contracts-r2.md`](Audit/B-02-03-GeometriaFactory-Contracts-r2.md) |
| B (02, 03) | `GeometriaFactory-Contracts` | r3 | APROBADO CON OBSERVACIONES | [`Audit/B-02-03-GeometriaFactory-Contracts-r3.md`](Audit/B-02-03-GeometriaFactory-Contracts-r3.md) |
| B (02, 03) | `GeometriaFactory-Visor` | r1 | 12 hallazgos (0 P0, 0 P1, 3 P2, 9 P3); condiciones en su §6 | [`Audit/B-02-03-GeometriaFactory-Visor-r1.md`](Audit/B-02-03-GeometriaFactory-Visor-r1.md) |
| B (02, 03) | `GeometriaFactory-Visor` | r2 | Los doce hallazgos de r1 **cerrados** | [`Audit/B-02-03-GeometriaFactory-Visor-r2.md`](Audit/B-02-03-GeometriaFactory-Visor-r2.md) |
| B (02, 03) | `GeometriaFactory-Application` | r1 | **RECHAZADO** (un P0) | [`Audit/B-02-03-GeometriaFactory-Application-r1.md`](Audit/B-02-03-GeometriaFactory-Application-r1.md) |
| B (02, 03) | `GeometriaFactory-Application` | r2 | APROBADO CON OBSERVACIONES | [`Audit/B-02-03-GeometriaFactory-Application-r2.md`](Audit/B-02-03-GeometriaFactory-Application-r2.md) |
| B (02, 03) | `GeometriaFactory-Infrastructure` | r1 | **RECHAZADO** | [`Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md`](Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md) |
| B (02, 03) | `GeometriaFactory-Infrastructure` | r2 | **APROBADO**, con tres P3 a absorber | [`Audit/B-02-03-GeometriaFactory-Infrastructure-r2.md`](Audit/B-02-03-GeometriaFactory-Infrastructure-r2.md) |
| B (02, 03) | `GeometriaFactory-Web` | r1 | APROBADO CON OBSERVACIONES; la Fase B2 puede arrancar | [`Audit/B-02-03-GeometriaFactory-Web-r1.md`](Audit/B-02-03-GeometriaFactory-Web-r1.md) |
| B (02, 03) | **`GeometriaFactory-Api`** | r1 | **RECHAZADO** (un P0, cinco P1, seis P2, cinco P3); emitido **tardíamente** y declarándolo en su §0 | [`Audit/B-02-03-GeometriaFactory-Api-r1.md`](Audit/B-02-03-GeometriaFactory-Api-r1.md) |
| B2 (maqueta) | `GeometriaFactory-Web` | r1 | **RECHAZADO** por un P0; condiciones de corrección puntual en su §8 | [`Audit/B2-Maqueta-GeometriaFactory-Web-r1.md`](Audit/B2-Maqueta-GeometriaFactory-Web-r1.md) |
| B2 (maqueta) | `GeometriaFactory-Web` | r2 | **APROBADO** — se levanta el rechazo de r1; cuatro hallazgos nuevos, ninguno P0 | [`Audit/B2-Maqueta-GeometriaFactory-Web-r2.md`](Audit/B2-Maqueta-GeometriaFactory-Web-r2.md) |
| C (05) | Los siete | r1 | **RECHAZADO** (tres P1) | [`Audit/C-05-Arquitectura-Siete-Proyectos-r1.md`](Audit/C-05-Arquitectura-Siete-Proyectos-r1.md) |
| C (05) | Los siete | r2 | **APROBADO** | [`Audit/C-05-Arquitectura-Siete-Proyectos-r2.md`](Audit/C-05-Arquitectura-Siete-Proyectos-r2.md) |
| D (06, 07) | Los siete | r1 | **APROBADO** | [`Audit/D-06-07-Backlog-Siete-Proyectos-r1.md`](Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) |
| E (08) | Los siete | r1 | **RECHAZADO** (sin P0; la cobertura cierra) | [`Audit/E-08-Calidad-Siete-Proyectos-r1.md`](Audit/E-08-Calidad-Siete-Proyectos-r1.md) |
| E (08) | Los siete | r2 | **APROBADO** — se levanta el rechazo | [`Audit/E-08-Calidad-Siete-Proyectos-r2.md`](Audit/E-08-Calidad-Siete-Proyectos-r2.md) |
| F (09) | Los siete | r1 | **APROBADO** (cinco hallazgos, ninguno P0 ni P1) | [`Audit/F-09-Devops-Siete-Proyectos-r1.md`](Audit/F-09-Devops-Siete-Proyectos-r1.md) |
| G (10) | Los siete | r1 | **RECHAZADO** (P0-1) | [`Audit/G-10-Examples-Siete-Proyectos-r1.md`](Audit/G-10-Examples-Siete-Proyectos-r1.md) |
| G (10) | Los siete | r2 | **APROBADO**, con un P3 nuevo y dos observaciones | [`Audit/G-10-Examples-Siete-Proyectos-r2.md`](Audit/G-10-Examples-Siete-Proyectos-r2.md) |
| Transversal (F-26) | Los siete | r1 | **RECHAZADO** | [`Audit/F26-Propagacion-r1.md`](Audit/F26-Propagacion-r1.md) |
| Transversal (F-26) | Los siete | r2 | **APROBADO** — se levanta el rechazo | [`Audit/F26-Propagacion-r2.md`](Audit/F26-Propagacion-r2.md) |
| Transversal (coherencia) | Corpus completo | r1 | **RECHAZADO** (dos P0) | [`Audit/Coherencia-Corpus-r1.md`](Audit/Coherencia-Corpus-r1.md) |
| Transversal (coherencia) | Corpus completo | r2 | **APROBADO** — los doce hallazgos de r1 cerrados | [`Audit/Coherencia-Corpus-r2.md`](Audit/Coherencia-Corpus-r2.md) |
| H (final) | Corpus completo | r1 | **APTO PARA HANDOFF** — cero P0 y cero P1 | [`Audit/H-Final-Consolidado-r1.md`](Audit/H-Final-Consolidado-r1.md) |

**Estado de las fases, en una línea.** Todas las fases con dictamen tienen su última ronda aprobada **salvo una**: la **Fase B de `GeometriaFactory-Api`**, cuyo informe se emitió el 2026-08-11 —tarde, y declarándolo— con dictamen **RECHAZADO**. La **Fase B2 de la maqueta de `GeometriaFactory-Web`** dejó de estar en esa lista: su ronda 2 se emitió el mismo día y **levanta el rechazo**. La que queda va al bloque 6.

---

## 6. Decisiones pendientes

**Este es el bloque que le importa a quien empiece a construir.** Lo que sigue es lo que se va a chocar el primer día. Cada fila dice qué falta decidir, quién es el titular y **qué se rompe si se empieza a construir sin decidirlo**.

Los puntos abiertos se leyeron abriendo la §11 de las siete `Arquitectura-Proyecto-Codigo.md`, la §8 de [`README.md`](README.md), la §7 de [`Producto/11-Documentacion/README.md`](Producto/11-Documentacion/README.md) y la §22 del intake. Contados: **47 puntos abiertos `PA-XX` en las siete categorías 05** —Api 10, Infrastructure 11, Web 7, Application 6, Visor 5, Contracts 4, Domain 4—, de los cuales **3 están declarados RESUELTOS** (`Api` `PA-10`, `Contracts` `PA-03`, `Infrastructure` `PA-08`) y **44 siguen vivos**; **7** de nivel producto; y **4** asunciones vivas en el intake (`A-2` a `A-5`).

### 6.1 Bloqueos de proceso, antes que de contenido

| # | Qué falta | Titular | Qué se rompe si se construye sin esto |
| --- | --- | --- | --- |
| `B-1` | ~~**El informe de auditoría de Fase B de `GeometriaFactory-Api`.**~~ **CERRADO EN SU PARTE DE PROCESO, ABIERTO EN SU DICTAMEN.** El informe faltante se emitió el 2026-08-11: [`Audit/B-02-03-GeometriaFactory-Api-r1.md`](Audit/B-02-03-GeometriaFactory-Api-r1.md), y ya son siete para siete. Su dictamen es **RECHAZADO**, con quince hallazgos —un P0, cinco P1, seis P2 y cinco P3— **todos de recuento y de cita, y ninguno sobre una decisión de contrato**, de modo que lo que la ronda 1 pedía no obliga a rehacer nada de lo construido encima. Lo que queda abierto es **la ronda 2 que levante el rechazo** | Orquestador SDD | Ya no es cierto que las categorías 02 y 03 del proyecto de código principal nunca se auditaron: se auditaron, tarde, y el informe declara en su §0 qué pudo y qué no pudo observar por llegar después de las fases C a H. Lo que se rompe si se construye sin cerrar esto es menor de lo que esta fila decía: mientras el rechazo no se levante, la especificación del proyecto de código principal se lee con quince correcciones declaradas encima |
| `B-2` | ~~**La Fase B2 de la maqueta de `GeometriaFactory-Web` tiene un solo informe y su veredicto es RECHAZADO.**~~ **CERRADO.** La redacción original era correcta al escribirse. El 2026-08-11 se emitió [`Audit/B2-Maqueta-GeometriaFactory-Web-r2.md`](Audit/B2-Maqueta-GeometriaFactory-Web-r2.md), con dictamen **APROBADO** y levantando expresamente el rechazo de la ronda 1 | Orquestador SDD y Product Owner | El P0 de r1 está cerrado **por recuento propio del auditor y no por declaración**: la cobertura de la matriz de sensado da 211 de 211. La línea de base del bloque 8 mide contra una fase aprobada. La ronda 2 deja cuatro hallazgos nuevos de corrección puntual —1 P1, 2 P2, 1 P3—, **ninguno de ellos condición de bloqueo**, y el más importante, `NB2-03`, es un hueco de validación de `RN-16` que se declara y se cierra en la iteración 5 de maqueta |
| `B-3` | **El intake está en estado `Borrador`** (cabecera de `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.26), mientras el manifiesto derivado está `Aprobado` y todo el corpus se derivó de él | Product Owner | Es la fuente de verdad del producto. Un documento fuente en borrador puede seguir cambiando, y cada cambio suyo dispara propagación: el propio corpus registra **veintiséis versiones** del intake, varias de ellas correcciones que obligaron a re-tocar documentos de cinco fases |
| `B-4` | **Ningún documento del corpus está en estado `Aprobado`**: 589 dicen `Propuesto` y 166 `Propuesta`, y cero dicen `Aprobado` | Product Owner | Se entra a construcción contra una especificación que formalmente nadie firmó. No impide arrancar, pero deja sin punto de corte la pregunta «¿contra qué versión se construyó esto?» |
| `B-5` | **Los tres artefactos de la Fase H marcados como pendientes por `H-Final-Consolidado-r1.md` §4** siguen sin corregir: las dos líneas de `P2-1`, la unificación del estado de los ocho README de la categoría 11 (`P2-2`) y las cuatro filas de tabla de `P3-1` | Orquestador SDD | `P2-2` está verificado abierto: los ocho README de la categoría 11 declaran `status: Planificado` en su encabezado estructurado, `Estado: Propuesto` en su cabecera de prosa y `Vigente` en su propia fila de la tabla de artefactos. Tres estados distintos para el mismo documento |

### 6.2 Decisiones de arquitectura sin cerrar

Los `PA-XX` que siguen vivos y que **bloquean o condicionan** trabajo de la etapa `a`, que es el primer tramo que se va a construir.

| # | Qué falta decidir | Dónde está declarado | Titular | Qué se rompe si se construye sin esto |
| --- | --- | --- | --- | --- |
| `A-1` | **El nombre del cuarto puerto**, el de repositorio de cuentas. El puerto existe y su identificador no está fijado | `Application/05` §11 `PA-01`; `Infrastructure/05` §11 `PA-01` | El equipo, en el punto de control de la etapa `a` | Es un nombre de tipo público de `GeometriaFactory-Application` que `GeometriaFactory-Infrastructure` implementa y `GeometriaFactory-Api` conecta en su composición de raíz. Elegirlo mal y cambiarlo después toca tres proyectos de código y su documentación |
| `A-2` | **Los nombres definitivos de tipos y de espacios de nombres**, abiertos en **los siete** proyectos de código | `PA-01`/`PA-02`/`PA-05`/`PA-07` de las siete categorías 05, según el proyecto | El equipo, en el punto de control de la etapa `a` | Es literalmente la primera línea de código de cada proyecto. Además, mientras estén abiertos, **ningún `Recorrido-Codigo.md` de la categoría 11 puede escribir una ruta verificable** ([`Producto/11-Documentacion/README.md`](Producto/11-Documentacion/README.md) §7) |
| `A-3` | **Cuál de las dos funciones de derivación de clave se ancla**, y con qué parámetros. El intake declara «PBKDF2 o Argon2» y **no elige** | `Infrastructure/05` §11 `PA-03`; `ADR-04` §7 | Product Owner y equipo, en la etapa `a` | Es `BT-03` de la etapa `a` de `Infrastructure`. La credencial derivada se guarda con sus parámetros versionados; cambiar la función después de tener cuentas creadas obliga a una migración de credenciales |
| `A-4` | **Las rutas y los verbos definitivos de los quince puntos de acceso.** Las únicas dos cosas que la fuente declara son el punto de canje de credenciales y la **existencia** de un punto de salud. Las quince filas son propuesta derivada rotulada fila por fila | `Api/05` §11 `PA-01`; `Api/05` §3.4 | Product Owner y equipo, en el punto de control de la etapa `a` | Es `BT-07` de la etapa `a` de `Api`. Todo el cliente HTTP de `GeometriaFactory-Web`, la colección de peticiones de `CU-12` y las tres sondas `VER-XX` de `Api` se escriben contra esas rutas |
| `A-5` | **El formato de intercambio y su configuración** —nombres de campos al serializar, tratamiento de valores ausentes—. **No se puede decidir de un solo lado**: los dos extremos tienen que coincidir o el contrato deja de ser el mismo | `Web/05` §11 `PA-03`; `Api` `ADR-02`; `Contracts/05` §11 `PA-03` (declarado resuelto por reasignación) | Las categorías 05 de `Api` y de `Web`, en conjunto | Es `BT-08` de `Api` y `BT-12` de `Web`, las dos en la etapa `a`. Si cada extremo elige por su cuenta, el front deserializa mal lo que el backend serializa bien, y el síntoma aparece en integración y no en compilación |
| `A-6` | **La vigencia exacta del acceso firmado.** El intake declara «corta» y sin acceso de refresco, y no fija número | `Api/05` §11 `PA-04`; `ADR-03` | El equipo en la etapa `a`, y el Product Owner | Es `BT-10` de la etapa `a` de `Api`. Un valor puesto sin criterio expulsa al alumno en medio de una clase o deja un acceso vivo mucho después |
| `A-7` | **El valor del límite de tamaño del cuerpo de una petición.** `ADR-02` fija la **forma** —un solo límite para todo el producto, que rechaza y nunca trunca— y deja el número abierto | `Api/05` §11 `PA-05` | El equipo en la etapa `a` | Es `BT-09` de la etapa `a` de `Api`. Un límite bajo rechaza el JSON del escenario `E-1`, que es el más grande que la fuente documenta |
| `A-8` | **Si el bundle generado del visor se versiona en el repositorio o se ignora.** El intake admite las dos | `Visor/05` §11 `PA-05`; `Web/05` §11 `PA-07` | La categoría 09 | Es `BT-03` de la etapa `a` de `Visor`. Alcanza también a `Web`, porque el bundle vive en su directorio de recursos estáticos. Decidirlo después obliga a reescribir el flujo de publicación de dos proyectos de código |
| `A-9` | **El criterio de comparación de dos correos** —tal cual o normalizados—, que la unicidad exige decidir. `Domain` lo dejó abierto, `Application` no lo reabrió y `Infrastructure` lo tiene asignado | `Application/05` §11 `PA-03`; `Domain/11` §6 | La capa que ejerce la verificación, antes de cerrar la etapa `d` | `RN-02` («correo del alumno único») no es verificable sin este criterio. Dos alumnos pueden registrarse con el mismo correo escrito distinto |
| `A-10` | **Los sellos de alta, de modificación y de desenlace**: el intake los sostiene como verificables en prueba, pero el modelo del dominio **no los declara como atributos** | `Application/05` §11 `PA-04` | Product Owner, y `GeometriaFactory-Domain` si decide incorporarlos | Si son atributos del dominio, entran en las entidades y en el esquema de persistencia. Descubrirlo después de crear el esquema obliga a una transformación de esquema sobre datos reales |
| `A-11` | **La ambigüedad del intake sobre `RN-12` e `INV-09`**: su columna de reglas sostenidas y su prosa dicen cosas distintas. La categoría 02 adoptó la columna y elevó la consolidación; la 05 hereda esa lectura sin resolverla | `Domain/05` §11 `PA-03` | Product Owner, sobre `PRODUCT-INTAKE` §17.1.P.2 | Es una regla del dominio con dos lecturas. Se construye una de las dos y hay una probabilidad de la mitad de construir la equivocada |
| `A-12` | **El alcance de la colección de peticiones reproducible**, declarado en dos lugares de la fuente con alcances distintos: el intake §16.1 dice «los escenarios `E-1` a `E-8` como cuerpo» —ocho— y §18 `S-2` dice «con los cuerpos de `E-2` y `E-5`» —dos— | `Api/05` §11 `PA-06`; `Api/10-Examples/README.md` §4 | Product Owner | Cambia el alcance de `VER-02` de `Api` y de la fila `SD-02` de su matriz de sensado. **`H-Final-Consolidado-r1.md` §6 declara que no abrió los dos lugares de la fuente**, de modo que la contradicción está reportada pero no verificada por la auditoría final |
| `A-13` | **Hasta dónde llega el conjunto de tipos de figura reconstruibles.** Los seis que los escenarios ejercitan son los que la pieza que dibuja sabe dibujar; el análisis del que sale el intake menciona siete clases en un ejemplo y diez en el otro, y **ninguna fuente las enumera** | `Infrastructure/05` §11 `PA-04` | Product Owner | Un tipo fuera del conjunto produce error de validación, que es correcto pero puede no ser lo deseado. Si el docente da una actividad con un séptimo tipo, el trabajo del alumno queda en `Borrador` sin que nadie lo haya decidido |
| `A-14` | **La condición derivada `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`**, que **ninguna fuente enuncia** y que la categoría 02 declaró con su fundamento | `Infrastructure/05` §11 `PA-10` | Product Owner, para confirmarla o reemplazarla | Es una condición de error que el sistema va a producir y que ninguna fuente pidió. Si el Product Owner la reemplaza, cambia el flujo de ejecución del validador |
| `A-15` | **La frecuencia del respaldo.** El intake la declara explícitamente «a definir por el docente» | `Infrastructure/05` §11 `PA-07` | Product Owner, y `09-Devops` | No bloquea construcción, sí bloquea puesta en producción: es una base SQLite con los trabajos de una comisión entera |
| `A-16` | **El umbral numérico de fluidez de la interacción del visor.** Ninguna fuente lo declara y ninguna categoría lo inventa | `Visor/05` §11 `PA-03` | Product Owner, o la categoría 08 al fijar su guion de medición | `PT-02` y `PT-03` se miden antes de comprometer la etapa `g`. Sin umbral, la puerta técnica se verifica «a ojo» |
| `A-17` | **La versión del motor de dibujo tridimensional** que se adopta | `Visor/05` §11 `PA-01` | El equipo, al implementar la capa 3 | Si es posterior a la del visualizador previo, hay un cambio de interfaz que documentar. Descubrirlo con el bundle a medio hacer cuesta reescritura |
| `A-18` | **El punto de quiebre principal en 768 px** y la **proporción próxima a 4:3 de la escena**, los dos rotulados `[ASUNCIÓN]` por la categoría 03 | `Web/05` §11 `PA-05` | Product Owner, sobre la línea de base visual | Están en la línea de base contra la que se mide deriva. Si no se confirman, la matriz de sensado marca deriva sobre valores que nadie fijó |
| `A-19` | **El volumen de la comisión**, `[A VERIFICAR]`: los dos listados suponen decenas y no cientos, y por eso **no incorporan paginación** | `Web/05` §11 `PA-06` | Product Owner, antes de comprometer la etapa `e` | Si resultara mucho mayor, hay que agregar paginación a `Listado-De-La-Comision`, que es una superficie de la línea de base |
| `A-20` | **Qué código del contrato recibe una operación de administrador pedida por quien no lo es** fuera del desenlace, y **qué código recibe un envío o reedición forzados fuera de `Borrador`**. El conjunto cerrado no tiene código para ninguno de los dos, y la categoría 05 **no inventa códigos** | `Api/05` §11 `PA-02` y `PA-03` | Product Owner, y `GeometriaFactory-Contracts` | Dos huecos del conjunto cerrado de quince códigos. Hoy se resuelven con el genérico. Si el Product Owner agrega códigos después, cambia el conjunto cerrado, que es una superficie pública con regla de no reciclado |
| `A-21` | **La zona horaria y la precisión del campo de momento** del tipo de error. Ninguna fuente las declara | `Contracts/05` §11 `PA-02` | El equipo, junto con la elección del formato de intercambio | Es un campo de un DTO público. Cambiarlo después es un cambio mayor del contrato |

### 6.3 Marcas `[A VERIFICAR]` y `[ASUNCIÓN]` que condicionan puertas

Contadas con `grep -o` sobre el corpus vivo: **50** ocurrencias de `[A VERIFICAR]` y **244** de `[ASUNCIÓN]`.

| # | Qué está sin confirmar | Dónde | Titular | Qué se rompe |
| --- | --- | --- | --- | --- |
| `V-1` | **Las coberturas mínimas** —90/85 en Domain, 85/80 en Application, 85/80 con 95 en el validador de Infrastructure, 75/70 en Api— | Intake §22 `A-3`; `PA` de coberturas en las siete categorías 05 | Product Owner sobre su propio documento | Son gates **bloqueantes** del pipeline. Se van a fijar antes de que exista una sola prueba, y si el número es el equivocado o bloquea sin sentido o no bloquea nada |
| `V-2` | **Los NFR numéricos** —500 ms de validación en Application, 200 ms en el validador, p99 de 500 ms y 20 peticiones por minuto en la Api, 30 s de arranque en frío, 10 s de la batería de dominio— | Intake §22 `A-5` | Product Owner | Son lo que la categoría 08 verifica como NFR-tests. `QG-14` de `Api` ya está declarado **condicionado** por esto |
| `V-3` | **Los gates no basados en cobertura** de Contracts, Web y Visor | Intake §22 `A-4` | Product Owner | Cambia la forma del gate, no su carácter bloqueante |
| `V-4` | **La versión de plataforma que soporta el hosting** del front, `[A VERIFICAR]` en la fuente. Es `PT-01.a` | `Web/05` §11 `PA-02` | Se resuelve **midiendo**, en la etapa `a` | Si no pasa, la salida declarada es bajar la versión objetivo del front y no la del backend. Es una medición que hay que hacer antes de escribir el front, no después |
| `V-5` | **La versión exacta de la biblioteca de componentes de interfaz**, `[A VERIFICAR]` | `Web/05` §11 `PA-01` | El equipo, al crear el andamiaje | Es `BT-02` de la etapa `a` de `Web` |
| `V-6` | **La construcción de la imagen en destino desde el repositorio**, rotulada `[A VERIFICAR]` por el intake, que exige probarla una vez antes de depender del mecanismo | `Api/05` §11 `PA-08` | `09-Devops`, midiendo, antes de la etapa de despliegue real | Si el motor de contenedores del destino no resuelve la referencia al repositorio, el mecanismo de despliegue entero cambia |
| `V-7` | **Los targets de las cuatro métricas de negocio** —8 de 8 etapas, ≥ 80 % de alumnos que entregan, 100 % de entregas revisadas, ≥ 1 advertencia por alumno— | Intake §22 `A-2` | Product Owner | Sólo cambia el intake §8 y lo que la categoría 01 derive de ahí. **No bloquea construcción** |

### 6.4 Discrepancias de la fuente que ninguna fase se atribuyó resolver

| # | Qué discrepa | Titular | Qué se rompe |
| --- | --- | --- | --- |
| `X-1` | **Cuántas aristas de compilación tiene el producto.** El manifiesto declara ocho en su §2, dibuja siete en su §3 y valida siete en su §4. **Yo conté ocho** sobre la columna «Dependencias» de §2 | Product Owner, sobre el manifiesto | El grafo de §3 **no dibuja la arista `GeometriaFactory-Application` → `GeometriaFactory-Api`**, que la fila de `Api` de §2 sí declara. Quien arme el archivo de proyecto de `Api` leyendo §3 no va a poner esa referencia, y va a depender de que llegue transitivamente por `Infrastructure`. `H-Final-Consolidado-r1.md` §5 lo enuncia como uno de los puntos donde el constructor se detiene |
| `X-2` | ~~**El estado de la Fase B2**: aprobada según el manifiesto §5 y `Linea-Base-Visual.md`, rechazada según el único informe de auditoría que existe de ella~~ **CERRADO.** Las dos declaraciones dicen hoy lo mismo: la fase está aprobada por el Product Owner y por su ronda 2 de auditoría | Orquestador SDD y Product Owner | Ver `B-2` |

**Ninguno de los puntos de 6.2 a 6.4 impide arrancar la etapa `a`**, y la mayoría están atados precisamente a su punto de control, que es donde corresponde cerrarlos. Los de 6.1 sí son de otra naturaleza: son huecos de proceso. `B-1` y `B-2` eran los dos que un equipo que arranque no podía cerrar por su cuenta, y **los dos informes que faltaban se emitieron el 2026-08-11**: `B-2` queda cerrado y `B-1` queda cerrado en su parte de proceso, con su ronda 2 pendiente.

---

## 7. Flags activos

Valores finales, leídos de [`../Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md`](../Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md) 1.3 §5. **Los flags quedaron inmutables desde la confirmación del Product Owner del 2026-08-08** (`Master-Prompt.md` §4). Un cambio posterior obliga a retroceder a la fase más temprana afectada.

### 7.1 Flags de producto

| Flag | Valor final | Origen y efecto |
| --- | --- | --- |
| `equipo_n` | **1** | Intake §2. Efecto verificado: la categoría 07 emite únicamente `Mini-Plan.md` en los siete proyectos de código, y no existe ningún `Plan-Iteracion-Sprint-XX.md`, `Template-Sprint-Review.md`, `Template-Sprint-Retrospectiva.md` ni `Velocidad-Equipo.md` |

### 7.2 Flags por proyecto de código

| Proyecto de código | `usa_llm` | `tiene_ui_final` | `multi_tenant` | `tiene_auth` | `tiene_portal_developers` | `tiene_extensibilidad` | `tiene_persistencia` | `requiere_compliance` | `tiene_observabilidad_critica` | `requiere_maqueta` |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| `GeometriaFactory-Domain` | false | false (DX) | false | **true** | false | false | false | false | false | false |
| `GeometriaFactory-Contracts` | false | false (DX) | false | false | false | false | false | false | false | false |
| `GeometriaFactory-Visor` | false | false (DX) | false | false | false | **true** | false | false | false | **true** |
| `GeometriaFactory-Application` | false | false (DX) | false | **true** | false | false | false | false | false | false |
| `GeometriaFactory-Web` | false | **true** (UX/UI) | false | **true** | false | false | false | false | false | **true** |
| `GeometriaFactory-Infrastructure` | false | false (DX) | false | **true** | false | false | **true** | false | false | false |
| `GeometriaFactory-Api` | false | false (DX) | false | **true** | false | false | **true** | false | **true** | false |

**Efectos verificados con herramienta:**

- `usa_llm` false en los siete → **no existe ningún directorio `04-*`** en el corpus.
- `tiene_extensibilidad` true sólo en `Visor` → es el único con `Guia-Extension.md` en su plan de categoría 11.
- `requiere_maqueta` true en `Web` y `Visor` → existe `../Maquetas/GeometriaFactory-Web/`, y **no existe** `../Maquetas/GeometriaFactory-Visor/`, coherente con lo que el manifiesto §5 declara: hubo **una sola maqueta** y la validación de la fachada del visor se integró en ella por decisión del Product Owner. Los tres artefactos de línea de base viven en la categoría 03 de `Web` y no están duplicados en la de `Visor`.
- `tiene_ui_final` true sólo en `Web` → es el único con `Wireframes-*.md`: **once** archivos.

---

## 8. Línea de base y sensado de deriva

`requiere_maqueta` es true en **dos** proyectos de código. Ejecutaron la Fase B2 los dos, con **una sola maqueta**.

### 8.1 Maqueta aprobada

**Ruta:** [`../Maquetas/GeometriaFactory-Web/`](../Maquetas/GeometriaFactory-Web/)

Contenido verificado con `find`: **17 archivos**, entre ellos `index.html`, **once** archivos HTML de superficie —`Aprovisionamiento-Inicial`, `Registro-De-Cuenta`, `Ingreso`, `Credencial-Propia`, `Panel-De-Trabajos-Del-Alumno`, `Envio-De-Trabajo`, `Vista-De-Trabajo`, `Resolucion-Del-Trabajo`, `Panel-De-Cuentas`, `Listado-De-La-Comision` y `Estado-Degradado-Y-Reconexion`—, el directorio `assets/` y un `README.md`.

Aprobada por el Product Owner el **2026-08-09**, tras **cuatro iteraciones** registradas en [`Proyectos/GeometriaFactory-Web/03-UX-UI-DX/Bitacora-Validacion-Maqueta.md`](Proyectos/GeometriaFactory-Web/03-UX-UI-DX/Bitacora-Validacion-Maqueta.md). **La fase está auditada y aprobada**: [`Audit/B2-Maqueta-GeometriaFactory-Web-r1.md`](Audit/B2-Maqueta-GeometriaFactory-Web-r1.md) la rechazó por un P0 y [`Audit/B2-Maqueta-GeometriaFactory-Web-r2.md`](Audit/B2-Maqueta-GeometriaFactory-Web-r2.md), del 2026-08-11, **levanta ese rechazo** tras recontar la cobertura de la matriz de sensado por extracción propia. La salvedad `B-2` del bloque 6 queda cerrada; sus cuatro hallazgos nuevos son de corrección puntual y no condicionan el dictamen.

### 8.2 Elementos de la línea de base, por tipo

Artefacto canónico: [`Proyectos/GeometriaFactory-Web/03-UX-UI-DX/Linea-Base-Visual.md`](Proyectos/GeometriaFactory-Web/03-UX-UI-DX/Linea-Base-Visual.md) 1.4. Contados con `grep -o '<TIPO>-[0-9][0-9]' | sort -u | wc -l` sobre ese archivo.

| Tipo | Cantidad contada | Sección del documento |
| --- | --- | --- |
| `SUP` — superficies | **11** | §2 |
| `CMP` — componentes | **73** | §3 |
| `EST` — estados | **74** | §4 |
| `NAV` — rutas de navegación | **24** | §5 |
| `DM` — elementos de modelo de datos | **0** | No emite. El documento no tiene sección de `DM-XX`: el modelo de datos vive en `GeometriaFactory-Infrastructure`, que no ejecutó Fase B2 |

**Total: 182 elementos identificados.** Coincide con la declaración de §1 del propio documento (11, 73, 74 y 24). Ninguna fila está `Retirado`: todas nacen vigentes.

Los otros dos artefactos de la Fase B2, emitidos en la misma categoría 03 de `Web` y **no duplicados** en la de `Visor`: [`Contrato-Datos-Maqueta.md`](Proyectos/GeometriaFactory-Web/03-UX-UI-DX/Contrato-Datos-Maqueta.md) y [`Bitacora-Validacion-Maqueta.md`](Proyectos/GeometriaFactory-Web/03-UX-UI-DX/Bitacora-Validacion-Maqueta.md).

### 8.3 Matrices de sensado de deriva

Contadas con `grep -cE '^\| \`SD-[0-9]{2}\`'` sobre cada `Matriz-Sensado-Deriva.md`.

| Proyecto de código | Matriz | Filas `SD-XX` | Estado de cada fila |
| --- | --- | --- | --- |
| `GeometriaFactory-Web` | [`Proyectos/GeometriaFactory-Web/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](Proyectos/GeometriaFactory-Web/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) | **62** | **Las 62 en `Sin verificar`** |
| `GeometriaFactory-Visor` | [`Proyectos/GeometriaFactory-Visor/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](Proyectos/GeometriaFactory-Visor/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) | **15** | **Las 15 en `Sin verificar`** |

La matriz de `Visor` incluye además una tabla de **8** correspondencias que relacionan sus `SD-XX` con los de `Web` —`SD-01`↔`SD-43`, `SD-02`↔`SD-43`, `SD-03`↔`SD-47`, `SD-06`↔`SD-39` y `SD-40`, `SD-07`↔`SD-41` y `SD-45`, `SD-09`↔`SD-18`, `SD-11`↔`SD-44`, `SD-46` y `SD-48`, `SD-12`↔`SD-42`—; esas filas reutilizan identificadores ya contados y no suman sondas.

**Los cinco proyectos de código que no ejecutaron Fase B2 también emitieron matriz de sensado**, con **3 filas cada uno**, todas en `Sin verificar`: `Api`, `Application`, `Contracts`, `Domain` e `Infrastructure`. Sus sondas no nacen de una línea de base visual sino de los contratos `VER-XX` de la categoría 10. **Total del producto: 92 filas de sensado, ninguna verificada.**

**Ninguna fila de ninguna matriz tiene evidencia**, porque no hay código. Este es el instrumento que el equipo se lleva al ciclo de codificación para verificar, sprint a sprint, que lo construido sigue siendo lo aprobado.

---

## 9. Plan documental de la categoría 11

Es el **Momento 1** del modelo de documentación viva: el índice de qué artefactos va a tener cada proyecto de código, a qué rol de intervención sirve cada uno y en qué estado está. **Nada de esto está redactado.**

Contado sobre la §2 de cada plan de proyecto de código y la §3 del plan de producto.

| Ámbito | Artefactos planificados | Rol de intervención | Estado | Plan |
| --- | --- | --- | --- | --- |
| Nivel producto | **7** (incluido el `AGENTS.md` de la raíz del repositorio) | Todos, mantenedor, operador, agentes de IA | 1 `Vigente` (el propio README) + **6** `Planificado` | [`Producto/11-Documentacion/README.md`](Producto/11-Documentacion/README.md) |
| `GeometriaFactory-Domain` | **9** | Integrador (obligatorio), mantenedor (obligatorio), operador no aplica | 1 `Vigente` + **8** `Planificado` | [`Proyectos/GeometriaFactory-Domain/11-Documentacion/README.md`](Proyectos/GeometriaFactory-Domain/11-Documentacion/README.md) |
| `GeometriaFactory-Contracts` | **10** | Integrador (obligatorio), mantenedor (obligatorio), operador no aplica | 1 `Vigente` + **9** `Planificado` | [`Proyectos/GeometriaFactory-Contracts/11-Documentacion/README.md`](Proyectos/GeometriaFactory-Contracts/11-Documentacion/README.md) |
| `GeometriaFactory-Visor` | **11** | Integrador (obligatorio), mantenedor (obligatorio, **con `Guia-Extension`**), operador no aplica | 1 `Vigente` + **10** `Planificado` | [`Proyectos/GeometriaFactory-Visor/11-Documentacion/README.md`](Proyectos/GeometriaFactory-Visor/11-Documentacion/README.md) |
| `GeometriaFactory-Application` | **9** | Integrador (obligatorio), mantenedor (obligatorio), operador no aplica | 1 `Vigente` + **8** `Planificado` | [`Proyectos/GeometriaFactory-Application/11-Documentacion/README.md`](Proyectos/GeometriaFactory-Application/11-Documentacion/README.md) |
| `GeometriaFactory-Web` | **6** | Integrador omitido salvo troubleshooting resumido, mantenedor (obligatorio), operador (obligatorio) | 1 `Vigente` + **5** `Planificado` | [`Proyectos/GeometriaFactory-Web/11-Documentacion/README.md`](Proyectos/GeometriaFactory-Web/11-Documentacion/README.md) |
| `GeometriaFactory-Infrastructure` | **9** | Integrador (obligatorio), mantenedor (obligatorio), operador no aplica | 1 `Vigente` + **8** `Planificado` | [`Proyectos/GeometriaFactory-Infrastructure/11-Documentacion/README.md`](Proyectos/GeometriaFactory-Infrastructure/11-Documentacion/README.md) |
| `GeometriaFactory-Api` | **11** | Integrador (obligatorio), mantenedor (obligatorio), operador (obligatorio) | 1 `Vigente` + **10** `Planificado` | [`Proyectos/GeometriaFactory-Api/11-Documentacion/README.md`](Proyectos/GeometriaFactory-Api/11-Documentacion/README.md) |
| **Total** | **72** | — | **8** `Vigente` (los ocho README) + **64** `Planificado` | — |

**El estado de los ocho README no es uno solo, y hay que decirlo.** Cada uno declara `status: Planificado` en su encabezado estructurado, `Estado: Propuesto` en su cabecera de prosa y `Vigente` en su propia fila de la tabla de artefactos. Es el hallazgo `P2-2` de [`Audit/H-Final-Consolidado-r1.md`](Audit/H-Final-Consolidado-r1.md), declarado abierto en su §4 y verificado abierto acá. Ver `B-5` del bloque 6.

**El Momento 2 no puede adelantarse a la etapa `a`.** [`Producto/11-Documentacion/README.md`](Producto/11-Documentacion/README.md) §7 lo declara con su motivo: mientras los nombres de tipos y de espacios de nombres estén abiertos, ningún `Recorrido-Codigo.md` puede escribir una ruta verificable, y la regla dura de ese documento es que toda ruta citada exista.

---

## 10. Contratos de verificación pendientes

**Diecinueve sondas `VER-XX`, emitidas en la Fase G, todas con `evidencia` en `No verificado — sin código`.** Contadas con `grep -o 'VER-[0-9][0-9]' | sort -u` sobre cada `10-Examples/`, y verificada la correspondencia uno a uno contra las carpetas de [`../../samples/`](../../samples/).

Cada contrato vive en la §9 del documento del sample, como bloque `verificacion:` con `id`, `verifica`, `comando`, `precondiciones`, `criterio_aceptacion` y `evidencia`. El `criterio_aceptacion` es siempre `exit_code: 0` más una lista de aserciones literales sobre la salida (`stdout_contiene` / `stdout_no_contiene`, y `http` en el caso de `Api`). La columna «criterio de aceptación» de la tabla resume la cantidad de aserciones, contadas con `grep -c '^ *- "'` sobre el rango del bloque.

| Sonda | Proyecto de código | Qué verifica | Criterio de aceptación | Comando previsto | Evidencia |
| --- | --- | --- | --- | --- | --- |
| `VER-01` | `GeometriaFactory-Domain` | `CU-01`, `CU-02`, `CU-03`, `CU-04`, `CU-12`; `US-01`, `US-04`, `US-06`, `US-24`, `US-27` | `exit_code: 0` + **5** aserciones de salida | `dotnet run --project samples/domain/01-basico` | `No verificado — sin código` |
| `VER-02` | `GeometriaFactory-Domain` | `CU-05` a `CU-08`; `US-09` a `US-16` | `exit_code: 0` + **7** aserciones | `dotnet run --project samples/domain/02-intermedio` | `No verificado — sin código` |
| `VER-03` | `GeometriaFactory-Domain` | `CU-09`, `CU-10`, `CU-11`, `CU-13`; `US-18` a `US-23`, `US-26` | `exit_code: 0` + **6** aserciones | `dotnet run --project samples/domain/03-avanzado` | `No verificado — sin código` |
| `VER-01` | `GeometriaFactory-Contracts` | `CU-01`, `CU-02`; `US-01` a `US-05` | `exit_code: 0` + **5** aserciones | `dotnet run --project samples/contracts/01-basico` | `No verificado — sin código` |
| `VER-02` | `GeometriaFactory-Contracts` | `CU-03`, `CU-04`, `CU-05`; `US-06`, `US-07`, `US-08`, `US-11`, `US-12`, `US-13`, `US-18`, `US-19` | `exit_code: 0` + **6** aserciones | `dotnet run --project samples/contracts/02-intermedio` | `No verificado — sin código` |
| `VER-03` | `GeometriaFactory-Contracts` | `CU-06`, `CU-07`, `CU-08`; `US-14` a `US-17`, `US-21`, `US-22` | `exit_code: 0` + **7** aserciones, entre ellas los **15** códigos vivos sobre **18** emitidos | `dotnet run --project samples/contracts/03-avanzado` | `No verificado — sin código` |
| `VER-01` | `GeometriaFactory-Visor` | `CU-01`, `CU-02`, `CU-05`; `US-01`, `US-04`, `US-07`, `US-08`, `US-11` | `exit_code: 0` + **6** aserciones | `bash scripts/build-visor.sh && npm --prefix samples/visor/01-basico run verify` | `No verificado — sin código` |
| `VER-02` | `GeometriaFactory-Visor` | `CU-02`, `CU-03`, `CU-04`; `US-05`, `US-06`, `US-07`, `US-09`, `US-10` | `exit_code: 0` + **8** aserciones | `bash scripts/build-visor.sh && npm --prefix samples/visor/02-intermedio run verify` | `No verificado — sin código` |
| `VER-03` | `GeometriaFactory-Visor` | `CU-06`, `CU-07`; `US-02`, `US-12`, `US-13`, `US-14` | `exit_code: 0` + **9** aserciones | `bash scripts/build-visor.sh && npm --prefix samples/visor/03-avanzado run verify` | `No verificado — sin código` |
| `VER-01` | `GeometriaFactory-Application` | `CU-01`, `CU-03`, `CU-10`; `US-01`, `US-02`, `US-03`, `US-07`, `US-09`, `US-28`, `US-30`, `US-32` | `exit_code: 0` + **7** aserciones | `dotnet run --project samples/application/01-basico` | `No verificado — sin código` |
| `VER-02` | `GeometriaFactory-Application` | `CU-04`, `CU-05`, `CU-06`, `CU-09`; `US-10` a `US-19`, `US-26` | `exit_code: 0` + **9** aserciones | `dotnet run --project samples/application/02-intermedio` | `No verificado — sin código` |
| `VER-03` | `GeometriaFactory-Application` | `CU-02`, `CU-07`, `CU-08`, `CU-11`; `US-04` a `US-06`, `US-08`, `US-20` a `US-25`, `US-27`, `US-29`, `US-31` | `exit_code: 0` + **9** aserciones | `dotnet run --project samples/application/03-avanzado` | `No verificado — sin código` |
| `VER-01` | `GeometriaFactory-Web` | `CU-05`, `CU-06`, `CU-08`; `US-11`, `US-15`, `US-17`, `US-22`, `US-23` | `exit_code: 0` + **10** aserciones | `bash samples/web/01-datos-seed/run.sh` | `No verificado — sin código` |
| `VER-01` | `GeometriaFactory-Infrastructure` | `CU-01`, `CU-02`; `US-01` a `US-07` | `exit_code: 0` + **10** aserciones | `dotnet run --project samples/infrastructure/01-basico` | `No verificado — sin código` |
| `VER-02` | `GeometriaFactory-Infrastructure` | `CU-03`, `CU-04`, `CU-05`; `US-08` a `US-16` | `exit_code: 0` + **10** aserciones | `dotnet run --project samples/infrastructure/02-intermedio` | `No verificado — sin código` |
| `VER-03` | `GeometriaFactory-Infrastructure` | `CU-06` a `CU-10`; `US-17` a `US-25` | `exit_code: 0` + **15** aserciones | `dotnet run --project samples/infrastructure/03-avanzado` | `No verificado — sin código` |
| `VER-01` | `GeometriaFactory-Api` | `CU-01`, `CU-02`, `CU-09`; `US-01` a `US-06`, `US-24`, `US-25` | `exit_code: 0` + **7** aserciones, con códigos de respuesta HTTP | `bash samples/api/01-basico/run.sh` | `No verificado — sin código` |
| `VER-02` | `GeometriaFactory-Api` | `CU-03` a `CU-08`, `CU-12`; `US-07` a `US-23`, `US-30` | `exit_code: 0` + **12** aserciones. **Su alcance depende de la decisión `A-12` del bloque 6** | `bash samples/api/02-intermedio/run.sh` | `No verificado — sin código` |
| `VER-03` | `GeometriaFactory-Api` | `CU-10`, `CU-11`; `US-26` a `US-29` | `exit_code: 0` + **11** aserciones | `bash samples/api/03-avanzado/run.sh` | `No verificado — sin código` |

**Estado de `/samples/` verificado con `find`: 20 archivos, todos `README.md`.** Las **19** carpetas de sample existen y cada una declara `Estado de esta carpeta: Esqueleto — sin código`. **Ningún `run.sh`, ningún proyecto `.csproj` y ningún `package.json` existe todavía**, y `scripts/build-visor.sh` —precondición de las tres sondas de `Visor`— tampoco. Los comandos de la tabla son **comandos previstos**, no ejecutables hoy.

**Cobertura declarada por las sondas, contada sobre la columna «verifica»:** las 19 sondas cubren los **71** casos de uso del producto y **178** historias. Esto es lo que el equipo se lleva para completar durante la codificación: cada vez que una sonda corra, su campo `evidencia` pasa de `No verificado — sin código` a un veredicto con fecha, y la fila correspondiente de la matriz de sensado del bloque 8 deja de estar en `Sin verificar`.

---

## 11. Divergencias entre lo contado y lo declarado

`Master-Prompt.md` §12 pide un inventario, y un inventario que copia cifras no es un inventario. Estas son las diferencias entre lo que conté con herramienta y lo que otro documento del corpus declara. **No las resuelvo**: las dejo escritas.

| # | Cifra | Lo que conté | Lo que otro documento declara | Dónde |
| --- | --- | --- | --- | --- |
| `D-1` | Aristas de compilación del producto | **8**, sobre la columna «Dependencias» del manifiesto §2 | **7** en el grafo en prosa del manifiesto §3 y **7** en su §4 («las siete aristas resuelven») | `PRODUCT-MANIFEST` §2, §3 y §4. Ya declarado como punto abierto de nivel producto en [`README.md`](README.md) §8 y en `Producto/Vista-Producto.md` §3.1. Es `X-1` del bloque 6 |
| `D-2` | Artefactos de la categoría 11 en estado `Planificado` | **64**, contando filas `Planificado` en los ocho planes | **65** en [`Audit/H-Final-Consolidado-r1.md`](Audit/H-Final-Consolidado-r1.md) §6, punto 3: «los 65 artefactos planificados de la categoría 11, que no existen» | El total de **72** sí coincide: 72 menos los **8** README que están `Vigente` da 64, no 65 |
| `D-3` | Informes de auditoría de Fase B | **7**, uno por proyecto de código, con el de `GeometriaFactory-Api` emitido el 2026-08-11 | El corpus lo declaraba como hueco en [`README.md`](README.md) §8 y en `Producto/Vista-Producto.md` §1.1, **y los dos quedaron actualizados**. **Coincide**: ya no hay divergencia, y la fila se conserva porque era el hueco más caro del bloque 6 | — |
| `D-4` | Estado de los ocho README de la categoría 11 | **Tres valores distintos por documento**: `Planificado` en el encabezado estructurado, `Propuesto` en la cabecera de prosa, `Vigente` en la fila de la tabla de artefactos | `H-Final-Consolidado-r1.md` §4 lo declara como `P2-2` **pendiente de corregir**. **Coincide** y sigue abierto | — |
| `D-5` | Filas de la matriz de sensado de `GeometriaFactory-Visor` | **15** filas `SD-XX` reales | Un `grep` ingenuo de identificadores únicos devuelve **26**, porque la tabla de correspondencias con `Web` reutiliza identificadores de los dos proyectos de código | Se aclara acá para que ninguna ronda posterior lo levante como divergencia |
| `D-6` | Casos límite del intake §7 | **11** filas (`CL-1` a `CL-11`) | El control cruzado de referencia habla de «diez casos de batería», que es otra cosa: son los **10** casos de la batería del validador cruzados en el intake §21, no los casos límite de §7. **Las dos cifras son correctas y refieren a conjuntos distintos** | Se aclara acá para evitar que se confundan |

Los nueve recuentos de control cruzado —71 casos de uso con su reparto por proyecto de código, 16 reglas, 9 invariantes, 8 escenarios, 10 casos de batería, 15 códigos vivos sobre 18 emitidos, 15 puntos de acceso, 6 funciones de fachada y 19 sondas— **cerraron todos contra mi propio recuento**, y también cerraron los 77 quality gates, las 45 decisiones de arquitectura y los 72 artefactos documentales planificados.

---

## 12. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial del resumen ejecutivo de check-out exigido por `Master-Prompt.md` 5.2 §12, con sus **diez** bloques. Todas las cifras se contaron con herramienta sobre el árbol vivo de `SDD/Docs/`, `SDD/Maquetas/` y `/samples/`, excluyendo `_legacy/`; ninguna se heredó de otro documento. Inventaría **7** proyectos de código sin código escrito, **639** documentos vivos por **≈ 9,4 MB**, la cadena de trazabilidad de los diez eslabones por proyecto de código con **2** huérfanos declarados y **3** ADR de versionado que trazan al intake y no a la cadena, **48** ítems de la etapa `a` listos para codear, **30** informes de auditoría con su veredicto, **47** puntos abiertos `PA-XX` en las categorías 05 —**44** vivos—, **7** de nivel producto y **4** asunciones vivas del intake, los flags inmutables del manifiesto §5, **182** elementos de línea de base visual y **92** filas de sensado todas en `Sin verificar`, **72** artefactos documentales planificados de los cuales **64** en `Planificado`, y las **19** sondas `VER-XX` sin evidencia. Declara **6** divergencias entre lo contado y lo declarado por otros documentos, sin resolver ninguna. **No toma ninguna decisión**: todo lo no decidido está en el bloque 6, con titular y con la consecuencia de construir sin decidirlo. | Orquestador SDD |
| 1.1 | 2026-08-11 | **Absorbe la emisión de los dos informes de auditoría que este check-out declaraba faltantes**, y con ella el hallazgo `NB2-01` (P1) de [`Audit/B2-Maqueta-GeometriaFactory-Web-r2.md`](Audit/B2-Maqueta-GeometriaFactory-Web-r2.md) 1.0, cuya recomendación pide actualizar la salvedad `B-2` y el bloque 8. **§5**: los informes contados con `ls` pasan de **30** a **32**; entran la fila de la Fase B de `GeometriaFactory-Api` —r1, **RECHAZADO**, en lugar de «NO EMITIDO»— y la de la ronda 2 de la Fase B2 —**APROBADO**—; la línea de estado de las fases pasa de dos excepciones a **una**. **§6.1**: `B-1` pasa a **cerrado en su parte de proceso** con su ronda 2 pendiente, y `B-2` a **cerrado**, las dos con la fila original tachada y conservada, porque **su redacción era correcta al escribirse**. **§6.4**: `X-2` queda cerrado: el manifiesto y el informe dicen hoy lo mismo. **§8.1**: la nota de la maqueta deja de decir que no hay ronda 2. **Ninguna cifra contada sobre el árbol, ningún ítem de Sprint 1, ningún flag y ninguna decisión de arquitectura cambia**: lo que cambia es el estado de dos fases y el recuento de informes. Sube minor. | Orquestador SDD |

---

**Texto de cierre del orquestador, según `Master-Prompt.md` §12.**

> "Documentación `SDD/Docs/` del producto generado y auditada. Antes de avanzar a la generación de código, necesito confirmación explícita del usuario para arrancar el Sprint 1. Si confirmás, el siguiente paso es despachar al subagente de codificación con los items del Sprint 1 del proyecto de código que indiques, respetando el orden topológico de dependencias. Si no, este es el cierre del trabajo del orquestador de documentación."
