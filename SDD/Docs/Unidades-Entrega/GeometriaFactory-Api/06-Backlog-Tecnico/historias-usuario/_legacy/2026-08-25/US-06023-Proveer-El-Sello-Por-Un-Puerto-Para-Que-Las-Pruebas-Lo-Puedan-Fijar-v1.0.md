> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-06023-Proveer-El-Sello-Por-Un-Puerto-Para-Que-Las-Pruebas-Lo-Puedan-Fijar.md` en su versión **1.0**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-06023-Proveer-El-Sello-Por-Un-Puerto-Para-Que-Las-Pruebas-Lo-Puedan-Fijar.md`](../../US-06023-Proveer-El-Sello-Por-Un-Puerto-Para-Que-Las-Pruebas-Lo-Puedan-Fijar.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-06023 — Proveer el sello por un puerto, para que las pruebas lo puedan fijar

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-06023-Proveer-El-Sello-Por-Un-Puerto-Para-Que-Las-Pruebas-Lo-Puedan-Fijar.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** **Should**
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **obtener el momento actual por un puerto y no leyendo el reloj del entorno**, para **que las pruebas de las capas de adentro sean reproducibles sin fijar el reloj de la máquina**.

## 2. Contexto

`PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Application punto 3 declara que **el reloj es un puerto para que las fechas de alta y modificación sean verificables en prueba**, y `Domain ADR-06006` declara que el dominio no lee el reloj. El contrato de uso es [`CU-06009`](../../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06009-Proveer-El-Sello-Del-Reloj-Del-Sistema.md), que `02` llama **el contrato más corto de la capa y el que explica por qué la capa se puede probar entera con dobles**.

## 3. Criterios de aceptación

- Given una invocación al puerto de reloj, When se la resuelve, Then devuelve el momento actual.
- Given un doble del puerto en una batería de pruebas, When se repite una operación que sella, Then el sello es **reproducible** sin fijar el reloj del entorno.
- Given el adaptador, When se inspeccionan sus dependencias, Then **no depende del contexto de persistencia** y no tiene ninguna del producto.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | **Ninguna.** `02` §7.2 declara que `CU-06009` **no traza a ninguna necesidad de negocio**, y que inventarle una haría creer que hay una necesidad detrás de una decisión de testabilidad |
| CU cubiertos | CU-06009 |
| RN que ejerce | — |
| Componente de `05` §3.1 | Adaptador de reloj del sistema |
| Reglas conceptuales de modelo | `RC-06006`, tres sellos de tiempo distintos |
| ¿Toma alguna decisión de negocio? | **No** |
| ¿Toca el almacén? | **No** |
| BT derivadas | BT-06008, BT-06012 |
| Tests previstos en 08 | Su valor se mide en las pruebas de las capas de adentro que lo reemplazan por un doble |

## 5. Prioridad y estimación

**`Should`, y es la única de las veinticinco.** Su origen **no es una capacidad** del intake §4 sino una **decisión de testabilidad**, y su caso de uso es el único de los diez que **no traza a ninguna necesidad de negocio** (`02` §7.2). El producto funciona sin ella —las capas de adentro leerían el reloj del entorno— y lo que se pierde es que sus pruebas sean reproducibles. **Diferible, y con un costo que se paga en cada batería.** El fundamento completo está en [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.2.

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

**La zona horaria y la precisión no las decide esta historia sino BT-06008**, que cierra un punto abierto de la categoría 02: los sellos se producen y se guardan en tiempo universal coordinado, **sin truncar la precisión**, y la conversión a la zona de quien lee es de la superficie que lo muestra.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
