# US-05 — Derivar el valor desde las dimensiones y los componentes

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** US-05-Derivar-El-Valor-Desde-Las-Dimensiones-Y-Los-Componentes.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-05 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el área y el volumen se recalculen desde las dimensiones y los componentes de cada pieza**, para **poder comparar lo que el alumno declaró contra lo que la geometría dice**.

## 2. Contexto

`NB-05` pide visibilidad del error de cálculo y `F-10` del intake §4 lo declara `Must Have`. El contrato de uso es [`CU-02`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-02-Verificar-Los-Valores-Declarados-Contra-Los-Derivados.md), y la tabla de derivación por tipo vive en [`../../05-Arquitectura-Tecnica/Flujo-Ejecucion.md`](../../05-Arquitectura-Tecnica/Flujo-Ejecucion.md) §5.

## 3. Criterios de aceptación

- Given una pieza ya reconstruida con sus dimensiones y sus componentes, When se deriva su valor, Then el resultado sale de la tabla de derivación por tipo.
- Given una pieza volumétrica, When se deriva su área, Then se usa la **suma de sus componentes**, que es la forma que la fuente muestra dos veces y que coincide con la fórmula en el caso donde se cruzan.
- Given una verificación pedida **sin** las piezas reconstruidas, When se la intenta, Then se devuelve la condición correspondiente y **no «0 advertencias»**: devolver cero sería indistinguible de un trabajo verificado sin discrepancias.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-05 |
| CU cubiertos | CU-02 |
| RN que ejerce | RN-05 en su parte de insumo, RN-09 |
| Componente de `05` §3.1 | Motor de verificación de valores |
| Reglas conceptuales de modelo | `RC-03`, valor declarado y derivado por separado |
| ¿Toma alguna decisión de negocio? | **No** |
| ¿Toca el almacén? | **No** |
| BT derivadas | BT-17, BT-18, BT-19 |
| Tests previstos en 08 | Casos 5 y 6 de la batería, con `E-3`, `E-2` y `E-1` |

## 5. Prioridad y estimación

`Must` por derivar de `F-10`, `Must Have`, y porque sin la derivación no hay nada que comparar en US-06.

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

**La forma de derivar el área de una pieza volumétrica es una derivación declarada y no una transcripción**: `CU-02` §10 adopta la suma de componentes y lo dice. Es `PA-10` de [`../Product-Backlog.md`](../Product-Backlog.md) §6, cerrado como trabajo en BT-19.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
