> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `CU-06-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`CU-06-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md`](../../CU-02006-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# CU-06 — Reconstruir el conjunto de piezas del trabajo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-06-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-04`](../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) §1, §4 y §5; [`NB-06`](../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00006-Visualizacion-Dentro-Del-Producto.md) §5 (disposición estable y sincronización por posición de pieza); `00-Contexto/Vision-Producto.md` §9.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §17.1.P.11 (puntos 1, 2 y 4), §20.E-1, §20.E-2, §20.E-6, §20.E-7
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de GeometriaFactory-Domain; `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Actores](#2-actores)
- [3. Precondiciones](#3-precondiciones)
- [4. Flujo principal](#4-flujo-principal)
- [5. Flujos alternativos](#5-flujos-alternativos)
- [6. Excepciones y errores](#6-excepciones-y-errores)
- [7. Postcondiciones](#7-postcondiciones)
- [8. Criterios de aceptación](#8-criterios-de-aceptación)
- [9. Trazabilidad](#9-trazabilidad)
- [10. Notas y supuestos](#10-notas-y-supuestos)
- [11. Control de cambios](#11-control-de-cambios)
- [12. Compatibilidad de la superficie pública](#12-compatibilidad-de-la-superficie-pública)

---

## 1. Propósito

Incorporar al trabajo el conjunto de piezas y de componentes que resultó de interpretar su texto original, dándole a cada pieza su identidad posicional y guardando por separado el valor declarado y el derivado. Es el contrato por el que el resultado de la interpretación entra al dominio y queda sujeto a sus invariantes.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Entrega al trabajo el resultado de la interpretación que obtuvo del validador de figuras |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | Produce ese resultado detrás del puerto del validador. No conversa con el dominio directamente |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Verifica la identidad posicional, la coherencia de tipo y familia, y adopta las piezas |

## 3. Precondiciones

- El trabajo existe y conserva su texto original íntegro.
- El resultado de la interpretación llega con las piezas en el orden del conjunto raíz del texto del alumno.
- Cada pieza llega con su tipo, sus valores declarados y sus valores derivados ya calculados fuera del dominio.

## 4. Flujo principal

1. La capa de aplicación entrega al trabajo el conjunto de piezas interpretadas.
2. El dominio descarta el conjunto de piezas anterior, si lo hubiera.
3. El dominio asigna a cada pieza su posición en el conjunto raíz, contigua y empezando en 0. **Esa posición es su identidad** (PRODUCT-INTAKE §17.1.P.11 punto 2).
4. El dominio verifica que el tipo de cada pieza pertenezca al conjunto conocido.
5. El dominio deriva la familia plana o volumétrica desde el tipo, sin guardarla como dato propio ni admitir que se la declare.
6. El dominio adopta los componentes de cada pieza, cada uno con su posición dentro de su pieza y su papel.
7. El dominio adopta, para cada pieza, el valor declarado y el valor derivado de área y de volumen como dos atributos distintos.
8. El dominio devuelve el trabajo con su conjunto de piezas reconstruido.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Una pieza del conjunto raíz es una figura plana sin componentes | El dominio la adopta con 0 componentes: es el caso de las piezas planas del escenario E-7, que aparecen como figuras del conjunto raíz y no como partes de un volumen | Paso 7 |
| FA-02 | Una pieza trae una dimensión en 0.00 | El dominio la adopta igual y no la descarta: el criterio es de existencia del dato y no de veracidad geométrica (PRODUCT-INTAKE §20.E-6) | Paso 7 |
| FA-03 | El resultado de la interpretación trae una pieza de tipo desconocido | El dominio no adopta esa pieza y deja constancia de que la posición correspondiente quedó sin reconstruir; la observación de especie error de validación se registra por CU-07. Las piezas válidas del mismo conjunto se adoptan igual: un defecto en un elemento no descarta el resto (PRODUCT-INTAKE §20.E-5) | Paso 6, con las piezas restantes |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `POSICION_DE_PIEZA_NO_CONTIGUA` | El conjunto de piezas llega con posiciones repetidas o con huecos | Rechaza la reconstrucción entera: sin identidad posicional estable no hay forma de seleccionar ni de resaltar una pieza |
| `TIPO_DE_PIEZA_DESCONOCIDO` | El tipo no pertenece al conjunto conocido | No adopta esa pieza. La observación correspondiente se registra por CU-07 con su posición y su campo |
| `FAMILIA_DECLARADA_CONTRADICE_AL_TIPO` | Se aporta la familia plana o volumétrica como dato y contradice a la que el tipo deriva | Rechaza la reconstrucción: la familia se deriva del tipo y no se guarda (PRODUCT-INTAKE §17.1.P.11 punto 4) |
| `RECONSTRUCCION_SOBRE_TRABAJO_FINALIZADO` | Se intenta reconstruir el conjunto de piezas de un trabajo en estado `Finalizado` | Rechaza la operación y conserva el trabajo sin cambios |

## 7. Postcondiciones

- **Éxito:** el trabajo tiene un conjunto de piezas con posiciones contiguas desde 0, cada una con su tipo, sus componentes y sus cuatro valores de área y volumen —declarado y derivado—. El texto original no cambió.
- **Fallo:** el conjunto de piezas anterior se conserva o queda vacío, según el momento del rechazo, y el texto original tampoco cambió.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un trabajo con el texto del escenario E-1 y su interpretación | La capa de aplicación entrega el conjunto de piezas al trabajo | El dominio devuelve el trabajo con 3 piezas en las posiciones 0, 1 y 2, de tipos `Cilindro`, `Cubo` y `Ortoedro` |
| CA-02 | La pieza de posición 1 del escenario E-1, un `Cubo` con área declarada 36.00 y derivada 54.00 | La capa de aplicación entrega el conjunto de piezas | El dominio conserva los 2 valores por separado: área declarada 36.00 y área derivada 54.00, sin sustituir uno por el otro |
| CA-03 | Un trabajo con el texto del escenario E-6, con una pieza `Rectangulo` de `Largo` 0.00 | La capa de aplicación entrega el conjunto de piezas | El dominio adopta 1 pieza y no la descarta |
| CA-04 | Un conjunto de piezas con las posiciones 0 y 2, sin la 1 | La capa de aplicación lo entrega al trabajo | El dominio rechaza con el código `POSICION_DE_PIEZA_NO_CONTIGUA` |
| CA-05 | Una pieza de tipo `Cubo` a la que se le declara la familia plana | La capa de aplicación la entrega al trabajo | El dominio rechaza con el código `FAMILIA_DECLARADA_CONTRADICE_AL_TIPO` |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-04, y NB-06 en cuanto a la identidad posicional que hace posible seleccionar y resaltar una pieza |
| Reglas de negocio aplicables | [RN-08](../../../Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md), [RN-09](../../../Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) |
| Invariantes | INV-04 |
| Historias de usuario a generar en 06 | US de reconstrucción de piezas y componentes, US de identidad posicional estable |
| Componentes esperados en 05 | Entidades de pieza y de componente, y la tabla de consulta que deriva la familia desde el tipo |
| Tests previstos en 08 | Pruebas unitarias con los escenarios E-1, E-2, E-5, E-6 y E-7 como entrada de la reconstrucción, sin infraestructura |

## 10. Notas y supuestos

- **El dominio no parsea nada.** Las cuatro tolerancias del formato del emisor —la clave alternativa de las bases del ortoedro, la coma final, los dos discriminantes de la misma cara y el valor calculado erróneo que no se rechaza— las resuelve el validador de figuras en `GeometriaFactory-Infrastructure`. Acá entra el resultado, no el texto.
- El cálculo del valor derivado tampoco ocurre acá: llega calculado. Lo que el dominio garantiza es que los dos valores se conserven separados.
- La disposición con la que las piezas se dibujan se deriva de la posición y es determinista, pero el dibujo pertenece a `GeometriaFactory-Visor`.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |

## 12. Compatibilidad de la superficie pública

Ampliar el conjunto de tipos conocidos es compatible mientras la tabla que deriva la familia se amplíe con él. Cambiar la identidad de la pieza de posicional a un identificador propio es un cambio de alcance del modelo y contradice §17.1.P.11 punto 2 del intake: no se hace sin decisión del Product Owner.
